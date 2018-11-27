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
using MySql.Data.MySqlClient;

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
        string dados()
        {
            return "server=localhost;user id=root;password=;database=tschoolbd";
        }
        void carregamento()
        {
            barra_uhu.Value = 0;
            carregar.Visibility = Visibility.Visible;
        }
        void button_Click(object sender, RoutedEventArgs e)
        {
            if (tbusuario.Text !="" && tbsenha.Password != "")
            {
                carregamento();
                DataSet tb = new DataSet();
                MySqlConnection con = new MySqlConnection(dados());
                barra_uhu.Value += 10;
                MySqlCommand cmd = new MySqlCommand("SELECT tipo,codigo FROM ts_usuarios WHERE usuario='" + tbusuario.Text + "' AND senha='" + tbsenha.Password + "'", con);
                con.Open();
                barra_uhu.Value += 20;
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(tb, "0");
                barra_uhu.Value += 30;
                if (tb.Tables["0"].Rows.Count > 0)
                {
                    barra_uhu.Value += 40;
                    carregar.Visibility = Visibility.Hidden;
                    switch (tb.Tables["0"].Rows[0]["tipo"].ToString()) { case "1": AMenu tela1 = new AMenu((int)tb.Tables["0"].Rows[0]["codigo"]); con.Close(); tela1.Show(); this.Close(); break; case "2": PMenu tela2 = new PMenu((int)tb.Tables["0"].Rows[0]["codigo"]); con.Close(); tela2.Show(); this.Close(); break; case "3": SMenu tela3 = new SMenu((int)tb.Tables["0"].Rows[0]["codigo"]); con.Close(); tela3.Show(); this.Close(); break; }
                    
                }
                else
                {
                    carregar.Visibility = Visibility.Hidden;
                    MessageBox.Show("Não existes");
                }
                con.Close();
            }else
            {
                MessageBox.Show("Pow jamelão!");
            }
        }
    }
}
