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
    public partial class order : Form
    {
        public String ordernumber { get; set; }
        public DateTime datetime { get; set; }
        public string datetimes { get; set; }
        public String clienta { get; set; }
        public String price { get; set; }
        public Order currentorder { get; set; }
        public Order lastorder { get; set; }
        public order()
        {
            datetime = DateTime.Now;
            datetimes = DateTime.Now.ToString();
            ordernumber = (datetime.Month * 100000000 + datetime.Day * 1000000 + datetime.Hour * 10000 + datetime.Minute * 100 + datetime.Second).ToString();
            clienta = "无";
            price = "xxxxxx";
            currentorder = new Order(datetime, clienta);
            InitializeComponent();
            textBox1.DataBindings.Add("Text", this, "ordernumber");
            textBox2.DataBindings.Add("Text", this, "clienta");
            textBox3.DataBindings.Add("Text", this, "datetime");
            textBox4.DataBindings.Add("Text", this, "price");
            Itembindingsource.DataSource = currentorder.orderItems;
        }

        public order(Order order1):this()
        {
            clienta = order1.client;
            ordernumber = order1.ordernumber.ToString();
            datetimes = order1.time.ToString();
            price = order1.Totalprice.ToString();
            currentorder = order1;
            lastorder = order1;
            Itembindingsource.DataSource = currentorder.orderItems;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            orderitem form3 = new orderitem();
            form3.ShowDialog();
            if (form3.newitem != null)
            {
                currentorder.AddorderItem(form3.newitem);
                Itembindingsource.DataSource = currentorder.orderItems;
                Itembindingsource.ResetBindings(false);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "删除" && e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                string name = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                OrderItem item = new OrderItem();
                item = currentorder.finditem(name);
                currentorder.RemoveorderItem(item);
                Itembindingsource.DataSource = currentorder.orderItems;
                Itembindingsource.ResetBindings(false);
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "修改" && e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                string name = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                OrderItem item = new OrderItem();
                item = currentorder.finditem(name);
                orderitem form3 = new orderitem(item);
                form3.ShowDialog();
                if (form3.newitem != null)
                {
                    currentorder.AlterorderItem(form3.newitem);
                    Itembindingsource.DataSource = currentorder.orderItems;
                    Itembindingsource.ResetBindings(false);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentorder.client = textBox2.Text;
            lastorder = currentorder;
            Close();
        }
    }
}
