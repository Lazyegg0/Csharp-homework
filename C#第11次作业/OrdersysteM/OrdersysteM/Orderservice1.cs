using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;
using Ordersystem;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Ordersystem
{
    public class OrderService
    {
        public OrderService()
        {
            DSconnect();
        }
        public DataTable table=new DataTable();

        public List<Order> orders = new List<Order>();

        public void DSconnect()
        {
            string connStr = "server=localhost;port=3306;user=root;password=jhl794613285; database=ordersystem;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `order`", conn);
                MySqlDataAdapter reader = new MySqlDataAdapter(cmd);
                table.Clear();
                reader.Fill(this.table);

            }
            catch (MySqlException ex)
            {
            }
            conn.Close();
        }
        //增加订单
        public void Addorder(Order o)
        {
            string connStr = "server=localhost;port=3306;user=root;password=jhl794613285; database=ordersystem;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                MySqlCommand cmd = new MySqlCommand("INSERT INTO `order` (`订单号`, `客户`, `总价`, `时间`) VALUES('"+o.ordernumber+"', '"+o.client+"', '"+o.Totalprice+"', '"+o.time+"')", conn);
                MySqlDataAdapter reader = new MySqlDataAdapter(cmd);
                table.Clear();
                reader.Fill(this.table);

            }
            catch (MySqlException ex)
            {
            }
            conn.Close();

        }
        //删除订单
        public void RemoveOrder(Order o)
        {
            string connStr = "server=localhost;port=3306;user=root;password=jhl794613285; database=ordersystem;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                MySqlCommand cmd = new MySqlCommand("DELETE FROM `order` WHERE (`订单号`='"+o.ordernumber+"')", conn);
                MySqlDataAdapter reader = new MySqlDataAdapter(cmd);
                table.Clear();
                reader.Fill(this.table);

            }
            catch (MySqlException ex)
            {
            }
            conn.Close();
        }
        //修改订单
        public void AlterOrder(Order o)
        {
            string connStr = "server=localhost;port=3306;user=root;password=jhl794613285; database=ordersystem;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                MySqlCommand cmd = new MySqlCommand("UPDATE `order` SET `客户`= '"+o.client+"', `总价`= '"+o.Totalprice+"' WHERE (`订单号`='" + o.ordernumber + "')", conn);
                MySqlDataAdapter reader = new MySqlDataAdapter(cmd);
                table.Clear();
                reader.Fill(this.table);

            }
            catch (MySqlException ex)
            {
            }
            conn.Close();
        }
        //寻找订单    
        public void FindOrderbynumeber(long num)
        {
            string connStr = "server=localhost;port=3306;user=root;password=jhl794613285; database=ordersystem;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `order` WHERE `订单号` = '"+num+"'", conn);
                MySqlDataAdapter reader = new MySqlDataAdapter(cmd);
                table.Clear();
                reader.Fill(this.table);

            }
            catch (MySqlException ex)
            {
            }
            conn.Close();
        }

        public void FindOrderbyclient(string findcode)
        {
            
            string connStr = "server=localhost;port=3306;user=root;password=jhl794613285; database=ordersystem;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `order` WHERE `客户` LIKE '%"+ findcode + "%'ORDER BY `订单号`", conn);
                MySqlDataAdapter reader = new MySqlDataAdapter(cmd);
                table.Clear();
                reader.Fill(this.table);

            }
            catch (MySqlException ex)
            {
            }

            conn.Close();
        }

        public void FindOrderbygoods(string goods)
        {
            string connStr = "server=localhost;port=3306;user=root;password=jhl794613285; database=ordersystem;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
                string str= "SELECT * FROM `order` WHERE `订单号` = (SELECT `订单号` FROM `orderitem` WHERE `商品` LIKE '%" + goods + "%')ORDER BY `订单号`";
                MySqlCommand cmd = new MySqlCommand(str, conn);
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
