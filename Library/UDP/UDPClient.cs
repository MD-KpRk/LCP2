using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Library.UDP
{
    public class UDPClient
    {
        public static readonly IPAddress groupAddr = IPAddress.Parse("235.5.5.11");
        public readonly int targetPort;
        public UDPClient(int targetPort)
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
}
