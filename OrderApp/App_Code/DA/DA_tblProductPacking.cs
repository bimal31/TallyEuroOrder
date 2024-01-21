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
/// Summary description for DA_tblProductPacking
/// <summary>
public class DA_tblProductPacking : DALBase
{
    public DA_tblProductPacking()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool INSERT_tblProductPacking(BA_tblProductPacking objBA_tblProductPacking)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[10];
            p[0] = new SqlParameter("@ProductID", objBA_tblProductPacking.ProductID);
            p[1] = new SqlParameter("@ProductPck", objBA_tblProductPacking.ProductPck);
            p[2] = new SqlParameter("@PackingNos", objBA_tblProductPacking.PackingNos);
            p[3] = new SqlParameter("@ProductPckDetails", objBA_tblProductPacking.ProductPckDetails);
            p[4] = new SqlParameter("@PackingType", objBA_tblProductPacking.PackingType);
            p[5] = new SqlParameter("@XMLData", objBA_tblProductPacking.XmlData);
            p[6] = new SqlParameter("@IsScheme", objBA_tblProductPacking.IsScheme);
            p[7] = new SqlParameter("@Isdeleted", objBA_tblProductPacking.Isdeleted);
            p[8] = new SqlParameter("@CreateBy", objBA_tblProductPacking.CreateBy);
            p[9] = new SqlParameter("@UOM", objBA_tblProductPacking.UOM);
            

            bool flag = this.Execute_NonQuery("sproc_INSERT_tblProductPacking", p);

            if (flag)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool UPDATE_tblProductPacking(BA_tblProductPacking objBA_tblProductPacking)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[11];
            p[0] = new SqlParameter("@ProductPckID", objBA_tblProductPacking.ProductPckID);
            p[1] = new SqlParameter("@ProductID", objBA_tblProductPacking.ProductID);
            p[2] = new SqlParameter("@ProductPck", objBA_tblProductPacking.ProductPck);
            p[3] = new SqlParameter("@PackingNos", objBA_tblProductPacking.PackingNos);
            p[4] = new SqlParameter("@ProductPckDetails", objBA_tblProductPacking.ProductPckDetails);
            p[5] = new SqlParameter("@PackingType", objBA_tblProductPacking.PackingType);
            p[6] = new SqlParameter("@XMLData", objBA_tblProductPacking.XmlData);
            p[7] = new SqlParameter("@IsScheme", objBA_tblProductPacking.IsScheme);
            p[8] = new SqlParameter("@Isdeleted", objBA_tblProductPacking.Isdeleted);
            p[9] = new SqlParameter("@UpdateBy", objBA_tblProductPacking.UpdateBy);
            p[10] = new SqlParameter("@UOM", objBA_tblProductPacking.UOM);


            return this.Execute_NonQuery("sproc_UPDATE_tblProductPacking", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblProductPacking(BA_tblProductPacking objBA_tblProductPacking, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@ProductPckDetails", objBA_tblProductPacking.ProductPckDetails);
            return this.Get_Records("sproc_SELECT_ALL_tblProductPacking", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblProductPacking(BA_tblProductPacking objBA_tblProductPacking, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@ProductPckID", objBA_tblProductPacking.ProductPckID);
            return this.Get_Records("sproc_SELECT_tblProductPacking", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblProductById(BA_tblProductPacking objBA_tblProductPacking, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@ProductID", objBA_tblProductPacking.ProductID);
            return this.Get_Records("sproc_SELECT_tblProductById", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DELETE_RECORDS_FROM_tblProductPacking(BA_tblProductPacking objBA_tblProductPacking)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@ProductPckID", objBA_tblProductPacking.ProductPckID);
            return this.Execute_NonQuery("sproc_DELETE_tblProductPacking", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblProductPacking_API(BA_tblProductPacking objBA_tblProductPacking, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@ProductID", objBA_tblProductPacking.ProductID);
            return this.Get_Records("sproc_SELECT_ProductpackageList_API", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblProductPacking_APINew(long ProductID,long StateId, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@ProductID", ProductID);
            p[1] = new SqlParameter("@StateId", StateId);
            return this.Get_Records("sproc_SELECT_ProductpackageList_APINew", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}