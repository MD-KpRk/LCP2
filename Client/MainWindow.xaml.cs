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
using Library;

namespace Client
{
    public partial class MainWindow : Window
    {
        int targetPort = 8001;
        int recievePort = 8002;

        MainWindowViewModel viewModel = new MainWindowViewModel();

        public MainWindow()
        {
            DataContext = viewModel;
            InitializeComponent();
            UDP.Client udpClient = new UDP.Client(targetPort);
            UDP.Server server = new UDP.Server(recievePort);

            server.StartRecieving(AddNewRow);
            udpClient.SendBroadCastMessage(new LCPP(recievePort, targetPort, MyIP.IPv4, "6;Ку"));
        }

        public void AddNewRow(LCPP pocket)
        {
            MessageBox.Show(pocket.Command);
            ObservableCollection<UserModel> users = new ObservableCollection<UserModel>(viewModel.Users.ToList());
            users.Add(new UserModel() { HostName = Dns.GetHostEntry(pocket.SourceIP).HostName, IP = pocket.SourceIP.ToString() });
            viewModel.Users = users;
        }

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

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class UserModel
    {
        public string HostName = "";
        public string IP = "";
        public string Status = "";

    }

}
