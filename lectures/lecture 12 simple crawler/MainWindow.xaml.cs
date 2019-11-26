using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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

namespace lecture_12_simple_crawler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //threadpools allows you to start as many as threads you want immediately
            ThreadPool.SetMaxThreads(100000, 100000);
            ThreadPool.SetMinThreads(100000, 100000);
            ServicePointManager.DefaultConnectionLimit = 1000;//this increases your number of connections to per host at the same time

        }

        public class perDocument
        {
            public string srDocTitle = "";
            public List<string> lstExtractedUrls = new List<string>();
        }

        private static perDocument extractLinks(string srUrl)
        {
            var baseUri = new Uri(srUrl);
            HtmlWeb web = new HtmlWeb();
            web.AutoDetectEncoding = true;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = web.Load(srUrl);

            perDocument myDoc = new perDocument();

            // extracting all links
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))//xpath notation
            {
                HtmlAttribute att = link.Attributes["href"];
                //this is used to convert from relative path to absolute path
                var absoluteUri = new Uri(baseUri, att.Value.ToString());

                if (!absoluteUri.ToString().StartsWith("http://") && !absoluteUri.ToString().StartsWith("https://"))
                    continue;

                myDoc.lstExtractedUrls.Add(absoluteUri.ToString());
            }

            myDoc.lstExtractedUrls = myDoc.lstExtractedUrls.Distinct().ToList();

            var vrDocTitle = doc.DocumentNode.SelectSingleNode("//title")?.InnerText.ToString().Trim();
            vrDocTitle = System.Net.WebUtility.HtmlDecode(vrDocTitle);

            myDoc.srDocTitle = vrDocTitle;
            return myDoc;
        }

        private void BtnCrawlSingle_Click(object sender, RoutedEventArgs e)
        {
            var gg = extractLinks(txtUrl.Text);

            lstBoxFoundUrls.Items.Clear();
            lstBoxFoundUrls.Items.Add(gg.srDocTitle);
            foreach (var item in gg.lstExtractedUrls)
            {
                lstBoxFoundUrls.Items.Add(item);
            }
        }
    }
}
