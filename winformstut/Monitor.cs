using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using winformstut.utils;

namespace winformstut
{
    public partial class Monitor : Form
    {
        public Monitor()
        {
            InitializeComponent();
        }

        private async void RefreshList(object sender, EventArgs e)
        {
            if(CacheManager.CBack != null)
            {
                this.listView1.Items.Clear();
                Delegate[] dels = CacheManager.CBack.GetInvocationList();
                foreach(Delegate de in dels)
                {
                    ListViewItem item = new ListViewItem(de.GetMethodInfo().Name);
                    item.SubItems.Add(de.Target.ToString());
                    item.SubItems.Add(((Form)de.Target).IsDisposed ? "Dead" : "Alive");
                    if (((Form)de.Target).IsDisposed)
                    {
                        item.ForeColor = Color.Red;
                    }
                    else
                    {
                        item.ForeColor = Color.Green;
                    }

                    this.listView1.Items.Add(item);
                }
            }
        }
    }
}
