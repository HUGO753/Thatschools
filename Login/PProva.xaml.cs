using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
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
        private OpenFileDialog abrir = null;
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
        int codigo;
        void encherCombo()
        {
            System.Data.DataSet tb = new System.Data.DataSet();
            OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT ma.titulo, ma.codigo FROM materia ma INNER JOIN prof_mate pm ON ma.codigo=pm.cod_materia WHERE pm.cod_prof="+ captura("SELECT cod_tipo FROM usuarios WHERE cod="+codigo), con);
            da.Fill(tb, "0"); 
            int a = 0;
            comboBox1.Items.Clear();
            while (tb.Tables["0"].Rows.Count > a)
            {
                comboBox1.Items.Add(tb.Tables["0"].Rows[a]["codigo"].ToString() + "-" + tb.Tables["0"].Rows[a]["titulo"].ToString());
                a++;
            }
        }
        public PProva(int cod)
        {
            codigo = cod;
            InitializeComponent();
            abrir = new OpenFileDialog();
            abrir.FileOk += abrirtexto;
            encherCombo();
        }
        private void abrirtexto(object sender, System.ComponentModel.CancelEventArgs e)
        {
                //throw new System.ArgumentException("Parameter cannot be null", "original");
                TextReader leitor = null;
                FileInfo info = new FileInfo(abrir.FileName);
                Texto.Text = "";
                leitor = info.OpenText();
                // Lê linha por linha do arquivo e colocar ao controle "Conteudo.Text"
                string line = leitor.ReadLine();
                Titulo.Text = System.IO.Path.GetFileNameWithoutExtension(abrir.FileName);
                while (line != null)
                {
                    Texto.Text += line;
                    line = leitor.ReadLine();
                    if (line != null)
                    {
                        Texto.Text += "\n";
                    }
                }
                leitor.Close();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

            abrir.ShowDialog();
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            if ((Convert.ToInt32(tp_hr.Text) * 60 + Convert.ToInt32(tp_mn.Text)) != 0 && Titulo.Text != "" && Texto.Text != "" && comboBox1.Text != "")
            {
                DataSet tb = new DataSet();
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT titulo, codigo_prof FROM prova WHERE titulo='"+Titulo.Text+"' AND codigo_prof =" + codigo + ";", con);
                da.Fill(tb, "professor");
                con.Close();
                if (tb.Tables["professor"].Rows.Count > 0)
                {
                    MessageBox.Show("Já existe esse titulo! Insira o outro titulo!");
                }
                else
                {
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO prova(codigo_prof,titulo,texto,tempo) VALUES (" + captura("SELECT codigo FROM prof_mate WHERE cod_prof="+ captura("SELECT cod_tipo FROM usuarios WHERE cod=" + codigo)+" AND cod_materia="+comboBox1.Text.Split('-')[0]) + ",'" + Titulo.Text + "','" + Texto.Text + "'," + (Convert.ToInt32(tp_hr.Text) * 60 + Convert.ToInt32(tp_mn.Text)) + ")");
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Titulo.Text = "";Texto.Text = "";tp_hr.Text = "00";tp_mn.Text = "00";comboBox1.Text = "";
                    MessageBox.Show("Prova cadastrada com sucesso!");
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
                    MessageBox.Show("Insira o texto da prova!");
                }
                else if(comboBox1.Text == "")
                {
                    MessageBox.Show("Insira a matéria da prova!");
                }else
                {
                    MessageBox.Show("Insira o tempo de prova!");
                }
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PMenu a = new PMenu(codigo);
            a.Show();
        }
    }
}
