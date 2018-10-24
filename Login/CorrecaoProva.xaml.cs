using System;
using System.Collections.Generic;
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
    /// Interaction logic for CorrecaoProva.xaml
    /// </summary>
    public partial class CorrecaoProva : Window
    {
        int codigo; bool m=false,p=false,a=false;
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
        void atualizar()
        {
            System.Data.DataSet tb = new System.Data.DataSet();
            OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT m.codigo,m.titulo FROM materia m INNER JOIN prof_mate pm ON m.codigo=pm.cod_materia WHERE pm.cod_prof="+ captura("SELECT cod_tipo FROM usuarios WHERE cod=" + codigo), con);
            da.Fill(tb, "0");
            int a = 0;
            comboBox2.Items.Clear();
            while (tb.Tables["0"].Rows.Count > a)
            {
                comboBox2.Items.Add(tb.Tables["0"].Rows[a]["codigo"].ToString() + "-" + tb.Tables["0"].Rows[a]["titulo"].ToString());
                a++;
            }
        }

        public CorrecaoProva(int cod)
        {
            InitializeComponent();
            codigo = cod;
            atualizar();
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            PMenu a = new PMenu(codigo);
            a.Show();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

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
                p = false;
                a = false;
                comboBox1.Text = "";
                comboBox.Text = "";
            }
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            if (a)
            {
                //***** Implementar***///
            }
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            a = true;
        }

        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (p)
            {
                a = false;
                comboBox1.Text = "";
            }
        }
    }
}
