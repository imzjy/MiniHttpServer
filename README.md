MiniHttpServer
==============

## Introduction
Mini HTTP Server which can be embed in EXE.

Writen in C#(.net framework 2.0).

### What we provide

You can register the handlers with regular expression.
Url match will exist when matched, so that register the more specific in first.
For example:
```c
server.RegisterHandler("^/index/1$", FirstIndexHandler);   
server.RegisterHandler("^/index$", GeneralIndexHandler);
```
When url request be handled, stop propagation.
That's when we requst the "/index/1", and it's handle already. GeneralIndexHandler will NOT execute.

### What we NOT provide

We do not provide the template engine to keep http server simple.
Please apply your own template engine, it depends on you.

## Usage
```csharp
using Imzjy.MiniHttpServer;

//initialize server with port of 8081
MiniHttpServer server = new MiniHttpServer(8081); 

//register the handler(s) and start the server
server.Route("^/index$", "GET", IndexHandler);
server.Start();

static void IndexHandler(MiniRequest req, MiniResponse resp)
{
    string requestBody = req.GetBody();
    resp.SetBody(requestBody);
}
```


