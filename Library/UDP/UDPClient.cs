using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library.UDP
{
    public class UDPClient
    {
        public readonly int targetPort;
        public UDPClient(int targetPort)
        {
            this.targetPort = targetPort;
        }
        public void SendBroadCastMessage(LCPP pocket)
        {
            UdpClient sender = new UdpClient();
            try
            {
                byte[] data = Encoding.Unicode.GetBytes(DeEncrypter.Decrypt(pocket));
                sender.Send(data, data.Length, new IPEndPoint(IPAddress.Broadcast, pocket.DestPort));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


    }
}
