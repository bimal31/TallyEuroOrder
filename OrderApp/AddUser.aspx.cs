using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Data;

namespace OrderApp
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["q"] != null)
                    {
                        BA_tblUser ObjUser = new BA_tblUser();
                        DataTable dt = new DataTable();

                        string strKey = Convert.ToString(Request.QueryString["q"]);
                        Common cmn = new Common();
                        strKey = cmn.Decrypt(strKey);

                        ObjUser.UserID = Convert.ToInt32(strKey);
                        ObjUser.GET_RECORDS_FROM_tblUser(ref dt);

                        if (dt != null)
                        {
                            txtFirstName.Text = Convert.ToString(dt.Rows[0]["FirstName"]);
                            txtMiddleName.Text = Convert.ToString(dt.Rows[0]["MiddleName"]);
                            txtLastName.Text = Convert.ToString(dt.Rows[0]["LastName"]);
                            ddlUserType1.SelectedValue = Convert.ToString(dt.Rows[0]["UserType"]);
                            txtPhoneNo.Text = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                            txtMobileNo.Text = Convert.ToString(dt.Rows[0]["MobileNo"]);
                            txtUserName.Text = Convert.ToString(dt.Rows[0]["UserName"]);
                            txtPassword.Text = Convert.ToString(dt.Rows[0]["Pwd"]);
                            txtConfirmPassword.Text = Convert.ToString(dt.Rows[0]["Pwd"]);

                            reqUserName.Visible = false;
                            reqPassword.Visible = false;
                            reqConfirmPassword.Visible = false;

                            hdUserId.Value = Convert.ToString(dt.Rows[0]["UserId"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BA_tblUser ObjUser = new BA_tblUser();
                Common Cmn = new Common();

                if (Validateuser())
                {

                    if (hdUserId.Value != "")
                    {
                        ObjUser.UserID = Convert.ToInt32(hdUserId.Value);
                    }
                    ObjUser.UserName = txtUserName.Text;
                    ObjUser.UserType = ddlUserType1.SelectedValue;
                    ObjUser.FirstName = txtFirstName.Text;
                    ObjUser.MiddleName = txtMiddleName.Text;
                    ObjUser.LastName = txtLastName.Text;
                    ObjUser.PhoneNo = txtPhoneNo.Text;
                    ObjUser.MobileNo = txtMobileNo.Text;
                    if (txtPassword.Text != "")
                    {
                        ObjUser.Pwd = Cmn.Encrypt(txtPassword.Text);
                    }
                    else
                    {
                        ObjUser.Pwd = "";
                    }
                    ObjUser.UpdateBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);

                    bool output;
                    if (hdUserId.Value == "")
                    {
                        output = ObjUser.INSERT_tblUser();
                    }
                    else
                    {
                        output = ObjUser.UPDATE_tblUser();
                    }

                    if (output == true)
                    {
                        //if (hdUserId.Value != "")
                        //{
                        Response.Redirect("UserList.aspx", false);
                        //}
                        //else
                        //{
                        //    Response.Redirect("Login.aspx", false);
                        //}
                    }
                    else
                    {
                        lblErrorMessage.Text = CommMessage.Recordcouldnotable;
                        lblErrorMessage.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtUserName.Text = "";
                txtFirstName.Text = "";
                txtMiddleName.Text = "";
                txtLastName.Text = "";
                txtPhoneNo.Text = "";
                txtMobileNo.Text = "";
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
                hdUserId.Value = "";
                lblErrorMessage.Text = "";
                ddlUserType1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        public bool Validateuser()
        {
            try
            {
                BA_tblUser ObjUser = new BA_tblUser();
                ObjUser.UserName = txtUserName.Text;
                DataTable dt = new DataTable();

                ObjUser.CHK_RECORDS_FROM_tblUser(ref dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtUserName.Text = "";
                        lblErrorMessage.Text = CommMessage.usernameareadyexist;
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                        return false;
                    }
                    else
                    {
                        lblErrorMessage.Text = CommMessage.usernamevailable;
                        lblErrorMessage.ForeColor = System.Drawing.Color.Green;
                        return true;
                    }
                }
                else
                {
                    lblErrorMessage.Text = CommMessage.usernamevailable;
                    lblErrorMessage.ForeColor = System.Drawing.Color.Green;
                    return true;
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
                return false;
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserList.aspx", false);
        }
    }
}