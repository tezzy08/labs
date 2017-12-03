using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculation
{
    public partial class Pentagon : Form
    {
        public Pentagon()
        {
            InitializeComponent();
        }

        private static string POSTGET(string Url, string Data)
        {
            System.Net.WebRequest request = System.Net.WebRequest.Create(Url);
            request.Method = "POST";
            request.Timeout = 10000;
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
            request.ContentLength = sentData.Length;
            System.IO.Stream sendStream = request.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream ReciveStream = response.GetResponseStream();
            System.IO.StreamReader str = new StreamReader(ReciveStream, Encoding.UTF8);//зависит тот кодировки сайта
            Char[] read = new Char[256];
            int count = str.Read(read,0,256);
            string Out = String.Empty;
            while (count>0)
            {
                String strin = new String(read, 0, count);
                Out += strin;
                count = str.Read(read, 0, 256);
            }
            return Out;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user="Stepul";
            string pass = "777";
            textBox1.Text = POSTGET("http://www.zapomnika.zzz.com.ua/Lab4.php","user="+user+"&pass="+pass).Remove('<');
        }
    }
}
