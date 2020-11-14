using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;
using Ordersystem;

namespace Ordersystem
{
    public class OrderService
    {
        public OrderService(string filename)
        {
            Import(filename);
        }
        public List<Order> orders = new List<Order>();

        //增加订单
        public void Addorder(Order o)
        {
            orders.Add(o);
        }
        //删除订单
        public void RemoveOrder(Order o)
        {
            orders.Remove(o);
        }
        //修改订单
        public void AlterOrder(Order o)
        {
            Order oa = new Order();
            var m = from n in orders where n.ordernumber == o.ordernumber select n;
            oa = m.FirstOrDefault();
            orders.Remove(oa);
            orders.Add(o);
        }
        //寻找订单    
        public List<Order> FindOrderbynumeber(long num)
        {
            List<Order> orders1 = new List<Order>();
            var m = from n in orders where n.ordernumber == num select n;
            if (m.FirstOrDefault() == null) Console.WriteLine("订单不存在");
            foreach (Order o in m) orders1.Add(o);
            return orders1;
        }

        public List<Order> FindOrderbyclient(string findcode)
        {
            List<Order> orders1 = new List<Order>();
            var m = from n in orders where n.client == findcode orderby n.ordernumber descending select n;
            if (m.FirstOrDefault() == null) Console.WriteLine("订单不存在");
            foreach (Order o in m) orders1.Add(o);
            return orders1;
        }

        public List<Order> FindOrderbygoods(string goods)
        {
            List<Order> orders1 = new List<Order>();
            var m = from n in orders where n.IsExist(goods) orderby n.ordernumber descending select n;
            if (m.FirstOrDefault() == null) Console.WriteLine("订单不存在");
            foreach (Order o in m) orders1.Add(o);
            return orders1;
        }

        //订单排序
        public void Reorderbyprice()
        {
            orders = orders.OrderByDescending(s => s.Totalprice).ToList<Order>();         
        }

        public void Reorderbytime()
        {
            orders = orders.OrderByDescending(s => s.time).ToList<Order>();
        }

        public void Reorderbyordnum()
        {
            orders = orders.OrderBy(s => s.ordernumber).ToList<Order>();
        }

        public void Export(string filename)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(List<Order>));
            using(FileStream fs= new FileStream(filename, FileMode.Create))
            {
                xmlser.Serialize(fs, orders);
            }
        }

        public void Import(string filename)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                orders=(List<Order>)xmlser.Deserialize(fs);
            }
        }
    }
}
