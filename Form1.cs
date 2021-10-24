using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private decimal savedNum=0;
        private decimal lastNum = 0;
        private string sign = "";
        private bool clear = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            if (output.Text == "0"||clear)
                output.Text = "";
            output.Text += button.Text;
            clear = false;
        }

        private void button0_Click(object sender, EventArgs e)
        {
            if (output.Text == "0") return;
            if (clear) output.Text = "";
            output.Text += "0";
            clear = false;
        }

        private void buttonFloat_Click(object sender, EventArgs e)
        {
            if (!output.Text.Contains(",")) output.Text+=",";
        }

        private void buttonMS_Click(object sender, EventArgs e)
        {
            savedNum = decimal.Parse(output.Text);
            buttonMC.Enabled = true;
            buttonMR.Enabled = true;
        }

        private void buttonPlusDuff_Click(object sender,EventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            savedNum += button.Text switch
            {
                "M+" => decimal.Parse(output.Text),
                "M-" => -decimal.Parse(output.Text),
                _ => 0
            };
            buttonMC.Enabled = true;
            buttonMR.Enabled = true;
        }

        private void buttonMC_Click(object sender, EventArgs e)
        {
            savedNum = 0;
            buttonMC.Enabled = false;
            buttonMR.Enabled = false;
        }

        private void buttonPlusDiff_Click(object sender, EventArgs e)
        {
            if (output.Text.Contains('-'))
                output.Text = output.Text.Remove(0, 1);
            else
                output.Text = "-" + output.Text;
            clear = false;
        }

        private void buttonMath_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            if(sign==button.Text&&!clear)
            {
                buttonTotal_Click(null, null);
            }
            sign = button.Text;
            lastNum = decimal.Parse(output.Text);
            clear = true;
        }

        private void buttonTotal_Click(object sender, EventArgs e)
        {
            decimal temp = decimal.Parse(output.Text);
            if(temp==0&&sign== "÷")
            {
                output.Text = "Нельзя делить на 0";
                clear = true;
                return;
            }
            decimal result = sign switch
            {
                "+" => lastNum +temp,
                "-" when !clear=> lastNum -temp,
                "-" => temp - lastNum,
                "X" => lastNum *temp,
                "÷" when !clear => lastNum /temp,
                "÷" => temp / lastNum,
                _ => temp
            };
            if(!clear)lastNum = temp;
            output.Text = result.ToString();
            clear = true;
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            output.Text = "0";
            lastNum = 0;
            clear = true;
            sign = "";
        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            output.Text = "0";
        }

        private void buttonMR_Click(object sender, EventArgs e)
        {
            output.Text = savedNum.ToString();
        }

        private void unarMath_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            decimal temp = decimal.Parse(output.Text);
            decimal res = button.Text switch
            {
                "%" => lastNum * (temp / 100),
                "1/x" => 1 / temp,
                "x^2" => temp * temp,
                "√x" => (decimal)Math.Sqrt((double)temp),
                _ => temp
            };
            output.Text = res.ToString();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if(output.Text.Length==1)
            {
                output.Text = "0";
                return;
            }
            output.Text = output.Text.Substring(0, output.Text.Length - 1);
        }
    }
}
