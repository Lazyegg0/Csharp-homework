using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            double max;
            double min;
            double ave;
            double[] array = { 1, 2, 3, 4, 5, 6, 7 };
            Getmax(array, out max);
            Getmin(array, out min);
            Getave(array, out ave);
            Console.WriteLine("最大值：\t" + max);
            Console.WriteLine("最小值：\t" + min);
            Console.WriteLine("平均值：\t" + ave);
        }
        public static void Getmax(double[] array,out double max)
        {
            int maxId = 0;
            for (int i = 0; i < array.Length; i++)
                if (array[i] > array[maxId])
                    maxId = i;
            max = array[maxId];
        }
        public static void Getmin(double[] array, out double min)
        {
            int minId = 0;
            for (int i = 0; i < array.Length; i++)
                if (array[i] < array[minId])
                    minId = i;
            min = array[minId];
        }
        public static void Getave(double[] array, out double average)
        {
            double sum = 0;
            for (int i = 0; i < array.Length; i++)
                sum = sum + array[i];
            average = sum / array.Length;
        }
    }
}
