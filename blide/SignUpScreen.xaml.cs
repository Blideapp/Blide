using System.Windows;
using System.Windows.Controls;

namespace Blide
{
    public partial class SignUpScreen : UserControl
    {
        /*
         * Signup coming soon
         */
        MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        public SignUpScreen()
        {
            InitializeComponent();
        }
        /*
        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            wnd.SignUp(usernameText.Text, EmailText.Text, passwordText.Password);
        }

        private async void SigninLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SigninLabel.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#45abff"));
            await Task.Delay(200);
            SigninLabel.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2196F3"));
            wnd.showLogin();
        }
        */
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
