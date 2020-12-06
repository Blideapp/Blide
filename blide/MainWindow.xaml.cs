using BeautySolutions.View.ViewModel;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using Squirrel;
using System.Threading.Tasks;
using LibGit2Sharp;
using System.IO;
using System.Linq;
using System;



namespace Blide
{
    
    public partial class MainWindow : Window
    {
        ChatTool Chattool = new ChatTool();
        HypeTool Hypetool = new HypeTool();
        
        FirebaseSettings firebaseclient = new FirebaseSettings();

        public MainWindow()
        {
            InitializeComponent();
            //startUpdate();

            showLoading();
            /*
          var menuRegister = new List<SubItem>();
          menuRegister.Add(new SubItem("chat tool"));
          menuRegister.Add(new SubItem("botlist tool"));
          menuRegister.Add(new SubItem("about"));     
          var item3 = new ItemMenu("settings", menuRegister, PackIconKind.Register);
            */

            var item0 = new ItemMenu("chat tool", new ChatTool(), PackIconKind.ViewDashboard);

            var item1 = new ItemMenu("create botlist", new Botlist(), PackIconKind.InsertDriveFile);
            var item2 = new ItemMenu("settings", new Settings(), PackIconKind.Settings);
            var item3 = new ItemMenu("hype tool", new HypeTool(), PackIconKind.Biohazard);
            var item4 = new ItemMenu("halloween tool", new Halloween(), PackIconKind.Biohazard);

            Menu.Children.Add(new UserControlMenuItem(item0, this));
            Menu.Children.Add(new UserControlMenuItem(item1, this));
            Menu.Children.Add(new UserControlMenuItem(item3, this));
            //Menu.Children.Add(new UserControlMenuItem(item4, this)); // use for Halloween feature
            Menu.Children.Add(new UserControlMenuItem(item2, this));
        }

        private void startUpdate()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[12];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var dir = new String(stringChars);

            while (Directory.Exists(Path.Combine(Path.GetTempPath() + @"\blide" + dir)))
            {
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                dir = new String(stringChars);
            }
            Directory.CreateDirectory(Path.Combine(Path.GetTempPath() + @"\blide" + dir));

            Repository.Clone("https://github.com/sntlyeet/blide", Path.Combine(Path.GetTempPath() + @"\blide" + dir), new CloneOptions { BranchName = "UpdateFiles" });

            if(Directory.Exists(Path.Combine(Path.GetTempPath() + @"\blide" + dir)))
            {
                MessageBox.Show("testing update");
                CheckForUpdates(Path.Combine(Path.GetTempPath() + @"\blide" + dir));
            }
            

        }

        private async void CheckForUpdates(string updatepath)
        {
            using (var mgr = new UpdateManager(updatepath))
            {
                await mgr.UpdateApp();
            }

        }

        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);

            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }

        }
        public void showChatTool()
        {
            if (firebaseclient.isLoggedIn()) { 
            StackPanelMain.Children.Clear();
            StackPanelMain.Children.Add(Chattool);
            }
        }
        public void showBotList()
        {
            if (firebaseclient.isLoggedIn())
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(new Botlist());
            }
        }
        public void showLoading()
            {
                if (firebaseclient.isLoggedIn())
                {
                    StackPanelMain.Children.Clear();
                    StackPanelMain.Children.Add(new LoadingScreen());
                } }
        public void showSettings()
            {
                if (firebaseclient.isLoggedIn())
                {
                    StackPanelMain.Children.Clear();
                    StackPanelMain.Children.Add(new Settings());
                } }
        public void showHypeTool()
            {
                if (firebaseclient.isLoggedIn())
                {
                    StackPanelMain.Children.Clear();
                    StackPanelMain.Children.Add(Hypetool);
                } }
        public void showHalloween()
        {
            if (firebaseclient.isLoggedIn())
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(new Halloween());
            }
        }
        public void showLogin()
        {
            if (!firebaseclient.isLoggedIn())
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(new LoginScreen());
            }
        }

        public void showSignup()
        {
            
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(new SignUpScreen());
            
        }

        public void import(string path)
        {
            if (firebaseclient.isLoggedIn())
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(Chattool);
                Chattool.addFile(path);
            }
        }

        public void Login(string email, string password)
        {
            firebaseclient.login(email, password);
        }



        





    }
}
