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
/// Summary description for DA_tblScheme
/// <summary>
public class DA_tblScheme : DALBase
{
    public DA_tblScheme()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool INSERT_tblScheme(BA_tblScheme objBA_tblScheme)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@SchemeName", objBA_tblScheme.SchemeName);
            p[1] = new SqlParameter("@Schemedescription", objBA_tblScheme.Schemedescription);
            p[2] = new SqlParameter("@created_date", objBA_tblScheme.created_date);
            p[3] = new SqlParameter("@created_by", objBA_tblScheme.created_by);
            bool flag = this.Execute_NonQuery("sproc_INSERT_tblScheme", p);
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

    public bool UPDATE_tblScheme(BA_tblScheme objBA_tblScheme)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@SchemeId", objBA_tblScheme.SchemeId);
            p[1] = new SqlParameter("@SchemeName", objBA_tblScheme.SchemeName);
            p[2] = new SqlParameter("@Schemedescription", objBA_tblScheme.Schemedescription);
            p[3] = new SqlParameter("@modify_date", objBA_tblScheme.modify_date);
            p[4] = new SqlParameter("@modify_by", objBA_tblScheme.modify_by);
          
            return this.Execute_NonQuery("sproc_UPDATE_tblScheme", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblScheme(BA_tblScheme objBA_tblScheme, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@SchemeName", objBA_tblScheme.SchemeName);
            return this.Get_Records("sproc_SELECT_ALL_tblScheme", p, ref dt);
           
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblSchemeAPI(long ProductPckID, long StateId, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@ProductPckID", ProductPckID);
            p[1] = new SqlParameter("@StateId", StateId);
            return this.Get_Records("sproc_SELECT_ALL_tblSchemeAPI",p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblScheme(BA_tblScheme objBA_tblScheme, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@SchemeId", objBA_tblScheme.SchemeId);
            return this.Get_Records("sproc_SELECT_tblScheme", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DELETE_RECORDS_FROM_tblScheme(BA_tblScheme objBA_tblScheme)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@SchemeId", objBA_tblScheme.SchemeId);
            return this.Execute_NonQuery("sproc_DELETE_tblScheme", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool CHK_RECORDS_FROM_SchemeName(BA_tblScheme objScheme, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@SchemeId", objScheme.SchemeId);
            p[1] = new SqlParameter("@SchemeName", objScheme.SchemeName);
            return this.Get_Records("sproc_SELECT_FROM_SchemeName", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}