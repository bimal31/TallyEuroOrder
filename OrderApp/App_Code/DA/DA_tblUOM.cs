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
/// Summary description for DA_tbluom
/// <summary>
public class DA_tblUOM : DALBase
{
    public DA_tblUOM()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool INSERT_tbluom(BA_tblUOM objBA_tblUOM)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@uomName", objBA_tblUOM.uomName);
            p[1] = new SqlParameter("@uomdescription", objBA_tblUOM.uomdescription);
            p[2] = new SqlParameter("@created_date", objBA_tblUOM.created_date);
            p[3] = new SqlParameter("@created_by", objBA_tblUOM.created_by);
            bool flag = this.Execute_NonQuery("sproc_INSERT_tbluom", p);
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

    public bool UPDATE_tbluom(BA_tblUOM objBA_tblUOM)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@uomId", objBA_tblUOM.uomId);
            p[1] = new SqlParameter("@uomName", objBA_tblUOM.uomName);
            p[2] = new SqlParameter("@uomdescription", objBA_tblUOM.uomdescription);
            p[3] = new SqlParameter("@modify_date", objBA_tblUOM.modify_date);
            p[4] = new SqlParameter("@modify_by", objBA_tblUOM.modify_by);
          
            return this.Execute_NonQuery("sproc_UPDATE_tbluom", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tbluom(BA_tblUOM objBA_tblUOM, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@uomName", objBA_tblUOM.uomName);
            return this.Get_Records("sproc_SELECT_ALL_tbluom", p, ref dt);
           
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    
    public bool GET_RECORDS_FROM_tbluom(BA_tblUOM objBA_tblUOM, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@uomId", objBA_tblUOM.uomId);
            return this.Get_Records("sproc_SELECT_tbluom", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DELETE_RECORDS_FROM_tbluom(BA_tblUOM objBA_tblUOM)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@uomId", objBA_tblUOM.uomId);
            return this.Execute_NonQuery("sproc_DELETE_tbluom", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool CHK_RECORDS_FROM_uomName(BA_tblUOM objuom, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@uomId", objuom.uomId);
            p[1] = new SqlParameter("@uomName", objuom.uomName);
            return this.Get_Records("sproc_SELECT_FROM_uomName", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}