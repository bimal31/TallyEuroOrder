using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DA_Country
/// <summary>
public class DA_Country : DALBase
{
    public DA_Country()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool INSERT_Country(BA_Country objBA_Country)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[4];
           // p[0] = new SqlParameter("@countryId", objBA_Country.countryId);
            p[0] = new SqlParameter("@country_name", objBA_Country.country_name);
            p[1] = new SqlParameter("@created_date", objBA_Country.created_date);
           // p[3] = new SqlParameter("@modify_date", objBA_Country.modify_date);
            p[2] = new SqlParameter("@created_by", objBA_Country.created_by);
           // p[5] = new SqlParameter("@modify_by", objBA_Country.modify_by);
            p[3] = new SqlParameter("@is_del", objBA_Country.is_del);
            bool flag = this.Execute_NonQuery("sproc_INSERT_Country", p);
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

    public bool UPDATE_Country(BA_Country objBA_Country)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@countryId", objBA_Country.countryId);
            p[1] = new SqlParameter("@country_name", objBA_Country.country_name);
            p[2] = new SqlParameter("@modify_date", objBA_Country.modify_date);
            p[3] = new SqlParameter("@modify_by", objBA_Country.modify_by);
            return this.Execute_NonQuery("sproc_UPDATE_Country", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_Country(ref DataTable dt)
    {
        try
        {
            return this.Get_Records("sproc_SELECT_ALL_Country", ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_Country(BA_Country objBA_Country, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@countryId", objBA_Country.countryId);
            return this.Get_Records("sproc_SELECT_Country", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DELETE_RECORDS_FROM_Country(BA_Country objBA_Country)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@countryId", objBA_Country.countryId);         
            return this.Execute_NonQuery("sproc_DELETE_Country", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool CHK_RECORDS_FROM_Country(BA_Country objBA_Country, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@countryId", objBA_Country.countryId);
            p[1] = new SqlParameter("@CountryName", objBA_Country.country_name);
            return this.Get_Records("sproc_CheckexistCountryName", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}