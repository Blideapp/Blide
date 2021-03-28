using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Blide
{
    /// <summary>
    /// Interaction logic for LoadingScreen.xaml
    /// </summary>
    public partial class LoadingScreen : UserControl
    {
        MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        public LoadingScreen()
        {
            InitializeComponent();

            IsVisibleChanged += Loading_IsVisibleChanged;
        }

        private void Loading_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                loadingScreen();



            }
        }
        public async void loadingScreen()
        {
            LoadingBarRectangle.Width = 1;
            await Task.Delay(69);
            LoadingBarRectangle.Width = 69;
            await Task.Delay(187);
            LoadingBarRectangle.Width = 154;
            await Task.Delay(10);
            LoadingBarRectangle.Width = 200;
            await Task.Delay(69);
            LoadingBarRectangle.Width = 239;
            await Task.Delay(69);
            wnd.showChatTool();
        }
    }
}
