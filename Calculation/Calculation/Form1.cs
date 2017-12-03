using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculation
{
    public partial class Form1 : Form
    {
        double a, b;
        int count;
        bool znak = true;


        public void CalculAll()
        {
            if (textBox1.Text == null)
            {
            }
            switch (count)
            {
                case '+':
                    b = a + float.Parse(textBox1.Text);
                    textBox1.Text = b.ToString();
                    break;
                case '-':
                    b = a - float.Parse(textBox1.Text);
                    textBox1.Text = b.ToString();
                    break;
                case '*':
                    b = a * float.Parse(textBox1.Text);
                    textBox1.Text = b.ToString();
                    break;
                case '/':
                    b = a / float.Parse(textBox1.Text);
                    textBox1.Text = b.ToString();
                    break;
                case '#':
                    b = Math.Round(Math.Sqrt(a));
                    textBox1.Text = b.ToString();
                    break;
            }
            //тут заношу операнды в реестр
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey CalculKey = currentUserKey.CreateSubKey("CalculKey");
            CalculKey.SetValue("first operant", a);
            CalculKey.SetValue("second operant", b);
        }

        private bool IsStartupItem(bool autorun)
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (autorun)
            {
            rkApp.SetValue("Calculation", Application.ExecutablePath.ToString());
            }
            if (!autorun)
            {
            rkApp.DeleteValue("Calculation", false);
            }
            if (rkApp.GetValue("Calculation") == null)
            return false;
            else
            return true;
        }

       
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            IsStartupItem(true);

            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey CalculKey = currentUserKey.CreateSubKey("CalculKey");
            label1.Text = CalculKey.GetValue("first operant").ToString();
            textBox1.Text = CalculKey.GetValue("second operant").ToString();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            textBox1.Text= textBox1.Text + 0;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 1;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 2;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 3;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 4;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 5;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 6;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 7;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 8;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 9;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            a = float.Parse(textBox1.Text);
            textBox1.Clear();
            count = '+';
            label1.Text = a.ToString()+'+';
            znak = true;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            a = float.Parse(textBox1.Text);
            textBox1.Clear();
            count = '-';
            label1.Text = a.ToString()+'-';
            znak = true;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            a = float.Parse(textBox1.Text);
            textBox1.Clear();
            count = '*';
            label1.Text = a.ToString()+'*';
            znak = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            a = float.Parse(textBox1.Text);
            textBox1.Clear();
            count = '/';
            label1.Text = a.ToString()+'/';
            znak = true;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            CalculAll();
            label1.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            textBox1.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            a = float.Parse(textBox1.Text);
            textBox1.Clear();
            count = '#';
            label1.Text = a.ToString() +" SQRT";
            znak = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int length = textBox1.Text.Length-1;
            String text = textBox1.Text;
            textBox1.Clear();
            for (int i = 0; i < length; i++)
            {
                textBox1.Text = textBox1.Text + text[i];
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (znak == true)
            {
                textBox1.Text = "-" + textBox1.Text;
                znak = false;
            }else if (znak == false){
                textBox1.Text = textBox1.Text.Replace("-","");
                znak = true;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) / 100);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            a = float.Parse(textBox1.Text);
            textBox1.Clear();
            b = 1 / a;
            textBox1.Text = textBox1.Text+b;
        }

        private void файловаяСистinProgressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FileSyst().Show();
            this.Close();
        }

        private void взломПентагонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Pentagon().Show();
            this.Visible=false;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + ",";
        }
    }
}
