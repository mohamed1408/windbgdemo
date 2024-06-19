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
using winformstut.utils;

namespace winformstut
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button1.Click += Button1_Click1;
            this.button2.Click += Button2_Click2;
        }
        private void Button1_Click1(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
        private void Button2_Click2(object sender, EventArgs e)
        {
            if(this.textBox1.Text != null && this.textBox1.Text.Length > 0)
            {
                CacheManager.SendCallBack(this.textBox1.Text);
            }
        }

        private void openMonitor(object sender, EventArgs e)
        {
            Monitor monitor = new Monitor();
            monitor.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GC.Collect();
        }
    }
}
