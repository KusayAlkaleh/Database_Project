using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestranProjesi.Model
{
    public partial class frmCategoryAdd : SimpleAdd
    {
        public frmCategoryAdd()
        {
            InitializeComponent();
        }

        public int id = 0;
        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "";

            if(id == 0) // Insert
            {
                qry = "INSERT INTO katagori VALUES(@Name)";
            }
            else // update
            {
                qry = "UPDATE katagori SET katName = @Name WHERE karID = @id";
            }

            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);
        } 
    }
}
