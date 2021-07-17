using System.Windows;
using System.Windows.Controls;

namespace Blide
{
    /// <summary>
    /// Login not being used yet
    /// </summary>

    public partial class LoginScreen : UserControl
    {
        MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        public LoginScreen()
        {
            InitializeComponent();
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        /*
        private async void LoadingBarLabel_Copy2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoadingBarLabel_Copy2.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#45abff"));
            await Task.Delay(200);
            LoadingBarLabel_Copy2.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2196F3"));
            wnd.showSignup();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            wnd.Login(usernameText.Text, passwordText.Password);
        }*/
    }
}
