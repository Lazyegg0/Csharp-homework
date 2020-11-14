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
using MySql.Data.MySqlClient;



namespace OrdersysteM
{
    public partial class ordersystem : Form
    {
        OrderService Orderservice;
        Order Order=new Order();
        public String Keyword { get; set; }
        public ordersystem()
        {
            InitializeComponent();
            
            Orderservice = new OrderService( );
            dataGridView1.DataSource = Orderservice.table;
            selectcondition.SelectedIndex = 0;
            textBox1.DataBindings.Add("Text", this, "Keyword");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (selectcondition.SelectedIndex)
            {
                case 0://根据订单号查询
                    try
                    {
                        Orderservice.FindOrderbynumeber(Convert.ToInt64(Keyword));
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show(e2.Message);
                    }
                    break;
                case 1://根据客户查询
                    try
                    {
                        Orderservice.FindOrderbyclient(Keyword);
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show(e2.Message);
                    }
                    
                    break;
                case 2://根据货物查询
                    try
                    {
                        Orderservice.FindOrderbygoods(Keyword);
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show(e2.Message);
                    }              
                    break;
            }
            orderbindingSource.ResetBindings(false);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "查看" && e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                String numeber =dataGridView1.CurrentRow.Cells["订单号"].Value.ToString();
                Orderservice.FindOrderbynumeber(Convert.ToInt64(numeber));
                Order OrdeR = Order.transorder(Orderservice.table);
                order form2 = new order(OrdeR);
                form2.ShowDialog();
                Orderservice.AlterOrder(form2.lastorder);
                Orderservice.DSconnect();
                orderbindingSource.DataSource = Orderservice.table;
                orderbindingSource.ResetBindings(true);
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "删除" && e.RowIndex >= 0)
            {
                DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];
                String numeber = dataGridView1.CurrentRow.Cells["订单号"].Value.ToString();
                Orderservice.FindOrderbynumeber(Convert.ToInt64(numeber));
                Order OrdeR = Order.transorder(Orderservice.table);
                Orderservice.RemoveOrder(OrdeR);
                Orderservice.DSconnect();
                orderbindingSource.DataSource = Orderservice.table;
                orderbindingSource.ResetBindings(true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            order form4 = new order();
            form4.ShowDialog();
            if (form4.lastorder != null)
            {
                Orderservice.Addorder(form4.lastorder);
                Orderservice.DSconnect();
                orderbindingSource.DataSource = Orderservice.table;
                orderbindingSource.ResetBindings(true);
            }
                
        }
    }
}
