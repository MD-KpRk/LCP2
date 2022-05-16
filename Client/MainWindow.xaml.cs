using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;
using Library;
using Library.UDP;

namespace Client
{
    public partial class MainWindow : Window
    {
        int targetPort = 8001;
        int recievePort = 8002;

        MainWindowViewModel viewModel = new MainWindowViewModel();
        UDPServer server;
        public bool CanScroll { get; set; } = true;

        public MainWindow()
        {
            server = new UDPServer(recievePort);
            InitializeComponent();
            this.DataContext = viewModel;
            UDPClient udpClient = new UDPClient(targetPort);

            server.StartBroadCastRecieve(AddNewRow);
            udpClient.SendBroadCastMessage(new LCPP(recievePort, targetPort, MyIP.IPv4, CommandEnum.Ping),2);
        }

        public void AddNewRow(LCPP pocket)
        {
            viewModel.AddUser(new UserModel() { HostName = Dns.GetHostEntry(pocket.SourceIP).HostName, IP = pocket.SourceIP.ToString() });
        }

        #region Скроллинг юзеров
        Point scrollMousePoint = new Point();
        double hOff = 1;
        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollviewer);
            hOff = scrollviewer.VerticalOffset;
            if (CanScroll == true)
            {
                scrollviewer.CaptureMouse();
            }
        }
        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (scrollviewer.IsMouseCaptured)
            {
                scrollviewer.ScrollToVerticalOffset(hOff + (scrollMousePoint.Y - e.GetPosition(scrollviewer).Y));
            }
        }
        private void scrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollviewer.ReleaseMouseCapture();
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollviewer.ScrollToHorizontalOffset(scrollviewer.VerticalOffset + e.Delta);
        }
        #endregion

    }

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        ObservableCollection<UserModel> users = new ObservableCollection<UserModel>();

        public ObservableCollection<UserModel> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged("Users");
            }
        }
        public void AddUser(UserModel user)
        {
            if (Users.Any(elem => elem.IP == user.IP)) return; 
            Application.Current.Dispatcher.Invoke(delegate 
            {
                Users.Add(user);
            });
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class UserModel
    {
        public string HostName { get; set; } = "";
        public string IP { get; set; } = "";
        public string Status { get; set; } = "";

    }

}
