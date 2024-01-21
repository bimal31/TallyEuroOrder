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



public class DA_tblOrderFreeProduct : DALBase
{
    public DA_tblOrderFreeProduct()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool GET_RECORDS_FROM_OrderFreeProduct_to_Order(BA_tblOrderFreeProduct objBA_tblOrderFreeProduct, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@orderid", objBA_tblOrderFreeProduct.OrderID);
            return this.Get_Records("Sp_Get_OrderByFreeProduct", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
