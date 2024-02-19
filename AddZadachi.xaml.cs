using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZadachiWPF.Api.DTO;

namespace ZadachiWPF
{
    /// <summary>
    /// Логика взаимодействия для AddZadachi.xaml
    /// </summary>
    public partial class AddZadachi : Window, INotifyPropertyChanged
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        public List<StatusDTO> Statuses { get; set; }
        public StatusDTO SelectedStatus { get; set; }
        public AddZadachi()
        {
            InitializeComponent();
            Loadstatuses();
            DataContext = this;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private async void Loadstatuses()
        {
            var client = new Client();
            Statuses = await client.GetStatus();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Statuses)));
        }

        private async void Save(object sender, RoutedEventArgs e)
        {
            await Client.Instance.AddZadachi1(new ZadachiDTO
            {
                NameZadachi = Name,
                OpisanieZadachi = Description,

                IdStatus = SelectedStatus.Idstatus,
                Status0 = SelectedStatus.NameStatus
            });
            Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

