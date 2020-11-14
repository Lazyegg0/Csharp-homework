using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Graphical
    {
        public double area;
        public double perimeter;
        public virtual double AreaGraphical()
        {
            return area;
        }
        public virtual double PerimeterGraphical()
        {
            return perimeter;
        }
        public virtual bool IsLegal()
        {
            return true;
        }
    }
    class Circle : Graphical
    {
        public double radius;
        public Circle()
        {
            Console.Write("请输入半径：");
            string numInput = Console.ReadLine();
            double cleanNum = 0;
            while (!double.TryParse(numInput, out cleanNum))
            {
                Console.Write("输入半径错误，请重新输入：");
                numInput = Console.ReadLine();
            }
            this.radius = cleanNum;
        }
        public override double AreaGraphical()
        {
            area = 3.1415926 * radius * radius;
            return area;
        }
        public override double PerimeterGraphical()
        {
            perimeter = 3.1415926 * radius * 2;
            return perimeter;
        }
        public override bool IsLegal()
        {
            return radius > 0;
        }
    }
    class Rectangle : Graphical
    {
        public double width;
        public double length;
        public Rectangle()
        {
            Console.Write("请输入长的长度：");
            string numInput1 = Console.ReadLine();
            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("输入长度错误，请重新输入：");
                numInput1 = Console.ReadLine();
            }
            this.length = cleanNum1;
            Console.Write("请输入宽的长度：");
            string numInput2 = Console.ReadLine();
            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("输入长度错误，请重新输入：");
                numInput2 = Console.ReadLine();
            }
            this.width = cleanNum2;
        }
        public override double AreaGraphical()
        {
        area = width * length;
        return area;
        }
        public override double PerimeterGraphical()
        {
        perimeter = (width + length) * 2;
        return perimeter;
        }
        public override bool IsLegal()
        {
            return width > 0 && length>0;
        }
    }
    class Square : Rectangle
    {
        public Square()
        {
            
            Console.Write("请输入边长的长度：");
            string numInput2 = Console.ReadLine();
            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("输入长度错误，请重新输入：");
                numInput2 = Console.ReadLine();
            }
            this.width = cleanNum2;
        }
        public override double AreaGraphical()
        {
            area = width * width;
            return area;
        }
        public override double PerimeterGraphical()
        {
            perimeter = width * 4;
            return perimeter;
        }
        public override bool IsLegal()
        {
            return width > 0 ;
        }
    }
    class Triangle:Graphical
    {
        public double side1;
        public double side2;
        public double side3;
        public Triangle()
        {
            Console.Write("请输入边长1的长度：");
            string numInput1 = Console.ReadLine();
            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("输入长度错误，请重新输入：");
                numInput1 = Console.ReadLine();
            }
            this.side1 = cleanNum1;
            Console.Write("请输入边长2的长度：");
            string numInput2 = Console.ReadLine();
            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("输入长度错误，请重新输入：");
                numInput2 = Console.ReadLine();
            }
            this.side2 = cleanNum2;
            Console.Write("请输入边长3的长度：");
            string numInput3 = Console.ReadLine();
            double cleanNum3 = 0;
            while (!double.TryParse(numInput3, out cleanNum3))
            {
                Console.Write("输入长度错误，请重新输入：");
                numInput3 = Console.ReadLine();
            }
            this.side3 = cleanNum3;
        }
       
        public override double AreaGraphical()
        {
            double p = (side1 + side2 + side3)/2;
            double s = p * (p - side1) * (p - side2)*(p - side3);
            area = Math.Sqrt(s);
            return area;
        }
        public override double PerimeterGraphical()
        {
            perimeter = side1 + side2 + side3;
            return perimeter;
        }
        public override bool IsLegal()
        {
            return side1 > 0&&side2>0&&side3>0&&side1+side2>side3 && side1 + side3 > side2 && side3 + side2 > side1;
        }
    }

    public class GraphicalFactory
    {
        public static Graphical CreateGraphical(String shape)
        {
            Graphical open = null;
            switch (shape)
            {
                case "c":open = new Circle();
                    break;
                case "r": open = new Rectangle();
                    break;
                case "s": open = new Square();
                    break;
                case "t": open = new Triangle();
                    break;
            }
            return open;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            while (!endApp)
            {
                Console.WriteLine("请选择构造的形状:");
                Console.WriteLine("\tc - 圆");
                Console.WriteLine("\tr - 长方形");
                Console.WriteLine("\ts - 正方形");
                Console.WriteLine("\tt - 三角形");
                string op = Console.ReadLine();
                Graphical graphical = GraphicalFactory.CreateGraphical(op);
                if (graphical.IsLegal())
                {
                    graphical.area = graphical.AreaGraphical();
                    graphical.perimeter = graphical.PerimeterGraphical();
                    Console.WriteLine("该图形的面积为：" + graphical.area);
                    Console.WriteLine("该图形的周长为：" + graphical.perimeter);

                }
                else Console.WriteLine("\t输入图形不合法");
                Console.WriteLine("按“n”结束程序，或者其他键继续计算");
                if (Console.ReadLine() == "n") endApp = true;
            }
        }
    }
}
