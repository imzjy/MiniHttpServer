using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Imzjy.MiniHttpServer.Mini
{
    public class MiniResponse
    {
        public HttpListenerResponse Response = null;
        public MiniResponse(HttpListenerResponse resp)
        {
            Response = resp;
        }

        public void SetBody(string body)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(body);
            this.Response.ContentLength64 = buffer.Length;

            Stream output = this.Response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
        }

        // Must close the output stream.
        internal void Finish()
        {
            this.Response.OutputStream.Close();
        }
    }
}
