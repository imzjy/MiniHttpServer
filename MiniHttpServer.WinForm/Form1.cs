using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Jatsz.MiniHttpServer;
using Jatsz.MiniHttpServer.Utils;
using System.IO;
using System.Collections.Specialized;

namespace Jatsz.MiniHttpServer.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MiniHttpServer server = new MiniHttpServer(5,8080);

            server.RegisterHandler("/log", LogHandler);
            server.Start();

            btnStart.Enabled = false;
        }
        void LogHandler(object sender, ContextEventArgs e)
        {
            HttpListenerRequest request = e.Context.Request;
            if (request.HttpMethod.ToUpper() == "GET")
            {
                LogGetHandler(e);
            }
            else if (request.HttpMethod.ToUpper() == "POST")
            {
                LogPostHandler(e);
            }
            e.Context.Response.OutputStream.Close();
        }

        private static void LogPostHandler(ContextEventArgs e)
        {
            NameValueCollection postValues = MiniUtility.GetFormValues(e.Context.Request);
            string model = postValues["model"] == null ? postValues["model"] : string.Empty;
            string dateStart = postValues["dateStart"] == null ? postValues["dateStart"] : string.Empty;
            string dateEnd = postValues["dateEnd"] == null ? postValues["dateEnd"] : string.Empty;
        }

        private static void LogGetHandler(ContextEventArgs e)
        {
            string html = string.Empty;
            using (FileStream fs = new FileStream("Views/Log.html", FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    html = sr.ReadToEnd();
                }
            }
            using (StreamWriter sw = new StreamWriter(e.Context.Response.OutputStream))
            {
                sw.Write(html);
            }
        }
    }
}
