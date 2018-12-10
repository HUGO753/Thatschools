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
    /// Interaction logic for Cadastrar.xaml
    /// </summary>
    public partial class Cadastrar : Window
    {
        bool foi = false, proffoi = false, matfoi = false; int codigo;
        public Cadastrar(int cod)
        {
            InitializeComponent();
            codigo = cod;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foi = true;
        }
        void LimparUF()
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox7.Items.Clear();
        }
        void EncherUF(ComboBox comboa)
        {
            comboa.Items.Add("AC");
            comboa.Items.Add("AL");
            comboa.Items.Add("AP");
            comboa.Items.Add("AM");
            comboa.Items.Add("BA");
            comboa.Items.Add("CE");
            comboa.Items.Add("DF");
            comboa.Items.Add("ES");
            comboa.Items.Add("GO");
            comboa.Items.Add("MA");
            comboa.Items.Add("MT");
            comboa.Items.Add("MS");
            comboa.Items.Add("MG");
            comboa.Items.Add("PA");
            comboa.Items.Add("PB");
            comboa.Items.Add("PR");
            comboa.Items.Add("PE");
            comboa.Items.Add("PI");
            comboa.Items.Add("RJ");
            comboa.Items.Add("RN");
            comboa.Items.Add("RS");
            comboa.Items.Add("RO");
            comboa.Items.Add("RR");
            comboa.Items.Add("SC");
            comboa.Items.Add("SP");
            comboa.Items.Add("SE");
            comboa.Items.Add("TO");
        }
        void Enther_nome(string cpf)
        {
            System.Data.DataSet tb = new System.Data.DataSet();
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=tschoolbd"); // Conecta ao banco de dados
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT nome FROM ts_aluno WHERE cpf = '"+cpf+"'", con);
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
            int a = 0;
            System.Data.DataSet tb = new System.Data.DataSet();
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=tschoolbd"); // Conecta ao banco de dados
            con.Open();
            
            
            if (ab == 1)
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT m.titulo FROM ts_materia m INNER JOIN ts_curso c ON c.codigo=m.cod_curso WHERE c.titulo = '"+comboBox10.Text.Split('-')[0]+ "' AND c.periodo='"+comboBox10.Text.Split('-')[1]+"';", con);
                da.Fill(tb, "0");
                comboBox5.Items.Clear();
                while (tb.Tables["0"].Rows.Count > a)
                {
                    comboBox5.Items.Add(tb.Tables["0"].Rows[a]["titulo"].ToString());
                    a++;
                }
            }
            else
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT m.titulo, m.codigo FROM ts_materia m INNER JOIN ts_curso c ON c.codigo=m.cod_curso WHERE "+captura("SELECT cod_curso FROM ts_aluno WHERE CPF='"+ textBox1.Text+ "';")+"=c.codigo", con);
                da.Fill(tb, "0");
                comboBox13.Items.Clear();
                while (tb.Tables["0"].Rows.Count > a)
                {
                    comboBox13.Items.Add(tb.Tables["0"].Rows[a]["codigo"].ToString()+"-"+tb.Tables["0"].Rows[a]["titulo"].ToString());
                    a++;
                }
            }
            con.Close();
        }
        void Encher_professor()
        {
            System.Data.DataSet tb = new System.Data.DataSet();
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=tschoolbd"); // Conecta ao banco de dados
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT p.nome, p.codigo FROM ts_prof_mate pm INNER JOIN ts_professor p ON p.codigo=pm.cod_prof WHERE pm.cod_materia="+Convert.ToInt16(comboBox13.Text.Split('-')[0]), con);
            da.Fill(tb, "0");
            int a = 0;
            comboBox12.Items.Clear();
            while (tb.Tables["0"].Rows.Count > a)
            {
                comboBox12.Items.Add(tb.Tables["0"].Rows[a]["codigo"].ToString() + "-" + tb.Tables["0"].Rows[a]["nome"].ToString());
                a++;
            }
            proffoi = false;
        }
        void Encher_curso(int b)
        {
            int a = 0;
            System.Data.DataSet tb = new System.Data.DataSet();
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=tschoolbd"); // Conecta ao banco de dados
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT codigo, titulo, periodo FROM ts_curso", con);
            da.Fill(tb, "0");
            
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
        void BancodeDados(string comando)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;user id=root;password=;database=tschoolbd"); // Conecta ao banco de dados
            MySqlCommand cmd = new MySqlCommand(comando);
            cmd.Connection = con;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (foi == true)
            {
                LimparUF();
                switch (comboBox.Text)
                {
                    case "Aluno":
                        tirar();
                        Cad_aluno.Visibility = Visibility.Visible;
                        Encher_curso(3);
                        EncherUF(comboBox2);
                        break;
                    case "Professor":
                        tirar();
                        Cad_Professor.Visibility = Visibility.Visible;
                        EncherUF(comboBox3);
                        Encher_curso(2);
                        break;
                    case "Secretario":
                        tirar();
                        Cad_secretaria.Visibility = Visibility.Visible;
                        EncherUF(comboBox7);
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
                        comboBox12.IsEnabled = false;
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
                BancodeDados("INSERT INTO ts_aluno(nome,sexo,endereco,cidade,rg,cpf,uf,cod_curso) VALUES('" + textBox.Text + "','" + comboBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox17.Text + "','" + textBox16.Text + "','" + comboBox2.Text + "'," + Convert.ToInt16(captura("SELECT codigo FROM ts_curso WHERE titulo='" + comboBox11.Text.Split('-')[0] + "' AND periodo='" + comboBox11.Text.Split('-')[1]+"'"))+")");
                BancodeDados("INSERT INTO ts_usuarios(usuario,senha,tipo,cod_tipo) VALUES('" + textBox17.Text + "','" + textBox16.Text + "',1," + Convert.ToInt16(captura("SELECT MAX(codigo) FROM ts_aluno")) + ")");
                textBox.Text = "";comboBox1.Text = "";textBox2.Text = "";textBox3.Text = "";textBox17.Text = "";textBox16.Text = "";comboBox2.Text = "";comboBox11.Text = "";
                MessageBox.Show("Cadastro realizado com sucesso!");
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
            else if (textBox9.Text == "") MessageBox.Show("Insira o Numero de Registro!");
            else if (MessageBox.Show("Deseja Cadastrar?", "Cadastro", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                BancodeDados("INSERT INTO ts_professor(nome,sexo,endereco,cidade,rg,cpf,uf,N_Registro) VALUES('" + textBox4.Text + "','" + comboBox4.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','" + textBox5.Text + "','" + comboBox3.Text + "',"+Convert.ToInt32(textBox9.Text)+ ")");
                BancodeDados("INSERT INTO ts_usuarios(usuario,senha,tipo,cod_tipo) VALUES('" + textBox20.Text + "','" + textBox5.Text + "',2," + Convert.ToInt16(captura("SELECT MAX(codigo) FROM professor")) + ")");
                BancodeDados("INSERT INTO ts_prof_mate(cod_materia,cod_prof) VALUES(" + Convert.ToInt16(captura("SELECT codigo FROM curso WHERE titulo='" + comboBox10.Text.Split('-')[0] + "' AND periodo='" + comboBox10.Text.Split('-')[1] + "'")) + "," + Convert.ToInt16(captura("SELECT MAX(codigo) FROM ts_professor")) + ")");
                textBox4.Text = ""; textBox6.Text = ""; textBox7.Text = ""; textBox8.Text = ""; textBox9.Text = ""; comboBox3.Text = ""; comboBox4.Text = ""; comboBox5.Text = ""; textBox5.Text = ""; textBox20.Text = ""; comboBox10.Text = ""; comboBox5.IsEnabled = false; proffoi = false;
                MessageBox.Show("Cadastro realizado com sucesso!");
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
                BancodeDados("INSERT INTO ts_secretario(nome,sexo,endereco,cidade,rg,cpf,uf) VALUES('"+ textBox10.Text+ "','" + comboBox6.Text + "','" + textBox12.Text + "','" + textBox13.Text + "','" + textBox14.Text + "','" + textBox21.Text + "','" + comboBox7.Text + "')");
                BancodeDados("INSERT INTO ts_usuarios(usuario,senha,tipo,cod_tipo) VALUES('"+textBox11.Text+"','"+textBox22.Text+ "',3,"+Convert.ToInt16(captura("SELECT MAX(codigo) FROM ts_secretario")) +")");
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
                BancodeDados("INSERT INTO ts_curso(titulo, periodo, qtd_aluno_s) VALUES('" + textBox24.Text + "','" + comboBox8.Text + "'," + Convert.ToInt32(textBox25.Text) +")");
                comboBox8.Text = "";textBox24.Text = "";textBox25.Text = "";
                MessageBox.Show("Curso cadastrado com sucesso!");
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (textBox23.Text == "") MessageBox.Show("Digita o nome!");
            else if (textBox26.Text == "") MessageBox.Show("Digita a carga horaria!");
            else if (comboBox9.Text == "") MessageBox.Show("Selecione o curso!");
            else if(MessageBox.Show("Deseja Cadastrar?", "Cadastro", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                BancodeDados("INSERT INTO ts_materia(titulo, cod_curso, carga_horaria) VALUES('" + textBox23.Text + "','" + Convert.ToInt16(captura("SELECT codigo FROM ts_curso WHERE titulo='"+ comboBox9.Text.Split('-')[0] + "' AND periodo='"+ comboBox9.Text.Split('-')[1] + "'")) + "'," + Convert.ToInt32(textBox26.Text) + ")");
                textBox23.Text = "";textBox26.Text = "";comboBox9.Text = "";
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
            Enther_nome(textBox1.Text);

        }


        private void comboBox13_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            matfoi = true;
        }

        private void comboBox13_DropDownClosed(object sender, EventArgs e)
        {
            if (matfoi == true)
            {
                comboBox12.IsEnabled = true;
                Encher_professor();
            }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SMenu s = new SMenu(codigo);
            s.Show();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "") MessageBox.Show("Digite o CPF!");
            else if (textBox15.Text == "") MessageBox.Show("Aluno não selecionado!");
            else if (comboBox13.Text == "") MessageBox.Show("Selecione a matéria!");
            else if (comboBox12.Text == "") MessageBox.Show("Selecione o professor!");
            else
            {
                BancodeDados("INSERT INTO ts_matricula(cod_aluno, codigo_prof_mate, data_matricula, media) VALUES(" + Convert.ToInt16(captura("SELECT codigo FROM ts_aluno WHERE cpf='" + textBox1.Text + "'")) + "," + Convert.ToInt32(captura("SELECT codigo FROM ts_prof_mate WHERE cod_materia=" + Convert.ToInt32(comboBox13.Text.Split('-')[0]) + " AND cod_prof=" + Convert.ToInt32(comboBox12.Text.Split('-')[0]))) + ",'" + DateTime.Now + "'," + 0 + ")");
                textBox1.Text = ""; textBox15.Text = ""; comboBox13.Text = ""; comboBox13.IsEnabled = false; comboBox12.IsEnabled = false; comboBox12.Text="";
                MessageBox.Show("Matricula cadastrada com sucesso!");
            }
        }

        private void comboBox10_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            proffoi = true;
        }
    }
}
