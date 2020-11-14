using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "请输入第一个数字";
            label3.Text = "请输入第二个数字";
            double result = 0, num1 = 0, num2 = 0;
            string str1 = textBox1.Text;
            double.TryParse(str1, out num1);
            string str2 = textBox2.Text;
            double.TryParse(str2, out num2);

            switch (comboBox1.Text)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    result = num1 / num2;
                    break;
                default:
                    break;
            }
            try
            {
                if (double.IsNaN(result))
                {
                    textBox3.Text = "请检查是否输入正确";
                }
                else if(num2==0|| comboBox1.Text=="/")
                {
                    textBox3.Text = "除数不能为0";
                }
                else textBox3.Text = result.ToString();
            }
            catch (FormatException)
            { 
            }




        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Add("+");
            comboBox1.Items.Add("-");
            comboBox1.Items.Add("*");
            comboBox1.Items.Add("/");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
