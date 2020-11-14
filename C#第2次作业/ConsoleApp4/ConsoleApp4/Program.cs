using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("2-- 100之间所有的素数有: ");
            bool[] array = new bool[101];
            for (int i = 2; i <= 100; i++)
                array[i] = true;
            for (int i = 2; i <= 100; i++)
            {
                int a = 2;
                while (i * a <= 100)
                {
                    array[i * a] = false;
                    a++;
                }
            }
            for (int i = 2; i <= 100; i++)
                if (array[i] == true) Console.Write(i+"\t");
            Console. WriteLine();

        }
    }
}
