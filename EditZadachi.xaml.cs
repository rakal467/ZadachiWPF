using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
using System.Windows.Shapes;
using ZadachiWPF.Api.DTO;

namespace ZadachiWPF
{
    /// <summary>
    /// Логика взаимодействия для EditZadachi.xaml
    /// </summary>
    public partial class EditZadachi : Window, INotifyPropertyChanged
    {
        private ZadachiDTO selectedZadachi;
        private StatusDTO selectedStatus;

        public List<StatusDTO> Statuses { get; set; }
        public StatusDTO SelectedStatus
        {
            get => selectedStatus;
            set
            {
                selectedStatus = value;
                Signal();
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public ZadachiDTO SelectedZadachi
        {
            get => selectedZadachi;
            set
            {
                selectedZadachi = value;

            }
        }
        public EditZadachi(ZadachiDTO zombie)
        {
            InitializeComponent();

            SelectedZadachi = zombie;
            LoadStatus();
            DataContext = this;
        }

        private async Task LoadStatus()
        {
            var client = new Client();
            Statuses = await client.GetStatus();
            SelectedStatus = Statuses.FirstOrDefault(s => s.Idstatus == SelectedZadachi.IdStatus);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Statuses)));
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (SelectedZadachi == null)
            {
                MessageBox.Show("!!");
                return;
            }
            SelectedZadachi.IdStatus = SelectedStatus.Idstatus;
            SelectedZadachi.Status0 = SelectedStatus.NameStatus;
            Client.Instance.EditZadachi(SelectedZadachi, SelectedZadachi.Idzadachi);
            Close();
        }
    }

}

