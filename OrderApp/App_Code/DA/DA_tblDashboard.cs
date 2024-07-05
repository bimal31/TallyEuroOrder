using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;


public class DA_tblDashboard : DALBase
{
    public DA_tblDashboard()
    {
        //
        // TODO: Add constructor logic here
        //
    
    }

    public bool GET_RECORDS_FOR_Dashboard(BA_tblDashboard objBA_tblDashboard, ref DataSet ds)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@FromDate", objBA_tblDashboard.FromDate);
            p[1] = new SqlParameter("@ToDate", objBA_tblDashboard.ToDate);
            p[2] = new SqlParameter("@UserId", objBA_tblDashboard.UserId);
            p[3] = new SqlParameter("@UserType", objBA_tblDashboard.UserType);
            return this.Get_RecordsDataset("sproc_SELECT_tblOrder_ByDashboard", p, ref ds);
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public bool GET_RECORDS_FOR_OrderReport(BA_tblDashboard objBA_tblDashboard, string OrderStatus, ref DataSet ds)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@FromDate", objBA_tblDashboard.FromDate);
            p[1] = new SqlParameter("@ToDate", objBA_tblDashboard.ToDate);
            p[2] = new SqlParameter("@UserId", objBA_tblDashboard.UserId);
            p[3] = new SqlParameter("@UserType", objBA_tblDashboard.UserType);
            p[4] = new SqlParameter("@OrderStatus", OrderStatus);
            return this.Get_RecordsDataset("sproc_SELECT_tblOrder_OrderReport", p, ref ds);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FOR_OrderHistoryReport(BA_tblDashboard objBA_tblDashboard, ref DataSet ds)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@FromDate", objBA_tblDashboard.FromDate);
            p[1] = new SqlParameter("@ToDate", objBA_tblDashboard.ToDate);
            return this.Get_RecordsDataset("sproc_SELECT_tblOrder_OrderHistoryReport", p, ref ds);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
