//四则运算

using System;

namespace Calculator
{
    class Calculator
    {
        public static double DoOperation(double num1, double num2, string op)
        {
            double result = 0;
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    break;
                case "s":
                    result = num1 - num2;
                    break;
                case "m":
                    result = num1 * num2;
                    break;
                case "d":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    break;
                default:
                    break;
            }
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            while (!endApp)
            {
                string numInput1 = "";
                string numInput2 = "";
                double result = 0;

                Console.Write("请输入第一个数字：");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("输入数字错误，请重新输入：");
                    numInput1 = Console.ReadLine();
                }

                Console.Write("请输入另外一个数字：");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("输入数字错误，请重新输入：");
                    numInput2 = Console.ReadLine();
                }
                Console.WriteLine("请选择运算方式:");
                Console.WriteLine("\ta - 加");
                Console.WriteLine("\ts - 减");
                Console.WriteLine("\tm - 乘");
                Console.WriteLine("\td - 除");

                string op = Console.ReadLine();
                result = Calculator.DoOperation(cleanNum1, cleanNum2, op);


                Console.WriteLine("你的结果是: {0:0.##}\n", result);

                Console.WriteLine("------------------------\n");


                Console.Write("按“n”结束程序，或者其他键继续计算");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); 
            }
            return;
        }
    }
}
