using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordersystem
{
    public class Order
    {
        public long  ordernumber { get; set; }//订单号

        public string client { get; set; }//客户名

        public double Totalprice { get; set; }//总价格

        public DateTime time { get; set; }//时间

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
                orderItems.Add(o);
                Totalprice += o.totalprice;
        }
        //删除某个商品
        public void RemoveorderItem(OrderItem o)
        {
                orderItems.Remove(o);
                Totalprice -= o.totalprice;
        }
        //修改某个商品的明细
        public void AlterorderItem(OrderItem o)
        {
            OrderItem oa = new OrderItem();
            var m = from n in orderItems where n.goodsname == o.goodsname select n;
            oa = m.FirstOrDefault();
            orderItems.Remove(oa);
            orderItems.Add(o);
        }

        //判断商品在订单中是否存在
        public bool IsExist(string a)
        {
            foreach (OrderItem o in orderItems)
            {
                if (o.goodsname == a) return true;
            }
            return false;
        }
        public OrderItem finditem(string name)
        {
            var m = from n in orderItems where n.goodsname==name select n;
            if (m.FirstOrDefault() == null) Console.WriteLine("订单不存在");
            return m.FirstOrDefault();
        }
        //输出订单
        public void Tostring()
        {
            Console.WriteLine("订单号：" + ordernumber);
            Console.Write("客户:" + client);
            Console.SetCursorPosition(18, Console.CursorTop);
            Console.Write("总金额:" + Totalprice);
            Console.SetCursorPosition(34, Console.CursorTop);
            Console.WriteLine(time.ToString());
            Console.Write("商品");
            Console.SetCursorPosition(8, Console.CursorTop);
            Console.Write("单价");
            Console.SetCursorPosition(14, Console.CursorTop);
            Console.Write("数量");
            Console.SetCursorPosition(20, Console.CursorTop);
            Console.WriteLine("总价");
            foreach (OrderItem o in orderItems)
                o.Tostring();
            Console.WriteLine();
        }
        //判断两个订单是否相等
        public bool IsEquals(Order a)
        {
            if (client == a.client && Totalprice == a.Totalprice && orderItems.Count == a.orderItems.Count)
            {
                for (int i = orderItems.Count - 1; i >= 0; i--)
                {
                    for (int j = a.orderItems.Count - 1; j >= 0; j--)
                    {
                        if (orderItems[i].IsEquals2(a.orderItems[j])) { goto outer; }
                    }
                    return false;
                outer:;
                }
                return true;
            }
            return false;
        }
    }
}
