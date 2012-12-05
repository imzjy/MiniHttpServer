using System;
using System.Collections.Generic;
using System.Text;
using MiniHttpServer;

namespace MiniHttpServer.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MiniHttpServer server = new MiniHttpServer(10, new string[] { "http://localhost:8081/index/" });
            server.Start();
        }
    }
}
