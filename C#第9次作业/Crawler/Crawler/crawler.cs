using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrawlerForm
{


    class Crawler
    {
        public List<Page> pages = new List<Page>();

        private Dictionary<string, bool> Urls = new Dictionary<string, bool>();
        public Dictionary<string, bool> DownloadedPages { get => Urls; }

        private readonly string urlDetectRegex = @"(href|HREF)\s*=\s*[""'](?<url>[^""'#>]+)[""']";
        public static readonly string urlParseRegex = @"^(?<site>https?://(?<host>[\w\d.]+)(:\d+)?($|/))([\w\d]+/)*(?<file>[^#?]*)";

        public string HostFilter { get; set; }

        public string FileFilter { get; set; }

        public int MaxPage { get; set; }

        public string StartURL { get; set; }

        public Encoding HtmlEncoding { get; set; }

        private Queue<string> pending = new Queue<string>();



        public Crawler()
        {
            MaxPage = 100;
            HtmlEncoding = Encoding.UTF8;
        }

        public void Start()
        {
            Urls.Clear();
            pending.Clear();
            pending.Enqueue(StartURL);

            while (Urls.Count < MaxPage && pending.Count > 0)
            {
                string url = pending.Dequeue();
                try
                {
                    string html = DownLoad(url);
                    Urls[url] = true;
                    pages.Add(new Page(pages.Count+1, url, "success"));
                    Parse(html, url);
                }
                catch (Exception ex)
                {
                    pages.Add(new Page(pages.Count + 1, url, "  Error:" + ex.Message));
                }
            }
        }

        private string DownLoad(string url)
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string html = webClient.DownloadString(url);
            string fileName = Urls.Count.ToString();
            File.WriteAllText(fileName, html, Encoding.UTF8);
            return html;
        }

        private void Parse(string html, string pageUrl)
        {

            var matches = new Regex(urlDetectRegex).Matches(html);
            foreach (Match match in matches)
            {
                string linkUrl = match.Groups["url"].Value;
                if (linkUrl == null || linkUrl == "") continue;
                linkUrl = FixUrl(linkUrl, pageUrl);//转绝对路径

                Match linkUrlMatch = Regex.Match(linkUrl, urlParseRegex);
                string host = linkUrlMatch.Groups["host"].Value;
                string file = linkUrlMatch.Groups["file"].Value;
                if (file == "") file = "index.html";

                if (Regex.IsMatch(host, HostFilter) && Regex.IsMatch(file, FileFilter)
                  && !Urls.ContainsKey(linkUrl))
                {
                    pending.Enqueue(linkUrl);
                    Urls.Add(linkUrl, false);
                }
            }
        }


        //将相对路径转为绝对路径
        static private string FixUrl(string url, string baseUrl)
        {
            if (url.Contains("://"))
            {
                return url;
            }
            if (url.StartsWith("//"))
            {
                return "http:" + url;
            }
            if (url.StartsWith("/"))
            {
                Match urlMatch = Regex.Match(baseUrl, urlParseRegex);
                String site = urlMatch.Groups["site"].Value;
                return site.EndsWith("/") ? site + url.Substring(1) : site + url;//去掉多余的/
            }

            if (url.StartsWith("../"))
            {
                url = url.Substring(3);
                int length1 = baseUrl.LastIndexOf('/');
                return FixUrl(url, baseUrl.Substring(0, length1));//返回上一层目录
            }

            if (url.StartsWith("./"))
            {
                return FixUrl(url.Substring(2), baseUrl);//返回当前目录
            }

            int length = baseUrl.LastIndexOf("/");
            return baseUrl.Substring(0, length) + "/" + url;
        }
    }
}
