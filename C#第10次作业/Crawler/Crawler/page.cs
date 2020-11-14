using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler;

namespace CrawlerForm
{
    public class Page
    {
        public int number { get; set; }
        public string url { get; set; }
        public string status { get; set; }
        public Page( ) { }
        public Page(int number, string url, string status)
        {
            this.number = number;
            this.url = url;
            this.status = status;
        }
    }


}
