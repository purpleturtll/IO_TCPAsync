using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Server
{
    public class Server
    {
        IPAddress ip;
        TcpListener listener;
        Baza db;
        public delegate void TransmissionDataDelegate(NetworkStream stream);

        public Server()
        {
            ip = IPAddress.Loopback;
            db = new Baza("test");
            listener = new TcpListener(ip, 8080);
        }
        private void AcceptClient()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                TransmissionDataDelegate transmissionDelegate = BeginDataTransmission;
                transmissionDelegate.BeginInvoke(stream, TransmissionCallback, client);
            }
        }
        private void TransmissionCallback(IAsyncResult ar)
        {

        }
        private void BeginDataTransmission(NetworkStream stream)
        {
            byte[] login = new byte[1024];
            byte[] password = new byte[1024];
            byte[] command = new byte[1024];
            Regex alpha = new Regex("[^a-zA-Z0-9]");
            string clean_login = "";
            while (true)
            {
                try
                {
                    byte[] m1 = Encoding.ASCII.GetBytes("Enter login: ");
                    byte[] m2 = Encoding.ASCII.GetBytes("Enter password: ");
                    stream.Write(m1, 0, m1.Length);
                    stream.Read(login, 0, 1024);
                    stream.Write(m2, 0, m2.Length);
                    stream.Read(password, 0, 1024);
                    clean_login = alpha.Replace(Encoding.ASCII.GetString(login, 0, login.Length), "");
                    string clean_password = alpha.Replace(Encoding.ASCII.GetString(password, 0, password.Length), "");
                    bool correct = db.Select(clean_login, clean_password);
                    if (correct)
                    {
                        byte[] m3 = Encoding.ASCII.GetBytes("Welcome!\n");
                        stream.Write(m3, 0, m3.Length);
                        break;
                    }
                    else
                    {
                        byte[] m3 = Encoding.ASCII.GetBytes("Incorrect login or password!\n");
                        stream.Write(m3, 0, m3.Length);
                    }
                }
                catch (IOException e)
                {
                    Console.Error.WriteLine(e.Message);
                    stream.Close();
                    break;
                }
            }
            while (true)
            {
                try
                {
                    command = new byte[1024];
                    byte[] menu = Encoding.ASCII.GetBytes("1.Format text.\n2.Exit\n" + clean_login + "> ");
                    stream.Write(menu, 0, menu.Length);
                    stream.Read(command, 0, 1024);
                    Console.WriteLine(alpha.Replace(Encoding.ASCII.GetString(command), ""));
                    int clean_command = int.Parse(alpha.Replace(Encoding.ASCII.GetString(command, 0, command.Length), ""));
                    command = new byte[1024];
                    if(clean_command == 1)
                    {
                        byte[] m4 = Encoding.ASCII.GetBytes("Enter a string to format: ");
                        stream.Write(m4, 0, m4.Length);
                        stream.Read(command, 0, 1024);
                        string input_text =  alpha.Replace(Encoding.ASCII.GetString(command, 0, command.Length), "");
                        byte[] m5 = Encoding.ASCII.GetBytes(App.Lib.Prepare(input_text) + "\n");
                        stream.Write(m5, 0, m5.Length);
                    }
                    else if(clean_command == 2)
                    {
                        byte[] goodbye = Encoding.ASCII.GetBytes("Goodbye!\n");
                        stream.Write(goodbye, 0, goodbye.Length);
                        stream.Close();
                        break;
                    }
                }
                catch (IOException e)
                {
                    Console.Error.WriteLine(e.Message);
                    stream.Close();
                    break;
                }
            }
        }
        public void Start()
        {
            listener.Start();
            AcceptClient();
        }
    }
}
