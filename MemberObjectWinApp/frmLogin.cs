using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemberObjectWinApp
{
    public partial class frmLogin : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        private string ConnectionString { get; set; }   
        public frmLogin()
        {
            InitializeComponent();
            con = new SqlConnection("Server=DESKTOP-0RP7PVK\\CUONGPC;uid=sa;pwd=12345;database=MemberObject_Ass01Group8;TrustServerCertificate=True");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Select * From MemberObject WHERE Email='" + email + "'And Password= '" + password + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Login success Welcome to MemberManagemnt");
                frmMemberManagement frm = new frmMemberManagement();
                this.Hide();
                frm.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Invalid Login please check Email and password");
            }
            con.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
