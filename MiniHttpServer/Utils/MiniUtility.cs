using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace MiniHttpServer.Utils
{
    public class MiniUtility
    {
        public static NameValueCollection GetFormValues(HttpListenerRequest request)
        {
            string formdata = string.Empty;
            NameValueCollection formValues = new NameValueCollection();
            using (StreamReader sr = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                formdata = sr.ReadToEnd();
                formdata = formdata.Replace('+', ' ');  //restore the "+" to blank space http://www.w3.org/TR/html401/interact/forms.html#h-17.13.4
                string[] fileds = formdata.Split('&');
                for (int i = 0; i < fileds.Length; i++)
                {
                    string[] keyValues = fileds[i].Split('=');
                    formValues.Add(Uri.UnescapeDataString(keyValues[0]), Uri.UnescapeDataString(keyValues[1]));
                }
            }
            return formValues;
        }
    }
}
