using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Library;
using Library.UDP;

namespace Server
{
    public partial class MainWindow : Window
    {
        int targetPort = 8002;
        int recievePort = 8001;


        UDPServer udpServer;
        public MainWindow()
        {
            udpServer = new UDPServer(recievePort);
            InitializeComponent();

            udpServer.StartBroadCastRecieve(GetAnswer);
        }

        void GetAnswer(LCPP pocket)
        {
            MessageBox.Show(pocket.Command);

            UDPClient udpClient = new UDPClient(targetPort);
            udpClient.SendBroadCastMessage(new LCPP(recievePort, targetPort, MyIP.IPv4, CommandEnum.Pong.ToString()));

        }

    }
}
