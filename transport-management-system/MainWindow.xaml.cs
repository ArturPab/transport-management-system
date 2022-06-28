using System.Windows;
using System.Windows.Input;
using transport_management_system.Infrastructure;

namespace transport_management_system
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DbCreator.Create();
            InitializeComponent();
        }

        private void CloseApplicationOnClick(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void MinimizeApplicationOnClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
