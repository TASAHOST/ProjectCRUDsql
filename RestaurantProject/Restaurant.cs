using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantProject
{
    public partial class Restaurant : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=restaurant;Uid=root;Pwd=''");
        MySqlCommand  cmd;
        MySqlDataAdapter dataAdapter;
        DataSet dataSet;
        
        public Restaurant()
        {
            InitializeComponent();
        }

        
        private void Restaurant_Load(object sender, EventArgs e)
        {
            try 
            {
                
                con.Open();

                this.listView1.Columns.Add("ID", 50, HorizontalAlignment.Left);
                this.listView1.Columns.Add("NAME", 250, HorizontalAlignment.Left);
                this.listView1.Columns.Add("TYPE", 150, HorizontalAlignment.Left);
                this.listView1.Columns.Add("PRICE", 100, HorizontalAlignment.Left);
                this.listView1.View = View.Details;
                this.listView1.FullRowSelect = true;
                this.listView1.GridLines = true;

                showData();
            } 
            catch 
            {
                MessageBox.Show("Connection Error");
            }

        }
        private void showData()
        {
            this.listView1.Items.Clear();
            string sql = "select * from restaurants";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            dataAdapter = new MySqlDataAdapter(cmd);
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "restaurant");

            foreach (DataRow dr in dataSet.Tables["restaurant"].Rows)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems.Add("0");
                lvi.SubItems.Add("1");
                lvi.SubItems.Add("2");
                lvi.SubItems.Add("3");

           
                
                
                lvi.SubItems[0].Text = dr[0].ToString();
                lvi.SubItems[1].Text = dr[1].ToString();
                lvi.SubItems[2].Text = dr[2].ToString();
                lvi.SubItems[3].Text = dr[3].ToString();

                this.listView1.Items.Add(lvi);
            }


        }

       
        private void listView1_Click(object sender, EventArgs e)
        {
            if (this.listView1.Items.Count>=0)
            {
                this.textBoxid.Text = this.listView1.SelectedItems[0].SubItems[0].Text;
                this.textBoxname.Text = this.listView1.SelectedItems[0].SubItems[1].Text;
                this.textBoxtype.Text = this.listView1.SelectedItems[0].SubItems[2].Text;
                this.textBoxprice.Text = this.listView1.SelectedItems[0].SubItems[3].Text;
            }
        }
        private void textClear()
        {
            this.textBoxid.Text = null;
            this.textBoxname.Text = null;
            this.textBoxtype.Text = null;
            this.textBoxprice.Text = null;
        }
        private void buttonclear_Click(object sender, EventArgs e)
        {
            textClear();
        }

        private void buttonadd_Click(object sender, EventArgs e)
        {
            try
            {
            string sql = "INSERT INTO restaurants (id,name, type, price ) VALUES (@id,@name, @type, @price );";
            cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", this.textBoxid.Text).ToString();
            cmd.Parameters.AddWithValue("@name", this.textBoxname.Text).ToString();
            cmd.Parameters.AddWithValue("@type", this.textBoxtype.Text).ToString();
            cmd.Parameters.AddWithValue("@price", this.textBoxprice.Text).ToString();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Add Successfully!!");
                textClear();
                showData();
            }
            catch 
            {
                MessageBox.Show("Add Fail! Or Id Already!!");
            }
            
        }

        private void buttonedit_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "update restaurants set name=@name, type=@type, price=@price where id='"+this.textBoxid.Text.Trim()+"'";
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", this.textBoxname.Text);
                cmd.Parameters.AddWithValue("@type", this.textBoxtype.Text);
                cmd.Parameters.AddWithValue("@price", this.textBoxprice.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Successfully!!");
                textClear();
                showData();
            }
            catch
            {
               
                MessageBox.Show("Update Fail!!");
            }
        }


        private void buttondelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                string sql = "delete from restaurants  where id='" + this.textBoxid.Text.Trim() + "'";
                cmd = new MySqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Successfully!!");
                textClear();
                showData();
            }
            catch
            {
                MessageBox.Show("Delete Fail!!");
            }
        }

        

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void linkLabellogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            con.Close();
            this.Hide();
            Login login = new Login();
            login.Show();
        }
    }
}
