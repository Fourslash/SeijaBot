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
using System.Windows.Shapes;
using System.Threading;
using HtmlAgilityPack;
using SKYPE4COMLib;

namespace SkypeBot2
{
    class TenshiChecker
    {
        Skype skype;
        Thread thread;
        string link;
        string siteLink;
        List<string> bannedLinks = new List<string>
        {
            "http://gelbooru.com/layout/icons/question-small-white.png",
        };
        List<string> imageLinks = new List<string>();
        public TenshiChecker(string lk, Skype sk)
        {
            skype = sk;
            start(lk);
            //thread = new Thread(start);
            //thread.Start(lk);
        }
        void start(object lk_obj)
        {
            string lk = (string)lk_obj;
            link = lk;
            if (lk.Contains(".jpg") || lk.Contains(".png") || lk.Contains(".gif"))
                imageLinks.Add(lk);
            else
            {
                getSiteLink();
                getImageLinks();
            }
            FindTenshiInLinks();
        }
        void FindTenshiInLinks()
        {
            foreach (string lk in imageLinks)
            {
                if (bannedLinks.FindIndex(x => x == lk) == -1)
                    if (checkLink(lk) == true)
                    {
                        /*вывод информации*/
                        skype.SendMessage(SeijaCommander.Settings.Values.masterName, "Система противоТеншевой обороны обнаружиала опасность по ссылке " + lk + " (✿◕⁀◕)");
                        return;
                    }
            }
        }
        void getSiteLink()
        {
            string temp = link.Remove(0, link.IndexOf(":") + 3);
            int symbolPos = temp.IndexOf("/");
            if (symbolPos == -1)
                siteLink = link;
            else
            {
                temp = temp.Remove(symbolPos);
                siteLink = link.Remove(link.IndexOf(":") + 3) + temp + "/";
            }
        }
        void getImageLinks()
        {
            var Webget = new HtmlWeb();
            var doc = Webget.Load(link);
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//img"))
            {
                if (/*node.Attributes["href"].Value.Contains("http")&&*/(
                    node.Attributes["src"].Value.Contains(".jpg") ||
                    node.Attributes["src"].Value.Contains(".png") ||
                    node.Attributes["src"].Value.Contains(".gif")))
                {
                    if (node.Attributes["src"].Value.Contains("http"))
                        imageLinks.Add(node.Attributes["src"].Value);
                    else
                        imageLinks.Add(siteLink + node.Attributes["src"].Value);
                }
            }
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//*[@href]"))
            {
                if (node.Attributes["href"].Value.Contains("http") && (
                    node.Attributes["href"].Value.Contains(".jpg") ||
                    node.Attributes["href"].Value.Contains(".png") ||
                    node.Attributes["href"].Value.Contains(".gif")))
                    imageLinks.Add(node.Attributes["href"].Value);
            }
        }
        bool checkLink(string lk)
        {
            string sausNaoLink = "http://iqdb.org/?url=" + lk;
            var Webget = new HtmlWeb();
            var doc = Webget.Load(sausNaoLink);
            if (doc.DocumentNode.InnerHtml.Contains("1058042") || doc.DocumentNode.InnerHtml.Contains("tenshi"))
                return true;
            return false;
        }
    }
}