using networksharelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sendit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var broadcaster = new Broadcaster(54001);
            broadcaster.SayHello(54000);
        }
    }
}
