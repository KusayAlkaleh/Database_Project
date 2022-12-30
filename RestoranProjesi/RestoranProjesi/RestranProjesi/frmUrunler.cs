using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestranProjesi
{
    public partial class frmUrunler : Form
    {
        public frmUrunler()
        {
            InitializeComponent();
        }

        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;
        private int rowIndex;
        private void frmUrunler_Load(object sender, EventArgs e)
        {
            //con = new NpgsqlConnection();
            Select();
        }

        private void Select()
        {
            try
            {
                conn.Open();
                sql = @"SELECT * FROM st_select()";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();
                dgvData.DataSource = null;
                dgvData.DataSource = dt;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("ERORR, " + ex.Message);
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                rowIndex = e.RowIndex;
                txtAdi.Text = dgvData.Rows[e.RowIndex].Cells["adi"].Value.ToString();
                txtTuru.Text = dgvData.Rows[e.RowIndex].Cells["turu"].Value.ToString();
                txtFiyati.Text = dgvData.Rows[e.RowIndex].Cells["fiyati"].Value.ToString(); 
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            rowIndex = -1;
            txtAdi.Enabled = txtFiyati.Enabled = txtTuru.Enabled = true;
            txtAdi.Text = txtTuru.Text = txtFiyati.Text = null;
            txtAdi.Select();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(rowIndex < 0)
            {
                MessageBox.Show("Lütfen istediğiniz ürünü güncellemek seçiniz!!");
                return;
            }
            txtAdi.Enabled = txtFiyati.Enabled = txtTuru.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (rowIndex < 0)
            {
                MessageBox.Show("Lütfen istediğiniz ürünü silmek seçiniz!!");
                return;
            }

            try
            {
                conn.Open();
                sql = @"SELECT * FROM st_delete(:_id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", int.Parse(dgvData.Rows[rowIndex].Cells["id"].Value.ToString()));
                if((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Silme işlemi başarlı gerçekleşdi");
                    rowIndex = -1;
                    Select();
                }
                conn.Close();
            }
            catch (Exception ex)
            {

                conn.Close();
                MessageBox.Show("Deleted failed ERORR, " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            if(rowIndex < 0) // insert
            {
                try
                {
                    conn.Open();
                    sql = @"SELECT * FROM st_insert(:_adi, :_turu, :_fiyati)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_adi", txtAdi.Text);
                    cmd.Parameters.AddWithValue("_turu", txtTuru.Text);
                    cmd.Parameters.AddWithValue("_fiyati", txtFiyati.Text);
                    result = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if(result == 1)
                    {
                        MessageBox.Show("Insert new urun successfully");

                        Select();
                    }
                    else
                    {
                        MessageBox.Show("Insert fail");
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Inserted failed ERORR, " + ex.Message);
                    throw;
                }
            }
            else // update
            {
                try
                {
                    conn.Open();
                    sql = @"SELECT * FROM  st_update(:_id ,:_adi ,:_turu ,:_fiyati)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_id", int.Parse(dgvData.Rows[rowIndex].Cells["urunId"].Value.ToString()));
                    cmd.Parameters.AddWithValue("_adi", txtAdi.Text);
                    cmd.Parameters.AddWithValue("_turu", txtTuru.Text);
                    cmd.Parameters.AddWithValue("_fiyati", txtFiyati.Text);
                    result = (int)cmd.ExecuteScalar();

                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Update successfully");

                        Select();
                    }
                    else
                    {
                        MessageBox.Show("Update fail");
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Update failed ERORR, " + ex.Message);
                    
                }
                
            }
            result = 0;
            txtAdi.Text = txtTuru.Text = txtFiyati.Text = null;
            txtAdi.Enabled = txtFiyati.Enabled = txtTuru.Enabled = false;
        }
    }
}
