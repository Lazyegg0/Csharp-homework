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
    public partial class ordersystem : Form
    {
        OrderService Orderservice;
        Order Order;
        public String Keyword { get; set; }
        public ordersystem()
        {
            InitializeComponent();
            string filename = "orders.xml";
            Orderservice = new OrderService(filename);
            orderbindingSource.DataSource = Orderservice.orders;
            selectcondition.SelectedIndex = 0;
            textBox1.DataBindings.Add("Text", this, "Keyword");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (selectcondition.SelectedIndex)
            {
                case 0://根据订单号查询
                    orderbindingSource.DataSource = Orderservice.FindOrderbynumeber(Convert.ToInt64(Keyword));
                    break;
                case 1://根据客户查询
                    orderbindingSource.DataSource = Orderservice.FindOrderbyclient(Keyword);
                    break;
                case 2://根据货物查询
                    orderbindingSource.DataSource = Orderservice.FindOrderbygoods(Keyword);
                    break;
            }
            orderbindingSource.ResetBindings(true);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "查看" && e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                long numeber = Convert.ToInt64(dataGridView1.CurrentRow.Cells[0].Value);
                Order = Orderservice.FindOrderbynumeber(numeber).FirstOrDefault();
                order form2 = new order(Order);
                form2.ShowDialog();
                Orderservice.AlterOrder(form2.lastorder);
                orderbindingSource.DataSource = Orderservice.orders;
                orderbindingSource.ResetBindings(true);
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "删除" && e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                long numeber = Convert.ToInt64(dataGridView1.CurrentRow.Cells[0].Value);
                Order = Orderservice.FindOrderbynumeber(numeber).FirstOrDefault();
                Orderservice.RemoveOrder(Order);
                orderbindingSource.DataSource = Orderservice.orders;
                orderbindingSource.ResetBindings(true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Orderservice.Export("orders.xml");
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            order form4 = new order();
            form4.ShowDialog();
            if (form4.lastorder != null)
            {
                Orderservice.Addorder(form4.lastorder);
                orderbindingSource.DataSource = Orderservice.orders;
                orderbindingSource.ResetBindings(true);

            }
                
        }
    }
}
