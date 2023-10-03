using ch23.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ch23Console
{
    internal static class WebServerTest
    {

        public static void Run()
        {
            var server = new WebServer(8080);
            server.RequrstReceiver += OnRequestReceived;
            server.Start();
            Console.WriteLine("server start");
            Console.ReadLine();
        }

        private static void OnRequestReceived(object? Sender, RequrstReceiverEventArgs E)
        {
            var context = E.Context;
            Console.WriteLine("Conection{0}", context.Request.UserHostAddress);

            using var writer = new StreamWriter(context.Response.OutputStream);
            writer.WriteLine("WST");
        }

    }
}
