using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlTypes;


public class BA_tblDashboard
{
    DA_tblDashboard objDA_tblDashboard = new DA_tblDashboard();

    public BA_tblDashboard()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string _FromDate;
    public string FromDate
    {
        get { return _FromDate; }
        set { _FromDate = value; }
    }
    private string _ToDate;
    public string ToDate
    {
        get { return _ToDate; }
        set { _ToDate = value; }
    }
    private int _UserId;
    public int UserId
    {
        get { return _UserId; }
        set { _UserId = value; }
    }

    private string _UserType;
    public string UserType
    {
        get { return _UserType; }
        set { _UserType = value; }
    }

    public bool GET_RECORDS_FOR_Dashboard(ref DataSet ds)
    {
        try
        {
            if (objDA_tblDashboard.GET_RECORDS_FOR_Dashboard(this, ref ds))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FOR_OrderReport(string OrderStatus, ref DataSet ds)
    {
        try
        {
            if (objDA_tblDashboard.GET_RECORDS_FOR_OrderReport(this, OrderStatus, ref ds))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public bool GET_RECORDS_FOR_OrderHistoryReport(ref DataSet ds)
    {
        try
        {
            if (objDA_tblDashboard.GET_RECORDS_FOR_OrderHistoryReport(this, ref ds))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
