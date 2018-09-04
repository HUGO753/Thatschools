using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.OleDb;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (tbusuario.Text !="" && tbsenha.Password != "")
            {
                DataSet tb = new DataSet();
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\0040481621005\Documents\Visual Studio 2015\Projects\Thatschool\bd.accdb"); // Conecta ao banco de dados

                OleDbCommand cmd = con.CreateCommand();
                con.Open();

                OleDbDataAdapter da = new OleDbDataAdapter("SELECT tipo FROM usuarios WHERE usuario='" + tbusuario.Text + "' AND senha='" + tbsenha.Password + "'", con);
                da.Fill(tb, "0");
                if (tb.Tables["0"].Rows.Count>0) MessageBox.Show("Usuario: " + tb.Tables["0"].Rows[0]["tipo"] + "");
                else MessageBox.Show("Não existes");
                con.Close();
            }else
            {
                MessageBox.Show("Pow jamelão!");
            }
        }

        private void textBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            textBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00AEFF"));
        }

        private void textBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            textBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0074FF"));
        }

        private void textBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Ir para um lugar");
        }
    }
}
