using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ordersystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;




namespace Ordersystem.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {

        OrderService service = new OrderService();
        OrderItem apple = new OrderItem("apple", 10, 2);
        OrderItem egg = new OrderItem("egg", 1.2, 2);
        OrderItem milk = new OrderItem("milk", 4, 3);


        [TestInitialize()]
        public void init()
        {
            Order order1 = new Order();
            Order order2 = new Order();
            Order order3 = new Order();
            order1.orderItems.Add(apple);
            order1.ordernumber = 1;
            order2.ordernumber = 2;
            order3.ordernumber = 3;
            order2.orderItems.Add(egg);
            order3.orderItems.Add(milk);
            service = new OrderService();
            service.orders.Add(order1);
            service.orders.Add(order2);
            service.orders.Add(order3);
        }


        [TestMethod()]
        public void AddOrderTest()
        {
            Order order4 = new Order();
            order4.orderItems.Add(apple);
            order4.orderItems.Add(egg);
            order4.ordernumber = 4;
            order4.time = DateTime.Now;
            service.Addorder(order4);
            Assert.AreEqual(4, service.orders.Count);
            CollectionAssert.Contains(service.orders, order4);
        }


        [TestMethod()]
        public void RemoveOrderTest()
        {
            service.RemoveOrder(3);
            Assert.AreEqual(2, service.orders.Count);
            service.RemoveOrder(100);
            Assert.AreEqual(2, service.orders.Count);
        }

        [TestMethod()]
        public void AlterOrderTest()
        {
            Order order4 = new Order();
            order4.orderItems.Add(apple);
            order4.ordernumber = 3;
            service.AlterOrder(order4);
            Assert.AreEqual(3, service.orders.Count);
            CollectionAssert.Contains(service.orders, order4);
        }
        [TestMethod()]
        public void FindOrderTest()

        {
            Order order4 = new Order();
            order4 = service.FindOrderbynumeber(2);
            Assert.AreEqual(2, order4.ordernumber);
        }
        [TestMethod()]
        public void ReorderbyordnumTest()
        {

            service.Reorderbyordnum();
            Order order3 = new Order();
            order3.ordernumber = 3;
            order3.orderItems.Add(milk);
            for (int i = service.orders.Count - 1; i >= 0; i--)
                if (service.orders[i].IsEquals(order3))
                    Assert.AreEqual(2, i);
        }
        [TestMethod()]
        public void ExportTest()
        {
            String file = "temp.xml";
            service.Export(file);
            Assert.IsTrue(File.Exists(file));
            List<String> expectLines = File.ReadLines("temp.xml").ToList();
            List<String> outputLines = File.ReadLines(file).ToList();
            Assert.AreEqual(expectLines.Count, outputLines.Count);
            for (int i = 0; i < expectLines.Count; i++)
            {
                Assert.AreEqual(expectLines[i].Trim(), outputLines[i].Trim());
            }

        }
        [TestMethod()]
        public void ImportTest1()
        {
            String file = "temp.xml";
            service.Export(file);
            OrderService os = new OrderService();
            os.Import(file);
            Assert.AreEqual(3, os.orders.Count);
        }
    }
}
