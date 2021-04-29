using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Blide
{
    class SettingsManager
    {
        private static string botName = "";
        private static string channelNames = "";
        private static string twitchOAuth = "";
        private static int delay = 300;
        private static string prefix = "/ban ";
        public static Boolean joinMessage;
        public static Boolean settingsExist = false;
        public SettingsManager()
        {
            loadSettings();
        }

        //loads settings
        public void loadSettings()
        {
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BlideSettings.txt")))
            {
                string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string[] lines = System.IO.File.ReadAllLines(Path.Combine(docPath, "BlideSettings.txt"));
                botName = lines[0];
                channelNames = lines[1];
                twitchOAuth = lines[2];
                delay = Int32.Parse(lines[3]);
                prefix = "" + lines[4];
                joinMessage = Boolean.Parse(lines[5]);
                settingsExist = true;
            }
            else
            {
                createSettings();
                settingsExist = false;
            }
        }
        //write settings
        public void writeSettings()
        {
            string[] lines = { botName, channelNames, twitchOAuth, delay + "", prefix, joinMessage + "" };
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "BlideSettings.txt")))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }

        }
        //createsettings
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
        // check if settings are empty
        public Boolean settingsEmpty()
        {
            if (botName != null || channelNames != null || twitchOAuth != null || delay != 0 || prefix != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string getBotName()
        {
            loadSettings();
            return botName;
        }
        public string getChannelNames()
        {
            loadSettings();
            return channelNames;
        }
        public string getTwitchOAuth()
        {
            loadSettings();
            return twitchOAuth;
        }
        public int getDelay()
        {
            loadSettings();
            return delay;
        }
        public string getPrefix()
        {
            loadSettings();
            return prefix;
        }
        public bool getJoinMessage()
        {
            loadSettings();
            return joinMessage;
        }
        public bool getSettingsExist()
        {
            loadSettings();
            return settingsExist;
        }
        public bool getSettingsIsEmpty()
        {
            loadSettings();
            return settingsEmpty();
        }

        //set
        public void setBotName(string _botName)
        {
            botName = _botName;
            writeSettings();
        }
        public void setChannelNames(string _channelNames)
        {
            channelNames = _channelNames;
            writeSettings();
        }
        public void setTwitchOAuth(string _twitchOAuth)
        {
            twitchOAuth = _twitchOAuth;
            writeSettings();
        }
        public void setDelay(int _delay)
        {
            delay = _delay;
            writeSettings();
        }
        public void setPrefix(string _prefix)
        {
            prefix = _prefix;
            writeSettings();
        }
        public void setJoinMessage(bool _joinMessage)
        {
            joinMessage = _joinMessage;
            writeSettings();
        }
    }

}
