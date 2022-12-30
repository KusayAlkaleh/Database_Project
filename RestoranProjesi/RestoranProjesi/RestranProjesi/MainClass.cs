using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestranProjesi
{
    internal class MainClass
    {
        public static readonly string con_string = "server=localHost; port=5432; Database=testDB; user ID=postgres; password=Lio3038$$; ";
        public static NpgsqlConnection con = new NpgsqlConnection(con_string);


        public static bool isValidUcer(string user, string pass)
        {
            bool isValid = false;

            string qry = @"SELECT * FROM yonteci WHERE userName = '" + user + "' and userPass = '" + pass + "'";
            NpgsqlCommand cmd = new NpgsqlCommand(qry, con);

            DataTable dt = new DataTable();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            da.Fill(dt);

            if(dt.Rows.Count > 0)
            {
                isValid = true;
                USER = dt.Rows[0]["userLast"].ToString();
            }

            return isValid;
        }

        // create property for user

        public static string user;

        public static string USER
        {
            get { return user; }
            private set { user = value; }
        }

        // methord for crud operation

        public static int SQL(string qry, Hashtable ht)
        {
            int res = 0;

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;

                //foreach (DictionaryEntry item in ht)
                //{
                //    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                //}
                foreach (DictionaryEntry item in ht)
                {

                }

                if(con.State == ConnectionState.Closed) { con.Open(); }
                res = ExecuteNoQuery();
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                con.Close();
            }
             
            return res;
        }

        // for loading data from database

        public static void LoadData(string qry, DataGridView gv, ListBox lb)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for (int i = 0; i < lb.Items.Count; i++)
                {
                    string colName1 = ((DataGridViewColumn)lb.Items[i]).Name;
                    gv.Columns[colName1].DataPropertyName = dt.Columns[i].ToString();
                }

                gv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                con.Close();
            }
        }

        private static int ExecuteNoQuery()
        {
            throw new NotImplementedException();
        }
    }
}
