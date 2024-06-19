using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using winformstut.utils;

namespace winformstut
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            CacheManager.CBack += new CacheManager.Callback(RCallBack);
        }
        private void RCallBack(string message)
        {
            if (message != null)
            {
                this.textBox1.Text = message;
            }
        }

        private void unSubscribe(object sender, EventArgs e)
        {
            CacheManager.CBack -= new CacheManager.Callback(RCallBack);
        }
    }
}
