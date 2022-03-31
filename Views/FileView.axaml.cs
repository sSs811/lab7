using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Table.ViewModels;

namespace Table.Views

{
    public partial class FileView : UserControl
    {
        public FileView()
        {
            InitializeComponent();
            this.FindControl<Button>("Save").Click += async delegate
            {
                var taskPath = new SaveFileDialog().ShowAsync((Window)this.Parent);

                string path = await taskPath;
                var context = this.Parent.DataContext as MainWindowViewModel;
                if (path is not null)
                {
                    context.WriteToBinaryFile(path);
                }
                context.OpenMainView();

            };
            this.FindControl<Button>("Load").Click += async delegate
            {
                var taskPath = new OpenFileDialog().ShowAsync((Window)this.Parent);
                string[]? path = await taskPath;
                var context = this.Parent.DataContext as MainWindowViewModel;
                if (path is not null)
                {
                    context.ReadFromBinaryFile(string.Join("/", path));
                }
                context.OpenMainView();
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}