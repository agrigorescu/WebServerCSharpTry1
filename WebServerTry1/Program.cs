using System.Net;
using System.Threading;
using System;
using System.Text;

namespace WebServerTry1
{
    class Program
    {
        //adding a port listener??
        static HttpListener _httpListener = new HttpListener();

        /*
         * the main method
         * starting the local server and adding the localhost prefix to the server.
         * basically where the port is and listening for a response thread.
         */
        static void Main(string[] args)
        {
            Console.WriteLine("Starting server...");
            _httpListener.Prefixes.Add("http://localhost:5000/"); // add prefix "http://localhost:5000/"
            _httpListener.Start(); // start server (Run application as Administrator!)
            Console.WriteLine("Server started.");
            Thread _responseThread = new Thread(ResponseThread);
            _responseThread.Start(); // start the response thread
        }

        //the response thread??? no idea what this is. needs more investigating.
        static void ResponseThread()
        {
            while (true)
            {
                HttpListenerContext context = _httpListener.GetContext(); // get a context
                // Now, you'll find the request URL in context.Request.Url
                byte[] _responseArray = Encoding.UTF8.GetBytes("<html><head><title>Localhost server -- port 5000</title></head>" +
                "<body>Welcome to the <strong>Localhost server</strong> -- <em>port 5000!</em></body></html>"); // get the bytes to response
                context.Response.OutputStream.Write(_responseArray, 0, _responseArray.Length); // write bytes to the output stream
                context.Response.KeepAlive = false; // set the KeepAlive bool to false
                context.Response.Close(); // close the connection
                Console.WriteLine("Respone given to a request.");
            }
        }
    }
}
