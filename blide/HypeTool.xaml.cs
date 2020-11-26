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
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.ComponentModel;

namespace Blide
{
    /// <summary>
    /// Interaction logic for HypeTool.xaml
    /// </summary>
    public partial class HypeTool : UserControl
    {
        int amount = 1;
        int interval = 1;
        bool startStatus = false;
        MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        private static Boolean settingsExist = false;
        private static string _botName = "";
        private static string _broadcasterName = "";
        private static string _twitchOAuth = "";
        private static int delay = 300;
        private static string twitchChannel= "";
        private static string prefix = "/ban ";
        public static Boolean JoinMessage;
        private static Boolean  canRun = false; // hypetool runable
        private string emoteline = "";
        private int messageDelay = 1000;
        private int messagesSent = 0;
        private bool twentyPercent = false;
        //BG worker for chatreading
        private readonly BackgroundWorker Readworker = new BackgroundWorker();
        private readonly BackgroundWorker Spamworker = new BackgroundWorker();

        public HypeTool()
        {
            InitializeComponent();
            loadSettings();
            
            amountText.Text = amount + "";

            Readworker.DoWork += Readworker_DoWork;
            Readworker.RunWorkerCompleted += Readworker_RunWorkerCompleted;
            Spamworker.DoWork += Spamworker_DoWork;
            Spamworker.RunWorkerCompleted += Spamworker_RunWorkerCompleted;
            Spamworker.WorkerSupportsCancellation = true;
            Readworker.WorkerSupportsCancellation = true;

        }

        private void Content_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void randomizeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void amountButtonAdd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Int32.Parse(amountText.Text) < 99)
            {
                amountText.Text = Int32.Parse(amountText.Text) + 1 + "";
                amount = Int32.Parse(amountText.Text);
            }
        }

        private void amountButtonDec_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(Int32.Parse(amountText.Text) > 1)
            {
                amountText.Text = Int32.Parse(amountText.Text) - 1 + "";
            }
            amount = Int32.Parse(amountText.Text);


        }
        private void intervalButtonAdd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Int32.Parse(intervalText.Text) < 99)
            {
                intervalText.Text = Int32.Parse(intervalText.Text) + 1 + "";
                interval = Int32.Parse(intervalText.Text);
            }
        }

        private void intervalButtonDec_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Int32.Parse(intervalText.Text) > 1)
            {
                intervalText.Text = Int32.Parse(intervalText.Text) - 1 + "";
            }
            interval = Int32.Parse(intervalText.Text);
        }

        private void startBot1_Click(object sender, RoutedEventArgs e)
        {
            if (!startStatus)
            {
                startBot1.Content = "Stop";
                run();
                startStatus = true;
            }
            else
            {
                startBot1.Content = "Start";
                stop();
                startStatus = false;
            }
        }

        private void stop()
        {
            canRun = false;
            Readworker.CancelAsync();
            this.startBot1.Content = "Start";
            startStatus = false;
        }

        private async void run()
        {
            twentyPercent = randomizeButton.getStatus();
            canRun = true;
            twitchChannel = Channel.Text;
            emoteline = Content.Text + " ";
            string emotetemp;
            for(int i = 0; i < Int32.Parse(amountText.Text) -1; i++) // stringbuilder for message
            {
                emotetemp = emoteline;
                if((emotetemp + Content.Text + " ").Length < 500)
                {
                    emoteline += Content.Text + " ";
                }
            }
            messageDelay = Int32.Parse(intervalText.Text) * 1000;
            if (!Readworker.IsBusy)
            {
                Readworker.RunWorkerAsync();
            }
            
            Spamworker.RunWorkerAsync();

            UpdateMessageCount();

            await Task.Delay(2* 60 * 1000);
            canRun = false;
            stop();
            startStatus = false;
        }
        
        public async void UpdateMessageCount()
        {
            while (canRun)
            {
                messageCounter.Content = messagesSent;
                await Task.Delay(1000);
            }
        }

        private void Readworker_DoWork(object sender, DoWorkEventArgs e)
        {
            IrcClient irc = new IrcClient("irc.twitch.tv", 6667,_botName, _twitchOAuth, twitchChannel);

            while (canRun)
            {
                string message = irc.ReadMessage();
                if (message.Contains("PRIVMSG"))
                {
                    // Messages from the users will look something like this (without quotes):
                    // Format: ":[user]![user]@[user].tmi.twitch.tv PRIVMSG #[channel] :[message]"
                    // Modify message to only retrieve user and message
                    int intIndexParseSign = message.IndexOf('!');
                    string userName = message.Substring(1, intIndexParseSign - 1); // parse username from specific section (without quotes)
                                                                                   // Format: ":[user]!"
                                                                                   // Get user's message
                    intIndexParseSign = message.IndexOf(" :");
                    message = message.Substring(intIndexParseSign + 2);
                    // General commands anyone can use
                    if (message.Equals("!BlideStop", StringComparison.OrdinalIgnoreCase) || message.Equals("!stop", StringComparison.OrdinalIgnoreCase) || message.Equals("BlideStop", StringComparison.OrdinalIgnoreCase))
                    {
                        
                        Readworker.CancelAsync();
                        canRun = false;
                        irc.SendPublicChatMessage("stopped");
                    }
                }


            }


        }

        private void Readworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e){
            stop();
        }


        private async void Spamworker_DoWork(object sender, DoWorkEventArgs e)
        {
            IrcClient irc = new IrcClient("irc.twitch.tv", 6667, _botName, _twitchOAuth, twitchChannel);

            while (canRun)
            {
                if(emoteline != null && emoteline.Length <= 500 && canRun)
                {
                    irc.SendPublicChatMessage(emoteline);
                    messagesSent++;
                }
                if (twentyPercent)
                {
                    Random rnd = new Random();
                    double temp = rnd.NextDouble() * (1.2 - 0.8) + 0.8;
                    await Task.Delay((int)(messageDelay * temp));
                }
                else
                {
                    await Task.Delay(messageDelay);
                }
                
            }
        }
        private void Spamworker_RunWorkerCompleted(object sender,
                                                  RunWorkerCompletedEventArgs e)
        {
            //update ui once worker complete his work
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
    }

}
