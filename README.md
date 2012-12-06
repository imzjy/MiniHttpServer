MiniHttpServer
==============

## Introduction
Mini HTTP Server which can be embed in EXE

## Usage
```c
MiniHttpServer server = new MiniHttpServer(8081); //start http server on port of 8081

//register the handler and start the server
server.RegisterHandler("/index", IndexHandler);
server.Start();
```

