using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Ordersystem
{
    public class Order
    {
        public long  ordernumber { get; set; }//订单号

        public string client { get; set; }//客户名

        public double Totalprice { get; set; }//总价格

        public DateTime time { get; set; }//时间

        public DataTable table=new DataTable();
        
        public List<OrderItem> orderItems = new List<OrderItem>();//订单明细项List
        //建立订单
        public Order() { }
        public Order(DateTime a, string c)
        {
            this.ordernumber = a.Month * 100000000 + a.Day * 1000000 + a.Hour * 10000 + a.Minute * 100+a.Second;
            this.client = c;
            this.time = a;
        }
        //增加商品
        public void AddorderItem(OrderItem o)
        {
            string connStr = "server=localhost;port=3306;user=root;password=jhl794613285; database=ordersystem;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                MySqlCommand cmd = new MySqlCommand("INSERT INTO `orderitem` (`订单号`, `商品`, `单价`, `数量`) VALUES('" + this.ordernumber + "', '" + o.goodsname + "', '" + o.unitprice + "', '" + o.num + "')", conn);
                MySqlDataAdapter reader = new MySqlDataAdapter(cmd);
                table.Clear();
                reader.Fill(this.table);

            }
            catch (MySqlException ex)
            {
            }
            conn.Close();
            Totalprice += o.totalprice;
        }
        //删除某个商品
        public void RemoveorderItem(OrderItem o)
        {
            Totalprice -= o.totalprice;
            string connStr = "server=localhost;port=3306;user=root;password=jhl794613285; database=ordersystem;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                MySqlCommand cmd = new MySqlCommand("DELETE FROM `orderitem` WHERE(`订单号`= '"+this.ordernumber+ "'AND`商品`= '" + o.goodsname + "')", conn);
                MySqlDataAdapter reader = new MySqlDataAdapter(cmd);
                table.Clear();
                reader.Fill(this.table);

            }
            catch (MySqlException ex)
            {
            }
            conn.Close();
            
        }
        //修改某个商品的明细
        public void AlterorderItem(OrderItem o)
        {
            string connStr = "server=localhost;port=3306;user=root;password=jhl794613285; database=ordersystem;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                MySqlCommand cmd = new MySqlCommand("UPDATE `orderitem` SET `单价`='"+o.unitprice+"', `数量`='"+o.num+"' WHERE (`订单号`= '" + this.ordernumber + "'AND`商品`= '" + o.goodsname + "')", conn);
                MySqlDataAdapter reader = new MySqlDataAdapter(cmd);
                table.Clear();
                reader.Fill(this.table);

            }
            catch (MySqlException ex)
            {
            }
            conn.Close();
        }

        public Order transorder(DataTable dataTable)
        {
            Order ordeR = new Order();
            ordeR.ordernumber = Convert.ToInt64(dataTable.Rows[0][0].ToString());
            ordeR.client = dataTable.Rows[0][1].ToString();
            ordeR.Totalprice = double.Parse(dataTable.Rows[0][2].ToString());
            ordeR.time = Convert.ToDateTime(dataTable.Rows[0][3].ToString());
            return ordeR;
        }

        public void finditem(string name)
        {
            string connStr = "server=localhost;port=3306;user=root;password=jhl794613285; database=ordersystem;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `orderitem` WHERE `商品` = '" + name + "'", conn);
                MySqlDataAdapter reader = new MySqlDataAdapter(cmd);
                table.Clear();
                reader.Fill(this.table);

            }
            catch (MySqlException ex)
            {
            }
            conn.Close();
        }
         
    }
}
