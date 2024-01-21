using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class AddScheme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["q"] != null)
                    {
                        string strKey = Convert.ToString(Request.QueryString["q"]);
                        Common cmn = new Common();
                        strKey = cmn.Decrypt(strKey);

                        Int32 SchemeId = Convert.ToInt32(strKey);

                        BA_tblScheme ObjScheme = new BA_tblScheme();
                        DataTable dt = new DataTable();

                        ObjScheme.SchemeId = SchemeId;
                        ObjScheme.GET_RECORDS_FROM_tblScheme(ref dt);

                        if (dt != null)
                        {
                            txtSchemeName.Text = Convert.ToString(dt.Rows[0]["SchemeName"]);
                            txtSchemeDescription.Text = Convert.ToString(dt.Rows[0]["Schemedescription"]);
                            hdSchemeId.Value = Convert.ToString(dt.Rows[0]["SchemeId"]);
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
                BA_tblScheme ObjScheme = new BA_tblScheme();
                Common Cmn = new Common();
                ObjScheme.SchemeName = txtSchemeName.Text;
                ObjScheme.Schemedescription = txtSchemeDescription.Text;


                if (!CHKSchemeName(hdSchemeId.Value))
                {
                    bool output;
                    if (hdSchemeId.Value == "")
                    {
                        ObjScheme.created_by = Convert.ToInt32(Session["UserId"]);
                        ObjScheme.created_date = DateTime.Now;

                        ObjScheme.is_del = false;

                        output = ObjScheme.INSERT_tblScheme();
                    }
                    else
                    {
                        ObjScheme.modify_by = Convert.ToInt32(Session["UserId"]);
                        ObjScheme.modify_date = DateTime.Now;

                        ObjScheme.SchemeId = Convert.ToInt32(hdSchemeId.Value);
                        output = ObjScheme.UPDATE_tblScheme();
                    }


                    if (output == true)
                    {
                        Response.Redirect("SchemeList.aspx", false);
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
                txtSchemeName.Text = "";
                txtSchemeDescription.Text = "";
                hdSchemeId.Value = "";
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        public bool CHKSchemeName(string id)
        {
            try
            {
                BA_tblScheme ObjScheme = new BA_tblScheme();
                DataTable dt = new DataTable();
                if (id == "")
                {
                    ObjScheme.SchemeId = 0;
                }
                else
                {
                    ObjScheme.SchemeId = Convert.ToInt32(id);
                }

                ObjScheme.SchemeName = txtSchemeName.Text;
                ObjScheme.CHK_RECORDS_FROM_SchemeName(ref dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                      //  txtSchemeName.Text = "";
                        lblErrorMessage.Text = CommMessage.SchemeNamealreadyexist;
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                        return true;
                    }

                }
                return false;

            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
                return true;
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("SchemeList.aspx", false);
        }
    }
}