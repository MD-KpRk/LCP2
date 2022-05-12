using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Library
{
    public delegate void MessageProcesser(LCPP message);

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
            public void SendBroadCastMessage(LCPP pocket)
            {
                UdpClient sender = new UdpClient();
                IPEndPoint endPoint = new IPEndPoint(groupAddr, targetPort);
                try
                {
                    byte[] data = Encoding.Unicode.GetBytes(DeEncrypter.Decrypt(pocket));
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

            public void SendMessage(LCPP pocket, IPAddress ipAddress)
            {
                UdpClient sender = new UdpClient();
                try
                {
                    byte[] data = Encoding.Unicode.GetBytes(DeEncrypter.Decrypt(pocket));
                    sender.Send(data, data.Length, ipAddress.ToString(), pocket.DestPort);
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
            Thread receiveThread;
            bool stopped = false;

            public Server(int recievePort)
            {
                receiveThread = new Thread(new ParameterizedThreadStart(RecieveMessage));
                this.recievePort = recievePort;
            }

            public void RecieveMessage(object ?delg)
            {
                UdpClient receiver = new UdpClient(recievePort);
                receiver.JoinMulticastGroup(groupAddr, 10);
                IPEndPoint remoteIp = null;
                try
                {
                    while (true && stopped == false)
                    {
                        byte[] data = receiver.Receive(ref remoteIp);
                        string message = Encoding.Unicode.GetString(data);
                        try
                        {
                            MessageProcesser? messageProcessing = delg as MessageProcesser;
                            if (messageProcessing == null) return;
                            LCPP pocket = DeEncrypter.Encrypt(message);
                            messageProcessing(pocket);
                        }
                        catch(Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    receiver.Close();
                }
            }

            public void StartRecieving(MessageProcesser messageProcesser)
            {
                stopped = false;
                receiveThread.Start(new MessageProcesser(messageProcesser));
            }

            public void StopRecieving()
            {
                stopped = true;
            }
        }
    }
}
