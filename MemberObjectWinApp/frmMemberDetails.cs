using MemberObjectLibrary.BussinessObject;
using MemberObjectLibrary.Repository;
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
    public partial class frmMemberDetails : Form
    {
        
        public frmMemberDetails()
        {
            InitializeComponent();
        }
        //------------------------------------------------------
        public IMemberRepository MemberRepository { get; set; }
        public bool InsertOrUpdate { get; set; } //False : Insert, True : Update
        public MemberObject MemberInfo { get; set; }
        //----------------------------------------------------

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var member = new MemberObject
                {
                    email = txtEmail.Text,
                    memberName = txtMemberName.Text,
                    password = txtPassword.Text,
                    city = txtCity.Text,
                    country = txtCountry.Text,
                    isAdmin = int.Parse(cboIsAdmin.Text)

                };
                if(InsertOrUpdate == false)
                {
                    MemberRepository.InsertMember(member);
                }
                else
                {
                    MemberRepository.UpdateMember(member);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message,InsertOrUpdate==false?"Add a new Member": "Update a member");
            }
        }// end btnSave_Click

        private void frmMemberDetails_Load(object sender, EventArgs e)
        {
            cboIsAdmin.SelectedIndex = 0;
            txtEmail.Enabled = !InsertOrUpdate;
            if(InsertOrUpdate == true)
            {
                //Show car to perform updating
                txtEmail.Text = MemberInfo.email;
                txtMemberName.Text = MemberInfo.memberName;
                txtPassword.Text = MemberInfo.password;
                txtCity.Text = MemberInfo.city;
                txtCountry.Text = MemberInfo.country;
                cboIsAdmin.Text = MemberInfo.isAdmin.ToString();
            }
        } // end frmMemberDetails_Load

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
} // end form
