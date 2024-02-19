using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZadachiWPF.Api.DTO;

namespace ZadachiWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public List<StatusDTO> Statuses { get; set; }
        public ObservableCollection<ZadachiDTO> Zadachis { get; set; }
        public ZadachiDTO SelectedZadachi { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            LoadZadachis();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private async Task LoadZadachis()
        {
            Client client = new Client();
            await LoadZadachis(client);
        }

        private async Task LoadZadachis(Client client)
        {
            Zadachis = new ObservableCollection<ZadachiDTO>(await client.GetZadachis());
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Zadachis)));
        }

        private async void Edit(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            SelectedZadachi = b.Tag as ZadachiDTO;
            new EditZadachi(SelectedZadachi).ShowDialog();
            LoadZadachis();

        }

        private async void Delete(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            SelectedZadachi = b.Tag as ZadachiDTO;
            await Client.Instance.DeleteZadachi(SelectedZadachi.Idzadachi);
            await LoadZadachis();
        }

        private async void Add(object sender, RoutedEventArgs e)
        {
            new AddZadachi().ShowDialog();
            await LoadZadachis();
        }
    }
}