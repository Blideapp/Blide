using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Blide
{
    /// <summary>
    /// This part of the software was supposed to play random sounds from a specific folder at a random time
    /// It was used for a special halloween stream and might be developed into a full feature
    /// </summary>
    public partial class Halloween : UserControl
    {
        int max = 1;
        int min = 1;
        bool startStatus = false;
        int volume = 100;
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string[] files;
        int last = 0;
        Random rnd = new Random();
        MediaPlayer m_mediaPlayer = new MediaPlayer();
        bool running = false;
        string docPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "screamer");
        public Halloween()
        {
            InitializeComponent();
            Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "screamer"));
            SettingsLoad();
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

        private void maxButtonAdd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Int32.Parse(maxText.Text) < 99 && Int32.Parse(maxText.Text) + 1 >= Int32.Parse(minText.Text))
            {
                maxText.Text = Int32.Parse(maxText.Text) + 1 + "";
                max = Int32.Parse(maxText.Text);
            }
        }

        private void maxButtonDec_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Int32.Parse(maxText.Text) > 1 && Int32.Parse(maxText.Text) - 1 >= Int32.Parse(minText.Text))
            {
                maxText.Text = Int32.Parse(maxText.Text) - 1 + "";
            }
            max = Int32.Parse(maxText.Text);


        }
        private void minButtonAdd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Int32.Parse(minText.Text) < 99 && Int32.Parse(maxText.Text) >= Int32.Parse(minText.Text) + 1)
            {
                minText.Text = Int32.Parse(minText.Text) + 1 + "";
                min = Int32.Parse(minText.Text);
            }
        }

        private void minButtonDec_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Int32.Parse(minText.Text) > 1 && Int32.Parse(maxText.Text) >= Int32.Parse(minText.Text) - 1)
            {
                minText.Text = Int32.Parse(minText.Text) - 1 + "";
            }
            min = Int32.Parse(minText.Text);
        }



        private void chooseFolder_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = folderPath;
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                folderPath = dialog.FileName;
            }

        }

        private void avoidButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (!startStatus)
            {
                startButton.Content = "Stop";
                run();
                startStatus = true;
            }
            else
            {
                startButton.Content = "Start";
                stop();
                startStatus = false;
            }

        }

        private void stop()
        {
            running = false;
        }
        private async void run()
        {
            SettingsSave();
            int min = Int32.Parse(minText.Text);
            int max = Int32.Parse(maxText.Text);

            int minInMs = min * 60 * 1000;
            int maxInMs = max * 60 * 1000;
            running = true;
            while (running)
            {
                int delay = rnd.Next(minInMs, maxInMs);
                await Task.Delay(delay);
                if (running)
                {
                    runsound();
                }

            }

        }
        private void volumeButtonAdd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Int32.Parse(volumeText.Text) < 100)
            {
                volumeText.Text = Int32.Parse(volumeText.Text) + 1 + "";
                volume = Int32.Parse(volumeText.Text);
            }
        }

        private void volumeButtonDec_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Int32.Parse(volumeText.Text) > 1)
            {
                volumeText.Text = Int32.Parse(volumeText.Text) - 1 + "";
            }
            max = Int32.Parse(volumeText.Text);


        }

        private void testSound_Click(object sender, RoutedEventArgs e)
        {
            runsound();
        }

        private void stopSound_Click(object sender, RoutedEventArgs e)
        {
            m_mediaPlayer.Stop();
        }
        private void runsound()
        {
            files = Directory.GetFiles(folderPath);
            int random = rnd.Next(0, files.Length - 1);
            if (avoidButton.getStatus())
            {
                while (random == last)
                {
                    random = rnd.Next(0, files.Length);
                }
            }

            last = random;
            String soundPlayer = System.IO.Path.Combine(folderPath, files[random]);
            double volumeint = double.Parse(volumeText.Text);
            double volume = volumeint / 100;
            m_mediaPlayer.Volume = volume;
            m_mediaPlayer.Open(new System.Uri(soundPlayer));
            m_mediaPlayer.Play();
        }

        public void SettingsSave()
        {
            if (File.Exists(System.IO.Path.Combine(docPath, "screamer.txt")))
            {
                string[] lines = { folderPath, avoidButton.getStatus() + "", minText.Text, maxText.Text };
                using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, "screamer.txt")))
                {
                    foreach (string line in lines)
                        outputFile.WriteLine(line);
                }
            }
            else
            {
                SettingsCreate();
            }
        }
        public void SettingsLoad()
        {
            if (File.Exists(System.IO.Path.Combine(docPath, "screamer.txt")))
            {
                string[] lines = System.IO.File.ReadAllLines(System.IO.Path.Combine(docPath, "screamer.txt"));
                folderPath = lines[0];
                avoidButton.setStatus(Boolean.Parse(lines[1]));
                minText.Text = lines[2];
                maxText.Text = lines[3];

            }
            else
            {
                SettingsCreate();
            }
        }
        public void SettingsCreate()
        {
            string[] lines = { folderPath, "true", 1 + "", 1 + "" };

            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(docPath, "screamer.txt")))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
            SettingsLoad();
        }

    }
}
