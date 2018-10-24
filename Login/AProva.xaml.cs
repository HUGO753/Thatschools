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

        private void Window_Closed(object sender, EventArgs e)
        {
            AMenu a = new AMenu(codigo);
            a.Show();
        }

        void Encher()
        {
            listadeprovas.Items.Clear();
            DataSet tb = new DataSet();
            OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT p.titulo FROM (prova p LEFT JOIN provasfinalizadas pf ON p.codigo=pf.codigo_prova) INNER JOIN prof_mate pm ON pm.codigo=p.codigo_prof WHERE pf.codigo_prova IS NULL", con);
            da.Fill(tb,"aluno");
            int a=0;
            while (tb.Tables["aluno"].Rows.Count > a)
            {
                listadeprovas.Items.Add(tb.Tables["aluno"].Rows[a]["titulo"].ToString());
                a++;
            }
            con.Close();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            DataSet tb = new DataSet();
            OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0;Data Source = " + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
            con.Open();
            Titulo.Text = listadeprovas.Text;
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT codigo,texto,tempo FROM prova WHERE titulo='"+Titulo.Text+"';", con);
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
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
                OleDbCommand cmd = new OleDbCommand("INSERT INTO provasfinalizadas(codigo_aluno,codigo_prova,texto) VALUES (" + codigo + "," + codigo_prova + ",'" + Texto.Text + "');");
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
