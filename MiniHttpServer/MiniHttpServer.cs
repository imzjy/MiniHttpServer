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
        public event EventHandler<ContextEventArgs> ProcessRequest;

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
                ProcessRequest(this, new ContextEventArgs(context));
            }
        }
    }
}
