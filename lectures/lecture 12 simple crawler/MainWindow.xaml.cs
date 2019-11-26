using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
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
            startTimer();
        }

        private static HashSet<string> hsCrawledLinks = new HashSet<string>();
        private static HashSet<string> hsCrawlingLinks = new HashSet<string>();
        private static HashSet<string> hstoBeCrawledLinks = new HashSet<string>();
        private static int irMaxThreadCount = 10;

        private void startTimer()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.TimeOfDay;
        }

        public class perDocument
        {
            public string srDocTitle = "";
            public List<string> lstExtractedUrls = new List<string>();
        }

        private static perDocument extractLinks(string srUrl)
        {
            lock (swCrawling)
                swCrawling.WriteLine(DateTime.Now + "\t" + srUrl);

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

        static StreamWriter swTobeCrawled = new StreamWriter("tobecrawled.txt");
        static StreamWriter swCrawling = new StreamWriter("crawling.txt");
        static StreamWriter swCrawled = new StreamWriter("crawled.txt");

        private void BtnCrawlSingle_Click(object sender, RoutedEventArgs e)
        {
            swTobeCrawled.AutoFlush = true;
            swCrawling.AutoFlush = true;
            swCrawled.AutoFlush = true;

            hstoBeCrawledLinks.Add(txtUrl.Text);

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += startNewCrawl;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            dispatcherTimer.Start();
        }

        List<Task> lstRunninTasks = new List<Task>();
        private static bool blNewCrawl = false;



        private void startNewCrawl(object sender, EventArgs e)
        {
            if (blNewCrawl == true)
                return;
            blNewCrawl = true;
            lstRunninTasks = lstRunninTasks.Where(pr => pr.Status != TaskStatus.RanToCompletion && pr.Status!=TaskStatus.Faulted).ToList();

            if (lstRunninTasks.Count >= irMaxThreadCount)
            {
                blNewCrawl = false;
                return;
            }
             
            string srNewCrawlUrl = "";
            List<string> lstRemoveToCrawled = new List<string>();
            //you have to lock all of the objects that will be shared between different threads
            lock (hstoBeCrawledLinks)//this used to sync different threads
                foreach (var vrTobeCrawl in hstoBeCrawledLinks)
                {
                    lock (hsCrawlingLinks)
                        if (hsCrawlingLinks.Contains(vrTobeCrawl))
                            continue;
                    lock (hsCrawledLinks)
                        if (hsCrawledLinks.Contains(vrTobeCrawl))
                    {
                        //hstoBeCrawledLinks.Remove(vrTobeCrawl);//i dont do that because this would break foreach loop -- you would get collection modified error
                        lstRemoveToCrawled.Add(vrTobeCrawl);
                        continue;
                    }

                    srNewCrawlUrl = vrTobeCrawl;
                }

            lock (hstoBeCrawledLinks)
            {
                foreach (var item in lstRemoveToCrawled)
                {
                    hstoBeCrawledLinks.Remove(item);
                }
            }

            if(srNewCrawlUrl.Length>1)
            {
                var vrTask = Task.Factory.StartNew(() => {
                    startSingleCrawl(srNewCrawlUrl);
                });
                lstRunninTasks.Add(vrTask);
            }
            blNewCrawl = false;
        }

        private void startSingleCrawl(string srurl)
        {
            var gg = extractLinks(srurl);

            lock (hsCrawledLinks)
                hsCrawledLinks.Add(srurl);
            lock (hsCrawlingLinks)
                hsCrawlingLinks.Remove(srurl);

            lock(swCrawled)
            {
                swCrawled.WriteLine(DateTime.Now + "\t" + srurl);
            }

            lock(hstoBeCrawledLinks)
            {
                foreach (var item in gg.lstExtractedUrls)
                {
                    hstoBeCrawledLinks.Add(item);
                }
            }

            lock(swTobeCrawled)
            {
                foreach (var item in gg.lstExtractedUrls)
                {
                    swTobeCrawled.WriteLine(DateTime.Now + "\t" + item);
                }
              
            }

            return;

            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lstBoxFoundUrls.Items.Clear();
                lstBoxFoundUrls.Items.Add(gg.srDocTitle);
                foreach (var item in gg.lstExtractedUrls)
                {
                    lstBoxFoundUrls.Items.Add(item);
                }
            }));
        }
    }
}
