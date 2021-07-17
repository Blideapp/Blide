using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace Blide
{
    public class IrcClient
    {
        /*
         * This is used to use twitch irc backend
         */
        SettingsManager settings = new SettingsManager();
        public string userName;
        private string channel;

        private TcpClient _tcpClient;
        private StreamReader _inputStream;
        private StreamWriter _outputStream;

        public IrcClient(string ip, int port, string userName, string password, string channel)
        {

            try
            {
                this.userName = userName;
                this.channel = channel;

                _tcpClient = new TcpClient(ip, port);
                _inputStream = new StreamReader(_tcpClient.GetStream());
                _outputStream = new StreamWriter(_tcpClient.GetStream());

                // Try to join the room
                _outputStream.WriteLine("PASS " + password);
                _outputStream.WriteLine("NICK " + userName);
                _outputStream.WriteLine("USER " + userName + " 8 * :" + userName);
                _outputStream.WriteLine("JOIN #" + channel);
                _outputStream.Flush();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                settings.restartApplication();
            }
        }

        public void SendIrcMessage(string message) //sends irc message
        {

            try
            {
                _outputStream.WriteLine(message);
                _outputStream.Flush();
            }
            catch (Exception ex)
            {
                ReportError("an IRC error occured in function SendIrcMessage and Blide has been stopped. Error:" + ex.Message);


            }
        }

        public void SendPublicChatMessage(string message) //builds irc message
        {
            try
            {
                SendIrcMessage(":" + userName + "!" + userName + "@" + userName +
                ".tmi.twitch.tv PRIVMSG #" + channel + " :" + message);
            }
            catch (Exception ex)
            {
                ReportError("an IRC error occured in function SendPublicChatMessage and Blide has been stopped. Error:" + ex.Message);

            }
        }

        public string ReadMessage() //chat reader using irc
        {
            try
            {
                string message = _inputStream.ReadLine();
                return message;
            }
            catch (Exception ex)
            {
                ReportError("an IRC error occured in function ReadMessage and Blide has been stopped. Error:" + ex.Message);
                return "Error receiving message: " + ex.Message;
            }
        }

        private void ReportError(string error) //reports errors
        {

            WebClient client = new WebClient(); //reports error to blide api
            try
            {
                string apirequest = client.DownloadString("http://api.blideapp.de/blidestats.php?channel=" + settings.getChannelNames() + "&errorlog=" + error);
                if (apirequest != "")
                {
                    MessageBox.Show("Blide API error: " + apirequest);
                }


            }
            catch (WebException ex) //if api fails
            {
                String responseFromServer = ex.Message.ToString() + " ";
                if (ex.Response != null)
                {
                    using (WebResponse response = ex.Response)
                    {
                        Stream dataRs = response.GetResponseStream();
                        using (StreamReader reader = new StreamReader(dataRs))
                        {
                            responseFromServer += reader.ReadToEnd();
                        }
                    }
                }
                MessageBox.Show("Blide API not reachable - error: " + responseFromServer);
            }
            MessageBox.Show(error); //messagebox with error
            settings.restartApplication(); //restarts blide if clicked OK
        }

    }
}
