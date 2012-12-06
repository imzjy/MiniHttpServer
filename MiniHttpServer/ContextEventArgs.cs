using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace MyMiniHttpServer
{
    public class ContextEventArgs:EventArgs
    {
        public ContextEventArgs(HttpListenerContext context)
        {
            _context = context;
        }

        private HttpListenerContext _context;
        public HttpListenerContext Context
        {
            get
            {
                return _context;
            }
        }
    }
}
