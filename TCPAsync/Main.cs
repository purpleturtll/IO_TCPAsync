using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace Server
{
    class TCPServer
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Start();
        }
    }
}
