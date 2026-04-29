using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace ProjetoFinal
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void label6_Click(object sender, EventArgs e)
        {
            label6.ForeColor = Color.FromArgb(240, 98, 146);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
        
            AbrirLink("https://www.tiktok.com/_.laura.macedo");
        
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string email = txtUsuario.Text.Trim();
            string senha = txtSenha.Text.Trim();

            if (email == "" || senha == "")
            {
                MessageBox.Show("Preencha o e-mail e a senha.");
                return;
            }

            try
            {
                using (SqlConnection conn = Conexao.Conectar())
                {
                    conn.Open();

                    string sql = @"SELECT IdUsuario, Nome, TipoUsuario 
                           FROM Usuarios 
                           WHERE Email = @Email AND Senha = @Senha AND Ativo = 1";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Senha", senha);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string nome = reader["Nome"].ToString();
                            string tipoUsuario = reader["TipoUsuario"].ToString();

                            MessageBox.Show("Bem-vinda, " + nome + "!");

                            if (tipoUsuario == "Admin")
                            {
                                FormAdminPrinc formadmprinc = new FormAdminPrinc();
                                formadmprinc.Show();
                                this.Hide();
                            }
                            else if (tipoUsuario == "Cliente")
                            {
                                FormClientePrinc formClienteprinc = new FormClientePrinc();
                                formClienteprinc.Show();
                                this.Hide();
                            }
                            else if (tipoUsuario == "Profissional")
                            {
                                Formprofissionalprinc formprofPrinc = new Formprofissionalprinc();
                                formprofPrinc.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("E-mail ou senha incorretos.");
                        }

                        if (chkLembrar.Checked)
                        {
                            Properties.Settings.Default.EmailSalvo = txtUsuario.Text;
                            Properties.Settings.Default.SenhaSalva = txtSenha.Text;
                            Properties.Settings.Default.Lembrar = true;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            Properties.Settings.Default.EmailSalvo = "";
                            Properties.Settings.Default.SenhaSalva = "";
                            Properties.Settings.Default.Lembrar = false;
                            Properties.Settings.Default.Save();
                        }
                        MessageBox.Show("Login feito com sucesso!");
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro na conexão: " + erro.Message);
            }
        }

        
            bool senhaVisivel = false;

            private void FormLogin_Load(object sender, EventArgs e)
            {
                txtSenha.PasswordChar = '●';

                if (Properties.Settings.Default.Lembrar == true)
                {
                    txtUsuario.Text = Properties.Settings.Default.EmailSalvo;
                    txtSenha.Text = Properties.Settings.Default.SenhaSalva;
                    chkLembrar.Checked = true;
                }
            }

            private void txtSenha_IconRightClick(object sender, EventArgs e)
            {
                if (senhaVisivel == false)
                {
                    txtSenha.PasswordChar = '\0';
                    senhaVisivel = true;
                }
                else
                {
                    txtSenha.PasswordChar = '●';
                    senhaVisivel = false;
                }
            }

        private void btnCriarConta_Click(object sender, EventArgs e)
        {
            FormCriarConta criarConta = new FormCriarConta();
            criarConta.Show();
            this.Hide();
        }

        private void AbrirLink(string link)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = link,
                    UseShellExecute = true
                });
            }
            catch
            {
                MessageBox.Show("Não foi possível abrir o link.");
            }
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            AbrirLink("https://www.instagram.com/_.laura.macedo._");
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {
            AbrirLink("https://wa.me/351913885275?text=Olá,%20gostaria%20de%20marcar%20um%20serviço.");
        }
        
    }
}
