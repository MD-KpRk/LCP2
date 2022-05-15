using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Library.UDP
{
    public class UDPServer
    {
        public static readonly IPAddress groupAddr = IPAddress.Parse("235.5.5.11");
        public readonly int recievePort;
        Thread receiveThread;
        UdpClient receiver;

        public UDPServer(int recievePort)
        {
            receiver = new UdpClient(recievePort);
            receiveThread = new Thread(new ParameterizedThreadStart(RecieveMessage));
            this.recievePort = recievePort;
        }

        public void RecieveMessage(object? delg)
        {
            receiver.JoinMulticastGroup(groupAddr);
            IPEndPoint remoteIp = null;
            try
            {
                MessageProcesser? messagefunc = delg as MessageProcesser;
                if (messagefunc == null) return;

                while (true)
                {
                    byte[] data = receiver.Receive(ref remoteIp);
                    string message = Encoding.Unicode.GetString(data);
                    try
                    {
                        messagefunc(DeEncrypter.Encrypt(message));
                    }
                    catch (Exception ex)
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
            receiveThread.Start(new MessageProcesser(messageProcesser));
        }

        public void StopRecieving()
        {
            receiver?.Close();
        }
    }
}
