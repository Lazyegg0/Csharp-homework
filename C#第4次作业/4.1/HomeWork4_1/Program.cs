using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork4_1
{
    // 链表节点
    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Data { get; set; }

        public Node(T t)
        {
            Next = null;
            Data = t;
        }

    }

    //泛型链表类
    public class GenericList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public GenericList()
        {
            tail = head = null;
        }

        public Node<T> Head
        {
            get => head;
        }

        public void Add(T t)
        {
            Node<T> n = new Node<T>(t);
            if (tail == null)
            {
                head = tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }
        public void ForEach(Action<T> action)
        {

            Node<T> node = head;
            while (node != null)
            {
                action(node.Data);
                node = head.Next;
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            // 整型List
            GenericList<int> list = new GenericList<int>();
            for (int n = 0; n < 10; n++)
            {
                list.Add(n);
            }
            list.ForEach(n => Console.WriteLine(n));
            int min = -1;
            list.ForEach(n => { if (n < min) min = n; });
            Console.WriteLine($"最小值是:{min}");
            int max = 20;
            list.ForEach(n => { if (n > max) max = n; });
            Console.WriteLine($"最大值是：{max}");
            int sum = 0;
            list.ForEach(n => sum += n);
            Console.WriteLine($"总和为：{sum}");


        }
    }
}
