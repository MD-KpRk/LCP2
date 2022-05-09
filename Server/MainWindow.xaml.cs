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

namespace Server
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UDP.Server udpServer = new UDP.Server(8001);

            Thread receiveThread = new Thread(new ParameterizedThreadStart(udpServer.RecieveMessage));
            MessageProcesser msgdel = GetAnswer;
            receiveThread.Start(msgdel);

        }

        void GetAnswer(string str)
        {
            MessageBox.Show(str);
        }
    }
}
