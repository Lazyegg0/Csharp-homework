using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrawlerForm
{
    public partial class Form1 : Form
    {
        Crawler crawler= new Crawler();
        public Form1()
        {
            InitializeComponent();
            bindingSource1.DataSource = crawler.pages;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "爬取开始";
            crawler = new Crawler();
            crawler.StartURL = textBox1.Text;
            Match match = Regex.Match(crawler.StartURL, Crawler.urlParseRegex);
            if (match.Length == 0) return;
            string host = match.Groups["host"].Value;
            crawler.HostFilter = "^" + host + "$";
            crawler.FileFilter = ".html?$";
            crawler.Start();
            label1.Text = "爬取结束";
            bindingSource1.DataSource = crawler.pages;
            bindingSource1.ResetBindings(true);
        }
    }
}
