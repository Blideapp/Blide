using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Blide
{
    /// <summary>
    /// Interaction logic for HypeTool. Making hype in chat easy
    /// </summary>
    public partial class HypeTool : UserControl
    {
        //default values
        int amount = 1;
        int interval = 1;
        bool startStatus = false;

        MainWindow wnd = (MainWindow)Application.Current.MainWindow; //mainwindow
        SettingsManager settings = new SettingsManager(); // access settings
        private static string twitchChannel = "";
        private static Boolean canRun = false; // hypetool runable
        private string emoteline = "";
        private int messageDelay = 1000;
        private int messagesSent = 0;
        private bool useRandomDelay = false;
        static bool sendStopConfirmation = false;
        //BG worker for chatreading
        private readonly BackgroundWorker Readworker = new BackgroundWorker();
        private readonly BackgroundWorker Spamworker = new BackgroundWorker();

        public HypeTool()
        {
            InitializeComponent();

            amountText.Text = amount + "";
            // use workers to read chat and spam at the same time
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
            if (Int32.Parse(amountText.Text) > 1)
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
            Spamworker.CancelAsync();
            this.startBot1.Content = "Start";
            startStatus = false;
        }

        private async void run()
        {

            useRandomDelay = randomizeButton.getStatus();
            canRun = true;
            twitchChannel = Channel.Text;
            emoteline = Content.Text + " ";
            string emotetemp;
            for (int i = 0; i < Int32.Parse(amountText.Text) - 1; i++) // stringbuilder for message
            {
                emotetemp = emoteline;
                if ((emotetemp + Content.Text + " ").Length < 500)
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



            //countdown

            int minutes = 0;
            int seconds = 0;
            for (int i = 0; i <= 120; i++)
            {
                if (!canRun)
                {
                    break;
                }
                if (seconds >= 59)
                {
                    seconds = 0;
                    minutes++;
                }
                else
                {
                    seconds++;
                }

                string secondsstring = seconds + "";
                if (seconds < 10) // 1:2 -> 1:02
                {
                    secondsstring = "0" + secondsstring;
                }

                timerLabel.Content = minutes + ":" + secondsstring;
                await Task.Delay(1000);
            }
            canRun = false;
            stop();
            startStatus = false;
            timerLabel.Content = "00:00";
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
            IrcClient irc = new IrcClient("irc.twitch.tv", 6667, settings.getBotName(), settings.getTwitchOAuth(), twitchChannel);

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
                        if (sendStopConfirmation)
                        {
                            irc.SendPublicChatMessage("stopped");
                        }

                    }
                }


            }


        }

        private void Readworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            stop();
        }


        private async void Spamworker_DoWork(object sender, DoWorkEventArgs e)
        {
            IrcClient irc = new IrcClient("irc.twitch.tv", 6667, settings.getBotName(), settings.getTwitchOAuth(), twitchChannel);

            while (canRun)
            {
                if (emoteline != null && emoteline.Length <= 500 && canRun)
                {
                    irc.SendPublicChatMessage(emoteline);
                    messagesSent++;
                }
                if (useRandomDelay)
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
        private void Spamworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //update ui once worker complete his work
        }


    }

}
