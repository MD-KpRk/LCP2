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
        UDPServer udpServer = new UDPServer(8001);
        public MainWindow()
        {
            InitializeComponent();
            udpServer.StartBroadCastRecieve(GetAnswer);
        }

        void GetAnswer(LCPP pocket)
        {
            //MessageBox.Show(pocket.Command);

            //UDPClient udpClient = new UDPClient(8002);
            //udpClient.SendMessage(new LCPP(8001, 8002, MyIP.IPv4, "1","ADW"), pocket.SourceIP);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            udpServer.StopRecieving();
        }
    }
}
