using System;
using System.Data;
using System.Data.SqlClient;

public class DA_tblDealer : DALBase
{
    public DA_tblDealer() { }

    public bool INSERT_tblDealer(BA_tblDealer objBA_tblDealer)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[13];
            p[0] = new SqlParameter("@DealerCode", objBA_tblDealer.DealerCode);
            p[1] = new SqlParameter("@DealerName", objBA_tblDealer.DealerName);
            p[2] = new SqlParameter("@ContactName", objBA_tblDealer.ContactName);
            p[3] = new SqlParameter("@Address", objBA_tblDealer.Address);
            p[4] = new SqlParameter("@Area", objBA_tblDealer.Area);
            p[5] = new SqlParameter("@Pincode", objBA_tblDealer.Pincode);
            p[6] = new SqlParameter("@Phone", objBA_tblDealer.Phone);
            p[7] = new SqlParameter("@GST", objBA_tblDealer.GST);
            p[8] = new SqlParameter("@StateID", objBA_tblDealer.StateID);
            p[9] = new SqlParameter("@GSTPhoto", objBA_tblDealer.GSTPhoto);
            p[10] = new SqlParameter("@VisitCard", objBA_tblDealer.VisitCard);
            p[11] = new SqlParameter("@Isdeleted", objBA_tblDealer.Isdeleted);
            p[12] = new SqlParameter("@CreateBy", objBA_tblDealer.CreateBy);

            bool flag = this.Execute_NonQuery("sproc_INSERT_tblDealer", p);

            if (flag)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public int INSERT_tblDealer_api(BA_tblDealer objBA_tblDealer)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[10];
            p[0] = new SqlParameter("@DealerCode", objBA_tblDealer.DealerCode);
            p[1] = new SqlParameter("@DealerName", objBA_tblDealer.DealerName);
            p[2] = new SqlParameter("@ContactName", objBA_tblDealer.ContactName);
            p[3] = new SqlParameter("@Address", objBA_tblDealer.Address);
            p[4] = new SqlParameter("@Area", objBA_tblDealer.Area);
            p[5] = new SqlParameter("@Pincode", objBA_tblDealer.Pincode);
            p[6] = new SqlParameter("@Phone", objBA_tblDealer.Phone);
            p[7] = new SqlParameter("@GST", objBA_tblDealer.GST);
            p[8] = new SqlParameter("@Isdeleted", objBA_tblDealer.Isdeleted);
            p[9] = new SqlParameter("@CreateBy", objBA_tblDealer.CreateBy);
            object flag = this.Execute_Scalar("sproc_INSERT_tblDealer_api", p);
            int dealerid = Convert.ToInt32(flag);
            if (dealerid > 0)
            {
                return dealerid;
            }
            else
            {
                return dealerid;
            }
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public int INSERT_tblDealer_api_New(BA_tblDealer objBA_tblDealer)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[13];
            p[0] = new SqlParameter("@DealerCode", objBA_tblDealer.DealerCode);
            p[1] = new SqlParameter("@DealerName", objBA_tblDealer.DealerName);
            p[2] = new SqlParameter("@ContactName", objBA_tblDealer.ContactName);
            p[3] = new SqlParameter("@Address", objBA_tblDealer.Address);
            p[4] = new SqlParameter("@Area", objBA_tblDealer.Area);
            p[5] = new SqlParameter("@Pincode", objBA_tblDealer.Pincode);
            p[6] = new SqlParameter("@Phone", objBA_tblDealer.Phone);
            p[7] = new SqlParameter("@GST", objBA_tblDealer.GST);
            p[8] = new SqlParameter("@Isdeleted", objBA_tblDealer.Isdeleted);
            p[9] = new SqlParameter("@CreateBy", objBA_tblDealer.CreateBy);
            p[10] = new SqlParameter("@StateID", objBA_tblDealer.StateID);
            p[11] = new SqlParameter("@GSTPhoto", objBA_tblDealer.GSTPhoto);
            p[12] = new SqlParameter("@VisitCard", objBA_tblDealer.VisitCard);


            object flag = this.Execute_Scalar("sproc_INSERT_tblDealer_api_new", p);
            int dealerid = Convert.ToInt32(flag);
            if (dealerid > 0)
            {
                return dealerid;
            }
            else
            {
                return dealerid;
            }
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public bool UPDATE_tblDealer(BA_tblDealer objBA_tblDealer)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[14];
            p[0] = new SqlParameter("@DealerID", objBA_tblDealer.DealerID);
            p[1] = new SqlParameter("@DealerCode", objBA_tblDealer.DealerCode);
            p[2] = new SqlParameter("@DealerName", objBA_tblDealer.DealerName);
            p[3] = new SqlParameter("@ContactName", objBA_tblDealer.ContactName);
            p[4] = new SqlParameter("@Address", objBA_tblDealer.Address);
            p[5] = new SqlParameter("@Area", objBA_tblDealer.Area);
            p[6] = new SqlParameter("@Pincode", objBA_tblDealer.Pincode);
            p[7] = new SqlParameter("@Phone", objBA_tblDealer.Phone);
            p[8] = new SqlParameter("@GST", objBA_tblDealer.GST);
            p[9] = new SqlParameter("@StateID", objBA_tblDealer.StateID);
            p[10] = new SqlParameter("@GSTPhoto", objBA_tblDealer.GSTPhoto);
            p[11] = new SqlParameter("@VisitCard", objBA_tblDealer.VisitCard);
            p[12] = new SqlParameter("@Isdeleted", objBA_tblDealer.Isdeleted);
            p[13] = new SqlParameter("@UpdateBy", objBA_tblDealer.UpdateBy);

            return this.Execute_NonQuery("sproc_UPDATE_tblDealer", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblDealer(BA_tblDealer objBA_tblDealer, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@DealerCode", objBA_tblDealer.IsDealerCode);
            p[1] = new SqlParameter("@Search", objBA_tblDealer.DealerName);
            return this.Get_Records("sproc_SELECT_ALL_tblDealer", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblDealer(BA_tblDealer objBA_tblDealer, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@DealerID", objBA_tblDealer.DealerID);
            return this.Get_Records("sproc_SELECT_tblDealer", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblDealer_ByCode(BA_tblDealer objBA_tblDealer, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@DealerCode", objBA_tblDealer.DealerCode);
            return this.Get_Records("sproc_SELECT_tblDealerByCode", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DELETE_RECORDS_FROM_tblDealer(BA_tblDealer objBA_tblDealer)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@DealerID", objBA_tblDealer.DealerID);
            return this.Execute_NonQuery("sproc_DELETE_tblDealer", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblDealerPayPending(BA_tblDealer objBA_tblDealer, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@DealerCode", objBA_tblDealer.DealerCode);
            return this.Get_Records("sproc_SELECT_DealerPayPending", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool UPDATE_DealerPayPenidng(BA_tblDealer objBA_tblDealer)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@DealerCode", objBA_tblDealer.DealerCode);
            p[1] = new SqlParameter("@UpdateBy", objBA_tblDealer.UpdateBy);
            return this.Execute_NonQuery("sproc_UPDATE_DealerPayPenidng", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}