using MemberObjectLibrary.BussinessObject;
using MemberObjectLibrary.Repository;
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
    public partial class frmMemberManagement : Form
    {
        IMemberRepository memberRepository = new MemberRepository();
        BindingSource source;
        public frmMemberManagement()
        {
            InitializeComponent();
        }

        //---------------------------------------------------
        private void frmMemberMemberManagement_Load(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            //Register this event to open the frmMemberDetails form that perform updating
            dgvMemberList.CellDoubleClick += DgvMemberList_CellDoubleClick;
        }
        //----------------------------------------------
        private void DgvMemberList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmMemberDetails fr = new frmMemberDetails
            {
                Text = "Update Member",
                InsertOrUpdate = true,
                MemberInfo = GetMemberObject(),
                MemberRepository = memberRepository
            };
            if(fr.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                // set focus Member update
                source.Position = source.Count - 1;
            }
        }
        // --------------------------------
        //Clear text on textboxes
        private void ClearText()
        {
            txtEmail.Text = string.Empty;
            txtMemberName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtIsAdmin.Text = string.Empty;
        }

        private void LoadMemberList()
        {
            var members = memberRepository.GetMemberObjects();
            try
            {
                // the bindingSource component is designed tp simplify
                // the process of biding controls to an underluing data source
                source = new BindingSource();
                source.DataSource = members;

                txtEmail.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                txtCountry.DataBindings.Clear();
                txtIsAdmin.DataBindings.Clear();

                txtEmail.DataBindings.Add("Text", source, "Email");
                txtMemberName.DataBindings.Add("Text", source, "MemberName");
                txtPassword.DataBindings.Add("Text", source, "Password");
                txtCity.DataBindings.Add("Text", source, "City");
                txtCountry.DataBindings.Add("Text", source, "Country");
                txtIsAdmin.DataBindings.Add("Text", source, "isAdmin");

                dgvMemberList.DataSource = null;
                dgvMemberList.DataSource = source;
                if(members.Count() == 0)
                {
                    ClearText();
                    btnDelete.Enabled = false;
                }
                else
                {
                    btnDelete.Enabled = true;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Member List");
            }
        }//end LoadMemberList

        private MemberObject GetMemberObject()
        {
           MemberObject member = null;
            try
            {
                member = new MemberObject
                {
                    email = txtEmail.Text,
                    memberName = txtMemberName.Text,
                    password = txtPassword.Text,
                    city = txtCity.Text,
                    country = txtCountry.Text,
                    isAdmin = int.Parse(txtIsAdmin.Text)
                };
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Member");
            }
            return member;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadMemberList();
        }//end btnLoad_Click
        //------------------------------------------------------------

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmMemberDetails frmMemberDetails = new frmMemberDetails
            {
                Text = "Add car",
                InsertOrUpdate = false,
                MemberRepository = memberRepository
            };
            if(frmMemberDetails.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                //Set focus Member inserted
                source.Position = source.Count - 1;
            }
        }
        //------------------------------------------

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var member = GetMemberObject();
                memberRepository.DeleteMember(member.email);
                LoadMemberList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a car");
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Server=DESKTOP-0RP7PVK\\CUONGPC;uid=sa;pwd=12345;database=MemberObject_Ass01Group8;TrustServerCertificate=True"))
                {
                    if(con.State == ConnectionState.Closed)
                        con.Open();
                    using(DataTable dt = new DataTable("MemberObject"))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM MemberObject WHERE MemberName Like @MemberName Order By Membername ASC ", con))
                        {
                            cmd.Parameters.AddWithValue("MemberName", string.Format("%{0}%", txtSearchName.Text));
                          //  cmd.Parameters.AddWithValue("City", string.Format("%{0}%", txtSearchName.Text));
                          //  cmd.Parameters.AddWithValue("Country", string.Format("%{0}%", txtSearchName.Text));
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(dt);
                            dgvMemberList.DataSource = dt;
                      }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);  
            }
        }

        private void txtSearchName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // enter
                btnSearch.PerformClick();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Server=DESKTOP-0RP7PVK\\CUONGPC;uid=sa;pwd=12345;database=MemberObject_Ass01Group8;TrustServerCertificate=True"))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    using (DataTable dt = new DataTable("MemberObject"))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM MemberObject WHERE City Like @City Or Country Like @Country ", con))
                        { 
                              cmd.Parameters.AddWithValue("City", string.Format("%{0}%", txtFilterByCityOrCountry.Text));
                              cmd.Parameters.AddWithValue("Country", string.Format("%{0}%", txtFilterByCityOrCountry.Text));
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(dt);
                            dgvMemberList.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFilterByCityOrCountry_KeyPress(object sender, KeyPressEventArgs ex)
        {
            if(ex.KeyChar == (char)13)
            {
                btnFilter.PerformClick();
            }
        }
    }
}
