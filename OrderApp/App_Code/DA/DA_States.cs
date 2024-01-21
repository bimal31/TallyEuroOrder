using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DA_States
/// <summary>
public class DA_States : DALBase
{
    public DA_States()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool INSERT_States(BA_States objBA_States)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[5];
            //p[0] = new SqlParameter("@state_id", objBA_States.state_id);
            p[0] = new SqlParameter("@state_name", objBA_States.state_name);
            p[1] = new SqlParameter("@country_id", objBA_States.country_id);
            p[2] = new SqlParameter("@created_date", objBA_States.created_date);
          //  p[4] = new SqlParameter("@modify_date", objBA_States.modify_date);
            p[3] = new SqlParameter("@created_by", objBA_States.created_by);
         //   p[6] = new SqlParameter("@modify_by", objBA_States.modify_by);
            p[4] = new SqlParameter("@is_del", objBA_States.is_del);
            bool flag = this.Execute_NonQuery("sproc_INSERT_States", p);
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

    public bool UPDATE_States(BA_States objBA_States)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("@state_id", objBA_States.state_id);
            p[1] = new SqlParameter("@state_name", objBA_States.state_name);
            p[2] = new SqlParameter("@country_id", objBA_States.country_id);
            p[3] = new SqlParameter("@modify_date", objBA_States.modify_date);
            p[4] = new SqlParameter("@modify_by", objBA_States.modify_by);
            p[5] = new SqlParameter("@is_del", objBA_States.is_del);
            return this.Execute_NonQuery("sproc_UPDATE_States", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_States(BA_States objBA_States, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@statename", objBA_States.state_name);
            return this.Get_Records("sproc_SELECT_ALL_StateswithCountry", p, ref dt);
           
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_States_ByCountry(BA_States objBA_States, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@country_id", objBA_States.country_id);
            return this.Get_Records("sproc_SELECT_States_ByCountry", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_States(BA_States objBA_States, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@state_id", objBA_States.state_id);
            return this.Get_Records("sproc_SELECT_States", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DELETE_RECORDS_FROM_States(BA_States objBA_States)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@state_id", objBA_States.state_id);       
            return this.Execute_NonQuery("sproc_DELETE_States", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool CheckCoutnryWise_States(BA_States objBA_States, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@country_id", objBA_States.country_id);
            p[1] = new SqlParameter("@state_id", objBA_States.state_id);
            p[2] = new SqlParameter("@state_name", objBA_States.state_name);
            return this.Get_Records("sproc_CheckCoutnryWise_States", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}