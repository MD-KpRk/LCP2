using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Library
{
    public delegate void MessageProcesser(string message);

    public class UDP
    {
        public static readonly IPAddress groupAddr = IPAddress.Parse("235.5.5.11");

        public class Client
        {
            public readonly int targetPort;
            public Client(int targetPort)
            {
                this.targetPort = targetPort;
            }
            public void SendBroadCastMessage(string Message)
            {
                UdpClient sender = new UdpClient();
                IPEndPoint endPoint = new IPEndPoint(groupAddr, targetPort);
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
            public readonly int recievePort;
            public Server(int recievePort)
            {
                this.recievePort = recievePort;
            }

            public void RecieveMessage(object ?delg)
            {
                UdpClient receiver = new UdpClient(recievePort);
                receiver.JoinMulticastGroup(groupAddr, 10);
                IPEndPoint remoteIp = null;
                try
                {
                    while (true)
                    {
                        byte[] data = receiver.Receive(ref remoteIp);
                        string message = Encoding.Unicode.GetString(data);
                        try
                        {
                            MessageProcesser? messageProcessing = delg as MessageProcesser;
                            if (messageProcessing == null) return;
                            messageProcessing(message);
                        }
                        catch(Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }
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
