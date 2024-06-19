using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using winformstut.Model;

namespace winformstut
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(209, 15);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 22);
            this.button2.TabIndex = 1;
            this.button2.Text = "Emit";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(103, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(12, 41);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(776, 397);
            this.webBrowser1.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(713, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Monitor";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.openMonitor);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(330, 11);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "GC Collect";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        // this.Load += FormLoad;
        private void FormLoad(object sender, EventArgs e)
        {
            db = new mydbEntities();
            employees = new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "qwerty",
                    Address = "asdfg"
                },
                new Employee
                {
                    Id = 1,
                    Name = "qwerty",
                    Address = "asdfg"
                },
                new Employee
                {
                    Id = 2,
                    Name = "qwerty",
                    Address = "asdfg"
                },
                new Employee
                {
                    Id = 3,
                    Name = "qwerty",
                    Address = "asdfg"
                }
            };
        }
        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (sender is WebBrowser webBrowser)
            {
                webBrowser.Document.InvokeScript("eval", new object[] {
                    @"window.onerror = function (message, source, lineno, colno, error) {
                        window.external.LogJavaScriptError(message, source, lineno, colno, error);
                        return true; // Prevent the default handler
                    };"});
            }
        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            int currentprocessid = Process.GetCurrentProcess().Id;
            List<Process> processes = new List<Process>();
            var query = new ManagementObjectSearcher("SELECT * FROM Win32_Process");
            var a = new ManagementObjectSearcher("SELECT * FROM Win32_Process where Name like '%msedgewebview2%'")
            .Get()
            .OfType<ManagementObject>()
            .Select(p => new { Name = (string)p["Name"], ProcessId = (int)(UInt32)p["ProcessId"], ParentId = (int)(UInt32)p["ParentProcessId"] }).ToList();

            var webviews = a.Where(p => p.ParentId == currentprocessid).ToList();

            while(webviews.Count() > 0)
            {
                webviews.ForEach(x => processes.Add(Process.GetProcessById(x.ProcessId)));
                webviews = a.Where(p => webviews.Select(x => x.ProcessId).Contains(p.ParentId)).ToList();
            }
            processes.Reverse();
            foreach (Process p in processes)
            {
                if (!p.HasExited)
                {
                    p.Kill();
                }
            }
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            //Form2 webview = new Form2("C:\\Users\\USER\\Downloads\\Telegram Desktop\\Santhosh.pdf");
            //webview.ShowDialog();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private List<Employee> employees;
        private mydbEntities db;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private Button button3;
        private Button button4;
    }
    [ComVisible(true)]
    public class ScriptManager
    {
        public void LogJavaScriptError(string message, string source, int lineno, int colno, object error)
        {
            // Handle the JavaScript error here
            MessageBox.Show($"JavaScript Error: {message}\nSource: {source}\nLine: {lineno}\nColumn: {colno}\nError: {error}");
            // Log the error to a file or database
        }
    }
}

