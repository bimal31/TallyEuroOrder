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
/// Summary description for DA_tblProductPackingStateScheme
/// <summary>
public class DA_tblProductPackingStateScheme : DALBase
{
    public DA_tblProductPackingStateScheme()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool INSERT_tblProductPackingStateScheme(BA_tblProductPackingStateScheme objBA_tblProductPackingStateScheme)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@SrNo", objBA_tblProductPackingStateScheme.SrNo);
            p[1] = new SqlParameter("@ProductPckID", objBA_tblProductPackingStateScheme.ProductPckID);
            p[2] = new SqlParameter("@state_id", objBA_tblProductPackingStateScheme.state_id);
            p[3] = new SqlParameter("@SchemeIdData", objBA_tblProductPackingStateScheme.SchemeIdData);
            p[4] = new SqlParameter("@SchemeProductCode", objBA_tblProductPackingStateScheme.SchemeProductCode);
            p[5] = new SqlParameter("@created_date", objBA_tblProductPackingStateScheme.created_date);
            p[6] = new SqlParameter("@created_by", objBA_tblProductPackingStateScheme.created_by);
            bool flag = this.Execute_NonQuery("sproc_INSERT_tblProductPackingStateScheme", p);
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

    public bool UPDATE_tblProductPackingStateScheme(BA_tblProductPackingStateScheme objBA_tblProductPackingStateScheme)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("@SrNo", objBA_tblProductPackingStateScheme.SrNo);
            p[1] = new SqlParameter("@ProductPckID", objBA_tblProductPackingStateScheme.ProductPckID);
            p[2] = new SqlParameter("@state_id", objBA_tblProductPackingStateScheme.state_id);
            p[3] = new SqlParameter("@SchemeIdData", objBA_tblProductPackingStateScheme.SchemeIdData);
            p[4] = new SqlParameter("@SchemeProductCode", objBA_tblProductPackingStateScheme.SchemeProductCode);
            p[5] = new SqlParameter("@modify_date", objBA_tblProductPackingStateScheme.modify_date);
            p[6] = new SqlParameter("@modify_by", objBA_tblProductPackingStateScheme.modify_by);

            return this.Execute_NonQuery("sproc_UPDATE_tblProductPackingStateScheme", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblProductPackingStateScheme(long StateId,ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@StateId", StateId);
            return this.Get_Records("sproc_SELECT_ALL_tblProductPackingStateScheme",p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblProductPackingStateScheme(BA_tblProductPackingStateScheme objBA_tblProductPackingStateScheme, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@SrNo", objBA_tblProductPackingStateScheme.SrNo);
            return this.Get_Records("sproc_SELECT_tblProductPackingStateScheme", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblProductPackingStateScheme_By_ProdPackId
        (BA_tblProductPackingStateScheme objBA_tblProductPackingStateScheme, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@ProductPckID", objBA_tblProductPackingStateScheme.ProductPckID);

            return this.Get_Records("sproc_SELECT_tblProductPackingStateScheme_By_ProdPackId", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool CHECK_EXISTING_tblProductPackingStateScheme
        (BA_tblProductPackingStateScheme objBA_tblProductPackingStateScheme, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@StateId", objBA_tblProductPackingStateScheme.state_id);
            p[1] = new SqlParameter("@SchemeId", objBA_tblProductPackingStateScheme.SchemeIdData);
            p[2] = new SqlParameter("@SchemeProductCode", objBA_tblProductPackingStateScheme.SchemeProductCode);

            return this.Get_Records("CHECK_EXISTING_tblProductPackingStateScheme", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DELETE_RECORDS_FROM_tblProductPackingStateScheme(BA_tblProductPackingStateScheme objBA_tblProductPackingStateScheme)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@SrNo", objBA_tblProductPackingStateScheme.SrNo);

            return this.Execute_NonQuery("sproc_DELETE_tblProductPackingStateScheme", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GetProductPackingStateScheme(BA_tblProductPackingStateScheme objBA_tblProductPackingStateScheme, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@StateId", objBA_tblProductPackingStateScheme.state_id);
            p[1] = new SqlParameter("@ProductPckID", objBA_tblProductPackingStateScheme.ProductPckID);
            p[2] = new SqlParameter("@SchemeId", objBA_tblProductPackingStateScheme.SchemeIdData);
            return this.Get_Records("sproc_SELECT_ALL_tblProductPackingStateScheme", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}