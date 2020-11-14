using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class1;

namespace Orderserver
{
    public class OrderService
    {
        public List<Order> orders = new List<Order>();
        //增加订单
        public void Addorder()
        {
            Order o = new Order();
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
        public void RemoveOrder()
        {
            if (orders == null)
            {
                Console.WriteLine("当前订单数为0，不支持删除操作");
                goto outer;
            }
            Console.WriteLine("请输入订单号");
            int ornumber = int.Parse(Console.ReadLine());
            for (int i = orders.Count - 1; i >= 0; i--)
            {
                if (orders[i].ordernumber == ornumber)
                    orders.Remove(orders[i]);
            }
            Console.WriteLine("您输入的订单号不存在");
            RemoveOrder();
            outer:;
        }
        //修改订单
        public void AlterOrder()
        {
            if (orders == null)
            {
                Console.WriteLine("当前订单数为0，不支持修改操作");
                goto outer;
            }
            Console.WriteLine("请输入要修改的订单号");
            int ornumber = int.Parse(Console.ReadLine());
            for (int i = orders.Count - 1; i >= 0; i--)
            {
                if (orders[i].ordernumber == ornumber)
                {
                    bool endAlter = false;
                    while (!endAlter)
                    {
                        Console.WriteLine("1.增加商品\n2.删除已有商品\n3.修改已有商品的单价和数量");
                        string op = Console.ReadLine();
                        switch (op)
                        {
                            case "1": orders[i].AddorderItem();break;
                            case "2": orders[i].RemoveorderItem(); break;
                            case "3": orders[i].AlterorderItem(); break;
                        }
                        Console.WriteLine("此次修改成功，按“n”停止修改该订单");
                        if (Console.ReadLine() == "n")
                            goto outer;
                    }
                }
            }
            Console.WriteLine("您输入的订单号不存在");
            AlterOrder();
            outer:;
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
                        var m = from n in orders where n.ordernumber == num select n;
                        if (m.FirstOrDefault() == null) Console.WriteLine("订单不存在");
                        foreach (Order o in m) o.Tostring();
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
        //订单排序
        public void Reorder(string op)
        {
            switch (op)
            {
                case "f":
                    orders = orders.OrderByDescending(s => s.Totalprice).ToList<Order>();
                    break;
                case "g":
                    orders = orders.OrderByDescending(s => s.time).ToList<Order>();
                    break;
                case "h":
                    orders = orders.OrderBy(s => s.ordernumber).ToList<Order>();
                    break;
            }
        }
    }
}
