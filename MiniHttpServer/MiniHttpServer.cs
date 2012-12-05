using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace MiniHttpServer
{
    public class MiniHttpServer
    {
        public static void SimpleListenerExample(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            Console.WriteLine("Listening...");

            while (true)
            {

                // Note: The GetContext method blocks while waiting for a request. 
                HttpListenerContext context = listener.GetContext();

                string requestStr = string.Empty;

                HttpListenerRequest request = context.Request;
                using (StreamReader sr = new StreamReader(request.InputStream))
                {
                    requestStr = sr.ReadToEnd();
                }

                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                // Construct a response.
                string responseString = string.Format("<HTML><BODY> Hello world! {0} <div>{1}</div></BODY></HTML>", DateTime.Now, requestStr);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }

            listener.Stop();
        }
    }
}
