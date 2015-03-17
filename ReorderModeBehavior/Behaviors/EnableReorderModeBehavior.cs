using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace ReorderModeBehavior.Behaviors
{
    public class EnableReorderModeBehavior : BehaviorBase<FrameworkElement>
    {
        public ListViewBase ListView
        {
            get { return (ListViewBase)GetValue(ListViewProperty); }
            set { SetValue(ListViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListViewProperty =
            DependencyProperty.Register("ListView", typeof(ListViewBase), typeof(EnableReorderModeBehavior), new PropertyMetadata(null, ListViewPropertyChanged));

        private static void ListViewPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var action = (EnableReorderModeBehavior)d;
            action.ListView = (ListViewBase)e.NewValue;
        }

        protected override void ElementAttached()
        {
            base.ElementAttached();
            AssociatedElement.PointerPressed += ListViewPointerPressed;
        }

        protected override void ElementDetached()
        {
            AssociatedElement.PointerPressed -= ListViewPointerPressed;
            base.ElementDetached();
        }

        private void ListViewPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if (ListView != null && ListView.ReorderMode != ListViewReorderMode.Enabled)
            {
                ListView.ReorderMode = ListViewReorderMode.Enabled;
            }
        }
    }
}
