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


public class DA_tblOrderProductDetails : DALBase
{
    public DA_tblOrderProductDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool GET_RECORDS_FROM_OrderProductDetails_to_Order(BA_tblOrderProductDetails objBA_tblOrderFreeProduct, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@orderid", objBA_tblOrderFreeProduct.OrderID);
            return this.Get_Records("Sp_Get_OrderProductDetails", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
