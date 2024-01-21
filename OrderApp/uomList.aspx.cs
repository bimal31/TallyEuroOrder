using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace OrderApp
{
    public partial class uomList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetuomList();
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void txtSearch_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GetuomList();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

      
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Adduom.aspx", false);
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grduomList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditValue")
                {
                    Int32 uomId = Convert.ToInt32(e.CommandArgument);

                    Common cmn = new Common();
                    string strEncryptValue = cmn.Encrypt(Convert.ToString(uomId));

                    Response.Redirect("Adduom.aspx?q=" + strEncryptValue, false);
                    // Response.Redirect("Adduom.aspx?uomId=" + uomId, false);
                }

                if (e.CommandName == "DeleteValue")
                {
                    Int32 uomId = Convert.ToInt32(e.CommandArgument);

                    BA_tblUOM Objuom = new BA_tblUOM();

                    Objuom.uomId = uomId;
                    bool output = Objuom.DELETE_RECORDS_FROM_tbluom();

                    if (output == true)
                    {
                        GetuomList();
                    }
                    else
                    {
                        lblErrorMessage.Text = CommMessage.CouldnotabletoDelete;
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void GetuomList()
        {
            try
            {
                DataTable dt = new DataTable();
                BA_tblUOM objuom = new BA_tblUOM();
                objuom.uomName = txtSearch.Text;
                objuom.SELECT_ALL_tbluom(ref dt);

                

                grduomList.DataSource = dt;
                grduomList.DataBind();
                Session["dtuom"] = dt;
                ViewState["sortdr"] = "Asc";
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }



        protected void grduomList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtuom = new DataTable();
                dtuom = Session["dtuom"] as DataTable;

                grduomList.PageIndex = e.NewPageIndex;
                grduomList.DataSource = dtuom;
                grduomList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grduomList_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtrslt = (DataTable)Session["dtuom"];
            if (dtrslt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["sortdr"] = "Desc";
                }
                else
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["sortdr"] = "Asc";
                }
                grduomList.DataSource = dtrslt;
                grduomList.DataBind();


            }

        }
    }
}