using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RestaurantProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=restaurant;Uid=root;Pwd=''");

        private void button1_Click(object sender, EventArgs e)
        {       
           
            try 
            { 
                if (frist_name.Text!="" && last_name.Text != "" && gender.Text != "" && username.Text != "" && password.Text != "" && con_password.Text != "")
                {
                    if(password.Text == con_password.Text)
                    {
                        int v = Check(username.Text);
                        if (v != 1)
                        { 

                            con.Open();

                            MySqlCommand cmd = new MySqlCommand("INSERT INTO register (f_name, l_name, gender, username, password) VALUES " +
                                "('"+frist_name.Text+ "', '"+last_name.Text+"','"+gender.Text+"','"+username.Text+ "','"+password.Text+"');", con);
                            cmd.ExecuteNonQuery();

                            con.Close();

                            MessageBox.Show("Register Successfully!");

                            this.Hide();
                            Login login = new Login();
                            login.Show();

                            frist_name.Text = "";
                            last_name.Text = "";
                            gender.Text = "";
                            username.Text = "";
                            password.Text = "";
                            con_password.Text = "";
                           
                        }
                        else
                        {
                            MessageBox.Show("Your Already Registered!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password not match!!!");
                    }
                }
                else
                {
                    MessageBox.Show("Form You Empty!!!!");
                }
            } 
            
        catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int Check(string username)
        {
          

            con.Open();
            string sql = "SELECT COUNT(*) FROM Register WHERE username ='"+username+"'";

            MySqlCommand cmd = new MySqlCommand(sql, con);
            int v = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return v;
            
        }

        private void check_CheckedChanged(object sender, EventArgs e)
        {
            if(check.Checked)
            {
                password.UseSystemPasswordChar = false;
                con_password.UseSystemPasswordChar = false;
            }
            else
            {
                password.UseSystemPasswordChar = true;
                con_password.UseSystemPasswordChar = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            this.Hide();
            Login login = new Login();
            login.Show();
        }

      
    }
}
