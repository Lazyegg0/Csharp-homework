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
        public OrderService() { }
        public List<Order> orders = new List<Order>();
        //增加订单
        public void Addorder(Order o)
        {
            for (int i = orders.Count - 1; i >= 0; i--)
                if (orders[i].IsEquals(o))
                {
                    Console.WriteLine("您于"+ orders[i].time+"添加过一笔同样的订单，输入y确认添加，其他键取消");
                    if (Console.ReadLine() == "y") orders.Add(o);
                    else Order.orderNumber -= 1;
                    goto outer;
                }
            orders.Add(o);
        outer:;
        }
        //删除订单
        public void RemoveOrder(int o)
        {
            
            for (int i = orders.Count - 1; i >= 0; i--)
            {
                if (orders[i].ordernumber == o)
                {
                    orders.Remove(orders[i]);
                    goto outer;
                }
                    
            }
            Console.WriteLine("您输入的订单号不存在");
            outer:;
        }
        //修改订单
        public void AlterOrder(Order o)
        {
            Order oa = new Order();
            var m = from n in orders where n.ordernumber == o.ordernumber select n;
            if (m.FirstOrDefault() == null) Console.WriteLine("订单不存在");
            oa = m.FirstOrDefault();
            orders.Remove(oa);
            orders.Add(o);
        }
        //寻找订单
        public void FindOrder()
        {

            Console.WriteLine("a.订单号查询\nb.客户查询\nc.商品查询");
            string op = Console.ReadLine();
            switch (op)
            {
                case "a":
                    {
                        int num = int.Parse(Console.ReadLine());
                        Order target=new Order();
                        target=FindOrderbynumeber(num);
                        target.Tostring();
                        break;
                    }
                case "b":
                    {
                        string findcode = Console.ReadLine();
                        var m = from n in orders where n.client == findcode orderby n.ordernumber descending select n;
                        if (m.FirstOrDefault() == null) Console.WriteLine("订单不存在");
                        foreach (Order o in m) o.Tostring();
                        break;
                    }
                case "c":
                    {
                        string findcode = Console.ReadLine();
                        var m = from n in orders where n.IsExist(findcode) orderby n.ordernumber descending select n;
                        if (m.FirstOrDefault() == null) Console.WriteLine("订单不存在");
                        foreach (Order o in m) o.Tostring();
                        break;
                    }
            }
        }
        
        public Order FindOrderbynumeber(int num)
        {
            var m = from n in orders where n.ordernumber == num select n;
            if (m.FirstOrDefault() == null) Console.WriteLine("订单不存在");
            return m.FirstOrDefault();
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
                xmlser.Serialize(fs, this.orders);
            }
        }

        public void Import(string filename)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(List<Order>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                this.orders=(List<Order>)xmlser.Deserialize(fs);
            }
        }
    }
}
