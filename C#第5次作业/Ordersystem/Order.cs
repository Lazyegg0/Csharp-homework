using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class2;

namespace Class1
{
    public class Order
    {
        public static int orderNumber;//订单号生成基数

        public int ordernumber;//订单号

        public string client;//客户名

        public double Totalprice;//总价格

        public DateTime time;//时间

        public List<OrderItem> orderItems = new List<OrderItem>();//订单明细项List
        //增加商品
        public void AddorderItem()
        {
            bool endinput = false;
            while (!endinput)
            {
                Console.WriteLine("请依次输入商品名、单价、数量");
                string gsname = Console.ReadLine();
                double uprice = double.Parse(Console.ReadLine());
                int n;
                while (true)
                {
                    try
                    {
                        n = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("您输入的不是一个整数，请重新输入");
                    }
                };
                OrderItem o = new OrderItem(gsname, uprice, n);
                for (int i = orderItems.Count - 1; i >= 0; i--)
                {
                    if (orderItems[i].IsEquals1(o))
                    {
                        Console.WriteLine("您刚刚添加过一笔相同单价的该产品，输入y确认添加，其他键取消");
                        if (Console.ReadLine() == "y")
                        {
                            orderItems[i].num += o.num;
                            orderItems[i].totalprice += o.totalprice;
                            Totalprice += o.totalprice;
                        }
                        else Order.orderNumber -= 1;
                        goto outer;
                    }
                }
                orderItems.Add(o);
                Totalprice += o.totalprice;
                outer:Console.WriteLine("此次录入成功，按“n”结束增加商品，或者其他键继续录入");
                if (Console.ReadLine() == "n")
                    endinput = true;
            }
        }
        //删除某个商品
        public void RemoveorderItem()
        {
            Console.WriteLine("请输入要删除的商品名");
            string gsname = Console.ReadLine();
            for (int i = orderItems.Count - 1; i >= 0; i--)
            {
                if (orderItems[i].goodsname == gsname)
                {
                    Totalprice -= orderItems[i].totalprice;
                    orderItems.Remove(orderItems[i]);

                }
            }
        }
        //修改某个商品的明细
        public void AlterorderItem()
        {
            Console.WriteLine("请输入要修改的商品名");
            string gsname = Console.ReadLine();
            for (int i = orderItems.Count - 1; i >= 0; i--)
            {
                if (orderItems[i].goodsname == gsname)
                {
                    Console.WriteLine("请重新输入单价、数量");
                    while (true)
                    {
                        try
                        {
                            orderItems[i].unitprice = double.Parse(Console.ReadLine());
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("您输入错误，请重新输入");
                        }
                    };
                    while (true)
                    {
                        try
                        {
                            orderItems[i].num = int.Parse(Console.ReadLine());
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("您输入的不是一个整数，请重新输入");
                        }
                    };
                    Totalprice -= orderItems[i].totalprice;
                    orderItems[i].totalprice = orderItems[i].unitprice * orderItems[i].num;
                    Totalprice += orderItems[i].totalprice;
                }
            }
            Console.WriteLine("您输入的商品名字不存在");
            AlterorderItem();
        }
        //建立订单
        public Order()
        {
            this.ordernumber = orderNumber + 1;
            orderNumber += 1;
            this.Totalprice = 0;
            Console.WriteLine("请输入客户名");
            this.client = Console.ReadLine();
            this.time = DateTime.Now;
            AddorderItem();
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
