using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try {


                string p = "8UoNcPC3UoNC0oWCHXNwB7cIf52hVEBI17gw29WH7ZE=";
              
                Common cmn = new Common();
                p = cmn.Decrypt(p);
                //cmn.Decrypt(p.Trim()); 

            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BA_tblUser objUser = new BA_tblUser();
            Common cmn = new Common();
            DataTable dt = new DataTable();

            try
            {

                string pw = "8UoNcPC3UoNC0oWCHXNwB7cIf52hVEBI17gw29WH7ZE=";
                pw = cmn.Decrypt(pw);
                if (txtUserName.Value == "")
                {
                    lblErrorMessage.Text = "Please Enter UserName";
                }
                else if (txtPassword.Value == "")
                {
                    lblErrorMessage.Text = "Please Enter Password";
                }
                else
                {
                   
                    objUser.UserName = txtUserName.Value;
                    objUser.Pwd = cmn.Encrypt(txtPassword.Value);

                    objUser.GET_RECORDS_FROM_tblUser_Login(ref dt);

                    if (dt != null)
                    {
                        if (Convert.ToString(dt.Rows[0]["UserType"]).ToLower() != "salesman")
                        {
                            if (dt.Rows.Count > 0)
                            {
                                Session["UserName"] = Convert.ToString(dt.Rows[0]["UserName"]);
                                Session["FirstName"] = Convert.ToString(dt.Rows[0]["FirstName"]);
                                Session["LastName"] = Convert.ToString(dt.Rows[0]["LastName"]);
                                Session["UserType"] = Convert.ToString(dt.Rows[0]["UserType"]);
                                Session["UserId"] = Convert.ToString(dt.Rows[0]["UserId"]);

                                Response.Redirect("Dashboard.aspx", false);
                            }
                            else
                            {
                                lblErrorMessage.Text = "UserName or Password is wrong.";
                            }
                        }
                        else
                        {
                            lblErrorMessage.Text = "You are not authorize Login";
                        }
                    }
                    else
                    {
                        lblErrorMessage.Text = "UserName or Password is wrong.";
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }
    }
}