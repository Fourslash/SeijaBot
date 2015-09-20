using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKYPE4COMLib;

namespace SkypeBot2
{
    class SeijaCommander
    {
        public static Skype skype;
        public static Seija Seija;
        
        public static XmlStringsArray BND; // = new XmlStringsArray(@"/Settings/bnd.ext");
        public static XmlStringsArray DongerList; // = new XmlStringsArray(@"/Settings/DongerList.ext");
        public static XmlStringsArray KickList; // = new XmlStringsArray(@"/Settings/KickList.ext");
        public static XmlStringsArray PastaList; // = new XmlStringsArray(@"/Settings/PastaList.ext");
        //public static XmlStringsArray SimpleCommands = new XmlStringsArray(@"/Settings/SimpleCommands.ext");
        public static SeijaSettings Settings; // = new SeijaSettings();
        public static DongerBattler DongerBTL; //= new DongerBattler();
        public static XmlDictionary SimpleCommands;
        public static XmlDictionary SeijaChars;
        public static TwitchChecker TwitchChecker;
        public static DotaLongueChecker DotaLongueChecker;
        public static SkypeConnector skypeConnector;
        
        public static void Init(/*SKYPE4COMLib.Skype sk,SkypeConnector skk*/)
        {
            skype = new Skype();
            Settings = new SeijaSettings();
            skypeConnector = new SkypeConnector(skype);
            skypeConnector.Connect();
            Seija = new Seija();
            

            SimpleCommands = new XmlDictionary(@"Settings/SimpleCommands.ext");
            SeijaChars = new XmlDictionary(@"Settings/SeijaChars.ext");
            BND = new XmlStringsArray(@"Settings/bnd.ext");
            DongerList = new XmlStringsArray(@"Settings/DongerList.ext");
            KickList = new XmlStringsArray(@"Settings/KickList.ext");
            PastaList = new XmlStringsArray(@"Settings/PastaList.ext");
            DongerBTL = new DongerBattler();
            DongerBattler.Init();
            TwitchChecker = new TwitchChecker();
            DotaLongueChecker = new DotaLongueChecker();
            SeijaCommander.skype.ChangeUserStatus(TUserStatus.cusOnline);
            TereziCommunicator.Init();
            Seija.sendMOTD();
        }

        public static void REInit()
        {
            Settings = new SeijaSettings();
            SimpleCommands = new XmlDictionary(@"Settings/SimpleCommands.ext");
            SeijaChars = new XmlDictionary(@"Settings/SeijaChars.ext");
            BND = new XmlStringsArray(@"Settings/bnd.ext");
            DongerList = new XmlStringsArray(@"Settings/DongerList.ext");
            KickList = new XmlStringsArray(@"Settings/KickList.ext");
            PastaList = new XmlStringsArray(@"Settings/PastaList.ext");
            DongerBTL = new DongerBattler();
            DongerBattler.Init();
            TwitchChecker.UpdateStreamListSaveValues();
        }



    }
}
