﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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

namespace Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MyIP ip = new MyIP();

            MessageBox.Show("Local IP: " + ip.IPv4?.ToString());

            UDP.Client udpClient = new UDP.Client(8001);
            udpClient.SendBroadCastMessage("Hello all !");

            UDP.Server udpServer = new UDP.Server(8002);


            
        }
    }



}
