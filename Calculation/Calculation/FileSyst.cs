using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculation
{
    public partial class FileSyst : Form
    {
        public FileSyst()
        {
            InitializeComponent();
        }

        private void FileSyst_Load(object sender, EventArgs e)
        {
        }

        private void buildTree(String path, StreamWriter writer, int lvl)
        {
            lvl++;
            String indentDir = "";
            String indentFile = "";
            for (int i = 0; i < lvl; i++)
            {
                indentFile += " ";
            }
            for (int i = 0; i < lvl; i++)
            {
                indentDir += "-";
            }
            String[] files = Directory.GetFiles(path);
            String[] dirs = Directory.GetDirectories(path);
            foreach (String file in files)
            {
                writer.WriteLine(indentFile + "*" + file.Substring(path.Length));
            }
            foreach (String dir in dirs)
            {
                writer.WriteLine(indentDir + "+" + dir.Substring(path.Length));
                buildTree(dir, writer, lvl);
            }
        }
        //-----------------------------------------------------
        String eventSymbol = " ";
        double FirstOperand = 0;
        double SecondOperand = 0;
        Boolean negative = false;

        private void FileStart(string Start, bool FindO_o, int lvl, StreamWriter Thread)
        {
            if (!Directory.Exists(Start))
            {
                return;
            }

            string pad = new string('-', lvl++);
            try
            {
                string[] files = Directory.GetFiles(Start);
                foreach (string file in files)
                    if (Thread == null)
                        Console.WriteLine(string.Concat(pad, " ", Path.GetFileName(file)));
                    else
                        Thread.WriteLine(string.Concat(pad, " ", Path.GetFileName(file)));
                if (FindO_o)
                {
                    foreach (string folder in Directory.GetDirectories(Start))
                    {
                        FileStart(folder, FindO_o, lvl, Thread);
                    }
                }
            }
            catch (Exception ex)
            {
                if (Thread == null)
                    Console.WriteLine(string.Concat(pad, " Exception: ", ex.Message));
                else
                    Thread.WriteLine(string.Concat(pad, " Exception: ", ex.Message));
            }
            finally
            {
                if (Thread != null)
                {
                    Thread.Flush();
                    Thread.Close();
                    Thread.Dispose();
                    Thread = null;
                }
            }
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStart("C:\\", true,0);
            if (!File.Exists(currentPath + "tree.txt"))
            {
                File.Create(currentPath + "tree.txt");
            }

            FileStream file = new FileStream(currentPath + "tree.txt", FileMode.Create);
            StreamWriter writer = new StreamWriter(file);
            buildTree(currentPath, writer, 0);
            writer.Close();
            MessageBox.Show("see the 'tree.txt' file in exe directory");
            String currentPath = Directory.GetCurrentDirectory() + "/";
        }
    }
}
