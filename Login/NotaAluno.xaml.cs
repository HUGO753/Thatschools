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
    /// Interaction logic for NotaAluno.xaml
    /// </summary>
    public partial class NotaAluno : Window
    {
        int codigo; bool foi = false;
        void carregar()
        {
            
        }
        void MostrarNota() {
            DataTable tb = new DataTable();
            
            OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT p.titulo, pa.nota FROM (prova p INNER JOIN provasfinalizadas pf on p.codigo=pf.codigo_prova) LEFT JOIN provasavaliadas pa ON pa.cod_provafinalizada=pf.codigo WHERE pf.codigo_aluno = "+captura("SELECT cod_tipo FROM usuarios WHERE cod =" + codigo), con);
            DadosNota.Items.Clear();
            da.Fill(tb);
            DadosNota.ItemsSource = tb.DefaultView;
        }
        string captura(string query)
        {
            string a;
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
            OleDbCommand cmd = new OleDbCommand();

            con.Open();
            cmd.Connection = con;
            cmd.CommandText = query;
            a = cmd.ExecuteScalar().ToString();
            con.Close();
            return a;
        }
        //captura("SELECT cod_tipo FROM usuarios WHERE cod ="+codigo)
        void Preencher()
        {
            comboBox.Items.Clear();
            DataSet tb = new DataSet();
            
            OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT m.codigo,m.titulo FROM (materia m INNER JOIN prof_mate pm ON m.codigo=pm.cod_materia) INNER JOIN matricula ma ON pm.codigo=ma.codigo_prof_mate WHERE ma.cod_aluno="+captura("SELECT cod_tipo FROM usuarios WHERE cod =" + codigo), con);
            da.Fill(tb, "aluno");
            int a = 0;
            while (tb.Tables["aluno"].Rows.Count > a)
            {
                comboBox.Items.Add(tb.Tables["aluno"].Rows[a]["codigo"].ToString() + "-"+tb.Tables["aluno"].Rows[a]["titulo"].ToString());
                a++;
            }
            con.Close();
        }
        public NotaAluno(int cod)
        {
            InitializeComponent();
            codigo = cod;
            DataTable dt = new DataTable();
            Preencher();
        }


        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (foi == true)
            {
                MostrarNota();
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foi = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AMenu a = new AMenu(codigo);
            a.Show();
        }
    }
}
