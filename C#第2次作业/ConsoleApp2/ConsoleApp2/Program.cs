using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string numInput1 = "";
            Console.Write("请输入一个数字：");
            numInput1 = Console.ReadLine();

            int cleanNum1 = 0;
            while (!int.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("输入数字错误，请重新输入：");
                numInput1 = Console.ReadLine();
            }
            Console.WriteLine("该数的素数因子是：");
            if (cleanNum1 == 0)
            {
                Console.Write("0没有素数因子");
            }
            else if(cleanNum1 == 1)
            {
                Console.Write("1的素数因子是1");
            }
            else for (int i = 2; i <= cleanNum1;)
            {
                if (cleanNum1 % i == 0)
                {
                    cleanNum1 = cleanNum1 / i;
                    Console.WriteLine(i);
                }
                else i++;

            }

        }
    }
}
