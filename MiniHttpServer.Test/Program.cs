using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Data.SqlServerCe;

namespace Jatsz.MiniHttpServer.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MiniHttpServer server = new MiniHttpServer(8081);

            server.RegisterHandler("/index", IndexHandler);
            server.Start();

            Console.ReadKey();

            server.Stop();

            Console.ReadKey();


            server.Start();

            Console.ReadKey();
        }

        static void IndexHandler(object sender, ContextEventArgs e)
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
            //string responseString = string.Format("<HTML><BODY> Hello world! {0} <div>{1}</div><div>{2}</div></BODY></HTML>", DateTime.Now, requestStr, Thread.CurrentThread.ManagedThreadId);
            string responseString = GetRecords();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        
        }
        static string GetRecords()
        {
            string result = string.Empty;

            using (SqlCeConnection con = new SqlCeConnection(@"Data Source=C:\Users\jcu\Documents\Visual Studio 2010\Projects\ConsoleTester\ConsoleTester\bin\Debug\Logs.sdf;Max Database Size=4091"))
            {
                con.Open();
                SqlCeCommand cmd = con.CreateCommand();
                cmd.CommandText = "select top (10) a,b,c,d from log";
                SqlCeDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result += string.Format("{0},{1},{2},{3}\n",
                        reader.GetValue(0), reader.GetValue(1), reader.GetValue(2), reader.GetValue(3));
                }
            }

            return result;
        }
    }
}
