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
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = "+ System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados

                OleDbCommand cmd = con.CreateCommand();
                con.Open();

                OleDbDataAdapter da = new OleDbDataAdapter("SELECT tipo,cod FROM usuarios WHERE usuario='" + tbusuario.Text + "' AND senha='" + tbsenha.Password + "'", con);
                da.Fill(tb, "0");
                if (tb.Tables["0"].Rows.Count>0) switch (tb.Tables["0"].Rows[0]["tipo"].ToString()) { case "1": AMenu tela1 = new AMenu((int)tb.Tables["0"].Rows[0]["cod"]); con.Close(); tela1.Show(); this.Close(); break; case "2": PMenu tela2 = new PMenu((int)tb.Tables["0"].Rows[0]["cod"]); con.Close(); tela2.Show(); this.Close(); break; case "3": SMenu tela3 = new SMenu((int)tb.Tables["0"].Rows[0]["cod"]); con.Close(); tela3.Show(); this.Close(); break; }
                else MessageBox.Show("Não existes");
                con.Close();
            }else
            {
                MessageBox.Show("Pow jamelão!");
            }
        }


        private void textBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Ir para um lugar");
        }

        private void textBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            textBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0074FF"));
        }

        private void textBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            textBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00AEFF"));
        }
    }
}
