using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;

namespace WebDynamic

{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Mymethods mymethods = new Mymethods();
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
                return;
            }
            HttpListener listener = new HttpListener();
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
            Console.CancelKeyPress += delegate {
                listener.Stop();
                listener.Close();
                Environment.Exit(0);
            };


            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }

                // get url 
                Console.WriteLine($"Received request for {request.Url}");
                string result = "";
                //Récupère le nom de la méthode à appeler.
                string methodName = request.Url.Segments[request.Url.Segments.Length - 1];
                try
                {
                    Type type = typeof(Mymethods);
                    MethodInfo method = type.GetMethod(methodName);//Récupère la méthode correpondante au nom récupéré.
                    //Appel de la méthode et on récupère ce qui est retourné.
                    result = (string)method.Invoke(mymethods,
                        new Object[] { HttpUtility.ParseQueryString(request.Url.Query).Get("param1"), 
                            HttpUtility.ParseQueryString(request.Url.Query).Get("param2") });
                }
                catch (NullReferenceException e)
                {
                    Console.Write("Method not found");
                    
                }

                Console.WriteLine(documentContents);
                //Construction d'une réponse pour la requête.
                HttpListenerResponse response = context.Response;
                //Retourne la page web correspondante.
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(result);
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
    }
}