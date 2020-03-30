using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Odbc;

namespace Products
{
    public partial class frmLogin : frmInheritance
    {
        bool boolUserExists = false;

        string strAccessConnectionString = "Driver= {Microsoft Access Driver (*.mdb)}; Dbq=Products.mdb;Uid=Admin;Pwd=;";

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
    
        
            if (btnLogin.Text == "Login")
            {
                if (txtUsername.Text == "")
                {
                    MessageBox.Show("User Name field cannot be left empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtPassword.Text == "")
                {
                    MessageBox.Show("Password field cannot be left empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                checkIfUserExists();
                if(boolUserExists == false)
                {
                    MessageBox.Show("User Name does not exist, please check your details and enter correct details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearTextBoxes();
                    
                }
                else if (boolUserExists == true)
                {
                    string UserName = txtUsername.Text;
                    string Password = txtPassword.Text;
                    string query = "SELECT UserName, Password FROM Users WHERE UserName=@UserName and Password=@Password";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    if (UserName == txtUsername.Text && Password == txtPassword.Text)
                    {
                        frmMain frmMain = new frmMain();
                        frmMain.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Username Or Password are Incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                    
                }
            }
               
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ClearTextBoxes()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";

        }

        private void checkIfUserExists()
        {
            string query = "SELECT * FROM Users WHERE UserName='" + txtUsername.Text + "'";

            OdbcConnection OdbcConnection = new OdbcConnection();
            OdbcCommand cmd;
            OdbcDataReader dr;

            OdbcConnection.ConnectionString = strAccessConnectionString;
            OdbcConnection.Open();
            cmd = new OdbcCommand(query, OdbcConnection);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                boolUserExists = true;
            }
            dr.Close();
            OdbcConnection.Close();
            dr.Dispose();
            OdbcConnection.Dispose();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
