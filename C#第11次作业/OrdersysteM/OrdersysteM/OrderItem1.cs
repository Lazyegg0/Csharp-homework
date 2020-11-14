using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordersystem
{
    public class OrderItem
    {
        public string goodsname { get; set; }//商品名

        public double unitprice { get; set; }//单价

        public int num { get; set; }//商品数量

        public double totalprice { get; set; }//该商品总价
        public OrderItem() { }
        //构造商品明细
        public OrderItem(string a, double b, int c)
        {
            this.goodsname = a;
            this.unitprice = b;
            this.num = c;
            this.totalprice = b * c;
        }
        public OrderItem transorderitem(DataTable dataTable)
        {
            OrderItem ordeRItem = new OrderItem();
            ordeRItem.goodsname = dataTable.Rows[0][0].ToString();
            ordeRItem.unitprice = double.Parse(dataTable.Rows[0][1].ToString());
            ordeRItem.num = Convert.ToInt32(dataTable.Rows[0][2].ToString());
            ordeRItem.totalprice = ordeRItem.unitprice * ordeRItem.num;
            return ordeRItem;
        }
        //输出该商品明细
        public void Tostring()
        {
            Console.Write(goodsname );
            Console.SetCursorPosition(8, Console.CursorTop);
            Console.Write(unitprice );
            Console.SetCursorPosition(14, Console.CursorTop);
            Console.Write(num);
            Console.SetCursorPosition(20, Console.CursorTop);
            Console.WriteLine(totalprice);
        }
        //判断两个商品明细是否相同1(用于合并同样商品的不同次输入)
        public bool IsEquals1(OrderItem a)
        {
            if (goodsname == a.goodsname && unitprice == a.unitprice) return true;
            else return false;
        }
        //判断两个商品明细是否相同2(用于判断总订单是否相同)
        public bool IsEquals2(OrderItem a)
        {
            if (goodsname == a.goodsname && unitprice == a.unitprice && num == a.num) return true;
            else return false;
        }
    }
}
