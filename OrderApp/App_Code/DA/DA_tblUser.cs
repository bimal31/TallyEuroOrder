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
/// Summary description for DA_tblUser
/// <summary>
public class DA_tblUser : DALBase
{
    public DA_tblUser()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool INSERT_tblUser(BA_tblUser objBA_tblUser)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@UserName", objBA_tblUser.UserName);
            p[1] = new SqlParameter("@Pwd", objBA_tblUser.Pwd);
            p[2] = new SqlParameter("@FirstName", objBA_tblUser.FirstName);
            p[3] = new SqlParameter("@MiddleName", objBA_tblUser.MiddleName);
            p[4] = new SqlParameter("@LastName", objBA_tblUser.LastName);
            p[5] = new SqlParameter("@UserType", objBA_tblUser.UserType);
            p[6] = new SqlParameter("@PhoneNo", objBA_tblUser.PhoneNo);
            p[7] = new SqlParameter("@MobileNo", objBA_tblUser.MobileNo);
            //p[8]= new SqlParameter("@CreateBy",objBA_tblUser.CreateBy);
            //p[9]= new SqlParameter("@CreateData",objBA_tblUser.CreateData);
            //p[10]= new SqlParameter("@UpdateBy",objBA_tblUser.UpdateBy);
            //p[11]= new SqlParameter("@UpdateDate",objBA_tblUser.UpdateDate);
            bool flag = this.Execute_NonQuery("sproc_INSERT_tblUser", p);
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

    public bool UPDATE_tblUser(BA_tblUser objBA_tblUser)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[10];
            p[0] = new SqlParameter("@UserID", objBA_tblUser.UserID);
            p[1] = new SqlParameter("@UserName", objBA_tblUser.UserName);
            p[2] = new SqlParameter("@Pwd", objBA_tblUser.Pwd);
            p[3] = new SqlParameter("@FirstName", objBA_tblUser.FirstName);
            p[4] = new SqlParameter("@MiddleName", objBA_tblUser.MiddleName);
            p[5] = new SqlParameter("@LastName", objBA_tblUser.LastName);
            p[6] = new SqlParameter("@UserType", objBA_tblUser.UserType);
            p[7] = new SqlParameter("@PhoneNo", objBA_tblUser.PhoneNo);
            p[8] = new SqlParameter("@MobileNo", objBA_tblUser.MobileNo);
            p[9] = new SqlParameter("@UpdateBy", objBA_tblUser.UpdateBy);
            return this.Execute_NonQuery("sproc_UPDATE_tblUser", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblUser(BA_tblUser objBA_tblUser, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@UserType", objBA_tblUser.UserType);
            p[1] = new SqlParameter("@UserName", objBA_tblUser.UserName);
            return this.Get_Records("sproc_SELECT_ALL_tblUser", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblUser(BA_tblUser objBA_tblUser, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@UserID", objBA_tblUser.UserID);
            return this.Get_Records("sproc_SELECT_tblUser", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool CHK_RECORDS_FROM_tblUser(BA_tblUser objBA_tblUser, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@UserID", objBA_tblUser.UserID);
            p[1] = new SqlParameter("@UserName", objBA_tblUser.UserName);
            return this.Get_Records("sproc_CHK_tblUserByUserName", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblUser_Login(BA_tblUser objBA_tblUser, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@UserName", objBA_tblUser.UserName);
            p[1] = new SqlParameter("@Password", objBA_tblUser.Pwd);
            return this.Get_Records("sproc_Login_tblUser", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DELETE_RECORDS_FROM_tblUser(BA_tblUser objBA_tblUser)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@UserID", objBA_tblUser.UserID);
            return this.Execute_NonQuery("sproc_DELETE_tblUser", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblUser_Login_API(BA_tblUser objBA_tblUser, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@UserName", objBA_tblUser.UserName);
            p[1] = new SqlParameter("@Password", objBA_tblUser.Pwd);
            return this.Get_Records("sproc_Login_tblUser_api", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public bool UPDATE_tblUser_API(BA_tblUser objBA_tblUser)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@UserID", objBA_tblUser.UserID);
            p[1] = new SqlParameter("@Pwd", objBA_tblUser.Pwd);
            p[2] = new SqlParameter("@FirstName", objBA_tblUser.FirstName);
            p[3] = new SqlParameter("@MiddleName", objBA_tblUser.MiddleName);
            p[4] = new SqlParameter("@LastName", objBA_tblUser.LastName);
            p[5] = new SqlParameter("@PhoneNo", objBA_tblUser.PhoneNo);
            p[6] = new SqlParameter("@MobileNo", objBA_tblUser.MobileNo);
            p[7] = new SqlParameter("@UpdateBy", objBA_tblUser.UpdateBy);
            return this.Execute_NonQuery("sproc_UPDATE_tblUser_API", p);
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
            SqlParameter[] p = new SqlParameter[2];
            return this.Get_Records("sproc_SELECT_ALL_tblUserSalesman", ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }


}