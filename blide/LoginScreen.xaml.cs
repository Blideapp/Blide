using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blide
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
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
