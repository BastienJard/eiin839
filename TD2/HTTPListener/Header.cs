using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BasicServerHTTPlistener
{
    public class Header
    {

        public Header(HttpListenerRequest request)
        {
            string[] headers = {HttpRequestHeader.Accept.ToString(),
                HttpRequestHeader.AcceptCharset.ToString(),
                HttpRequestHeader.AcceptEncoding.ToString(),
                HttpRequestHeader.AcceptLanguage.ToString(),
                HttpRequestHeader.Allow.ToString(),
                HttpRequestHeader.Authorization.ToString(),
                HttpRequestHeader.Cookie.ToString(),
                HttpRequestHeader.From.ToString(),
                HttpRequestHeader.UserAgent.ToString() };
            foreach(string s in headers)
            {
                Console.WriteLine(s +" : " + request.Headers[s]);
            }
            
        }
    }
}
