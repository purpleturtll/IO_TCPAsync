using System;
using System.Net;
using System.Net.Sockets;

namespace TCPServer
{
    public abstract class TCPServer
    {
        IPAddress addr;
        int port;
        int buffer_size = 1024;
        protected bool running;
        TcpListener tcpListener;
        TcpClient tcpClient;
        NetworkStream networkStream;

        protected void StartListening()
        {
            tcpListener = new TcpListener(8080);
            tcpListener.Start();
        }
        protected abstract void AcceptClient();
        private void TransmissionCallback(IAsyncResult ar);
        protected abstract void BeginDataTransmission(NetworkStream stream);
        public void Start() {
            StartListening();
            AcceptClient();
        }
    }
}
