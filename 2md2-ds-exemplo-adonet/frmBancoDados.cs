﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace _2md2_ds_exemplo_adonet
{
    public partial class frmBancoDados : Form
    {
        private MySqlConnection conn = new MySqlConnection();
        private MySqlCommand cmd = new MySqlCommand();
        private MySqlDataReader dataReader;
        public frmBancoDados()
        {
            InitializeComponent();
        }

        private void FrmBancoDados_Load(object sender, EventArgs e)
        {
            try
            {
                conn.ConnectionString = "Server=localhost;Database=bdcapacitacao;User=root;Pwd=root";
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ==> {ex.Message}", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            try
            {
                // SQL Injection nem existe pro PDF né...
                string query = "Select * from tblagenda WHERE agdid = @cod";
                cmd.Connection = conn;
                cmd.CommandText = query;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cod", tbCodigo.Text);
                dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    MessageBox.Show("Código já existe", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbCodigo.Focus();
                }
                else
                {
                    if (!dataReader.IsClosed) dataReader.Close();
                    string insertQuery = "INSERT INTO tblagenda (agdid, agdnome, agdemail, agdtelefone, agdcpf) VALUES (@cod, @nome, @email, @tel, @cpf)";
                    cmd.Connection = conn;
                    cmd.CommandText = insertQuery;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@cod", tbCodigo.Text);
                    cmd.Parameters.AddWithValue("@nome", tbNome.Text);
                    cmd.Parameters.AddWithValue("@email", tbEmail.Text);
                    cmd.Parameters.AddWithValue("@tel", tbTelefone.Text);
                    cmd.Parameters.AddWithValue("@cpf", tbCPF.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro inserido com sucesso", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!dataReader.IsClosed) dataReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ==> {ex.Message}", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                string checkExistsQuery = "Select * From tblagenda Where agdid=@id";
                cmd.Connection = conn;
                cmd.CommandText = checkExistsQuery;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", tbCodigo.Text);
                dataReader = cmd.ExecuteReader();
                if (!dataReader.HasRows)
                {
                    MessageBox.Show("Código não existe", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else
                {
                    if (!dataReader.IsClosed) dataReader.Close();
                    string updateQuery = @"
Update tblagenda Set 
agdnome = @nome, 
agdemail = @email, 
agdtelefone = @tel, 
agdcpf = @cpf
Where agdid = @id
";
                    cmd.Connection = conn;
                    cmd.CommandText = updateQuery;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@nome", tbNome.Text);
                    cmd.Parameters.AddWithValue("@email", tbEmail.Text);
                    cmd.Parameters.AddWithValue("@tel", tbTelefone.Text);
                    cmd.Parameters.AddWithValue("@cpf", tbCPF.Text);
                    cmd.Parameters.AddWithValue("@id", tbCodigo.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Registro alterado com sucesso", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLimpar_Click(sender, e);
                }
                if (!dataReader.IsClosed) dataReader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ==> {ex.Message}", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            tbCodigo.Text = "";
            tbNome.Text = "";
            tbEmail.Text = "";
            tbTelefone.Text = "";
            tbCPF.Text = "";
            tbCodigo.Focus();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                string checkExistsQuery = "Select * From tblagenda Where agdid=@id";
                cmd.Connection = conn;
                cmd.CommandText = checkExistsQuery;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", tbCodigo.Text);
                dataReader = cmd.ExecuteReader();
                if (!dataReader.HasRows)
                {
                    MessageBox.Show("Código não existe", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbCodigo.Focus();
                } else
                {
                    if (!dataReader.IsClosed) dataReader.Close();
                    if (MessageBox.Show("Deseja Excluir?", "ADO.NET", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string delete = "Delete from tblagenda Where agdid = @id";
                        cmd.Connection = conn;
                        cmd.CommandText= delete;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@id", tbCodigo.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Registro removido com sucesso.", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Information );
                        btnLimpar_Click(sender, e);
                    }
                }
                if (!dataReader.IsClosed) dataReader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ==> {ex.Message}", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                string checkExistsQuery = "Select * From tblagenda Where agdid=@id";
                cmd.Connection = conn;
                cmd.CommandText = checkExistsQuery;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", tbCodigo.Text);
                dataReader = cmd.ExecuteReader();
                if (!dataReader.HasRows)
                {
                    MessageBox.Show("Código não existe", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbCodigo.Focus();
                } else
                {
                    dataReader.Read();
                    tbNome.Text = dataReader["agdnome"].ToString();
                    tbEmail.Text = dataReader["agdemail"].ToString();
                    tbTelefone.Text = dataReader["agdtelefone"].ToString();
                    tbCPF.Text = dataReader["agdcpf"].ToString();
                }
                if (!dataReader.IsClosed) dataReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ==> {ex.Message}", "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnConsultarListaDados_Click(object sender, EventArgs e)
        {
            frmConsultarLista consulta = new frmConsultarLista();
            consulta.ShowDialog();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            conn.Close();
            Application.Exit();
        }
    }
}
