using MySql.Data.MySqlClient;
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

namespace RestaurantProject
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=restaurant;Uid=root;Pwd=''");
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void check_CheckedChanged(object sender, EventArgs e)
        {
            
                if (check.Checked)
                {
                    password.UseSystemPasswordChar = false;
                }
                else
                {
                    password.UseSystemPasswordChar = true;
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (username.Text!="" && password.Text!="")
            {
                string sql = "select count(*) from register where username = '"+username.Text+"' and "+
                    "password = '"+password.Text+"' ";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(sql, con);
                int v = Convert.ToInt32(cmd.ExecuteScalar());
                if (v != 1)
                {
                    MessageBox.Show("You Not Yet Register!!!");
                    password.Text = "";
                }
                else
                {
                    MessageBox.Show("Login Successfully!");
                    Restaurant restaurant = new Restaurant();
                    this.Hide();
                    restaurant.Show();
                    

                }
            }
            else
            {
                MessageBox.Show("Your form Empty");
            }

            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }
    }
}
