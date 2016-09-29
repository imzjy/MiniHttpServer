using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Imzjy.MiniHttpServer.Mini
{
    class MiniRegisteredHandler
    {
        public string HttpMethod = string.Empty;
        public MiniRequestHandler RequestHandler = null;
        public Regex UrlPattern = null;

        public MiniRegisteredHandler(Regex urlPattern, string httpMethod, MiniRequestHandler handler)
        {
            HttpMethod = httpMethod;
            UrlPattern = urlPattern;
            RequestHandler = handler;
        }
    }
}
