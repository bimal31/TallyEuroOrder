using System;
using System.Data;

public class BA_tblScheme
{
    DA_tblScheme objDA_tblScheme = new DA_tblScheme();

    public BA_tblScheme() { }

    private long _SchemeId;
    public long SchemeId
    {
        get { return _SchemeId; }
        set { _SchemeId = value; }
    }
    private string _SchemeName;
    public string SchemeName
    {
        get { return _SchemeName; }
        set { _SchemeName = value; }
    }
    private string _Schemedescription;
    public string Schemedescription
    {
        get { return _Schemedescription; }
        set { _Schemedescription = value; }
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

    public bool INSERT_tblScheme()
    {
        try
        {
            return objDA_tblScheme.INSERT_tblScheme(this);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblScheme(ref DataTable dt)
    {
        try
        {
            if (objDA_tblScheme.SELECT_ALL_tblScheme(this, ref dt))
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


    public bool SELECT_ALL_tblSchemeAPI(long ProductPckID, long StateId, ref DataTable dt)
    {
        try
        {
            if (objDA_tblScheme.SELECT_ALL_tblSchemeAPI(ProductPckID, StateId, ref dt))
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

    public bool DELETE_RECORDS_FROM_tblScheme()
    {
        try
        {
            if (objDA_tblScheme.DELETE_RECORDS_FROM_tblScheme(this))
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

    public bool GET_RECORDS_FROM_tblScheme(ref DataTable dt)
    {
        try
        {
            if (objDA_tblScheme.GET_RECORDS_FROM_tblScheme(this, ref dt))
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

    public bool UPDATE_tblScheme()
    {
        try
        {
            if (objDA_tblScheme.UPDATE_tblScheme(this))
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

    public bool CHK_RECORDS_FROM_SchemeName(ref DataTable dt)
    {
        try
        {
            if (objDA_tblScheme.CHK_RECORDS_FROM_SchemeName(this, ref dt))
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
    //  objBA_tblScheme.SchemeId = "";
    //  objBA_tblScheme.SchemeName = "";
    //  objBA_tblScheme.Schemedescription = "";
    //  objBA_tblScheme.created_date = "";
    //  objBA_tblScheme.modify_date = "";
    //  objBA_tblScheme.created_by = "";
    //  objBA_tblScheme.modify_by = "";
    //  objBA_tblScheme.is_del = "";
}