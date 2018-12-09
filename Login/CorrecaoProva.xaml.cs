using System;
using System.Collections.Generic;
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

namespace Login
{
    /// <summary>
    /// Interaction logic for CorrecaoProva.xaml
    /// </summary>
    public partial class CorrecaoProva : Window
    {
        int codigo; bool m=false,p=false,a=false;
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
        void atualizar()
        {
            System.Data.DataSet tb = new System.Data.DataSet();
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=tschoolbd"); // Conecta ao banco de dados
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT m.codigo,m.titulo FROM materia m INNER JOIN prof_mate pm ON m.codigo=pm.cod_materia WHERE pm.cod_prof="+ captura("SELECT cod_tipo FROM usuarios WHERE cod=" + codigo), con);
            da.Fill(tb, "0");
            int a = 0;
            comboBox2.Items.Clear();
            while (tb.Tables["0"].Rows.Count > a)
            {
                comboBox2.Items.Add(tb.Tables["0"].Rows[a]["codigo"].ToString() + "-" + tb.Tables["0"].Rows[a]["titulo"].ToString());
                a++;
            }
        }
        void nota(string comando)
        {
            MySqlConnection con = new MySqlConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
            MySqlCommand cmd = new MySqlCommand(comando);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        void aluno()
        {
            System.Data.DataSet tb = new System.Data.DataSet();
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=tschoolbd"); // Conecta ao banco de dados
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT a.nome, pf.codigo FROM aluno a INNER JOIN provasfinalizadas pf ON pf.codigo_aluno=a.codigo WHERE pf.corrigida=0 and pf.codigo_prova="+comboBox.Text.Split('-')[0], con);
            da.Fill(tb, "0");
            int a = 0;
            comboBox1.Items.Clear();
            while (tb.Tables["0"].Rows.Count > a)
            {
                comboBox1.Items.Add(tb.Tables["0"].Rows[a]["codigo"].ToString() + "-" + tb.Tables["0"].Rows[a]["nome"].ToString());
                a++;
            }
        }
        void texto()
        {
            System.Data.DataSet tb = new System.Data.DataSet();
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=tschoolbd"); // Conecta ao banco de dados
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT texto FROM provasfinalizadas where codigo="+comboBox1.Text.Split('-')[0], con);
            da.Fill(tb, "0");
            int a = 0;
            while (tb.Tables["0"].Rows.Count > a)
            {
                Texto.Text = tb.Tables["0"].Rows[a]["texto"].ToString();
                a++;
            }
        }
        void prova()
        {
            System.Data.DataSet tb = new System.Data.DataSet();
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=tschoolbd"); // Conecta ao banco de dados
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT p.codigo,p.titulo FROM prova p INNER JOIN prof_mate pm ON p.codigo_prof=pm.codigo WHERE pm.cod_prof = " + captura("SELECT cod_tipo FROM usuarios WHERE cod=" + codigo) + " AND pm.cod_materia=" + comboBox2.Text.Split('-')[0], con);
            da.Fill(tb, "0");
            int a = 0;
            comboBox.Items.Clear();
            while (tb.Tables["0"].Rows.Count > a)
            {
                comboBox.Items.Add(tb.Tables["0"].Rows[a]["codigo"].ToString() + "-" + tb.Tables["0"].Rows[a]["titulo"].ToString());
                a++;
            }
        }

        public CorrecaoProva(int cod)
        {
            InitializeComponent();
            codigo = cod;
            atualizar();
        }



        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (Texto.Text == "") MessageBox.Show("Insira a prova!");
            else if (textBox1.Text == "") MessageBox.Show("Insira a nota!");
            else if(MessageBox.Show("Deseja avaliar?", "Nota", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                nota("UPDATE provasfinalizadas SET corrigida = 1 WHERE codigo="+ comboBox1.Text.Split('-')[0]);
                nota("INSERT INTO provasavaliadas(cod_provafinalizada,nota) VALUES ("+ comboBox1.Text.Split('-')[0] + ","+textBox1.Text+")");
                comboBox1.Text = "Aluno";
                comboBox.Text = "Prova";
                comboBox2.Text = "Materia";
                Texto.Text = "";
                atualizar();
                m = false;
                p = false;
                a = false;
                MessageBox.Show("Nota inserida com sucesso!");
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            p = true;
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m = true;
        }

        private void comboBox2_DropDownClosed(object sender, EventArgs e)
        {
            if (m)
            {
                comboBox1.Text = "Aluno";
                comboBox.Text = "Prova";
                Texto.Text = "";
                p = false;
                a = false;
                prova();
            }
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            if (a)
            {
                texto();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PMenu a = new PMenu(codigo);
            a.Show();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            a = true;
        }

        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (p)
            {
                comboBox1.Text = "Aluno";
                Texto.Text = "";
                a = false;
                aluno();
            }
        }
    }
}
