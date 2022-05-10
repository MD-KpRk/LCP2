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
            int targetPort = 8001;
            int recievePort = 8003;
            InitializeComponent();
            UDP.Client udpClient = new UDP.Client(targetPort);

            LCPP message;
            message = new LCPP(recievePort,targetPort,MyIP.IPv4, "Привет");

            udpClient.SendBroadCastMessage(message);
        }
    }



}
