using RestranProjesi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestranProjesi.View
{
    public partial class frmCategoryView : SimpleView
    {
        public frmCategoryView()
        {
            InitializeComponent();
        }

        public void getData()
        {
            string qry = "SELECT * FROM katagori WHERE catName like '%"+ txtSearch.Text +"%' ";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);

            MainClass.LoadData(qry, guna2DataGridView1, lb);
        }

        private void frmCategoryView_Load(object sender, EventArgs e)
        {
            getData();
        }

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            frmCategoryAdd frm = new frmCategoryAdd();
            frm.ShowDialog();
            getData();
        }

        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //create table first 
            getData();
        }
    }
}
