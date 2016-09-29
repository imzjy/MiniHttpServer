using System;
using System.Collections.Generic;
using System.Text;
using Imzjy.MiniHttpServer.Mini;

namespace Imzjy.MiniHttpServer.Mini
{

    // 摘要: 
    //     表示将处理事件的方法。泛型类型参数指定事件所生成的事件数据的类型。无法继承此类。
    [Serializable]
    public delegate void MiniRequestHandler(MiniRequest req, MiniResponse resp);

}
