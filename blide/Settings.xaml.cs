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
        SettingsManager settings = new SettingsManager();       

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
            settings.setJoinMessage(Bu.Toggled1);
        }

        public void settingsWindowWriteSettings()
        {
            settings.setChannelNames(SettingsTwitchChannel.Text);
            settings.setBotName(SettingsTwitchName.Text);
            settings.setTwitchOAuth(SettingsTwitchOAuth.Text);
            settings.setDelay(int.Parse(SettingsTwitchDelay.Text));
            settings.setPrefix(SettingsTwitchPrefix.Text);
            settings.setJoinMessage(Bu.getStatus());
        }
        public void settingsWindowGetSettings()
        {
            SettingsTwitchName.Text = settings.getBotName();
            SettingsTwitchChannel.Text = settings.getChannelNames();
            SettingsTwitchOAuth.Text = settings.getTwitchOAuth();
            SettingsTwitchDelay.Text = settings.getDelay() + "";
            SettingsTwitchPrefix.Text = settings.getPrefix();
            Bu.setStatus(settings.getJoinMessage());
        }

    }
}
