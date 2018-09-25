using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
    /// Interaction logic for ProvaA.xaml
    /// </summary>
    public partial class ProvaA : Window
    {
        public ProvaA()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (tb_titulo.Text != "" && tb_texto.Text != "" && tb_hr.Text != "" && tb_mn.Text != "")
            {
                DataSet tb = new DataSet();
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\0040481621005\Documents\Visual Studio 2015\Projects\Thatschool\bd.accdb"); // Conecta ao banco de dados

                OleDbCommand cmd = con.CreateCommand();
                con.Open();

                OleDbDataAdapter da = new OleDbDataAdapter("INSERT INTO prova(codigo_prof,titulo,texto,tempo) VALUES ("+2+",'"+tb_titulo+"','"+tb_texto+"',"+((Convert.ToInt32(tb_hr.Text)*60+Convert.ToInt32(tb_mn.Text)) +")", con);
                da.Fill(tb, "0");
                con.Close();
            }
            else
            {
                if(tb_titulo.Text != "") MessageBox.Show("Por favor digita o título da prova!");
                else if(tb_texto.Text != "") MessageBox.Show("Por favor insira o texto da prova!");
                else if(tb_hr.Text != "") MessageBox.Show("Por favor insira a quantidade de horas da prova!");
                else MessageBox.Show("Por favor insira a quantidade de minutos da prova!");
            }
        }
    }
}
