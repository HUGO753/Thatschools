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
    public partial class PMenu : Window
    {
        int codigo;
        public PMenu(int cod)
        {
            InitializeComponent();
            codigo = cod;
        }

        private void B_prova_Click(object sender, RoutedEventArgs e)
        {
            PProva prova = new PProva(codigo);
            prova.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CorrecaoProva a = new CorrecaoProva(codigo);
            a.Show();
            this.Close();
        }
    }
}
