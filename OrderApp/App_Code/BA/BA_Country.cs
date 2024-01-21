using System;
using System.Data;
using System.Configuration;
using System.Web;

/// <summary>
/// Summary description for BA_Country
/// </summary>
public class BA_Country
{
    DA_Country objDA_Country = new DA_Country();

    public BA_Country()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private long _countryId;
    public long countryId
    {
        get { return _countryId; }
        set { _countryId = value; }
    }
    private string _country_name;
    public string country_name
    {
        get { return _country_name; }
        set { _country_name = value; }
    }
    private DateTime _created_date;
    public DateTime created_date
    {
        get { return _created_date; }
        set { _created_date = value; }
    }
    private DateTime _modify_date;
    public DateTime modify_date
    {
        get { return _modify_date; }
        set { _modify_date = value; }
    }
    private long _created_by;
    public long created_by
    {
        get { return _created_by; }
        set { _created_by = value; }
    }
    private long _modify_by;
    public long modify_by
    {
        get { return _modify_by; }
        set { _modify_by = value; }
    }
    private bool _is_del;
    public bool is_del
    {
        get { return _is_del; }
        set { _is_del = value; }
    }

    public bool INSERT_Country()
    {
        try
        {
            return objDA_Country.INSERT_Country(this);
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
            if (objDA_Country.SELECT_ALL_Country(ref dt))
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

    public bool DELETE_RECORDS_FROM_Country()
    {
        try
        {
            if (objDA_Country.DELETE_RECORDS_FROM_Country(this))
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

    public bool GET_RECORDS_FROM_Country(ref DataTable dt)
    {
        try
        {
            if (objDA_Country.GET_RECORDS_FROM_Country(this, ref dt))
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

    public bool UPDATE_Country()
    {
        try
        {
            if (objDA_Country.UPDATE_Country(this))
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

    public bool CHK_RECORDS_FROM_Country(ref DataTable dt)
    {
        try
        {
            if (objDA_Country.CHK_RECORDS_FROM_Country(this, ref dt))
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

    //  *****************************************************************************
    //  You Can use following code to fillup form
    //  *****************************************************************************
    //  objBA_Country.countryId = "";
    //  objBA_Country.country_name = "";
    //  objBA_Country.created_date = "";
    //  objBA_Country.modify_date = "";
    //  objBA_Country.created_by = "";
    //  objBA_Country.modify_by = "";
    //  objBA_Country.is_del = "";

}