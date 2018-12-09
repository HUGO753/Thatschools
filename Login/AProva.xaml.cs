using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
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
using System.Windows.Threading;

namespace Login
{
    /// <summary>
    /// Interaction logic for AProva.xaml
    /// </summary>
   
    public partial class AProva : Window
    {
        int codigo,min,seg,codigo_prova;
        DispatcherTimer temp = new DispatcherTimer();
        string captura(string query)
        {
            string a;
            MySqlConnection con = new MySqlConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
            MySqlCommand cmd = new MySqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = query;
            a = cmd.ExecuteScalar().ToString();
            con.Close();
            return a;
        }
        public AProva(int cod)
        {
            
            codigo = cod;
            InitializeComponent();
            Encher();
            temp.Interval = new TimeSpan(0, 0, 1);
            temp.Tick += new EventHandler(contagem);
        }

        void contagem(object sender, EventArgs d) {
            seg -= 1;
            if (seg < 0)
            {
                seg = 59;
                min--;
            }
            InserirContagem();
            
        }
        void limpar()
        {
            Texto.Text = "";
            Titulo.Text = "";
            listadeprovas.Text = "";
            tempo.Content = "00:00";
        }
        void InserirContagem()
        {
            if (min < 10)
            {
                if (seg < 10) tempo.Content = "0" + min + ":0" + seg;
                else tempo.Content = "0" + min + ":" + seg;
            }
            else
            {
                if (seg < 10) tempo.Content = min + ":0" + seg;
                else tempo.Content = min + ":" + seg;
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AMenu a = new AMenu(codigo);
            a.Show();
        }

        void Encher()
        {
            listadeprovas.Items.Clear();
            DataSet tb = new DataSet();
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=tschoolbd"); // Conecta ao banco de dados
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT p.titulo FROM (prova p LEFT JOIN provasfinalizadas pf ON p.codigo=pf.codigo_prova) INNER JOIN prof_mate pm ON pm.codigo=p.codigo_prof WHERE (pf.codigo_aluno <> " + captura("SELECT cod_tipo FROM usuarios WHERE cod=" + codigo) + " OR pf.codigo_prova IS NULL) AND pm.codigo =  " + captura("SELECT m.codigo_prof_mate FROM (matricula m INNER JOIN aluno a ON m.cod_aluno=a.codigo) INNER JOIN usuarios u ON u.cod_tipo=a.codigo WHERE u.cod=" + codigo), con);
            da.Fill(tb,"aluno");
            int a=0;
            while (tb.Tables["aluno"].Rows.Count > a)
            {
                listadeprovas.Items.Add(tb.Tables["aluno"].Rows[a]["titulo"].ToString());
                a++;
            }
            con.Close();
        }
        int warning = 0;
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.LeftAlt)
            {
                warning++;
                MessageBox.Show("Você pressionou Alt ! warning:" + warning);
            }
            if (e.Key == Key.Tab)
            {
                warning++;
                MessageBox.Show("Você pressionou tab ! warning:"+warning);
            }
            if (warning > 3)
            {
                MessageBox.Show("Dead");
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            DataSet tb = new DataSet();
            MySqlConnection con = new MySqlConnection(@"Provider = Microsoft.ACE.OLEDB.12.0;Data Source = " + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
            con.Open();
            Titulo.Text = listadeprovas.Text;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT codigo,texto,tempo FROM prova WHERE titulo='"+Titulo.Text+"';", con);
            da.Fill(tb, "aluno");
            Texto.Text = tb.Tables["aluno"].Rows[0]["texto"].ToString();
            seg = Convert.ToInt32(tb.Tables["aluno"].Rows[0]["tempo"])%60;
            min = Convert.ToInt32(tb.Tables["aluno"].Rows[0]["tempo"])/60;
            codigo_prova = Convert.ToInt32(tb.Tables["aluno"].Rows[0]["codigo"]);
            InserirContagem();
            con.Close();
            temp.Start();
            listadeprovas.IsEnabled = false;
            Finalizar.IsEnabled = true;
            comecar.IsEnabled = false;
        }


        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Gostaria de finalizar?", "Finalizar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Finalizar.IsEnabled = false;
                comecar.IsEnabled = true;
                listadeprovas.IsEnabled = true;
                temp.Stop();
                MySqlConnection con = new MySqlConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
                MySqlCommand cmd = new MySqlCommand("INSERT INTO provasfinalizadas(codigo_aluno,codigo_prova,texto) VALUES (" + captura("SELECT cod_tipo FROM usuarios WHERE cod="+codigo) + "," + codigo_prova + ",'" + Texto.Text + "');");
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Encher();
                limpar();
            }
        }

    }
}
