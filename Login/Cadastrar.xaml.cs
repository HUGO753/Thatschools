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
    /// Interaction logic for Cadastrar.xaml
    /// </summary>
    public partial class Cadastrar : Window
    {
        bool foi = false, proffoi = false;
        public Cadastrar()
        {
            InitializeComponent();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foi = true;
        }
        void Enther_nome(int cpf)
        {
            System.Data.DataSet tb = new System.Data.DataSet();
            OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT nome FROM aluno WHERE cpf = "+cpf+"", con);
            da.Fill(tb, "0");
            if (tb.Tables["0"].Rows.Count > 0)
            {
                textBox15.Text = tb.Tables["0"].Rows[0]["nome"].ToString();
                Encher_materia(2);
                comboBox13.IsEnabled = true;
            }
            else MessageBox.Show("Erro! CPF não encontrado!");
        }
        void Encher_materia(int ab)
        {
            System.Data.DataSet tb = new System.Data.DataSet();
            OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT m.titulo FROM materia m INNER JOIN curso c ON c.codigo=m.cod_curso", con);
            da.Fill(tb, "0");
            int a = 0;
            if (ab == 0)
            {
                comboBox5.Items.Clear();
                while (tb.Tables["0"].Rows.Count > a)
                {
                    comboBox5.Items.Add(tb.Tables["0"].Rows[a]["titulo"].ToString());
                    a++;
                }
            }
            else
            {
                comboBox13.Items.Clear();
                while (tb.Tables["0"].Rows.Count > a)
                {
                    comboBox13.Items.Add(tb.Tables["0"].Rows[a]["titulo"].ToString());
                    a++;
                }
            }
            con.Close();
        }
        void Encher_curso(int b)
        {
            
            System.Data.DataSet tb = new System.Data.DataSet();
            OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT titulo, periodo FROM curso", con);
            da.Fill(tb, "0");
            int a = 0;
            if (b == 1)
            {
                comboBox9.Items.Clear();
                while (tb.Tables["0"].Rows.Count > a)
                {
                    comboBox9.Items.Add(tb.Tables["0"].Rows[a]["titulo"].ToString() + "-" + tb.Tables["0"].Rows[a]["periodo"].ToString());
                    a++;
                }
            }
            else if (b == 2)
            {
                comboBox10.Items.Clear();
                while (tb.Tables["0"].Rows.Count > a)
                {
                    comboBox10.Items.Add(tb.Tables["0"].Rows[a]["titulo"].ToString() + "-" + tb.Tables["0"].Rows[a]["periodo"].ToString());
                    a++;
                }
            }
            else if (b == 3)
            {
                comboBox11.Items.Clear();
                while (tb.Tables["0"].Rows.Count > a)
                {
                    comboBox11.Items.Add(tb.Tables["0"].Rows[a]["titulo"].ToString() + "-" + tb.Tables["0"].Rows[a]["periodo"].ToString());
                    a++;
                }
            }
            con.Close();
        }
        void tirar()
        {
            Cad_aluno.Visibility = Visibility.Hidden;
            Cad_Professor.Visibility = Visibility.Hidden;
            Cad_secretaria.Visibility = Visibility.Hidden;
            Cad_materia.Visibility = Visibility.Hidden;
            Cad_curso.Visibility = Visibility.Hidden;
            Cad_matricula.Visibility = Visibility.Hidden;
        }
        int captura(string query)
        {
            int a;
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
            OleDbCommand cmd = new OleDbCommand();
            
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = query;
            a = Convert.ToInt16(cmd.ExecuteScalar().ToString());
            con.Close();
            return a;
        }
        void BancodeDados(string comando)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\bd.accdb"); // Conecta ao banco de dados
            OleDbCommand cmd = new OleDbCommand(comando);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (foi == true)
            {
                switch (comboBox.Text)
                {
                    case "Aluno":
                        tirar();
                        Cad_aluno.Visibility = Visibility.Visible;
                        Encher_curso(3);
                        break;
                    case "Professor":
                        tirar();
                        Cad_Professor.Visibility = Visibility.Visible;
                        Encher_curso(2);
                        break;
                    case "Secretario":
                        tirar();
                        Cad_secretaria.Visibility = Visibility.Visible;
                        break;
                    case "Curso":
                        tirar();
                        Cad_curso.Visibility = Visibility.Visible;
                        break;
                    case "Materia":
                        tirar();
                        Cad_materia.Visibility = Visibility.Visible;
                        Encher_curso(1);
                        break;
                    case "Matricula":
                        tirar();
                        textBox15.IsEnabled = false;
                        comboBox13.IsEnabled = false;
                        Cad_matricula.Visibility = Visibility.Visible;
                        break;
                }
                foi = false;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text == "") MessageBox.Show("Insira o Nome!");
            else if (comboBox1.Text == "") MessageBox.Show("Insira o Genero!");
            else if (textBox2.Text == "") MessageBox.Show("Insira o Endereço!");
            else if (textBox3.Text == "") MessageBox.Show("Insira a Cidade!");
            else if (textBox17.Text == "") MessageBox.Show("Insira o RG!");
            else if (textBox16.Text == "") MessageBox.Show("Insira o CPF!");
            else if (comboBox2.Text == "") MessageBox.Show("Insira o UF!");
            else if (comboBox11.Text == "") MessageBox.Show("Insira o Curso!");
            else if(MessageBox.Show("Deseja Cadastrar?", "Cadastro", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                BancodeDados("INSERT INTO aluno(nome,sexo,endereco,cidade,rg,cpf,uf,cod_curso) VALUES('" + textBox.Text + "','" + comboBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox17.Text + "','" + textBox16.Text + "','" + comboBox2.Text + "','" + captura("SELECT codigo FROM curso WHERE titulo='" + comboBox11.Text.Split('-')[0] + "' AND periodo='" + comboBox11.Text.Split('-')[1] + "'") + "'')");
                BancodeDados("INSERT INTO usuarios(usuario,senha,tipo,cod_tipo) VALUES('" + textBox20.Text + "','" + textBox5.Text + "',2," + captura("SELECT MAX(codigo) FROM professor") + ")");
                textBox.Text = "";
                comboBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox17.Text = "";
                textBox16.Text = "";
                comboBox2.Text = "";
                comboBox11.Text = "";
            }
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox4.Text == "") MessageBox.Show("Insira o Nome!");
            else if (comboBox4.Text == "") MessageBox.Show("Insira o Genero!");
            else if (textBox6.Text == "") MessageBox.Show("Insira o Endereço!");
            else if (textBox7.Text == "") MessageBox.Show("Insira a Cidade!");
            else if (textBox8.Text == "") MessageBox.Show("Insira o RG!");
            else if (textBox5.Text == "") MessageBox.Show("insira o CPF!");
            else if (textBox20.Text == "") MessageBox.Show("Insira o Usuario!");
            else if (comboBox3.Text == "") MessageBox.Show("Insira o UF!");
            else if (comboBox10.Text == "") MessageBox.Show("Insira o Curso!");
            else if (comboBox5.Text == "") MessageBox.Show("Insira a Materia!");
            else if (MessageBox.Show("Deseja Cadastrar?", "Cadastro", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                BancodeDados("INSERT INTO professor(nome,sexo,endereco,cidade,rg,cpf,uf,materia) VALUES('" + textBox4.Text + "','" + comboBox4.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox5.Text + "','" + comboBox3.Text + "','"+ comboBox5.Text + "')");
                BancodeDados("INSERT INTO usuarios(usuario,senha,tipo,cod_tipo) VALUES('" + textBox20.Text + "','" + textBox5.Text + "',2," + captura("SELECT MAX(codigo) FROM professor") + ")");
                textBox4.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                comboBox5.Text = "";
                textBox5.Text = "";
                textBox20.Text = "";
                comboBox10.Text = "";
                comboBox5.IsEnabled = false;
                proffoi = false;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (textBox10.Text == "") MessageBox.Show("Insira o nome!");
            else if(textBox12.Text == "") MessageBox.Show("Insira o endereço!");
            else if(textBox13.Text == "") MessageBox.Show("Insira a cidade!");
            else if(textBox14.Text == "") MessageBox.Show("Insira o RG!");
            else if(textBox21.Text == "") MessageBox.Show("Insira o CPF!");
            else if(textBox11.Text == "") MessageBox.Show("Insira o Login!");
            else if(textBox22.Text == "") MessageBox.Show("Insira a Senha!");
            else if(comboBox6.Text == "") MessageBox.Show("Insira o Genero!");
            else if (comboBox7.Text == "") MessageBox.Show("Insira o UF!");
            else if(MessageBox.Show("Deseja Cadastrar?", "Cadastro", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                BancodeDados("INSERT INTO secretario(nome,sexo,endereco,cidade,rg,cpf,uf) VALUES('"+ textBox10.Text+ "','" + comboBox6.Text + "','" + textBox12.Text + "','" + textBox13.Text + "','" + textBox14.Text + "','" + textBox21.Text + "','" + comboBox7.Text + "')");
                BancodeDados("INSERT INTO usuarios(usuario,senha,tipo,cod_tipo) VALUES('"+textBox11.Text+"','"+textBox22.Text+ "',3,"+captura("SELECT MAX(codigo) FROM secretario") +")");
                textBox10.Text = ""; textBox12.Text = ""; textBox13.Text = ""; textBox14.Text = ""; textBox21.Text = ""; textBox11.Text = ""; textBox22.Text = ""; comboBox6.Text = ""; comboBox7.Text = "";
                MessageBox.Show("Cadastro realizado com sucesso!");
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {

            if (textBox24.Text == "" || textBox24.Text == " ") MessageBox.Show("Insira o nome do curso!");
            else if (comboBox8.Text == "") MessageBox.Show("Insira o periodo do curso!");
            else if(textBox25.Text == "" || textBox25.Text == "0") MessageBox.Show("Insira a quantidade valida de alunos no curso!");
            else if (MessageBox.Show("Deseja Cadastrar?", "Cadastro", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                BancodeDados("INSERT INTO curso(titulo, periodo, qtd_aluno_s) VALUES('" + textBox24.Text + "','" + comboBox8.Text + "'," + Convert.ToInt32(textBox25.Text) +")");
                comboBox8.Text = "";
                textBox24.Text = "";
                textBox25.Text = "";
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (textBox23.Text == "") MessageBox.Show("Digita o nome!");
            else if (textBox26.Text == "") MessageBox.Show("Digita a carga horaria!");
            else if (comboBox9.Text == "") MessageBox.Show("Selecione o curso!");
            else if(MessageBox.Show("Deseja Cadastrar?", "Cadastro", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                BancodeDados("INSERT INTO materia(titulo, cod_curso, carga_horaria) VALUES('" + textBox23.Text + "','" + captura("SELECT codigo FROM curso WHERE titulo='"+ comboBox9.Text.Split('-')[0] + "' AND periodo='"+ comboBox9.Text.Split('-')[1] + "'") + "'," + Convert.ToInt32(textBox26.Text) + ")");
                textBox23.Text = "";
                textBox26.Text = "";
                comboBox9.Text = "";
                MessageBox.Show("Materia cadastrada com sucesso!");
            }
        }

        private void comboBox10_DropDownClosed(object sender, EventArgs e)
        {
            if (proffoi == true)
            {
                comboBox5.IsEnabled = true;
                Encher_materia(1);
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            Enther_nome(Convert.ToInt16(textBox1.Text));

        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "") MessageBox.Show("Digite o CPF!");
            else if (textBox15.Text == "") MessageBox.Show("Aluno não selecionado!");
            else if (comboBox13.Text == "") MessageBox.Show("Selecione a matéria!");
            else
            {
                //////{!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!ARRUMAR O BANCO DE DADOS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!}//////
                BancodeDados("INSERT INTO matricula(cod_aluno, codigo_prof_mate, data_matricula, media) VALUES('" + captura("SELECT codigo FROM aluno WHERE cpf=" + Convert.ToInt16(textBox1.Text))+"'," + Convert.ToInt32(textBox26.Text) + "','" +  + ")");
                textBox1.Text = "";
                textBox15.Text = "";
                comboBox13.Text = "";
                MessageBox.Show("Materia cadastrada com sucesso!");
            }
        }

        private void comboBox10_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            proffoi = true;
        }
    }
}
