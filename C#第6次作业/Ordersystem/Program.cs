using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordersystem
{

    class Program
    {
        static void Main(string[] args)
        {
            OrderService orderservice=new OrderService();
            String xmlfile = "orders.xml";
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
                orderservice.Import(xmlfile);
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("当前的订单数："+ orderservice.orders.Count+"\n");
                foreach (Order o in orderservice.orders) o.Tostring();
                string op = Console.ReadLine();
                switch (op)
                {
                    case "a":
                        Order o = new Order(DateTime.Now);
                        orderservice.Addorder(o);
                        break;
                    case "b":
                        if (orderservice.orders == null)
                        {
                            Console.WriteLine("当前订单数为0，不支持删除操作");
                            break;
                        }
                        Console.WriteLine("请输入订单号");
                        int ornumber = int.Parse(Console.ReadLine());
                        orderservice.RemoveOrder(ornumber);
                        break;
                    case "c":
                        if (orderservice.orders == null)
                        {
                            Console.WriteLine("当前订单数为0，不支持修改操作");
                            break;
                        }
                        Console.WriteLine("请输入要修改的订单号");
                        int ornumber2 = int.Parse(Console.ReadLine());
                        Order neworder = new Order();
                        neworder = orderservice.FindOrderbynumeber(ornumber2);
                            bool endAlter = false;
                            while (!endAlter)
                            {
                                Console.WriteLine("1.增加商品\n2.删除已有商品\n3.修改已有商品的单价和数量");
                                 string op1 = Console.ReadLine();
                                 switch (op)
                                {
                                case "1": neworder.AddorderItem(); break;
                                case "2": neworder.RemoveorderItem(); break;
                                case "3": neworder.AlterorderItem(); break;
                                 }
                                 Console.WriteLine("此次修改成功，按“n”停止修改该订单");
                                 if (Console.ReadLine() == "n")
                                 goto outer;
                                
                            }
                        Console.WriteLine("您输入的订单号不存在");
                        outer:;
                        orderservice.AlterOrder(neworder);
                        break;
                    case "d":
                        orderservice.FindOrder();
                        break;
                    case "f":
                        orderservice.Reorderbyprice();
                        break;
                    case "g":
                        orderservice.Reorderbytime();
                        break;
                    case "h":
                        orderservice.Reorderbyordnum();
                        break;
                }
                Console.WriteLine("按“n”退出订单系统，或者其他键继续");
                orderservice.Export(xmlfile);
                if (Console.ReadLine() == "n")
                {
                    endApp = true;
                }
                Console.Clear();
            }
        }
    }
}
