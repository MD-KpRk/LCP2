using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Library
{
    public class UDP
    {
        public static readonly IPAddress groupAddr = IPAddress.Parse("235.5.5.11");

        const int remotePort = 8002; // порт для отправки данных
        const int localPort = 8001; // локальный порт для прослушивания входящих подключений


        public class Client
        {
                
            public void SendBroadCastMessage(string Message)
            {
                UdpClient sender = new UdpClient();
                IPEndPoint endPoint = new IPEndPoint(groupAddr, remotePort);
                try
                {
                    byte[] data = Encoding.Unicode.GetBytes(Message);
                    sender.Send(data, data.Length, endPoint);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    sender.Close();
                }
            }
        }

        public class Server
        {

            public void RecieveMessage()
            {
                UdpClient receiver = new UdpClient(localPort);
                receiver.JoinMulticastGroup(groupAddr, 10);
                IPEndPoint remoteIp = null;
                try
                {
                    while (true)
                    {
                        byte[] data = receiver.Receive(ref remoteIp);
                        string message = Encoding.Unicode.GetString(data);
                        MessageBox.Show(message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    receiver.Close();
                }
            }
        }
    }
}
