﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp8
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Process pr = RI();
            if (pr != null)
                MessageBox.Show("Приложение уже запущено!");
            else
                Application.Run(new Form1());
        }


        public static Process RI()
        {
            Process current = Process.GetCurrentProcess();
            Process[] pr = Process.GetProcessesByName(current.ProcessName);
            foreach (Process i in pr)
            {
                if (i.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return i;
                    }
                }
            }
            return null;
        }
    }
}
