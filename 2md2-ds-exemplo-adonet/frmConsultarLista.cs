using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2md2_ds_exemplo_adonet
{
    public partial class frmConsultarLista : Form
    {
        private MySqlConnection conn = new MySqlConnection();
        private MySqlCommand cmd = new MySqlCommand();
        private MySqlDataReader reader;
        public frmConsultarLista()
        {
            InitializeComponent();
        }

        private void frmConsultarLista_Load(object sender, EventArgs e)
        {
            try
            {
                conn.ConnectionString = "Server=localhost;Database=bdcapacitacao;User=root;Pwd=root";
                conn.Open();
            } catch (Exception ex) 
            {
                MessageBox.Show("Erro ==> " + ex.Message, "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                string sql;
                sql = "Select * from tblagenda Order By agdnome";
                cmd.Connection = conn;
                cmd.CommandText = sql;
                reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("Tabela vazia", "Consultar Lista", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(
                            reader["agdid"].ToString(),
                            reader["agdnome"].ToString(),
                            reader["agdemail"].ToString(),
                            reader["agdtelefone"].ToString(),
                            reader["agdcpf"].ToString()
                            );
                    }
                }
                reader.Close();
            } catch (Exception ex) 
            {
                MessageBox.Show("Erro ==> " + ex.Message, "ADO.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            conn.Close();
            Close();
        }
    }
}
