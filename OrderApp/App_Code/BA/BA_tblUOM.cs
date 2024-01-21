using System;
using System.Data;

public class BA_tblUOM
{
    DA_tblUOM objDA_tbluom = new DA_tblUOM();

    public BA_tblUOM() { }

    private long _uomId;
    public long uomId
    {
        get { return _uomId; }
        set { _uomId = value; }
    }
    private string _uomName;
    public string uomName
    {
        get { return _uomName; }
        set { _uomName = value; }
    }
    private string _uomdescription;
    public string uomdescription
    {
        get { return _uomdescription; }
        set { _uomdescription = value; }
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

    public bool INSERT_tbluom()
    {
        try
        {
            return objDA_tbluom.INSERT_tbluom(this);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tbluom(ref DataTable dt)
    {
        try
        {
            if (objDA_tbluom.SELECT_ALL_tbluom(this, ref dt))
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

    public bool DELETE_RECORDS_FROM_tbluom()
    {
        try
        {
            if (objDA_tbluom.DELETE_RECORDS_FROM_tbluom(this))
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

    public bool GET_RECORDS_FROM_tbluom(ref DataTable dt)
    {
        try
        {
            if (objDA_tbluom.GET_RECORDS_FROM_tbluom(this, ref dt))
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

    public bool UPDATE_tbluom()
    {
        try
        {
            if (objDA_tbluom.UPDATE_tbluom(this))
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

    public bool CHK_RECORDS_FROM_uomName(ref DataTable dt)
    {
        try
        {
            if (objDA_tbluom.CHK_RECORDS_FROM_uomName(this, ref dt))
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
    //  objBA_tbluom.uomId = "";
    //  objBA_tbluom.uomName = "";
    //  objBA_tbluom.uomdescription = "";
    //  objBA_tbluom.created_date = "";
    //  objBA_tbluom.modify_date = "";
    //  objBA_tbluom.created_by = "";
    //  objBA_tbluom.modify_by = "";
    //  objBA_tbluom.is_del = "";
}