using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for BA_tblUser
/// </summary>
public class BA_tblUser
{
    DA_tblUser objDA_tblUser = new DA_tblUser();

    public BA_tblUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private int _UserID;
    public int UserID
    {
        get { return _UserID; }
        set { _UserID = value; }
    }
    private string _UserName;
    public string UserName
    {
        get { return _UserName; }
        set { _UserName = value; }
    }
    private string _Pwd;
    public string Pwd
    {
        get { return _Pwd; }
        set { _Pwd = value; }
    }
    private string _FirstName;
    public string FirstName
    {
        get { return _FirstName; }
        set { _FirstName = value; }
    }
    private string _MiddleName;
    public string MiddleName
    {
        get { return _MiddleName; }
        set { _MiddleName = value; }
    }
    private string _LastName;
    public string LastName
    {
        get { return _LastName; }
        set { _LastName = value; }
    }
    private string _UserType;
    public string UserType
    {
        get { return _UserType; }
        set { _UserType = value; }
    }
    private string _PhoneNo;
    public string PhoneNo
    {
        get { return _PhoneNo; }
        set { _PhoneNo = value; }
    }
    private string _MobileNo;
    public string MobileNo
    {
        get { return _MobileNo; }
        set { _MobileNo = value; }
    }
    private int _CreateBy;
    public int CreateBy
    {
        get { return _CreateBy; }
        set { _CreateBy = value; }
    }
    private string _CreateData;
    public string CreateData
    {
        get { return _CreateData; }
        set { _CreateData = value; }
    }
    private int _UpdateBy;
    public int UpdateBy
    {
        get { return _UpdateBy; }
        set { _UpdateBy = value; }
    }
    private string _UpdateDate;
    public string UpdateDate
    {
        get { return _UpdateDate; }
        set { _UpdateDate = value; }
    }

    public bool INSERT_tblUser()
    {
        try
        {
            return objDA_tblUser.INSERT_tblUser(this);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblUser(ref DataTable dt)
    {
        try
        {
            if (objDA_tblUser.SELECT_ALL_tblUser(this, ref dt))
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

    public bool DELETE_RECORDS_FROM_tblUser()
    {
        try
        {
            if (objDA_tblUser.DELETE_RECORDS_FROM_tblUser(this))
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

    public bool GET_RECORDS_FROM_tblUser(ref DataTable dt)
    {
        try
        {
            if (objDA_tblUser.GET_RECORDS_FROM_tblUser(this, ref dt))
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

    public bool CHK_RECORDS_FROM_tblUser(ref DataTable dt)
    {
        try
        {
            if (objDA_tblUser.CHK_RECORDS_FROM_tblUser(this, ref dt))
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

    public bool GET_RECORDS_FROM_tblUser_Login(ref DataTable dt)
    {
        try
        {
            if (objDA_tblUser.GET_RECORDS_FROM_tblUser_Login(this, ref dt))
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

    public bool UPDATE_tblUser()
    {
        try
        {
            if (objDA_tblUser.UPDATE_tblUser(this))
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



    public bool GET_RECORDS_FROM_tblUser_Login_API(ref DataTable dt)
    {
        try
        {
            if (objDA_tblUser.GET_RECORDS_FROM_tblUser_Login_API(this, ref dt))
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

    public bool UPDATE_tblUser_API()
    {
        try
        {
            if (objDA_tblUser.UPDATE_tblUser_API(this))
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


    public bool SELECT_ALL_tblUserSalesman(ref DataTable dt)
    {
        try
        {
            if (objDA_tblUser.SELECT_ALL_tblUserSalesman(ref dt))
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
    //  objBA_tblUser.UserID = "";
    //  objBA_tblUser.UserName = "";
    //  objBA_tblUser.Pwd = "";
    //  objBA_tblUser.FirstName = "";
    //  objBA_tblUser.MiddleName = "";
    //  objBA_tblUser.LastName = "";
    //  objBA_tblUser.UserType = "";
    //  objBA_tblUser.PhoneNo = "";
    //  objBA_tblUser.MobileNo = "";
    //  objBA_tblUser.CreateBy = "";
    //  objBA_tblUser.CreateData = "";
    //  objBA_tblUser.UpdateBy = "";
    //  objBA_tblUser.UpdateDate = "";

}