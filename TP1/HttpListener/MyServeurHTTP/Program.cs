using System;
using System.IO;
using System.Net;
using System.Text;

namespace MyServeurHTTP
{
    internal class Program
    {
        static string HTTP_ROOT = "F:/SI4/SOC/eiin839/TP1/HttpListener/MyServeurHTTP/www/pub";
        private static void Main(string[] args)
        {

            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
                return;
            }
            HttpListener listener = new HttpListener();
            Console.CancelKeyPress += delegate
            
            {
                listener.Stop();
                System.Environment.Exit(0);
            };
            if (args.Length != 0)
            {
                foreach (string s in args)
                {
                    listener.Prefixes.Add(s);
                }
            }
            else
            {
                Console.WriteLine("Syntax error: the call must contain at least one web server url as argument");
            }
            listener.Start();
            foreach (string s in args)
            {
                Console.WriteLine("Listening for connections on " + s);
            }

            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;


                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                        Console.WriteLine(documentContents);
                    }
                }
                Console.WriteLine($"Received request for {request.Url}");
                Console.WriteLine(request);
                Console.WriteLine(documentContents);
                Header header = new Header(request);
                HttpListenerResponse response = context.Response;

                string reponse = "";
                string path = HTTP_ROOT + request.RawUrl;              
                if (!File.Exists(path))
                {
                    reponse = "http / 1.0 404 Not found";
                }
                else
                {
                    reponse = "http / 1.0 200 OK \n \n";
                    using (StreamReader sr = File.OpenText(path))
                    {
                        string s;
                        while ((s = sr.ReadLine()) != null)
                        {
                            reponse = reponse + "\n" + s;
                        }
                    }
                }                    
                
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(reponse);
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
     
    }
    class Header
    { 
        public Header(HttpListenerRequest request)
        {
            foreach(String key in request.Headers.Keys)
            {
                HttpRequestHeader httpRequestHeader;
                string[] names = Enum.GetNames(typeof(HttpRequestHeader));
                foreach (var name in names)
                {
                    HttpRequestHeader httpRequest = (HttpRequestHeader)Enum.Parse(typeof(HttpRequestHeader), name);
                    Console.WriteLine(httpRequest);
                }

            }
           
        }
    }
}
