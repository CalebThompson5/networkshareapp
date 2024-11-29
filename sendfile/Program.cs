using networksharelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sendfile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var hostname = args[0];
            var file = args[1];

            var transferFile = new TransferFile(hostname, file);
            transferFile.Start();
            Console.Write("Press any key");
            Console.ReadLine();
        }
    }
}
