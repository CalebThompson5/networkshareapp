using networksharelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace waitforit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // wait for connection
            // add connection to list
            // show list
            var broadcaster = new Broadcaster();
            broadcaster.MessageReceived += Message_Received;
            broadcaster.Listen();
            while (true)
            {
                Thread.Sleep(1000);
            }

        }

        static void Message_Received(object sender, BroadcastPayload payload)
        {
            Console.WriteLine($"{payload.Message} - {payload.Client}");
        }
    }
}
