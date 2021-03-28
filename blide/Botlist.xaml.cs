using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Path = System.IO.Path;

namespace Blide
{
    /* 

    remove:
    uhrzeit 
    Prime Gaming
    Abonnent
    Abzeichen)
    Moderator
    Badge)
    GLHF Pledge
    Subscriber
    \r\n
    00:00AbostatusModNAME:message \r\n

    Chatterino:
    between 00:00 and next :
    -> :xxSTRING:
*/
    
    public partial class Botlist : UserControl
    {

        MainWindow wnd = (MainWindow)Application.Current.MainWindow;
        Boolean useTimestamps = false;
        Boolean chatterino = false;
        string clipboardtext = "";
        string[] emotes = new string[] {"PrideShrug", "PrideFloat", "PridePog", "PrideCute", "PrideLaugh", "PrideUwu", "PrideWave", "PrideUnicorn", "PrideDragon", "PrideToucan", "PrideLGBTea", "PridePenguin", "PrideFlower", "PrideLion", "PrideHeartR", "PrideHeartL", "PrideRise", "PrideWorld", "PrideCrown", "PrideKoala", "PrideHeyyy", "PrideLove", "PrideStrong", "PridePaint", "HyperTiger", "HyperLost", "HyperCrate", "HyperCooldown", "HyperJump", "HyperSlam", "HyperReveal", "HyperHex", "HyperCheese", "HyperParkour", "HyperHaste", "HyperMine", "HyperGravity", "HyperMayhem", "HyperCrown", "KPOPselfie", "KPOPTT", "KPOPmerch", "KPOPvictory", "KPOPheart", "KPOPlove", "KPOPglow", "KPOPfan", "KPOPdance", "KPOPcheer", "HypeSideeye", "SirUwU", "SirPrise", "SirSad", "SirMad", "SirSword", "SirShield", "ALLINTOVOTE", "ShowOfHands", "FootGoal", "FootYellow", "FootBall", "BlackLivesMatter", "SingsMic", "SingsNote", "PorscheWIN", "BOP", "VirtualHug", "ExtraLife", "TwitchSings", "SoonerLater", "HolidayTree", "HolidaySanta", "HolidayPresent", "HolidayOrnament", "FBChallenge", "FBPenalty", "PixelBob", "GunRun", "HolidayCookie", "HolidayLog", "FBCatch", "FBBlock", "FBSpiral", "FBPass", "FBRun", "GenderFluidPride", "NonBinaryPride", "MaxLOL", "IntersexPride", "TwitchRPG", "PansexualPride", "AsexualPride", "MercyWing2", "PinkMercy", "BisexualPride", "LesbianPride", "GayPride", "TransgenderPride", "MercyWing1", "PartyHat", "EarthDay", "TombRaid", "PopCorn", "FBtouchdown", "PurpleStar", "GreenTeam", "RedTeam", "TPFufun", "TwitchVotes", "DarkMode", "HSWP", "TPcrunchyroll", "HSCheers", "TwitchUnity", "PowerUpL", "Squid4", "PowerUpR", "Squid3", "LUL", "Squid2", "EntropyWins", "Squid1", "SabaPing", "BegWan", "BigPhish", "TearGlove", "TehePelo", "InuyoFace", "TwitchLit", "Kappu", "CarlSmile", "KonCha", "CrreamAwk", "PunOko", "ThankEgg", "PartyTime", "MorphinTime", "RlyTho", "TheIlluminati", "UWot", "TBAngel", "YouDontSay", "MVGame", "KAPOW", "NinjaGrumpy", "ItsBoshyTime", "CoolStoryBob", "TriHard", "SuperVinlin", "FreakinStinkin", "Poooound", "CurseLit", "UncleNox", "RaccAttack", "StrawBeary", "PrimeMe", "BrainSlug", "BatChest", "WTRuck", "TooSpicy", "Jebaited", "DogFace", "BlargNaut", "TakeNRG", "DatSheffy", "UnSane", "copyThis", "pastaThat", "imGlitch", "GivePLZ", "TheTarFu", "PicoMause", "TinyFace", "DrinkPurple", "DxCat", "RuleFive", "FutureMan", "OpieOP", "DoritosChip", "PJSugar", "VoteYea", "VoteNay", "ChefFrank", "StinkyCheese", "NomNom", "SmoocherZ", "cmonBruh", "KappaWealth", "OhMyDog", "OSFrog", "SeriousSloth", "KomodoHype", "VoHiYo", "MikeHogu", "KappaClaus", "KappaRoss", "MingLee", "SeemsGood", "twitchRaid", "KappaPride", "bleedPurple", "CoolCat", "DendiFace", "NotLikeThis", "riPepperonis", "duDudu", "ShadyLulu", "ArgieB8", "CorgiDerp", "PraiseIt", "TTours", "mcaT", "BuddhaBar", "WutFace", "PRChase", "Mau5", "HeyGuys", "NotATK", "PermaSmug", "panicBasket", "BabyRage", "HassaanChop", "TheThing", "EleGiggle", "EleGiggle", "PanicVis", "ANELE", "BrokeBack", "PipeHype", "YouWHY", "RitzMitz", "GrammarKing", "RalpherZ", "TF2John", "SoBayed", "ThunBeast", "BigBrother", "WholeWheat", "Keepo", "DAESuppy", "Kippa", "FailFish", "PogChamp", "OneHand", "PMSTwin", "FUNgineer", "DBstyle", "AsianGlow", "ResidentSleeper", "4Head", "BibleThump", "HotPokket", "ShazBotstix", "FrankerZ", "SMOrc", "ArsonNoSexy", "PunchTrees", "SSSsss", "Kreygasm", "KevinTurtle", "PJSalt", "SwiftRage", "DansGame", "GingerPower", "BCWarrior", "StoneLightning", "TheRinger", "RedCoat", "Kappa", "JonCarnage", "MrDestructoid", "OptimizePrime", "JKanStyle", "PedoBear", "RebeccaBlack", "CiGrip", "DatSauce", "ForeverAlone", "GabeN", "HailHelix", "HerbPerve", "iDog", "rStrike", "ShoopDaWhoop", "SwedSwag", "M&Mjc", "bttvNice", "TopHam", "TwaT", "WatChuSay", "SavageJerky", "Zappa", "tehPoleCat", "AngelThump", "HHydro", "TaxiBro", "BroBalt", "ButterSauce", "BaconEffect", "SuchFraud", "CandianRage", "She'llBeRight", "D:", "VisLaud", "KaRappa", "YetiZ", "miniJulia", "FishMoley", "Hhhehehe", "KKona", "PoleDoge", "sosGame", "CruW", "RarePepe", "iamsocal", "haHAA", "FeelsBirthdayMan", "RonSmug", "KappaCool", "FeelsBadMan", "BasedGod", "bUrself", "ConcernDoge", "FeelsGoodMan", "FireSpeed", "NaM", "SourPls", "LuL", "SaltyCorn", "FCreep", "monkaS", "VapeNation", "ariW", "notsquishY", "FeelsAmazingMan", "DuckerZ", "FeelsPumpkinMan", "SqShy", "Wowee", "WubTF", "cvR", "cvL", "cvHazmat", "cvMask" };
        public Botlist()
        {
            InitializeComponent();

        }

        private void Bu1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Bu1.Toggled1 == true)
            {
                useTimestamps = true;
            }
            else
            {
                useTimestamps = false;
            }

        }

        private void Bu2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Bu2.Toggled1 == true)
            {
                chatterino = true;
            }
            else
            {
                chatterino = false;
            }
        }


        //:xxSTRING:asdasd:xx:xxString:asdasd:xx:xxString
        private void ExtractButton_Click(object sender, RoutedEventArgs e)
        {
            char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', };
            string[] toRemove = { "turbo", "Turbo", "verschenkt", "Gaming", "zeichen)", "bonennt", "Moderator", "Badge", "Pledge", "ubscriber", "nkabos", "VIP", "fiziert", "cheer 1,000,000", "cheer 900,000", "cheer 800,000", "cheer 700,000", "cheer 600,000", "cheer 500,000", "cheer 400,000", "cheer 300,000", "cheer 200,000", "cheer 100,000", "cheer 75,000", "cheer 50,000", "cheer 25,000", "cheer 10,000", "cheer 5,000", "cheer 1,000", "cheer 1.000.000", "cheer 900.000", "cheer 800.000", "cheer 700.000", "cheer 600.000", "cheer 500.000", "cheer 400.000", "cheer 300.000", "cheer 200.000", "cheer 100.000", "cheer 75.000", "cheer 50.000", "cheer 25.000", "cheer 10.000", "cheer 5.000", "cheer 1.000", "cheer 100", "cheer 1", "Leader 1", "Leader 2", "Leader 3", "Brawlhalla", "Founder", "Horde", "1979 Revolution", "60 Seconds!", "H1Z1", "Battle Chef Brigade", "Battlerite", "Charity 2018", "Brawlhalla", "Broken Age", "Strike Back", "Cry HD Collection", "Devilian", "Duelyst", "Innerspace", "Access Pass 2018", "Access Pass 2019", "Access Pass 2020", " Ranger", "Staff", " Gift Subs", "Subscriber", "The Surge", "badge", "AutoMod", "TwitchCon 2017", "TwitchCon 2018", "Amsterdam 2020", "TwitchCon 2020", "EU 2019", "gelöscht", "Gifter", ")" };
            string stringBuilder = "";
            if (chatterino)
            {
                char[] temp = usernames.Text.ToCharArray();
                for (int i = 0; i < temp.Length; i++)
                {
                    if (numbers.Contains(temp[i]) && numbers.Contains(temp[i + 1]) && temp[i + 2] == ':' && numbers.Contains(temp[i + 3]) && numbers.Contains(temp[i + 4])) //if zz:zz -> danach name bis :
                    {
                        int j = i + 6;
                        while (temp[j] != ':')
                        {
                            stringBuilder = stringBuilder + temp[j] + "";
                            j++;
                        }
                        stringBuilder = stringBuilder + ";";
                        i = i + 5;

                    }
                    if(numbers.Contains(temp[i]) && temp[i + 1] == ':' && numbers.Contains(temp[i + 2]) && numbers.Contains(temp[i + 3])) //if z:zz -> danach name bis :
                    {
                        int j = i + 5;
                        while (temp[j] != ':')
                        {
                            stringBuilder = stringBuilder + temp[j] + "";
                            j++;
                        }
                        stringBuilder = stringBuilder + ";";
                        i = i + 4;

                    }


                }

            }
            else if (useTimestamps)
            {
                string[] lines = usernames.Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None); //devide in lines

                for (int i = 0; i < lines.Length; i++) // every line
                {
                    if (lines[i] != null && lines[i].Length > 4)
                    {

                        lines[i] = lines[i].Remove(0, 4); //remove timestamp      

                        if (lines[i].Contains("VerifiziertStreamElement")) { lines[i] = ""; }
                        if (lines[i].Contains("aboniert"))
                        {
                            lines[i] = "";
                            lines[i - 1] = "";
                            lines[i + 1] = "";
                            lines[i + 2] = "";
                        }
                        for (int r = 0; r < toRemove.Length; r++)
                        {
                            lines[i] = getAfter(lines[i], toRemove[r]);
                        }


                        if (lines[i].Contains(":"))
                        {
                            lines[i] = lines[i].Substring(0, lines[i].IndexOf(":", 0)); // clear everything after :
                        }
                    }
                }

                for (int j = 0; j < lines.Length; j++)
                {
                    if (lines[j] != "" && lines[j] != null)
                    {
                        if (emotes.Contains(lines[j]) || emotes.Contains(lines[j].Remove(0, 1)) || emotes.Contains(lines[j].Remove(lines[j].Length - 1)))
                        {
                            lines[j] = "";
                        }
                    }
                    stringBuilder = stringBuilder + lines[j] + ";";

                }



            }
            else
            {
                string[] lines = usernames.Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None); //devide in lines

                for (int i = 0; i < lines.Length; i++) // every line
                {
                    if (lines[i] != null)
                    {
                        if (lines[i].Contains("Streamelements")) { lines[i] = ""; }
                        if (lines[i].Contains("VerifiziertStreamElement")) { lines[i] = ""; }
                        for (int r = 0; r < toRemove.Length; r++)
                        {
                            lines[i] = getAfter(lines[i], toRemove[r]);
                        }
                        
                        if (lines[i].Contains(":"))
                        {
                            lines[i] = lines[i].Substring(0, lines[i].IndexOf(":", 0)); // clear everything after :
                        }
                    }
                }

                
                for (int j = 0; j < lines.Length; j++)
                {
                    if(lines[j] != "" && lines[j] != null) {
                    if (emotes.Contains(lines[j]) || emotes.Contains(lines[j].Remove(0, 1)) || emotes.Contains(lines[j].Remove(lines[j].Length - 1)))
                    {
                        lines[j] = "";
                    }}
                    stringBuilder = stringBuilder + lines[j] + ";";

                }




            }



            string stringBuilderA = stringBuilder.Replace(";", "\n");
            string[] noDuplicates = RemoveDuplicates(stringBuilderA.Split('\n'));
            string stringBuilderANoDuplicates = string.Join("\n", noDuplicates);
            usernames.Text = stringBuilderANoDuplicates;
        }

        public string[] RemoveDuplicates(string[] myList)
        {
            System.Collections.ArrayList newList = new System.Collections.ArrayList();

            foreach (string str in myList)
                if (!newList.Contains(str))
                    newList.Add(str);
            return (string[])newList.ToArray(typeof(string));
        }

        public string getAfter(string strSource, string strStart)
        {
            if (strSource.Contains(strStart))
            {
                int Start;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                return strSource.Remove(0, Start);
            }

            return strSource;
        }
        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            //string writer:
            string usernamesfinal = usernames.Text;
            usernamesfinal = usernamesfinal.Replace("\r\n", ";");

            //get broadcaster name
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string[] lines = System.IO.File.ReadAllLines(Path.Combine(docPath, "BlideSettings.txt"));
            string broadcaster = lines[1];


            //choose folder
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            saveFileDialog.FileName = "" + DateTime.Now.ToString(@"MM.dd.yyyy") + "_" + broadcaster + "_" + "botlist";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (saveFileDialog.ShowDialog() == true) { File.WriteAllText(saveFileDialog.FileName, usernamesfinal); }

        }

        private void SaveImportButton_Click(object sender, RoutedEventArgs e)
        {
            //string writer:
            string usernamesfinal = usernames.Text;
            usernamesfinal = usernamesfinal.Replace("\r\n", ";");

            //get broadcaster name
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string[] lines = System.IO.File.ReadAllLines(Path.Combine(docPath, "BlideSettings.txt"));
            string broadcaster = lines[1];


            //choose folder
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            saveFileDialog.FileName = "" + DateTime.Now.ToString(@"MM.dd.yyyy") + "_" + broadcaster + "_" + "botlist";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (
                saveFileDialog.ShowDialog() == true) { File.WriteAllText(saveFileDialog.FileName, usernamesfinal);
                wnd.import("" + saveFileDialog.FileName);
            }

        }


        private void LoadClipboardButton_Click(object sender, RoutedEventArgs e)
        {
            clipboardtext = Clipboard.GetText();
            usernames.Text = clipboardtext;
        }

        private void replaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (toReplace.Text != "" && toReplace.Text != null && replacement.Text != null)
            {
                usernames.Text = usernames.Text.Replace(toReplace.Text, replacement.Text);
            }
        }

        
    }
}
