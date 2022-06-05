using System.Windows;
using System.Windows.Input;

namespace transport_management_system
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
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
