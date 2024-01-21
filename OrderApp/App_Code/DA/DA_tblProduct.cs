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

/// <summary>
/// Summary description for DA_tblProduct
/// <summary>
public class DA_tblProduct : DALBase
{
    public DA_tblProduct()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool INSERT_tblProduct(BA_tblProduct objBA_tblProduct)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("@ProductName", objBA_tblProduct.ProductName);
            p[1] = new SqlParameter("@ProductDesc", objBA_tblProduct.ProductDesc);
            p[2] = new SqlParameter("@FromScheme", objBA_tblProduct.FromScheme);
            p[3] = new SqlParameter("@ToScheme", objBA_tblProduct.ToScheme);

            p[4] = new SqlParameter("@Isdeleted", objBA_tblProduct.Isdeleted);
            p[5] = new SqlParameter("@CreateBy", objBA_tblProduct.CreateBy);
            bool flag = this.Execute_NonQuery("sproc_INSERT_tblProduct", p);
            if (flag)
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

    public bool UPDATE_tblProduct(BA_tblProduct objBA_tblProduct)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@ProductId", objBA_tblProduct.ProductId);
            p[1] = new SqlParameter("@ProductName", objBA_tblProduct.ProductName);
            p[2] = new SqlParameter("@ProductDesc", objBA_tblProduct.ProductDesc);
            p[3] = new SqlParameter("@FromScheme", objBA_tblProduct.FromScheme);
            p[4] = new SqlParameter("@ToScheme", objBA_tblProduct.ToScheme);

            p[5] = new SqlParameter("@Isdeleted", objBA_tblProduct.Isdeleted);
            p[6] = new SqlParameter("@UpdateBy", objBA_tblProduct.UpdateBy);
            return this.Execute_NonQuery("sproc_UPDATE_tblProduct", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblProduct(BA_tblProduct objBA_tblProduct, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@ProductName", objBA_tblProduct.ProductName);
            return this.Get_Records("sproc_SELECT_ALL_tblProduct", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblProduct(BA_tblProduct objBA_tblProduct, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@ProductId", objBA_tblProduct.ProductId);
            return this.Get_Records("sproc_SELECT_tblProduct", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool CHK_RECORDS_FROM_tblProduct(BA_tblProduct objBA_tblProduct, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@ProductId", objBA_tblProduct.ProductId);
            p[1] = new SqlParameter("@ProductName", objBA_tblProduct.ProductName);
            return this.Get_Records("sproc_CheckProduct", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DELETE_RECORDS_FROM_tblProduct(BA_tblProduct objBA_tblProduct)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@ProductId", objBA_tblProduct.ProductId);
            return this.Execute_NonQuery("sproc_DELETE_tblProduct", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblProduct_API(ref DataTable dt)
    {
        try
        {
            return this.Get_Records("sproc_SELECT_ALL_tblProduct_API", ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblProduct_APINew(long Stateid, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Stateid", Stateid);
            return this.Get_Records("sproc_SELECT_ALL_tblProduct_APINew", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

}