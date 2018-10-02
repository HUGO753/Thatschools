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
    /// Interaction logic for Cadastrar.xaml
    /// </summary>
    public partial class Cadastrar : Window
    {
        public Cadastrar()
        {
            InitializeComponent();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(comboBox.Text)
            {
                case "Aluno":
                    Cad_aluno.Visibility = Visibility.Visible;
                    Cad_Professor.Visibility = Visibility.Hidden;
                    Cad_secretaria.Visibility = Visibility.Hidden;
                    break;
                case "Professor":
                    Cad_aluno.Visibility = Visibility.Hidden;
                    Cad_Professor.Visibility = Visibility.Visible;
                    Cad_secretaria.Visibility = Visibility.Hidden;
                    break;
                case "Secretario":
                    Cad_aluno.Visibility = Visibility.Hidden;
                    Cad_Professor.Visibility = Visibility.Hidden;
                    Cad_secretaria.Visibility = Visibility.Visible;
                    break;
                case "Turma":

                    break;
                case "Curso":

                    break;
                case "Matricula":

                    break;
            }
        }
    }
}
