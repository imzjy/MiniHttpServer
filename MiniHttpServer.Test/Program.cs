using System;
using System.Collections.Generic;
using System.Text;
using MiniHttpServer;
using System.Net;
using System.IO;
using System.Threading;

namespace MiniHttpServer.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MiniHttpServer server = new MiniHttpServer(10, new string[] { "http://localhost:8081/index/" });
            server.ProcessRequest += new EventHandler<ContextEventArgs>(server_ProcessRequest);
            server.Start();
        }

        static void server_ProcessRequest(object sender, ContextEventArgs e)
        {

            string requestStr = string.Empty;

            HttpListenerRequest request = e.Context.Request;
            using (StreamReader sr = new StreamReader(request.InputStream))
            {
                requestStr = sr.ReadToEnd();
            }

            // Obtain a response object.
            HttpListenerResponse response = e.Context.Response;
            // Construct a response.
            string responseString = string.Format("<HTML><BODY> Hello world! {0} <div>{1}</div><div>{2}</div></BODY></HTML>", DateTime.Now, requestStr, Thread.CurrentThread.ManagedThreadId);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        
        }
    }
}
