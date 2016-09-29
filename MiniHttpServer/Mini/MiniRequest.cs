using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Imzjy.MiniHttpServer.Mini
{
    public class MiniRequest
    {
        public HttpListenerRequest Request = null;
        private string requestBody = null;
        public MiniRequest(HttpListenerRequest request)
        {
            Request = request;
        }

        public string GetBody()
        {
            if (requestBody == null)
            {
                using (StreamReader readStream = new StreamReader(this.Request.InputStream, Encoding.UTF8))
                {
                    requestBody = readStream.ReadToEnd();
                }
            }

            return requestBody;
        }
    }
}
