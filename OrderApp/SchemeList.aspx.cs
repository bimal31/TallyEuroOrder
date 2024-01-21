using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace OrderApp
{
    public partial class SchemeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetSchemeList();
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
                GetSchemeList();
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
                Response.Redirect("AddScheme.aspx", false);
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdSchemeList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditValue")
                {
                    Int32 SchemeId = Convert.ToInt32(e.CommandArgument);

                    Common cmn = new Common();
                    string strEncryptValue = cmn.Encrypt(Convert.ToString(SchemeId));

                    Response.Redirect("AddScheme.aspx?q=" + strEncryptValue, false);
                    // Response.Redirect("AddScheme.aspx?SchemeId=" + SchemeId, false);
                }

                if (e.CommandName == "DeleteValue")
                {
                    Int32 SchemeId = Convert.ToInt32(e.CommandArgument);

                    BA_tblScheme ObjScheme = new BA_tblScheme();

                    ObjScheme.SchemeId = SchemeId;
                    bool output = ObjScheme.DELETE_RECORDS_FROM_tblScheme();

                    if (output == true)
                    {
                        GetSchemeList();
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

        protected void GetSchemeList()
        {
            try
            {
                DataTable dt = new DataTable();
                BA_tblScheme objScheme = new BA_tblScheme();
                objScheme.SchemeName = txtSearch.Text;
                objScheme.SELECT_ALL_tblScheme(ref dt);

                

                grdSchemeList.DataSource = dt;
                grdSchemeList.DataBind();
                Session["dtScheme"] = dt;
                ViewState["sortdr"] = "Asc";
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }



        protected void grdSchemeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtScheme = new DataTable();
                dtScheme = Session["dtScheme"] as DataTable;

                grdSchemeList.PageIndex = e.NewPageIndex;
                grdSchemeList.DataSource = dtScheme;
                grdSchemeList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdSchemeList_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtrslt = (DataTable)Session["dtScheme"];
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
                grdSchemeList.DataSource = dtrslt;
                grdSchemeList.DataBind();


            }

        }
    }
}