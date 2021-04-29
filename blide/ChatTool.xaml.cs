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
        SettingsManager settings = new SettingsManager();
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

        public ChatTool()
        {
            InitializeComponent();
            DataContext = dc;
            IsVisibleChanged += ChatTool_IsVisibleChanged;

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

                if (MessageBox.Show(settings.getChannelNames()+ " is currently live. Continue anyways?", settings.getChannelNames() + " is live", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
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
            if (settings.getSettingsExist())
            {
                if (settings.getJoinMessage()) { TwitchLib(); }


                foreach (String item in paths)
                {
                    string[] lines = File.ReadAllLines(item);
                    dc.ConsoleOutput.Add("loaded file: " + item);
                    dc.ConsoleOutput.Add("starting process...");
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (CanRun)
                        {
                            irc.SendPublicChatMessage(settings.getPrefix() + lines[i] + "");
                            await Task.Delay(settings.getDelay());
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

        protected void startIrc()
        {
            if (settings.getSettingsExist() ||settings.getSettingsIsEmpty())
            {
                dc.ConsoleOutput.Add("missing settings file or parts of settings are empty");
               
            }
            
            if (!settings.getSettingsIsEmpty() && settings.getSettingsExist())
            {
                irc = new IrcClient("irc.twitch.tv", 6667,
                                settings.getBotName(), settings.getTwitchOAuth(), settings.getChannelNames()) ;
                dc.ConsoleOutput.Add("found settings - connecting to " + settings.getChannelNames());
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


        //live check using api

        public Boolean isLive()
        {
            
            string strPagecode = client.DownloadString("https://twitchapi.jcmt.dev/v1/getStreamStatus/" + settings.getChannelNames());
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
