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
    //public class UDPServer
    //{
    //    public static readonly IPAddress groupAddr = IPAddress.Parse("235.5.5.11");
    //    public readonly int recievePort;
    //    Thread receiveThread;
    //    UdpClient receiver;

    //    public UDPServer(int recievePort)
    //    {
    //        receiver = new UdpClient(recievePort);
    //        receiveThread = new Thread(new ParameterizedThreadStart(RecieveMessage));
    //        this.recievePort = recievePort;
    //    }

    //    public void RecieveMessage(object? delg)
    //    {
    //        //receiver.JoinMulticastGroup(groupAddr);
    //        IPEndPoint? remoteIp = null;
    //        try
    //        {
    //            MessageProcesser? messagefunc = delg as MessageProcesser;
    //            if (messagefunc == null) throw new ArgumentNullException();

    //            while (true)
    //            {
    //                byte[] data = receiver.Receive(ref remoteIp);
    //                string message = Encoding.Unicode.GetString(data);
    //                try
    //                {
    //                    messagefunc(DeEncrypter.Encrypt(message));
    //                }
    //                catch (Exception ex)
    //                {
    //                    Debug.WriteLine(ex.Message);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.WriteLine(ex.Message);
    //        }
    //        finally
    //        {
    //            receiver.Close();
    //        }
    //    }

    //    public void StartRecieving(MessageProcesser messageProcesser)
    //    {
    //        receiveThread.Start(new MessageProcesser(messageProcesser));
    //    }

    //    public void StopRecieving()
    //    {
    //        receiver?.Close();
    //    }
    //}

    public class UDPServer
    {
        UdpClient udpClient = new UdpClient();
        IPEndPoint from = new IPEndPoint(0, 0);
        Task? task;
        public readonly int recievePort;


        public void StartBroadCastRecieve(MessageProcesser messagefunc)
        {
            if (messagefunc == null) throw new ArgumentNullException();
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, recievePort));

            task = Task.Run(() =>
            {
                while (true)
                {
                    task.Wait(10000);
                    try
                    {
                        byte[] recvBuffer = udpClient.Receive(ref from);
                        messagefunc(DeEncrypter.Encrypt(Encoding.UTF8.GetString(recvBuffer)));
                    }
                    catch (Exception)
                    { }
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
