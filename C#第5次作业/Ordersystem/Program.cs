using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orderserver;
using Class1;

namespace Ordersystem
{

    class Program
    {
        static void Main(string[] args)
        {
            OrderService orderservice=new OrderService();
            bool endApp = false;
            while (!endApp)
            {
                //打印界面
                Console.WriteLine("欢迎进入订单系统:");
                Console.SetCursorPosition(55, 2);
                Console.Write("a.添加订单");
                Console.SetCursorPosition(55, 3);
                Console.Write("b.删除订单");
                Console.SetCursorPosition(55, 4);
                Console.Write("c.修改订单");
                Console.SetCursorPosition(55, 5);
                Console.Write("d.查找订单");
                Console.SetCursorPosition(55, 6);
                Console.Write("f.订单按金额大小排序");
                Console.SetCursorPosition(55, 7);
                Console.Write("g.订单按时间先后排序");
                Console.SetCursorPosition(55, 8);
                Console.Write("h.恢复默认排序方式");
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("当前的订单数："+ orderservice.orders.Count+"\n");
                foreach (Order o in orderservice.orders) o.Tostring();
                string op = Console.ReadLine();
                switch (op)
                {
                    case "a":
                        orderservice.Addorder();
                        break;
                    case "b":
                        orderservice.RemoveOrder();
                        break;
                    case "c":
                        orderservice.AlterOrder();
                        break;
                    case "d":
                        orderservice.FindOrder();
                        break;
                    case "f":
                        orderservice.Reorder("f");
                        break;
                    case "g":
                        orderservice.Reorder("g");
                        break;
                    case "h":
                        orderservice.Reorder("h");
                        break;
                }
                Console.WriteLine("按“n”退出订单系统，或者其他键继续");
                if (Console.ReadLine() == "n") endApp = true;
                Console.Clear();
            }
        }
    }
}
