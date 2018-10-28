using System;
using System.Collections.Generic;
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

namespace Login
{
    /// <summary>
    /// Interaction logic for AMenu.xaml
    /// </summary>
    public partial class AMenu : Window
    {
        int codigo; bool sair=true;
        public AMenu(int cod)
        {
            InitializeComponent();
            codigo = cod;
        }

        private void B_prova_Click(object sender, RoutedEventArgs e)
        {
            AProva prova = new AProva(codigo);
            prova.Show();
            sair = false;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (sair) {
                if (MessageBox.Show("Deseja Sair?", "Sair", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MainWindow a = new MainWindow();
                    a.Show();
                }
                else e.Cancel = true;
            }
        }
    }
}
