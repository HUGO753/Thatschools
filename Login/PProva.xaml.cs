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
    /// Interaction logic for PProva.xaml
    /// </summary>
    public partial class PProva : Window
    {
        int codigo;
        public PProva(int cod)
        {
            codigo = cod;
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            if ((Convert.ToInt32(tp_hr.Text) * 60 + Convert.ToInt32(tp_mn.Text)) != 0 && Titulo.Text != "" && Texto.Text != "")
            {
                DataSet tb = new DataSet();
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Hugo\Desktop\Thatschool\bd.accdb"); // Conecta ao banco de dados
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT titulo FROM prova WHERE titulo='"+Titulo.Text+"';", con);
                da.Fill(tb, "professor");
                con.Close();
                if (tb.Tables["professor"].Rows.Count > 0)
                {
                    MessageBox.Show("Já existe esse titulo! Insira o outro titulo!");
                }
                else
                {
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO prova(codigo_prof,titulo,texto,tempo) VALUES (" + codigo + ",'" + Titulo.Text + "','" + Texto.Text + "'," + (Convert.ToInt32(tp_hr.Text) * 60 + Convert.ToInt32(tp_mn.Text)) + ")");
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Titulo.Text = "";
                    Texto.Text = "";
                    tp_hr.Text = "00";
                    tp_mn.Text = "00";
                    MessageBox.Show("Inserido com sucesso!");
                }
            }
            else
            {
                if (Titulo.Text == "")
                {
                    MessageBox.Show("Insira o titulo da prova!");
                }
                else if (Texto.Text == "")
                {
                    MessageBox.Show("Insira o texto!");
                }
                else
                {
                    MessageBox.Show("Insira o tempo de prova!");
                }
            }
        }
    }
}
