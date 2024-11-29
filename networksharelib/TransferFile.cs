using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace networksharelib
{
    public class TransferFile
    {
        private TcpClient _client;
        private readonly string _hostname;
        private readonly string _filename;
        private readonly int _port;

        public EventHandler TransferComplete;

        public TransferFile(string hostname, string filename, int port = 54000)
        {
            _hostname = hostname;
            _filename = filename;
            _port = port;
        }

        public void Start()
        {
            _client = new TcpClient(_hostname, _port);
            _client.Connect(_hostname, _port);

            var buffer = CreateBuffer();

            _client.GetStream().BeginWrite(buffer, 0, buffer.Length, Write_Result, _client);
        }

        private void Write_Result(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                var client = result.AsyncState as TcpClient;
                client.GetStream().EndWrite(result);
                client.Close();
                TransferComplete?.Invoke(this, EventArgs.Empty);
            }
        }

        private byte[] CreateBuffer()
        {
            byte[] buffer = null;

            using (MemoryStream ms = new MemoryStream())
            {

                FileInfo fi = new FileInfo(_filename);

                WriteString(ms, Path.GetFileName(_filename) + "\r\n");
                WriteString(ms, fi.Length.ToString() + "\r\n\r\n");

                var fileContents = File.ReadAllBytes(_filename);
                ms.Write(fileContents, 0, fileContents.Length);
                buffer = ms.ToArray();
            }

            return buffer;
        }

        private void WriteString(MemoryStream ms, string s)
        {
            var bytes = Encoding.ASCII.GetBytes(s);
            ms.Write(bytes, 0, bytes.Length);
        }
    }
}
