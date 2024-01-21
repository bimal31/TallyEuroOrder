using System;
using System.Data;
using System.Configuration;
using System.Web;

/// <summary>
/// Summary description for BA_States
/// </summary>
public class BA_States
{
    DA_States objDA_States = new DA_States();

    public BA_States()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private long _state_id;
    public long state_id
    {
        get { return _state_id; }
        set { _state_id = value; }
    }
    private string _state_name;
    public string state_name
    {
        get { return _state_name; }
        set { _state_name = value; }
    }
    private long _country_id;
    public long country_id
    {
        get { return _country_id; }
        set { _country_id = value; }
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


    private string _country_name;
    public string country_name
    {
        get { return _country_name; }
        set { _country_name = value; }
    }


    public bool INSERT_States()
    {
        try
        {
            return objDA_States.INSERT_States(this);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_States(ref DataTable dt)
    {
        try
        {
            if (objDA_States.SELECT_ALL_States(this,ref dt))
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

    public bool SELECT_States_ByCountry(ref DataTable dt)
    {
        try
        {
            if (objDA_States.SELECT_States_ByCountry(this, ref dt))
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

    public bool DELETE_RECORDS_FROM_States()
    {
        try
        {
            if (objDA_States.DELETE_RECORDS_FROM_States(this))
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

    public bool GET_RECORDS_FROM_States(ref DataTable dt)
    {
        try
        {
            if (objDA_States.GET_RECORDS_FROM_States(this, ref dt))
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

    public bool UPDATE_States()
    {
        try
        {
            if (objDA_States.UPDATE_States(this))
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


    public bool CheckCoutnryWise_States(ref DataTable dt)
    {
        try
        {
            if (objDA_States.CheckCoutnryWise_States(this, ref dt))
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
    //  objBA_States.state_id = "";
    //  objBA_States.state_name = "";
    //  objBA_States.country_id = "";
    //  objBA_States.created_date = "";
    //  objBA_States.modify_date = "";
    //  objBA_States.created_by = "";
    //  objBA_States.modify_by = "";
    //  objBA_States.is_del = "";

}