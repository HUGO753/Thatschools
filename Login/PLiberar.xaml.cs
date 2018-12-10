using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace Login
{
    /// <summary>
    /// Lógica interna para PLiberar.xaml
    /// </summary>
    public partial class PLiberar : Window
    {
        int codigo; bool mudado=false;
        void nota(string comando)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=tschoolbd"); // Conecta ao banco de dados
            MySqlCommand cmd = new MySqlCommand(comando);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        string captura(string query)
        {
            string a;
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=tschoolbd"); // Conecta ao banco de dados
            MySqlCommand cmd = new MySqlCommand();

            con.Open();
            cmd.Connection = con;
            cmd.CommandText = query;
            a = cmd.ExecuteScalar().ToString();
            con.Close();
            return a;
        }
        void Encher()
        {
            DataSet tb = new DataSet();
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=tschoolbd"); // Conecta ao banco de dados
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT status,texto FROM ts_prova WHERE codigo='" + Combobox.Text.Split('-')[0] + "';", con);
            da.Fill(tb, "aluno");
            Texto.Text = tb.Tables["aluno"].Rows[0]["texto"].ToString();
            con.Close();
            butao.IsEnabled = true;
            if (Convert.ToInt16(tb.Tables["aluno"].Rows[0]["status"]) == 0) {
                butao.Content = "Liberar";
            }
            else
            {
                butao.Content = "Bloquear";
            }

        }
        void Atualizar()
        {
            System.Data.DataSet tb = new System.Data.DataSet();
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=tschoolbd"); // Conecta ao banco de dados
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT p.codigo,p.titulo FROM ts_prova p INNER JOIN ts_prof_mate pm ON p.codigo_prof=pm.codigo WHERE pm.cod_prof = " + captura("SELECT cod_tipo FROM ts_usuarios WHERE codigo=" + codigo), con);
            da.Fill(tb, "0");
            int a = 0;
            Combobox.Items.Clear();
            while (tb.Tables["0"].Rows.Count > a)
            {
                Combobox.Items.Add(tb.Tables["0"].Rows[a]["codigo"].ToString() + "-" + tb.Tables["0"].Rows[a]["titulo"].ToString());
                a++;
            }
        }
        public PLiberar(int cod)
        {
            InitializeComponent();
            codigo = cod;
            Atualizar();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PMenu a = new PMenu(codigo);
            a.Show();
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (mudado == true)
            {
                Encher();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mudado = true;
        }

        private void butao_Click(object sender, RoutedEventArgs e)
        {
            if (butao.Content.Equals("Liberar"))
            {
                nota("UPDATE ts_prova SET status = 1 WHERE codigo=" + Combobox.Text.Split('-')[0]);
                butao.Content = "Bloquear";
            }
            else
            {
                nota("UPDATE ts_prova SET status = 0 WHERE codigo=" + Combobox.Text.Split('-')[0]);
                butao.Content = "Liberar";
            }
        }
    }
}
