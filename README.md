MiniHttpServer
==============

## Introduction
Mini HTTP Server which can be embed in EXE.

Writen in C#(.net framework 2.0).

### What we provide

HTTP request dispatch/route, you can register the handlers for a specific url.
Url match using start with in order, so that register the more specific in first.
For example:
```c
server.RegisterHandler("/index/1", FirstIndexHandler);   
server.RegisterHandler("/index", GeneralIndexHandler);
```
When url request be handled, stop propagation.
That's when we requst the "/index/1", and it's handle already. GeneralIndexHandler will NOT execute.

### What we NOT provide

We do not provide the template engine to keep http server simple.
Please apply your own template engine, it depends on you.

## Usage
```c
MiniHttpServer server = new MiniHttpServer(8081); //start http server on port of 8081

//register the handler(s) and start the server
server.RegisterHandler("/index", IndexHandler);
server.Start();

void IndexHandler(object sender, ContextEventArgs e)
{
    string responseString = string.Format("<HTML><BODY> Hello world! {0}</BODY></HTML>", DateTime.Now);

    // Obtain a response object.
    HttpListenerResponse response = e.Context.Response;
    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
    // Get a response stream and write the response to it.
    response.ContentLength64 = buffer.Length;
    System.IO.Stream output = response.OutputStream;
    output.Write(buffer, 0, buffer.Length);
    // You must close the output stream.
    output.Close();
}
```


