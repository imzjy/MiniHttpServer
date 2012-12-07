using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

namespace Jatsz.MiniHttpServer
{
    public class MiniHttpServer
    {
        private int _numOfProcessThread = 1;
        private int _listenPort = 8080;
        private Dictionary<string, EventHandler<ContextEventArgs>> _registeredHandlers = new Dictionary<string, EventHandler<ContextEventArgs>>();
        private HttpListener listener = new HttpListener();

        public MiniHttpServer(int numOfProcessThread, int listenPort)
        {
            _numOfProcessThread = numOfProcessThread;
            _listenPort = listenPort;

            string prefix = string.Format("http://*:{0}/",_listenPort);
            listener.Prefixes.Add(prefix);
 
        }
        public MiniHttpServer(int listenPort)
            :this(1,listenPort)
        { }

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
        }

        public void Stop()
        {
            listener.Stop();
        }

        public void RegisterHandler(string prefix, EventHandler<ContextEventArgs> handler)
        {
            _registeredHandlers.Add(prefix, handler);
        }

        private void WorkerThread(object objListener)
        {
            HttpListener listener = objListener as HttpListener;

            while (listener.IsListening)
            {
                try
                {
                    HttpListenerContext context = listener.GetContext();
                    ProcessRequest(context);
                }
                catch (HttpListenerException)
                {
                    //catch exception when stop listener 
                }
            }
        }


        /// <summary>
        /// URL dispatch/router
        /// </summary>
        /// <param name="context"></param>
        private void ProcessRequest(HttpListenerContext context)
        {
            bool isHandled = false;

            HttpListenerRequest request = context.Request;
            foreach (var handler in _registeredHandlers)
            {
                if (request.RawUrl.StartsWith(handler.Key))
                {
                    handler.Value(this, new ContextEventArgs(context));
                    isHandled = true;
                    break; //it's been handled, stop propagation
                }
            }

            if (!isHandled)
            {
                DefaultHander(context);
            }
        }

        /// <summary>
        /// Handle URL which do not registered the corresponding handler
        /// Just return the server time
        /// </summary>
        /// <param name="context"></param>
        private void DefaultHander(HttpListenerContext context)
        {
            string html = string.Format("<HTML><BODY> <div>Not Handled:{0}</div> <div>{1}</div></BODY></HTML>", context.Request.RawUrl, DateTime.Now);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(html);

            HttpListenerResponse response = context.Response;
            response.ContentLength64 = buffer.Length;

            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            // You must close the output stream.
            output.Close();
        }
    }
}
