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
    public partial class SMenu : Window
    {
        int codigo; bool sair = true;
        public SMenu(int cod)
        {
            InitializeComponent();
            codigo = cod;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Cadastrar w = new Cadastrar(codigo);
            w.Show();
            sair = false;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (sair)
            {
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
