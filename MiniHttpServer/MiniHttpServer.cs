using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

namespace MiniHttpServer
{
    public class MiniHttpServer
    {
        private int _numOfProcessThread = 1;
        private HttpListener listener = new HttpListener();

        public MiniHttpServer(int numOfProcessThread, string[] prefixes)
        {
            _numOfProcessThread = numOfProcessThread;

            // URI prefixes are required, for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
        }

        public void Start()
        {
            if (!HttpListener.IsSupported)
                throw new NotSupportedException("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
            

            listener.Start();
            Console.WriteLine("Listening...");

            for (int i = 0; i < _numOfProcessThread; i++)
            {
                ThreadPool.QueueUserWorkItem(WorkerThread, listener);
            }

            while (listener.IsListening)
            { }
        }

        public void Stop()
        {
            listener.Stop();
        }

        private void WorkerThread(object objListener)
        {
            HttpListener listener = objListener as HttpListener;

            while (listener.IsListening)
            {
                HttpListenerContext context = listener.GetContext();
                ProcessRequest(context);
            }
        }

        private static void ProcessRequest(HttpListenerContext context)
        {
            string requestStr = string.Empty;

            HttpListenerRequest request = context.Request;
            using (StreamReader sr = new StreamReader(request.InputStream))
            {
                requestStr = sr.ReadToEnd();
            }

            // Obtain a response object.
            HttpListenerResponse response = context.Response;
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
