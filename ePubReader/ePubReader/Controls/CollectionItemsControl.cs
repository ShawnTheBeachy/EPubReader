using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ePubReader.Controls
{
    public class CollectionItemsControl : ItemsControl
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CollectionItemContainer();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is CollectionItemContainer;
        }
    }
}
