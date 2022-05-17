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
        UdpClient udpClient = new UdpClient();
        IPEndPoint from = new IPEndPoint(0, 0);
        public readonly int recievePort;

        public void StartBroadCastRecieve(MessageProcesser messagefunc)
        {
            if (messagefunc == null) throw new ArgumentNullException();
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, recievePort));

            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        byte[] recvBuffer = udpClient.Receive(ref from);
                        messagefunc(DeEncrypter.Encrypt(Encoding.Unicode.GetString(recvBuffer)));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            });
        }

        public void StopRecieving()
        {
            udpClient.Close();
        }

        public UDPServer(int recievePort)
        {
            this.recievePort = recievePort;
        }
    }


}
