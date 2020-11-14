using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ordersystem;

namespace OrdersysteM
{
    public partial class orderitem : Form
    {
        public OrderItem newitem { get; set; }
        public string goodsname { get; set; }
        public string unitprice { get; set; }
        public string num { get; set; }
        public orderitem()
        {
            InitializeComponent();
        }
        public orderitem(OrderItem item)
        {
            InitializeComponent();
            newitem = item;
            goodsname = item.goodsname;
            unitprice = item.unitprice.ToString();
            num = item.num.ToString();
            textBox1.DataBindings.Add("Text", this, "goodsname");
            textBox2.DataBindings.Add("Text", this, "unitprice");
            textBox3.DataBindings.Add("Text", this, "num");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newitem = new OrderItem(textBox1.Text, Double.Parse(textBox2.Text), int.Parse(textBox3.Text));
            this.Close();
        }
    }
}
