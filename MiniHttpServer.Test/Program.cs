using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Data.SqlServerCe;
using Imzjy.MiniHttpServer.Mini;

namespace Imzjy.MiniHttpServer.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MiniHttpServer server = new MiniHttpServer(8383);

            server.Route("^/index$", "GET", IndexHandler);
            server.Route("^/index$", "POST", IndexHandler);
            server.Start();

            Console.ReadKey();

            server.Stop();

            Console.ReadKey();


            server.Start();

            Console.ReadKey();
        }

        static void IndexHandler(MiniRequest req, MiniResponse resp)
        {
            string requestBody = req.GetBody();
            string requestStr2 = req.GetBody();

            resp.SetBody(requestStr2);
        }
    }
}
