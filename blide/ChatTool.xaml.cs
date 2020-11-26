using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;


namespace Blide
{

    public partial class ChatTool : UserControl
    {
        MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        private static Boolean settingsExist = false;
        private static string _botName = "";
        private static string _broadcasterName = "";
        private static string _twitchOAuth = "";
        private static int delay = 300;
        private static string prefix = "/ban ";
        public static Boolean JoinMessage;
        public static string runAnyways = "";

        static IrcClient irc;

        static WebClient client = new WebClient();

        //console
        ConsoleContent dc = new ConsoleContent();
        //hotkey
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID = 9000;
        public Boolean StartBotUsage = true;
        //Modifiers:        
        private const uint MOD_CONTROL = 0x0002; //CTRL
        //CAPS LOCK:
        private const uint VK_CAPITAL = 0x70;
        List<string> paths = new List<string>();
        Boolean CanRun = false;
        private IntPtr _windowHandle;

        public ChatTool()
        {
            InitializeComponent();
            DataContext = dc;
            IsVisibleChanged += ChatTool_IsVisibleChanged;

            /*
            _windowHandle = new WindowInteropHelper(wnd).Handle;
            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);

            RegisterHotKey(_windowHandle, HOTKEY_ID, MOD_CONTROL, VK_CAPITAL); //CTRL + CAPS_LOCK
            */
        }

        private void ChatTool_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (IsVisible)
            {


            }
        }
        //buttons:
        public void btnOpenFiles_Click(object sender, RoutedEventArgs e)
        {
            CanRun = false;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (openFileDialog.ShowDialog() == true)
            {

                foreach (String item in openFileDialog.FileNames)
                {
                    dc.ConsoleOutput.Add("adding file: " + item);
                    lbFiles.Items.Add(item);
                    paths.Add(item);
                }
            }
        }
        public void addFile(string path)
        {
            dc.ConsoleOutput.Add("adding file: " + path);
            lbFiles.Items.Add(path);
            paths.Add(path);
        }
        public void startBot_Click(object sender, RoutedEventArgs e)
        {
            if (StartBotUsage)
            {
                startBot.Content = "Stop";
                countdown();
                StartBotUsage = false;
            }
            else
            {
                startBot.Content = "Start";
                Stop();
                StartBotUsage = true;
            }

        }
        private async void countdown()
        {
            CanRun = false;
            if (isLive())
            {

                if (MessageBox.Show(_broadcasterName + " is currently live. Continue anyways?", _broadcasterName + " is live", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    dc.ConsoleOutput.Add("stopping - streamer is live");
                    startBot.Content = "Start";
                    Stop();
                }
                else
                {
                    dc.ConsoleOutput.Add("starting");
                    await Task.Delay(100);
                    CanRun = true;
                    run();
                    StartBotUsage = true;
                }


            }
            else
            {
                dc.ConsoleOutput.Add("starting");
                await Task.Delay(100);
                CanRun = true;
                run();
                StartBotUsage = true;
            }
        }


        private async void run()
        {
            startIrc();
            if (settingsExist)
            {
                if (JoinMessage) { TwitchLib(); }


                foreach (String item in paths)
                {
                    string[] lines = File.ReadAllLines(item);
                    dc.ConsoleOutput.Add("loaded file: " + item);
                    dc.ConsoleOutput.Add("starting process...");
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (CanRun)
                        {
                            irc.SendPublicChatMessage(prefix + lines[i] + "");

                            await Task.Delay(delay);
                        }
                        else
                        {
                            break;
                        }
                    }
                    dc.ConsoleOutput.Add("finished file: " + item);
                }
                dc.ConsoleOutput.Add("finished");
                CanRun = false;
                await Task.Delay(500);
                //remove all
                dc.ConsoleOutput.Add("removed all files from list");
                paths.Clear();
                lbFiles.Items.Clear();
                StartBotUsage = true;
                startBot.Content = "Start";
            }
        }
        private void TwitchLib()
        {
            irc.SendPublicChatMessage("Blide initialized - everything is working properly");
            dc.ConsoleOutput.Add("bot initialized in chat - login successful");
        }
        private void Stop()
        {
            dc.ConsoleOutput.Add("stopped");
            CanRun = false;
        }
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {

            if (lbFiles.SelectedItems.Count != 0)
            {
                while (lbFiles.SelectedIndex != -1)
                {
                    dc.ConsoleOutput.Add("removed file at position " + lbFiles.SelectedIndex);
                    paths.RemoveAt(lbFiles.SelectedIndex);
                    lbFiles.Items.RemoveAt(lbFiles.SelectedIndex);

                }
            }

        }

        //global hotkey
        private HwndSource _source;
        protected void startIrc()
        {
            if (settingsEmpty() || !File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BlideSettings.txt")))
            {
                dc.ConsoleOutput.Add("missing settings file or part of settings");
                if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BlideSettings.txt")))
                {
                    dc.ConsoleOutput.Add("trying to create settings file");
                    createSettings();
                    dc.ConsoleOutput.Add("new settings file created - please edit");
                    settingsExist = false;
                }
                //Stop();
            }
            else
            {
                settingsExist = true;
            }
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BlideSettings.txt")) && settingsExist)
            {
                loadSettings();
                irc = new IrcClient("irc.twitch.tv", 6667,
                                _botName, _twitchOAuth, _broadcasterName);
                dc.ConsoleOutput.Add("found settings - connecting to " + _broadcasterName);
            }

        }

        // Abbruch hotkey:
        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            int vkey = (((int)lParam >> 16) & 0xFFFF);
                            if (vkey == VK_CAPITAL)
                            {
                                //code execution:
                                CanRun = false;
                                Stop();

                            }
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        //settings:
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

        //live check using api

        public Boolean isLive()
        {
            loadSettings();
            string strPagecode = client.DownloadString("https://twitchapi.jcmt.dev/v1/getStreamStatus/" + _broadcasterName);
            string[] stringparts = strPagecode.Split('"', ':', '}');
            if (bool.TryParse(stringparts[6], out bool result))
            {
                return result;
            }
            else
            {
                dc.ConsoleOutput.Add("error: cant check if streamer is currently live");
                dc.ConsoleOutput.Add("would you like to continue anyways? Y/N");
                return true;
            }


        }

        

    }

    public class ConsoleContent : INotifyPropertyChanged
    {
        string consoleInput = string.Empty;
        ObservableCollection<string> consoleOutput = new ObservableCollection<string>() { "Console loaded..." };

        public string ConsoleInput
        {
            get
            {
                return consoleInput;
            }
            set
            {
                consoleInput = value;
                OnPropertyChanged("ConsoleInput");
            }
        }

        public ObservableCollection<string> ConsoleOutput
        {
            get
            {
                return consoleOutput;
            }
            set
            {
                consoleOutput = value;
                OnPropertyChanged("ConsoleOutput");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
