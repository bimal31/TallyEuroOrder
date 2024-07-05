using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["UserName"] == null)
                {
                    Response.Redirect("Login.aspx",false);
                }
                else
                {
                    if (Session["UserName"] != null)
                    {
                       lblUserName.InnerText = Convert.ToString(Session["FirstName"]) + " " + Convert.ToString(Session["LastName"]);
                    }

                    if (Session["UserType"] != null)
                    {
                        string UserType = "";
                        UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);

                        if (UserType == "Factory")
                        {
                            liDealer.Visible = false;
                            liProductPacking.Visible = false;
                            liProduct.Visible = false;
                            liProductPacking.Visible = false;
                            liProduct.Visible = false;
                            liState.Visible = false;
                            liScheme.Visible = false;
                            ///liUser.Visible = false;
                        }
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Abandon();
                Response.Redirect("Login.aspx",false);
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }
    }
}