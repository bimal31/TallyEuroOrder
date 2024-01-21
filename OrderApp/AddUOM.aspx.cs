using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class AddUOM : System.Web.UI.Page
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

                        Int32 UOMId = Convert.ToInt32(strKey);

                        BA_tblUOM ObjUOM = new BA_tblUOM();
                        DataTable dt = new DataTable();

                        ObjUOM.uomId = UOMId;
                        ObjUOM.GET_RECORDS_FROM_tbluom(ref dt);

                        if (dt != null)
                        {
                            txtUOMName.Text = Convert.ToString(dt.Rows[0]["UOMName"]);
                            txtUOMDescription.Text = Convert.ToString(dt.Rows[0]["UOMdescription"]);
                            hdUOMId.Value = Convert.ToString(dt.Rows[0]["UOMId"]);
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
                BA_tblUOM ObjUOM = new BA_tblUOM();
                Common Cmn = new Common();
                ObjUOM.uomName = txtUOMName.Text;
                ObjUOM.uomdescription = txtUOMDescription.Text;


                if (!CHKUOMName(hdUOMId.Value))
                {
                    bool output;
                    if (hdUOMId.Value == "")
                    {
                        ObjUOM.created_by = Convert.ToInt32(Session["UserId"]);
                        ObjUOM.created_date = DateTime.Now;

                        ObjUOM.is_del = false;

                        output = ObjUOM.INSERT_tbluom();
                    }
                    else
                    {
                        ObjUOM.modify_by = Convert.ToInt32(Session["UserId"]);
                        ObjUOM.modify_date = DateTime.Now;

                        ObjUOM.uomId = Convert.ToInt32(hdUOMId.Value);
                        output = ObjUOM.UPDATE_tbluom();
                    }


                    if (output == true)
                    {
                        Response.Redirect("UOMList.aspx", false);
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
                txtUOMName.Text = "";
                txtUOMDescription.Text = "";
                hdUOMId.Value = "";
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        public bool CHKUOMName(string id)
        {
            try
            {
                BA_tblUOM ObjUOM = new BA_tblUOM();
                DataTable dt = new DataTable();
                if (id == "")
                {
                    ObjUOM.uomId = 0;
                }
                else
                {
                    ObjUOM.uomId = Convert.ToInt32(id);
                }

                ObjUOM.uomName = txtUOMName.Text;
                ObjUOM.CHK_RECORDS_FROM_uomName(ref dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                      //  txtUOMName.Text = "";
                        lblErrorMessage.Text = CommMessage.UOMNamealreadyexist;
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
            Response.Redirect("UOMList.aspx", false);
        }
    }
}