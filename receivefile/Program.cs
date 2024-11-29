using networksharelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace receivefile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var receiveFile = new ReceiveFile(54000);
            receiveFile.Listen();
        }
    }
}
