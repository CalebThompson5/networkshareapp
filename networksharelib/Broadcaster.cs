﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace networksharelib
{
    public class Broadcaster
    {
        private readonly UdpClient _client;
        private readonly int _port;

        // message types
        //      HEL - new to network
        //      CON - confirm still connected
        //      ACK - acknowledge everything is okay

        public const string HEL = nameof(HEL);
        public const string CON = nameof(CON);
        public const string ACK = nameof(ACK);

        public EventHandler<BroadcastPayload> MessageReceived;

        public Broadcaster(int port = 54000)
        {
            _port = port;
            _client = new UdpClient(_port);
        }

        public void SayHello(int port)
        {
            var helloString = Encoding.ASCII.GetBytes(HEL);
            _client.Send(helloString,
                helloString.Length,
                new IPEndPoint(IPAddress.Broadcast, port));
        }

        public void Listen()
        {
            _client.BeginReceive(Client_MessageReceived, _client);
        }

        private void Client_MessageReceived(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                var sender = new IPEndPoint(IPAddress.Any, 0);
                var client = result.AsyncState as UdpClient;
                var received = client.EndReceive(result, ref sender);

                if (received.Length > 0)
                {
                    var msg = Encoding.ASCII.GetString(received);
                    switch (msg)
                    {
                        case CON:
                            OnMessageReceived(BroadcastMessage.Confirm, sender);
                            break;
                        case ACK:
                            OnMessageReceived(BroadcastMessage.Acknowledge, sender);
                            break;
                        default:
                            OnMessageReceived(BroadcastMessage.Hello, sender);
                            break;
                    }
                }
            }
        }

        private void OnMessageReceived(BroadcastMessage message, IPEndPoint client)
        {
            MessageReceived?.Invoke(this, 
                                    new BroadcastPayload(message, client));   
        }
    }
}
