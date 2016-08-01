using ePubReader.Models;
using ePubReader.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace ePubReader.Controllers
{
    public static class ImportController
    {
        public static async Task<bool> ImportEPubAsync()
        {
            try
            {
                var ePubPicker = new FileOpenPicker();
                ePubPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                ePubPicker.FileTypeFilter.Add(".epub");

                var files = await ePubPicker.PickMultipleFilesAsync();

                if (files.Count > 0)
                {
                    StorageFolder ePubDirectory;

                    ePubDirectory = (StorageFolder)await ApplicationData.Current.LocalFolder.TryGetItemAsync("ePubs");

                    if (ePubDirectory == null)
                        ePubDirectory = await ApplicationData.Current.LocalFolder.CreateFolderAsync("ePubs");

                    foreach (var file in files)
                    {
                        var fileDirectory = await ePubDirectory.CreateFolderAsync($"{file.DisplayName}.{DateTime.Now.ToFileTime()}");
                        var copiedFile = await file.CopyAsync(fileDirectory);
                        await Task.Run(() => ZipFile.ExtractToDirectory(copiedFile.Path, fileDirectory.Path));
                    }
                }

                return true;
            }

            catch
            {
                return false;
            }
        }

        public static async Task<IEnumerable<ePub>> GetImportedEPubsAsync()
        {
            var returnEPubs = new List<ePub>();

            StorageFolder ePubDirectory;

            ePubDirectory = (StorageFolder)await ApplicationData.Current.LocalFolder.TryGetItemAsync("ePubs");

            if (ePubDirectory == null)
                ePubDirectory = await ApplicationData.Current.LocalFolder.CreateFolderAsync("ePubs");

            var ePubs = await ePubDirectory.GetFoldersAsync();

            foreach (var ePub in ePubs)
            {
                var newEPub = new ePub
                {
                    Id = ePub.DisplayName
                };

                #region Get MimeType
                var mimeTypeFile = await ePub.GetFileAsync("mimetype");
                var mimeType = await FileIO.ReadTextAsync(mimeTypeFile);
                newEPub.MimeType = mimeType;
                #endregion GetMimeType

                #region Get OPF file
                var metaInfFolder = await ePub.GetFolderAsync("META-INF");
                var containerFile = await metaInfFolder.GetFileAsync("container.xml");
                var containerXml = await FileIO.ReadTextAsync(containerFile);
                var containerXDoc = XDocument.Parse(containerXml);
                var containerJson = JsonConvert.SerializeXNode(containerXDoc);
                var containerJsonDoc = JObject.Parse(containerJson);
                var rootFilePath = (string)containerJsonDoc.SelectToken("container.rootfiles.rootfile.@full-path");
                var rootReplaced = rootFilePath.Replace('/', '\\');
                var rootSplits = rootReplaced.Split('\\');
                newEPub.RootFolderPath = ePub.Path + "\\" + (rootSplits.Count() > 1 ? string.Join("\\", rootSplits.Take(rootSplits.Count() - 1)) : string.Empty);
                var opfPath = $"{ePub.Path}\\{rootReplaced}";
                var opfFile = await StorageFile.GetFileFromPathAsync(opfPath);
                #endregion Get OPF file

                var tocFile = await StorageFile.GetFileFromPathAsync($"{newEPub.RootFolderPath}\\toc.ncx");
                var jToc = EPubToJsonConverter.TableOfContentsConverter.TableOfContentsToJson(await FileIO.ReadTextAsync(tocFile));

                #region Get OPF XDoc
                var opfXml = await FileIO.ReadTextAsync(opfFile);
                var opfXDoc = XDocument.Parse(opfXml);
                XNamespace opfNamespace = "http://www.idpf.org/2007/opf";
                #endregion Get OPF XDoc

                var packageNode = (from xml in opfXDoc.Descendants(opfNamespace + "package")
                                   select xml).FirstOrDefault();

                #region Get metadata
                XNamespace purlElementsNamespace = "http://purl.org/dc/elements/1.1/";

                var metadataNode = (from xml in packageNode.Descendants(opfNamespace + "metadata")
                                    select xml).FirstOrDefault();

                var dcTitle = metadataNode.GetInnerXml(purlElementsNamespace, "title");
                var dcLang = metadataNode.GetInnerXml(purlElementsNamespace, "language");
                var dcContributor = metadataNode.GetInnerXml(purlElementsNamespace, "contributor");
                var dcCoverage = metadataNode.GetInnerXml(purlElementsNamespace, "coverage");
                var dcFormat = metadataNode.GetInnerXml(purlElementsNamespace, "format");
                var dcIdentifier = metadataNode.GetInnerXml(purlElementsNamespace, "identifier");
                var dcPublisher = metadataNode.GetInnerXml(purlElementsNamespace, "publisher");
                var dcRelation = metadataNode.GetInnerXml(purlElementsNamespace, "relation");
                var dcCreator = metadataNode.GetInnerXml(purlElementsNamespace, "creator");
                var dcDate = DateTime.Parse(metadataNode.GetInnerXml(purlElementsNamespace, "date"));
                var dcRights = metadataNode.GetInnerXml(purlElementsNamespace, "rights");
                var dcSource = metadataNode.GetInnerXml(purlElementsNamespace, "source");
                var dcType = metadataNode.GetInnerXml(purlElementsNamespace, "type");
                var dcSubjects = (from xml in metadataNode.Descendants(purlElementsNamespace + "subject")
                                  select xml.ReadInnerXml()).ToList();
                newEPub.CoverId = (from xml in metadataNode.Descendants(opfNamespace + "meta")
                                   where xml.Attribute("name") != null && xml.Attribute("name").Value == "cover"
                                   select xml).FirstOrDefault()?.Attribute("content").Value;

                var newMetadata = new Metadata
                {
                    Contributor = dcContributor,
                    Coverage = dcCoverage,
                    Creator = dcCreator,
                    Date = dcDate,
                    Format = dcFormat,
                    Identifier = dcIdentifier,
                    Language = dcLang,
                    Publisher = dcPublisher,
                    Relation = dcRelation,
                    Rights = dcRights,
                    Source = dcSource,
                    Subjects = dcSubjects,
                    Title = dcTitle,
                    Type = dcType
                };

                newEPub.Metadata = newMetadata;
                #endregion Get metadata

                #region Get manifest
                var manifestNode = (from xml in packageNode.Descendants(opfNamespace + "manifest")
                                    select xml).FirstOrDefault();
                var manifestElements = (from xml in manifestNode.Descendants(opfNamespace + "item")
                                        select xml).ToList();

                foreach (var manifestElement in manifestElements)
                {
                    var itemId = manifestElement.Attribute("id").Value;
                    var itemHref = manifestElement.Attribute("href").Value;
                    var itemMediaType = manifestElement.Attribute("media-type").Value;

                    var newManifestItem = new ManifestItem
                    {
                        Id = itemId,
                        Href = itemHref,
                        MediaType = itemMediaType
                    };

                    newEPub.Manifest.Add(newManifestItem);
                }
                #endregion Get manifest

                #region Get spine
                var spineNode = (from xml in packageNode.Descendants(opfNamespace + "spine")
                                    select xml).FirstOrDefault();

                var toc = spineNode.Attribute("toc").Value;

                var spineElements = (from xml in spineNode.Descendants(opfNamespace + "itemref")
                                     select xml).ToList();

                var newSpine = new Spine
                {
                    TableOfContents = toc
                };

                foreach (var spineElement in spineElements)
                {
                    var idRef = spineElement.Attribute("idref").Value;

                    var newSpineItem = new SpineItem
                    {
                        IdRef = idRef
                    };

                    newSpine.Items.Add(newSpineItem);
                }

                newEPub.Spine = newSpine;
                #endregion Get spine

                #region Get cover stream
                var coverManifestItem = newEPub.Manifest.Find(a => a.Id == newEPub.CoverId);

                if (coverManifestItem != null)
                {
                    var coverPath = $"{newEPub.RootFolderPath}\\{coverManifestItem.Href.Replace('/', '\\')}";
                    var coverFile = await StorageFile.GetFileFromPathAsync(coverPath);
                    newEPub.CoverStream = await coverFile.OpenReadAsync();
                }
                #endregion Get cover stream

                returnEPubs.Add(newEPub);
            }

            return returnEPubs;
        }

        public static async Task ChangeEPubCoverAsync(ePub ePub)
        {
            var coverFilePicker = new FileOpenPicker();
            coverFilePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            coverFilePicker.FileTypeFilter.Add(".bmp");
            coverFilePicker.FileTypeFilter.Add(".jpg");
            coverFilePicker.FileTypeFilter.Add(".jpeg");
            coverFilePicker.FileTypeFilter.Add(".png");

            var file = await coverFilePicker.PickSingleFileAsync();

            if (file != null)
            {
                var coverManifestItem = ePub.Manifest.Find(a => a.Id == ePub.CoverId);
                var coverSplits = coverManifestItem.Href.Replace('/', '\\').Split('\\');
                var coverPath = coverSplits.Count() > 1 ? string.Join("\\", coverSplits.Take(coverSplits.Count() - 1)) : string.Empty;
                await file.CopyAsync(await StorageFolder.GetFolderFromPathAsync($"{ePub.RootFolderPath}{coverPath}"), coverSplits[coverSplits.Count() - 1], NameCollisionOption.ReplaceExisting);
                ePub.CoverStream = await file.OpenReadAsync();
            }
        }
    }
}
