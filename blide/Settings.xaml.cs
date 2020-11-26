using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Blide
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        private static string _botName = "";
        private static string _broadcasterName = "";
        private static string _twitchOAuth = "";
        private static int delay = 300;
        private static string prefix = "/ban ";
        public static Boolean JoinMessage;

        public Settings()
        {
            InitializeComponent();
            settingsWindowGetSettings();
        }



        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.twitchapps.com/tmi/");
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            wnd.showChatTool();
        }
        private void buttonApply_Click(object sender, RoutedEventArgs e)
        {
            settingsWindowWriteSettings();
        }
        private void buttonOkay_Click(object sender, RoutedEventArgs e)
        {
            settingsWindowWriteSettings();
            wnd.showChatTool();
        }
        //toggle
        private void Bu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Bu.Toggled1 == true)
            {
                JoinMessage = true;
            }
            else
            {
                JoinMessage = false;
            }


        }

        public void loadSettings()
        {
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BlideSettings.txt")))
            {
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string[] lines = System.IO.File.ReadAllLines(Path.Combine(docPath, "BlideSettings.txt"));
                _botName = lines[0];
                _broadcasterName = lines[1];
                _twitchOAuth = lines[2];
                delay = Int32.Parse(lines[3]);
                prefix = "" + lines[4];
                JoinMessage = Boolean.Parse(lines[5]);
            }
            else
            {
                createSettings();
            }
        }
        public void writeSettings()
        {
            string[] lines = { _botName, _broadcasterName, _twitchOAuth, delay + "", prefix, JoinMessage + "" };
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "BlideSettings.txt")))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }

        }
        public void createSettings()
        {
            string[] lines = { "yourname", "streamername", "OAuth token", "300", "/ban ", "false" };
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "BlideSettings.txt")))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
            loadSettings();
        }

        public void settingsWindowWriteSettings()
        {
            _broadcasterName = SettingsTwitchChannel.Text;
            _botName = SettingsTwitchName.Text;
            _twitchOAuth = SettingsTwitchOAuth.Text;
            delay = int.Parse(SettingsTwitchDelay.Text);
            prefix = SettingsTwitchPrefix.Text;
            JoinMessage = Bu.getStatus();
            writeSettings();
        }
        public void settingsWindowGetSettings()
        {
            loadSettings();
            SettingsTwitchName.Text = _botName;
            SettingsTwitchChannel.Text = _broadcasterName;
            SettingsTwitchOAuth.Text = _twitchOAuth;
            SettingsTwitchDelay.Text = delay + "";
            SettingsTwitchPrefix.Text = prefix;
            Bu.setStatus(JoinMessage);
        }

        public Boolean settingsEmpty()
        {
            if (_botName != null || _broadcasterName != null || _twitchOAuth != null || delay != 0 || prefix != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
