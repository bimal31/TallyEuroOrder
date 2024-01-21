using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class ErrorLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetErrorLog();
            }
        }
        public void GetErrorLog()
        {
            try
            {
                DataTable dt = new DataTable();
                BA_ErrorLog objBA_ErrorLog = new BA_ErrorLog();
                objBA_ErrorLog.SELECT_ALL_ErrorLog(ref dt);

                Session["dtOrder"] = dt;

                grdError.DataSource = dt;
                grdError.DataBind();
            }
            catch (Exception)
            {

            }
        }

        protected void grdError_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtOrder = new DataTable();
                dtOrder = Session["dtOrder"] as DataTable;

                grdError.PageIndex = e.NewPageIndex;
                grdError.DataSource = dtOrder;
                grdError.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }
    }
}