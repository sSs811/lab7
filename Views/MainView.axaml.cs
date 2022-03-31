using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Table.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }
        public void ShowAboutWindow(object sender, RoutedEventArgs e)
        {
            var dialogWindow = new AboutView();
            dialogWindow.ShowDialog((Window)this.Parent);
        }


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}