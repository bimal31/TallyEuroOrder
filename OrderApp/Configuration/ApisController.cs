using Newtonsoft.Json;
using OrderApp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace OrderApp
{
    [RoutePrefix("api")]
    public class ApisController : ApiController
    {
        public ApisController()
        {

        }

        #region Login Page
        [HttpGet]
        [Route("Login/{userName}/{password}")]
        public WSResponseObject LoginUser(string userName, string password)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jSearializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            WSResponseObject response = new WSResponseObject();
            BA_tblUser objUser = new BA_tblUser();
            var blankarray = new BA_tblUser();
            dynamic blankData = "";
            response.total_records = 0;
            response.Message = "";
            try
            {

                if (userName.Trim() == "")
                {
                    response.status = 1;
                    objUser = newobj();
                    response.Data = objUser;
                    response.Message = "Please Enter UserName";

                }
                else if (password.Trim() == "")
                {
                    response.status = 1;
                    objUser = newobj();
                    response.Data = objUser;
                    response.Message = "Please Enter Password";
                }


                Common cmn = new Common();
                DataTable dt = new DataTable();
                objUser.UserName = userName.Trim();
                objUser.Pwd = cmn.Encrypt(password.Trim());
                objUser.GET_RECORDS_FROM_tblUser_Login_API(ref dt);
                if (dt != null)
                {

                    if (dt.Rows.Count > 0)
                    {


                        if (Convert.ToString(dt.Rows[0]["UserType"]).ToLower() == "salesman")
                        {

                            objUser.UserID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                            objUser.UserName = Convert.ToString(dt.Rows[0]["UserName"]);
                            objUser.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                            objUser.MiddleName = Convert.ToString(dt.Rows[0]["MiddleName"]);
                            objUser.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                            objUser.UserType = Convert.ToString(dt.Rows[0]["UserType"]);
                            objUser.PhoneNo = Convert.ToString(dt.Rows[0]["PhoneNo"]);
                            objUser.MobileNo = Convert.ToString(dt.Rows[0]["MobileNo"]);
                            objUser.CreateData = "";
                            objUser.UpdateDate = "";

                            response.status = 0;
                            response.Message = "Success";
                            response.Data = objUser;
                        }
                        else
                        {



                            response.status = 1;
                            response.Message = "you are not access this app.";
                            objUser = newobj();
                            response.Data = objUser;
                        }
                    }
                    else
                    {

                        response.status = 1;
                        response.Message = "Username not found.";
                        objUser = newobj();
                        response.Data = objUser;
                    }


                }
                else
                {

                    response.status = 1;
                    response.Message = "Username or password is not valid.";
                    //response.Data = blankarray;
                    objUser = newobj();
                    response.Data = objUser;

                }
            }
            catch (Exception ex)
            {

                response.status = 1;
                response.Message = (("Error LoginUser: " + ex.Message) ?? "");
                objUser = newobj();
                response.Data = objUser;

            }

            return response;
        }

        public BA_tblUser newobj()
        {
            BA_tblUser objUser = new BA_tblUser();
            objUser.UserID = 0;
            objUser.UserName = "";
            objUser.Pwd = "";
            objUser.FirstName = "";
            objUser.MiddleName = "";
            objUser.LastName = "";
            objUser.UserType = "";
            objUser.PhoneNo = "";
            objUser.MobileNo = "";
            objUser.CreateData = "";
            objUser.UpdateDate = "";
            return objUser;
        }
        #endregion

        #region OrderList
        [HttpGet]
        [Route("OrderList/{dFrom}/{dTo}/{UserId}")]
        public WSResponseObject OrderList(string dFrom, string dTo, int UserId)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {
                DateTime FromDt = Convert.ToDateTime(dFrom, CultureInfo.InvariantCulture);
                DateTime ToDt = Convert.ToDateTime(dTo, CultureInfo.InvariantCulture);

                DataTable dt = new DataTable();
                BA_tblOrder objBA_tblOrder = new BA_tblOrder();
                objBA_tblOrder.FromDate = FromDt;

                DateTime ToDate = Convert.ToDateTime(dTo);
                ToDate = Convert.ToDateTime(new DateTime(ToDate.Year, ToDate.Month, ToDate.Day, 23, 59, 59).ToString("MM/dd/yyyy hh:mm:ss tt"));
                objBA_tblOrder.ToDate = ToDate;
                objBA_tblOrder.CreateBy = UserId;

                objBA_tblOrder.GET_Order_API(ref dt);


                if (dt != null && dt.Rows.Count > 0)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = dt.Rows.Count;
                    response.Data = dt;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error OrderList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion

        public DateTime getDate(string date)
        {
            DateTime dt;
            try
            {
                //  dt = DateTime.ParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //var formats = new[] { "dd-MM-yyyy" };

                //  dt = DateTime.ParseExact(date.Replace("-", "/"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                dt = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                return dt;
            }
            catch (Exception)
            {
                dt = new DateTime();
                return dt;
            }
        }

        #region FreeOrderList
        [HttpGet]
        [Route("FreeOrderList/{dFrom}/{dTo}/{UserId}")]
        public WSResponseObject FreeOrderList(string dFrom, string dTo, int UserId)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {
                DateTime FromDt = Convert.ToDateTime(dFrom, CultureInfo.InvariantCulture);
                DateTime ToDt = Convert.ToDateTime(dTo, CultureInfo.InvariantCulture);

                DataTable dt = new DataTable();
                BA_tblOrder objBA_tblOrder = new BA_tblOrder();
                objBA_tblOrder.FromDate = FromDt;

                DateTime ToDate = Convert.ToDateTime(dTo);
                ToDate = Convert.ToDateTime(new DateTime(ToDate.Year, ToDate.Month, ToDate.Day, 23, 59, 59).ToString("MM/dd/yyyy hh:mm:ss tt"));
                objBA_tblOrder.ToDate = ToDate;
                objBA_tblOrder.CreateBy = UserId;


                objBA_tblOrder.GET_FreeOrder_API(ref dt);


                if (dt != null && dt.Rows.Count > 0)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = dt.Rows.Count;
                    response.Data = dt;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error OrderList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion

        #region OrderList
        [HttpGet]
        [Route("OrderView/{OrderId}")]
        public WSResponseObject OrderView(int OrderId)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {

                DataSet ds = new DataSet();
                BA_tblOrder objBA_tblOrder = new BA_tblOrder();

                objBA_tblOrder.OrderID = Convert.ToString(OrderId);

                objBA_tblOrder.GET_OrderView_API(ref ds);


                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    string json = Common.GetJSON(ds);
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = 1;
                    response.Data = ds;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error OrderList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }


        [HttpGet]
        [Route("OrderViewNew/{OrderId}")]
        public WSResponseObject OrderViewNew(int OrderId)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {

                DataSet ds = new DataSet();
                BA_tblOrder objBA_tblOrder = new BA_tblOrder();

                objBA_tblOrder.OrderID = Convert.ToString(OrderId);

                objBA_tblOrder.GET_OrderView_API(ref ds);


                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    string json = Common.GetJSON(ds);
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = 1;
                    response.Data = ds;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error OrderList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion

        #region Free OrderList Get
        [HttpGet]
        [Route("FreeOrderView/{OrderId}")]
        public WSResponseObject FreeOrderView(int OrderId)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {

                DataSet ds = new DataSet();
                BA_tblOrder objBA_tblOrder = new BA_tblOrder();

                objBA_tblOrder.OrderID = Convert.ToString(OrderId);

                objBA_tblOrder.GET_FreeOrderView_API(ref ds);
                DataTable dts = ds.Tables[0];

                bool ischange = ChangeColumnDataType(dts, "OrderDate", typeof(string));
                ChangeColumnDataType(dts, "PurchaseDurationFromDate", typeof(string));
                ChangeColumnDataType(dts, "PurchaseDurationToDate", typeof(string));
                foreach (DataRow row in ds.Tables[0].Rows)
                {

                    DateTime OrderDate = DateTime.Parse(row["OrderDate"].ToString());
                    row["OrderDate"] = OrderDate.ToString("yyyy-MM-dd");
                    DateTime PurchaseDurationFromDate = DateTime.Parse(row["PurchaseDurationFromDate"].ToString());
                    row["PurchaseDurationFromDate"] = PurchaseDurationFromDate.ToString("yyyy-MM-dd");
                    DateTime PurchaseDurationToDate = DateTime.Parse(row["PurchaseDurationToDate"].ToString());
                    row["PurchaseDurationToDate"] = PurchaseDurationToDate.ToString("yyyy-MM-dd");
                }
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    string json = Common.GetJSON(ds);
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = 1;
                    response.Data = ds;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error OrderList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion

        public static bool ChangeColumnDataType(DataTable table, string columnname, Type newtype)
        {
            if (table.Columns.Contains(columnname) == false)
                return false;

            DataColumn column = table.Columns[columnname];
            if (column.DataType == newtype)
                return true;

            try
            {
                DataColumn newcolumn = new DataColumn("temporary", newtype);
                table.Columns.Add(newcolumn);
                foreach (DataRow row in table.Rows)
                {
                    try
                    {
                        row["temporary"] = Convert.ChangeType(row[columnname], newtype);
                    }
                    catch
                    {
                    }
                }
                table.Columns.Remove(columnname);
                newcolumn.ColumnName = columnname;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        #region Add Dealer
        [HttpPost]
        [Route("AddDealer")]//A For Add And E For Edit
        public WSResponseObject AddDealer(BA_tblDealer list)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new BA_tblDealer();
            response.total_records = 0;
            response.Message = "";
            try
            {

                BA_tblDealer ObjDealer = new BA_tblDealer();
                Common Cmn = new Common();
                ObjDealer = list;
                //ObjDealer.DealerName = list.DealerName;
                //ObjDealer.Address = list.Address;
                //ObjDealer.Area = list.Area;
                //ObjDealer.GST = list.GST;
                //ObjDealer.Phone = list.Phone;
                //ObjDealer.Transport = list.Transport;
                //ObjDealer.CreateBy = list.CreateBy;
                //ObjDealer.Isdeleted = false;
                //ObjDealer.UpdateBy = list.CreateBy;

                int dealerid = ObjDealer.INSERT_tblDealer_api();
                if (dealerid > 0)
                {
                    DataTable dtdeal = new DataTable();
                    ObjDealer.DealerID = dealerid;
                    ObjDealer.GET_RECORDS_FROM_tblDealer(ref dtdeal);
                    if (dtdeal != null && dtdeal.Rows.Count > 0)
                    {

                        ObjDealer.DealerID = Convert.ToInt32(dtdeal.Rows[0]["DealerID"]);
                        ObjDealer.DealerCode = Convert.ToString(dtdeal.Rows[0]["DealerCode"]);
                        ObjDealer.DealerName = Convert.ToString(dtdeal.Rows[0]["DealerName"]);
                        ObjDealer.ContactName = Convert.ToString(dtdeal.Rows[0]["ContactName"]);
                        ObjDealer.Address = Convert.ToString(dtdeal.Rows[0]["Address"]);
                        ObjDealer.Area = Convert.ToString(dtdeal.Rows[0]["Area"]);
                        ObjDealer.Pincode = Convert.ToString(dtdeal.Rows[0]["Pincode"]);
                        ObjDealer.Phone = Convert.ToString(dtdeal.Rows[0]["Phone"]);
                        ObjDealer.GST = Convert.ToString(dtdeal.Rows[0]["GST"]);
                        ObjDealer.CreateDate = "";
                        ObjDealer.UpdateDate = "";
                        if (dtdeal.Rows.Count > 0)
                        {
                            response.status = 0;
                            response.Message = "Success";
                            response.Data = ObjDealer;
                        }
                        else
                        {
                            response.status = 1;
                            response.Message = "Dealer not found.";
                            ObjDealer = retrundealer();
                            response.Data = ObjDealer;
                        }
                    }
                    else
                    {
                        response.status = 1;
                        response.Message = "Dealer does not exist.";
                        ObjDealer = retrundealer();
                        response.Data = ObjDealer;
                    }


                }
                else
                {
                    response.status = 0;
                    response.Message = "Record could not able to store, please try again.";
                    response.Data = blankarray;
                }
            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error AddDealer: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion

        #region View Dealer
        [HttpGet]
        [Route("ViewDealer/{Dealercode}")]
        public WSResponseObject ViewDealer(string Dealercode)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jSearializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            WSResponseObject response = new WSResponseObject();
            BA_tblDealer ObjDealer = new BA_tblDealer();
            DataTable dt = new DataTable();

            //var blankarray = new BA_tblDealer();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {

                if (Dealercode.Trim() == "")
                {
                    response.status = 1;
                    ObjDealer = retrundealer();
                    response.Data = ObjDealer;
                    response.Message = "Please Enter Dealercode";
                }



                ObjDealer.DealerCode = Dealercode;
                ObjDealer.GET_RECORDS_FROM_tblDealer_ByCode(ref dt);

                if (dt != null && dt.Rows.Count > 0)
                {

                    ObjDealer.DealerID = Convert.ToInt32(dt.Rows[0]["DealerID"]);
                    ObjDealer.DealerCode = Convert.ToString(dt.Rows[0]["DealerCode"]);
                    ObjDealer.DealerName = Convert.ToString(dt.Rows[0]["DealerName"]);
                    ObjDealer.ContactName = Convert.ToString(dt.Rows[0]["ContactName"]);
                    ObjDealer.Address = Convert.ToString(dt.Rows[0]["Address"]);
                    ObjDealer.Area = Convert.ToString(dt.Rows[0]["Area"]);
                    ObjDealer.Pincode = Convert.ToString(dt.Rows[0]["Pincode"]);
                    ObjDealer.Phone = Convert.ToString(dt.Rows[0]["Phone"]);
                    ObjDealer.GST = Convert.ToString(dt.Rows[0]["GST"]);
                    ObjDealer.CreateDate = Convert.ToString(dt.Rows[0]["CreateDate"]);
                    ObjDealer.UpdateDate = Convert.ToString(dt.Rows[0]["UpdateDate"]);
                    ObjDealer.StateID = Convert.ToInt32(dt.Rows[0]["StateID"]);
                    ObjDealer.Ispaymentpending = Convert.ToBoolean(dt.Rows[0]["Ispaymentpending"]);
                    if (dt.Rows.Count > 0)
                    {
                        response.status = 0;
                        response.Message = "Success";
                        response.Data = ObjDealer;
                    }
                    else
                    {
                        response.status = 1;
                        response.Message = "Dealer not found.";
                        ObjDealer = retrundealer();
                        response.Data = ObjDealer;
                    }
                }
                else
                {
                    response.status = 1;
                    response.Message = "Dealer does not exist.";
                    ObjDealer = retrundealer();
                    response.Data = ObjDealer;
                }
            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error ViewDealer : " + ex.Message) ?? "");

                ObjDealer = retrundealer();
                response.Data = ObjDealer;
            }
            return response;
        }


        [HttpGet]
        [Route("ViewDealerNew/{Dealercode}")]
        public WSResponseObject ViewDealerNew(string Dealercode)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jSearializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            WSResponseObject response = new WSResponseObject();
            BA_tblDealer ObjDealer = new BA_tblDealer();
            DataTable dt = new DataTable();

            //var blankarray = new BA_tblDealer();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {

                if (Dealercode.Trim() == "")
                {
                    response.status = 1;
                    ObjDealer = retrundealer();
                    response.Data = ObjDealer;
                    response.Message = "Please Enter Dealercode";
                }



                ObjDealer.DealerCode = Dealercode;
                ObjDealer.GET_RECORDS_FROM_tblDealer_ByCode(ref dt);

                if (dt != null && dt.Rows.Count > 0)
                {

                    ObjDealer.DealerID = Convert.ToInt32(dt.Rows[0]["DealerID"]);
                    ObjDealer.DealerCode = Convert.ToString(dt.Rows[0]["DealerCode"]);
                    ObjDealer.DealerName = Convert.ToString(dt.Rows[0]["DealerName"]);
                    ObjDealer.ContactName = Convert.ToString(dt.Rows[0]["ContactName"]);
                    ObjDealer.Address = Convert.ToString(dt.Rows[0]["Address"]);
                    ObjDealer.Area = Convert.ToString(dt.Rows[0]["Area"]);
                    ObjDealer.Pincode = Convert.ToString(dt.Rows[0]["Pincode"]);
                    ObjDealer.Phone = Convert.ToString(dt.Rows[0]["Phone"]);
                    ObjDealer.GST = Convert.ToString(dt.Rows[0]["GST"]);
                    ObjDealer.StateID = Convert.ToInt32(dt.Rows[0]["StateID"]);
                    ObjDealer.CreateDate = Convert.ToString(dt.Rows[0]["CreateDate"]);
                    ObjDealer.UpdateDate = Convert.ToString(dt.Rows[0]["UpdateDate"]);
                    ObjDealer.StateID = Convert.ToInt32(dt.Rows[0]["StateID"]);
                    ObjDealer.Ispaymentpending = Convert.ToBoolean(dt.Rows[0]["Ispaymentpending"]);
                    if (dt.Rows.Count > 0)
                    {
                        response.status = 0;
                        response.Message = "Success";
                        response.Data = ObjDealer;
                    }
                    else
                    {
                        response.status = 1;
                        response.Message = "Dealer not found.";
                        ObjDealer = retrundealer();
                        response.Data = ObjDealer;
                    }
                }
                else
                {
                    response.status = 1;
                    response.Message = "Dealer does not exist.";
                    ObjDealer = retrundealer();
                    response.Data = ObjDealer;
                }
            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error ViewDealer : " + ex.Message) ?? "");

                ObjDealer = retrundealer();
                response.Data = ObjDealer;
            }
            return response;
        }

        public BA_tblDealer retrundealer()
        {
            BA_tblDealer ObjDealer = new BA_tblDealer();
            ObjDealer.DealerID = 0;
            ObjDealer.DealerCode = "";
            ObjDealer.DealerName = "";
            ObjDealer.Address = "";
            ObjDealer.Area = "";
            ObjDealer.Phone = "";
            ObjDealer.ContactName = "";
            ObjDealer.Pincode = "";
            ObjDealer.GST = "";
            ObjDealer.CreateDate = "";
            ObjDealer.UpdateDate = "";
            ObjDealer.StateID = 0;
            return ObjDealer;
        }
        #endregion

        #region ProductList
        [HttpGet]
        [Route("ProductList")]
        public WSResponseObject ProductList()
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {
                DataTable dt = new DataTable();
                BA_tblProduct objUser = new BA_tblProduct();
                objUser.SELECT_ALL_tblProduct_API(ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = dt.Rows.Count;
                    response.Data = dt;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error ProductList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }


        [HttpGet]
        [Route("ProductListNew/{Stateid}")]
        public WSResponseObject ProductListNew(long Stateid)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {
                DataTable dt = new DataTable();
                BA_tblProduct objUser = new BA_tblProduct();
                objUser.SELECT_ALL_tblProduct_APINew(Stateid, ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = dt.Rows.Count;
                    response.Data = dt;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error ProductList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion

        #region Product package details
        [HttpGet]
        [Route("Productpackage/{ProductId}")]
        public WSResponseObject ProductpackageList(int ProductId)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {

                BA_tblProductPacking ObjProductPck = new BA_tblProductPacking();
                DataTable dt = new DataTable();

                ObjProductPck.ProductID = Convert.ToString(ProductId);
                ObjProductPck.GET_RECORDS_FROM_tblProductPacking_API(ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = dt.Rows.Count;
                    response.Data = dt;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error ProductpackageList : " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }


        [HttpGet]
        [Route("ProductpackageNew/{ProductID}/{StateId}")]

        public WSResponseObject ProductpackageNew(long ProductID, long StateId)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {

                BA_tblProductPacking ObjProductPck = new BA_tblProductPacking();
                DataTable dt = new DataTable();

                ObjProductPck.GET_RECORDS_FROM_tblProductPacking_APINew(ProductID, StateId, ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = dt.Rows.Count;
                    response.Data = dt;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error ProductpackageList : " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion

        #region Add Order
        [HttpPost]
        [Route("AddOrder")]//A For Add And E For Edit
        public WSResponseObject AddOrder(AddOrderList listOrder)
        // public WSResponseObject AddOrder(BA_tblOrder listOrder)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {
                BA_tblOrder ObjOrder = new BA_tblOrder();

                ObjOrder.OrderType = listOrder.Order.OrderType;
                ObjOrder.DealerId = listOrder.Order.DealerId;

                ObjOrder.FreeSchemeFrom = listOrder.Order.FreeSchemeFrom;
                ObjOrder.FreeSchemeTO = listOrder.Order.FreeSchemeTO;
                ObjOrder.TotalKgGm = listOrder.Order.TotalKgGm;

                ObjOrder.Transport = listOrder.Order.Transport;
                ObjOrder.Other = listOrder.Order.Other;
                ObjOrder.POP = listOrder.Order.POP;
                ObjOrder.SiteDelivery = listOrder.Order.SiteDelivery;


                ObjOrder.ParentOrderId = listOrder.Order.ParentOrderId;
                ObjOrder.CreateBy = listOrder.Order.CreateBy;

                string XML = "";
                XML = "<OrderProduct>";
                if (listOrder.OrderProductDetails.Count > 0)
                {
                    foreach (var item in listOrder.OrderProductDetails)
                    {
                        XML += "<TABLE>";
                        XML += "<ProductId>" + item.ProductId + "</ProductId>";
                        XML += "<ProductPckIds>" + item.ProductPckID + "</ProductPckIds>";
                        XML += "<ProductPck>" + item.ProductPck + "</ProductPck>";
                        XML += "<PackingNos>" + item.PackingNos + "</PackingNos>";
                        XML += "<PackingType>" + item.PackingType + "</PackingType>";
                        XML += "<BoxORNos>" + item.BoxORNos + "</BoxORNos>";
                        XML += "<PckTotalKg>" + item.PckTotalKg + "</PckTotalKg>";
                        XML += "<ProductQty>" + item.ProductQty + "</ProductQty>";
                        XML += "<IsScheme>" + item.IsScheme + "</IsScheme>";
                        XML += "<Scheme>" + item.Scheme + "</Scheme>";
                        XML += "</TABLE>";

                    }


                }
                XML += "</OrderProduct>";
                ObjOrder.xmlProd = XML;

                int ReturnId = 0;
                bool output;
                output = ObjOrder.INSERT_tblOrder_API(ref ReturnId);
                if (output == true)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.Data = blankarray;
                }
                else
                {
                    response.status = 0;
                    response.Message = "Record could not able to store, please try again.";
                    response.Data = blankarray;
                }
            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error AddOrder : " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }



        [HttpPost]
        [Route("AddOrderNew")]//A For Add And E For Edit
        public WSResponseObject AddOrderNew(AddOrderList listOrder)
        // public WSResponseObject AddOrder(BA_tblOrder listOrder)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {
                BA_tblOrder ObjOrder = new BA_tblOrder();

                ObjOrder.OrderType = listOrder.Order.OrderType;
                ObjOrder.DealerId = listOrder.Order.DealerId;

                ObjOrder.FreeSchemeFrom = listOrder.Order.FreeSchemeFrom;
                ObjOrder.FreeSchemeTO = listOrder.Order.FreeSchemeTO;
                ObjOrder.TotalKgGm = listOrder.Order.TotalKgGm;

                ObjOrder.Transport = listOrder.Order.Transport;
                ObjOrder.Other = listOrder.Order.Other;
                ObjOrder.POP = listOrder.Order.POP;
                ObjOrder.SiteDelivery = listOrder.Order.SiteDelivery;


                ObjOrder.ParentOrderId = listOrder.Order.ParentOrderId;
                ObjOrder.CreateBy = listOrder.Order.CreateBy;

                string XML = "";
                XML = "<OrderProduct>";
                if (listOrder.OrderProductDetails.Count > 0)
                {
                    foreach (var item in listOrder.OrderProductDetails)
                    {
                        XML += "<TABLE>";
                        XML += "<ProductId>" + item.ProductId + "</ProductId>";
                        XML += "<ProductPckIds>" + item.ProductPckID + "</ProductPckIds>";
                        XML += "<ProductPck>" + item.ProductPck + "</ProductPck>";
                        XML += "<PackingNos>" + item.PackingNos + "</PackingNos>";
                        XML += "<PackingType>" + item.PackingType + "</PackingType>";
                        XML += "<BoxORNos>" + item.BoxORNos + "</BoxORNos>";
                        XML += "<PckTotalKg>" + item.PckTotalKg + "</PckTotalKg>";
                        XML += "<ProductQty>" + item.ProductQty + "</ProductQty>";
                        XML += "<IsScheme>" + item.IsScheme + "</IsScheme>";
                        XML += "<Scheme>" + item.Scheme + "</Scheme>";
                        XML += "<SchemeId>" + item.SchemeId + "</SchemeId>";
                        XML += "<ProductCode>" + item.ProductCode + "</ProductCode>";
                        XML += "</TABLE>";

                    }


                }
                XML += "</OrderProduct>";
                ObjOrder.xmlProd = XML;

                int ReturnId = 0;
                bool output;
                output = ObjOrder.INSERT_tblOrder_API_New(ref ReturnId);
                if (output == true)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.Data = blankarray;
                }
                else
                {
                    response.status = 0;
                    response.Message = "Record could not able to store, please try again.";
                    response.Data = blankarray;
                }
            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error AddOrder : " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }



        [HttpPost]
        [Route("AddOrderWithBill")]
        public WSResponseObject AddOrderWithBill(AddOrderWithFreeBillList listOrder)
        // public WSResponseObject AddOrder(BA_tblOrder listOrder)
        {
            WSResponseObject response = new WSResponseObject();

            List<FreeOrderProductData> OrderProductDetailDatadtl = new List<FreeOrderProductData>();
            List<FreePurchaseOrderProductData> OrderFreeProductdtl = new List<FreePurchaseOrderProductData>();

            OrderProductDetailDatadtl = listOrder.OrderProductDetails;
            OrderFreeProductdtl = listOrder.OrderFreeProductDetails;

            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {
                BA_tblOrder ObjOrder = new BA_tblOrder();

                ObjOrder.OrderType = listOrder.Order.OrderType;
                ObjOrder.DealerId = listOrder.Order.DealerId;

                ObjOrder.FreeSchemeFrom = listOrder.Order.FreeSchemeFrom;
                ObjOrder.FreeSchemeTO = listOrder.Order.FreeSchemeTO;
                ObjOrder.TotalKgGm = listOrder.Order.TotalKgGm;

                ObjOrder.Transport = listOrder.Order.Transport;
                ObjOrder.Other = listOrder.Order.Other;
                ObjOrder.POP = listOrder.Order.POP;
                ObjOrder.SiteDelivery = listOrder.Order.SiteDelivery;


                ObjOrder.ParentOrderId = listOrder.Order.ParentOrderId;
                ObjOrder.CreateBy = listOrder.Order.CreateBy;

                ObjOrder.SalesId = listOrder.Order.SalesId;
                ObjOrder.OrderID = listOrder.Order.OrderID;

                string XML = "";
                XML = "<OrderProduct>";
                if (listOrder.OrderProductDetails.Count > 0)
                {
                    foreach (var item in listOrder.OrderProductDetails)
                    {
                        XML += "<TABLE>";   
                        XML += "<ProductId>" + item.ProductID + "</ProductId>";
                        XML += "<ProductPckIds>" + item.ProductPckID + "</ProductPckIds>";
                        XML += "<ProductPck>" + item.ProductPck + "</ProductPck>";
                        XML += "<PackingNos>" + item.PackingNos + "</PackingNos>";
                        XML += "<PackingType>" + item.PackingType + "</PackingType>";
                        XML += "<BoxORNos>" + item.BoxORNos + "</BoxORNos>";
                        XML += "<PckTotalKg>" + item.PckTotalKg + "</PckTotalKg>";
                        XML += "<ProductQty>" + item.QTY + "</ProductQty>";
                        XML += "<IsScheme>" + item.isscheme + "</IsScheme>";
                        XML += "<Scheme>" + item.Scheme + "</Scheme>";
                        XML += "<SchemeId>" + item.SchemeId + "</SchemeId>";
                        XML += "<ProductCode>" + item.ProductCode + "</ProductCode>";
                        XML += "</TABLE>";

                    }


                }
                XML += "</OrderProduct>";
                ObjOrder.xmlProd = XML;




                string XMLFreeProduct = "";
                if (listOrder.OrderFreeProductDetails.Count > 0)
                {
                    XMLFreeProduct = "<FreeOrderProduct>";
                    foreach (var itemFree in listOrder.OrderFreeProductDetails)
                    {
                        XMLFreeProduct += "<TABLE>";
                        XMLFreeProduct += "<OrderSrNo>" + Convert.ToInt64(itemFree.OrderSrNo) + "</OrderSrNo>";
                        XMLFreeProduct += "<ProductId>" + Convert.ToInt64(itemFree.ProductID) + "</ProductId>";
                        XMLFreeProduct += "<Fromscheme>" + Convert.ToInt64(itemFree.Fromscheme) + "</Fromscheme>";
                        XMLFreeProduct += "<Toscheme>" + Convert.ToInt64(itemFree.Toscheme) + "</Toscheme>";
                        XMLFreeProduct += "<Totalkg>" + Convert.ToDecimal(itemFree.Totalkg) + "</Totalkg>";
                        XMLFreeProduct += "<FreeSchemeTotalkg>" + Convert.ToDecimal(itemFree.FreeSchemeTotalkg) + "</FreeSchemeTotalkg>";
                        XMLFreeProduct += "</TABLE>";
                    }
                    XMLFreeProduct += "</FreeOrderProduct>";
                }

                ObjOrder.xmlFreeProd = XMLFreeProduct;
                int ReturnId = 0;
                bool output;
                output = ObjOrder.INSERT_tblOrderFreeNew(ref ReturnId);
                if (output == true)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.Data = blankarray;
                }
                else
                {
                    response.status = 0;
                    response.Message = "Record could not able to store, please try again.";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error AddOrder : " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }

        #endregion

        #region Update User
        [HttpPost]
        [Route("UpdateUser")]//A For Add And E For Edit
        public WSResponseObject UpdateUser(BA_tblUser list)
        {
            WSResponseObject response = new WSResponseObject();
            BA_tblUser ObjUser = new BA_tblUser();
            Common Cmn = new Common();
            var blankarray = new BA_tblUser();
            response.total_records = 0;
            response.Message = "";
            try
            {


                ObjUser = list;
                if (list.Pwd != "")
                    ObjUser.Pwd = Cmn.Encrypt(list.Pwd);
                else
                    ObjUser.Pwd = "";

                bool output;
                output = ObjUser.UPDATE_tblUser_API();
                if (output == true)
                {

                    ObjUser.CreateBy = ObjUser.UpdateBy;
                    ObjUser.CreateData = "";
                    ObjUser.UpdateDate = "";
                    response.status = 0;
                    response.Message = "Success";
                    response.Data = ObjUser;
                }
                else
                {
                    ObjUser.UserID = 0;
                    ObjUser.UserName = "";
                    ObjUser.FirstName = "";
                    ObjUser.MiddleName = "";
                    ObjUser.LastName = "";
                    ObjUser.PhoneNo = "";
                    ObjUser.MobileNo = "";
                    response.status = 0;
                    ObjUser.CreateBy = 0;
                    ObjUser.CreateData = "";
                    ObjUser.UpdateBy = 0;
                    ObjUser.UpdateDate = "";
                    ObjUser.UserType = "";

                    response.Message = "Record could not able to store, please try again.";
                    response.Data = ObjUser;
                }
            }
            catch (Exception ex)
            {
                ObjUser.UserID = 0;
                ObjUser.UserName = "";
                ObjUser.FirstName = "";
                ObjUser.MiddleName = "";
                ObjUser.LastName = "";
                ObjUser.PhoneNo = "";
                ObjUser.MobileNo = "";
                ObjUser.CreateBy = 0;
                ObjUser.CreateData = "";
                ObjUser.UpdateBy = 0;
                ObjUser.UpdateDate = "";
                ObjUser.UserType = "";


                response.status = 1;
                response.Message = (("Error UpdateUser: " + ex.Message) ?? "");
                response.Data = ObjUser;
            }
            return response;
        }
        #endregion

        #region Class Member Declaration
        public class AddOrderList
        {
            public BA_tblOrder Order { get; set; }//Day Of Week
            public List<BA_tblOrderProductDetails> OrderProductDetails { get; set; }
        }

        public class AddOrderWithFreeBillList
        {
            public BA_tblOrder Order { get; set; }//Day Of Week
            public List<FreeOrderProductData> OrderProductDetails { get; set; }

            public List<FreePurchaseOrderProductData> OrderFreeProductDetails { get; set; }
        }

        #endregion

        #region Add Dealer Order Free Scheme
        [HttpPost]
        [Route("AddDealerOrderScheme")]
        public WSResponseObject AddDealerOrderScheme(AllDataOrder data)
        {
            int ReturnId = 0;
            string xmlOrderFreeCreates = "";
            string xmlOrderProductDetails = "";
            WSResponseObject response = new WSResponseObject();
            List<OrderProductDetailData> OrderProductDetailDatadtl = new List<OrderProductDetailData>();
            List<OrderFreeProduct> OrderFreeProductdtl = new List<OrderFreeProduct>();
            OrderProductDetailDatadtl = data._BA_Product_dtl;
            OrderFreeProductdtl = data._BA_Free_product;

            BA_tblOrder ObjOrder = new BA_tblOrder();
            ObjOrder = data._BA_tblOrder;
            ObjOrder.SalesId = ObjOrder.CreateBy;
            ObjOrder.OrderType = CommMessage.OrderType_FreeScheme;
            ObjOrder.PurchaseDurationFromDate = DateTime.ParseExact(data._BA_tblOrder.PurchaseDurationFromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));

            ObjOrder.PurchaseDurationToDate = DateTime.ParseExact(data._BA_tblOrder.PurchaseDurationToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
            ObjOrder.PurchaseKgs = Convert.ToDecimal(data._BA_tblOrder.PurchaseKgs);
            ObjOrder.TotalKgGm = Convert.ToDecimal(data._BA_tblOrder.TotalKgGm);
            ObjOrder.CreateBy = Convert.ToInt32(data._BA_tblOrder.CreateBy);

            try
            {
                xmlOrderFreeCreates = xmlOrderFreeCreate(OrderFreeProductdtl);
                xmlOrderProductDetails = xmlOrderProductDetail(OrderProductDetailDatadtl);
                if (xmlOrderFreeCreates != "")
                {
                    ObjOrder.xmlProd = xmlOrderProductDetails;
                    ObjOrder.xmlFreeProd = xmlOrderFreeCreates;

                    ObjOrder.INSERT_tblOrderDealerScheme(ref ReturnId);
                    if (ReturnId != 0 || ReturnId.ToString() != "")
                    {

                        response.status = 0;
                        response.Message = "Success";
                        response.total_records = 0;
                        response.Data = ReturnId;
                    }
                    else
                    {
                        response.status = 1;
                        response.Message = "Data not save";
                        response.total_records = 0;
                        response.Data = ReturnId;
                    }

                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error Free Scheme: " + ex.Message) ?? "");
                response.total_records = 0;
                response.Data = 0;
            }

            return response;
        }

        public static string xmlOrderFreeCreate(List<OrderFreeProduct> OrderFreeProductdtl)
        {
            string XML = "";
            XML = "<OrderFree>";
            try
            {


                for (int i = 0; i < OrderFreeProductdtl.Count; i++)
                {

                    XML += "<TABLE>";
                    XML += "<OrderSrNo>" + OrderFreeProductdtl[i].hdProdSrno + "</OrderSrNo>";
                    XML += "<ProductId>" + Convert.ToInt64(OrderFreeProductdtl[i].item) + "</ProductId>";
                    XML += "<AnnualPurchasQty>" + Convert.ToDecimal(OrderFreeProductdtl[i].PurchaseK) + "</AnnualPurchasQty>";
                    XML += "<FreeSchemeFrom>" + Convert.ToDecimal(OrderFreeProductdtl[i].FromScheme) + "</FreeSchemeFrom>";
                    XML += "<FreeSchemeTO>" + Convert.ToInt64(OrderFreeProductdtl[i].ToScheme) + "</FreeSchemeTO>";

                    XML += "</TABLE>";


                }

            }
            catch (Exception)
            {
                return XML = "";
            }
            return XML += "</OrderFree>";
        }

        public static string xmlOrderProductDetail(List<OrderProductDetailData> OrderProductDetailDatadtl)
        {

            string XML = "";
            try
            {
                decimal totalkg = 0;
                XML = "<OrderProduct>";
                for (int i = 0; i < OrderProductDetailDatadtl.Count; i++)
                {

                    XML += "<TABLE>";
                    XML += "<ProductId>" + Convert.ToInt64(OrderProductDetailDatadtl[i].Productid) + "</ProductId>";
                    if (OrderProductDetailDatadtl[i].ProductPckID != "")
                    {
                        XML += "<ProductPckIds>" + Convert.ToInt64(OrderProductDetailDatadtl[i].ProductPckID) + "</ProductPckIds>";
                    }
                    else
                    {
                        XML += "<ProductPckIds>" + 0 + "</ProductPckIds>";

                    }

                    XML += "<ProductPck>" + OrderProductDetailDatadtl[i].PKG + "</ProductPck>";
                    XML += "<PackingNos>" + Convert.ToInt32(OrderProductDetailDatadtl[i].QTY) + "</PackingNos>";
                    XML += "<PackingType>" + OrderProductDetailDatadtl[i].PackingType + "</PackingType>";
                    XML += "<BoxORNos>NO</BoxORNos>";
                    XML += "<PckTotalKg>" + OrderProductDetailDatadtl[i].PckTotalKg + "</PckTotalKg>";
                    XML += "<ProductQty>" + Convert.ToInt32(OrderProductDetailDatadtl[i].QTY) + "</ProductQty>";
                    XML += "<IsScheme>" + OrderProductDetailDatadtl[i].isscheme + "</IsScheme>";
                    XML += "<Scheme>" + OrderProductDetailDatadtl[i].Schemetext + "</Scheme>";
                    XML += "<SchemeId>" + OrderProductDetailDatadtl[i].SchemeId + "</SchemeId>";
                    XML += "<ProductCode>" + OrderProductDetailDatadtl[i].ProductCode + "</ProductCode>";
                    XML += "<PDtlSrno>" + Convert.ToInt32(OrderProductDetailDatadtl[i].srno) + "</PDtlSrno>";
                    XML += "<FreeOrderSRNO>" + Convert.ToInt32(OrderProductDetailDatadtl[i].arrOrderProductDetailscounterdata) + "</FreeOrderSRNO>";
                    XML += "</TABLE>";
                    // totalkg = totalkg + staticCaltotal(Convert.ToDecimal(OrderProductDetailDatadtl[i].PckTotalKg), Convert.ToString(OrderProductDetailDatadtl[i].PackingType), Convert.ToInt32(OrderProductDetailDatadtl[i].QTY));
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return XML += "</OrderProduct>";


        }

        #endregion

        #region Add Dealer Order Free Scheme New
        [HttpPost]
        [Route("AddDealerOrderSchemeNew")]
        public WSResponseObject AddDealerOrderSchemeNew(AllDataOrder data)
        {
            int ReturnId = 0;
            string xmlOrderFreeCreates = "";
            string xmlOrderProductDetails = "";

            //BA_XmlData ObjBA_XmlData = new BA_XmlData();
            //string s = JsonConvert.SerializeObject(data);
            //ObjBA_XmlData.INSERT_XmlData(xmlOrderProductDetails, 0);

            WSResponseObject response = new WSResponseObject();
            List<OrderProductDetailData> OrderProductDetailDatadtl = new List<OrderProductDetailData>();
            List<OrderFreeProduct> OrderFreeProductdtl = new List<OrderFreeProduct>();
            OrderProductDetailDatadtl = data._BA_Product_dtl;
            OrderFreeProductdtl = data._BA_Free_product;

            BA_tblOrder ObjOrder = new BA_tblOrder();
            ObjOrder = data._BA_tblOrder;

            ObjOrder.SalesId = ObjOrder.CreateBy;
            ObjOrder.OrderType = CommMessage.OrderType_FreeScheme;

            ObjOrder.PurchaseDurationFromDate = DateTime.ParseExact(data._BA_tblOrder.PurchaseDurationFromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
            ObjOrder.PurchaseDurationToDate = DateTime.ParseExact(data._BA_tblOrder.PurchaseDurationToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));

            ObjOrder.PurchaseKgs = Convert.ToDecimal(data._BA_tblOrder.PurchaseKgs);
            ObjOrder.TotalKgGm = Convert.ToDecimal(data._BA_tblOrder.TotalKgGm);
            ObjOrder.CreateBy = Convert.ToInt32(data._BA_tblOrder.CreateBy);

            try
            {
                xmlOrderFreeCreates = xmlOrderFreeCreate(OrderFreeProductdtl);
                xmlOrderProductDetails = xmlOrderProductDetailNew(OrderProductDetailDatadtl);

               
                //ObjBA_XmlData.INSERT_XmlData(xmlOrderFreeCreates, 0);

                
                //ObjBA_XmlData.INSERT_XmlData(xmlOrderProductDetails, 0);

                if (xmlOrderFreeCreates != "")
                {
                    ObjOrder.xmlProd = xmlOrderProductDetails;
                    ObjOrder.xmlFreeProd = xmlOrderFreeCreates;

                    ObjOrder.INSERT_tblOrderDealerSchemeNew(ref ReturnId);
                    if (ReturnId != 0 || ReturnId.ToString() != "")
                    {

                        response.status = 0;
                        response.Message = "Success";
                        response.total_records = 0;
                        response.Data = ReturnId;
                    }
                    else
                    {
                        response.status = 1;
                        response.Message = "Data not save";
                        response.total_records = 0;
                        response.Data = ReturnId;
                    }

                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error Free Scheme: " + ex.Message) ?? "");
                response.total_records = 0;
                response.Data = 0;
            }

            return response;
        }

        public static string xmlOrderProductDetailNew(List<OrderProductDetailData> OrderProductDetailDatadtl)
        {

            string XML = "";
            try
            {

                XML = "<OrderProduct>";
                for (int i = 0; i < OrderProductDetailDatadtl.Count; i++)
                {

                    XML += "<TABLE>";
                    XML += "<ProductId>" + Convert.ToInt64(OrderProductDetailDatadtl[i].Productid) + "</ProductId>";
                    if (OrderProductDetailDatadtl[i].ProductPckID != "")
                    {
                        XML += "<ProductPckIds>" + Convert.ToInt64(OrderProductDetailDatadtl[i].ProductPckID) + "</ProductPckIds>";
                    }
                    else
                    {
                        XML += "<ProductPckIds>" + 0 + "</ProductPckIds>";

                    }

                    XML += "<ProductPck>" + Convert.ToInt32(OrderProductDetailDatadtl[i].PKG) + "</ProductPck>";
                    XML += "<PackingNos>" + Convert.ToInt32(OrderProductDetailDatadtl[i].QTY) + "</PackingNos>";
                    XML += "<PackingType>" + OrderProductDetailDatadtl[i].PackingType + "</PackingType>";
                    XML += "<BoxORNos>NO</BoxORNos>";
                    XML += "<PckTotalKg>" + OrderProductDetailDatadtl[i].PckTotalKg + "</PckTotalKg>";
                    XML += "<ProductQty>" + Convert.ToInt32(OrderProductDetailDatadtl[i].QTY) + "</ProductQty>";
                    XML += "<IsScheme>" + OrderProductDetailDatadtl[i].isscheme + "</IsScheme>";
                    if (OrderProductDetailDatadtl[i].isscheme == "")
                    {
                        XML += "<Scheme></Scheme>";
                    }
                    else
                    {
                        XML += "<Scheme>" + OrderProductDetailDatadtl[i].Schemetext + "</Scheme>";
                    }
                    XML += "<PDtlSrno>" + Convert.ToInt32(OrderProductDetailDatadtl[i].srno) + "</PDtlSrno>";
                    XML += "<FreeOrderSRNO>" + Convert.ToInt32(OrderProductDetailDatadtl[i].arrOrderProductDetailscounterdata) + "</FreeOrderSRNO>";
                    XML += "<SchemeId>" + Convert.ToInt32(OrderProductDetailDatadtl[i].SchemeId) + "</SchemeId>";
                    XML += "<ProductCode>" + OrderProductDetailDatadtl[i].ProductCode + "</ProductCode>";
                    XML += "</TABLE>";
                    // totalkg = totalkg + staticCaltotal(Convert.ToDecimal(OrderProductDetailDatadtl[i].PckTotalKg), Convert.ToString(OrderProductDetailDatadtl[i].PackingType), Convert.ToInt32(OrderProductDetailDatadtl[i].QTY));

                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return XML += "</OrderProduct>";


        }

        #endregion



        [HttpGet]
        [Route("FreeOrderGet/{OrderId}")]
        public WSResponseObject FreeOrderGet(int OrderId)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {

                DataSet ds = new DataSet();
                BA_tblOrder ObjOrder = new BA_tblOrder();
                ObjOrder.OrderID = Convert.ToString(OrderId);
                ObjOrder.GET_RECORDS_FROM_tblOrderByOrderIdNew(ref ds);

                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    string json = Common.GetJSON(ds);
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = 1;
                    response.Data = ds;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error OrderList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }

        #region new enchantment 


        #region StateList
        [HttpGet]
        [Route("StateList")]
        public WSResponseObject StateList()
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {


                DataTable dt = new DataTable();
                BA_States objScheme = new BA_States();
                objScheme.SELECT_ALL_States(ref dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = dt.Rows.Count;
                    response.Data = dt;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error StateList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion

        #region SchemeList
        [HttpGet]
        [Route("SchemeList/{ProductPckID}/{StateId}")]
        public WSResponseObject SchemeList(long ProductPckID, long StateId)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {


                DataTable dt = new DataTable();
                BA_tblScheme objScheme = new BA_tblScheme();
                objScheme.SELECT_ALL_tblSchemeAPI(ProductPckID, StateId, ref dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = dt.Rows.Count;
                    response.Data = dt;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error SchemeList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion

        #region ProductPackingStateScheme
        [HttpGet]
        [Route("ProductPackingStateSchemeList/{StateId}")]
        public WSResponseObject ProductPackingStateSchemeList(long StateId)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new List<string>();
            response.total_records = 0;
            response.Message = "";
            try
            {


                DataTable dt = new DataTable();
                BA_tblProductPackingStateScheme objProductPackingStateScheme = new BA_tblProductPackingStateScheme();
                objProductPackingStateScheme.SELECT_ALL_tblProductPackingStateScheme(StateId, ref dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = dt.Rows.Count;
                    response.Data = dt;
                }
                else
                {
                    response.status = 1;
                    response.Message = "No record found";
                    response.Data = blankarray;
                }

            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error ProductPackingStateSchemeList: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion

        public class ImageModel
        {
            public string base64Image { get; set; }
            public string imagename { get; set; }
        }

        [HttpPost]
        [Route("setimage")]
        public WSResponseObject setimage(ImageModel Imodel)
        {
            string base64Image = Imodel.base64Image;
            string imagename = Imodel.imagename;
            WSResponseObject response = new WSResponseObject();
            try
            {

                string ImgName = imagename;
                if (!string.IsNullOrEmpty(base64Image))
                {
                    //Image image = Base64ToImage(Image.base64Image);
                    String path = HttpContext.Current.Server.MapPath("~/DealerImage"); //Path
                    //Check if directory exist
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                        //If directory doesn't exist then Create it 
                    }
                    string imageName = ImgName + ".jpg";
                    //set the image path
                    string imgPath = Path.Combine(path, imageName);
                    if (base64Image.Contains("data:image"))
                    {
                        //Need To remove some header information at the beginning if image data contains
                        //ImageDataUrl = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD....";
                        //Otherwise, this will give an error.
                        //Remove everything in front of the DataUrl and including the first comma.
                        //ImageDataUrl = "9j/4AAQSkZJRgABAQAAAQABAAD...
                        base64Image = base64Image.Substring(base64Image.LastIndexOf(',') + 1);
                        // removing extra header information 
                    }
                    byte[] imageBytes = Convert.FromBase64String(base64Image);
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                    image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    response.status = 0;
                    response.Message = "Success";
                    response.total_records = 0;
                    response.Data = 1;

                }
                else
                {
                    response.status = 0;
                    response.Message = "Content not found";
                    response.total_records = 0;
                    response.Data = 0;
                }
            }
            catch (Exception ex)
            {
                response.status = 0;
                response.Message = ex.ToString();
                response.total_records = 0;
                response.Data = 0;
            }
            return response;
        }



        #region Add Dealer
        [HttpPost]
        [Route("AddDealerNew")]//A For Add And E For Edit
        public WSResponseObject AddDealerNew(BA_tblDealer list)
        {
            WSResponseObject response = new WSResponseObject();
            var blankarray = new BA_tblDealer();
            response.total_records = 0;
            response.Message = "";
            try
            {

                BA_tblDealer ObjDealer = new BA_tblDealer();
                Common Cmn = new Common();
                ObjDealer = list;

                ObjDealer.GSTPhoto = setimage("GST", ObjDealer.GSTPhotobase64Image, ObjDealer.GSTPhoto);

                ObjDealer.VisitCard = setimage("Card", ObjDealer.VisitCardbase64Image, ObjDealer.VisitCard);
                int dealerid = ObjDealer.INSERT_tblDealer_api_New();
                if (dealerid > 0)
                {
                    DataTable dtdeal = new DataTable();
                    ObjDealer.DealerID = dealerid;
                    ObjDealer.GET_RECORDS_FROM_tblDealer(ref dtdeal);
                    if (dtdeal != null && dtdeal.Rows.Count > 0)
                    {

                        ObjDealer.DealerID = Convert.ToInt32(dtdeal.Rows[0]["DealerID"]);
                        ObjDealer.DealerCode = Convert.ToString(dtdeal.Rows[0]["DealerCode"]);
                        ObjDealer.DealerName = Convert.ToString(dtdeal.Rows[0]["DealerName"]);
                        ObjDealer.ContactName = Convert.ToString(dtdeal.Rows[0]["ContactName"]);
                        ObjDealer.Address = Convert.ToString(dtdeal.Rows[0]["Address"]);
                        ObjDealer.Area = Convert.ToString(dtdeal.Rows[0]["Area"]);
                        ObjDealer.Pincode = Convert.ToString(dtdeal.Rows[0]["Pincode"]);
                        ObjDealer.Phone = Convert.ToString(dtdeal.Rows[0]["Phone"]);
                        ObjDealer.GST = Convert.ToString(dtdeal.Rows[0]["GST"]);
                        ObjDealer.StateID = Convert.ToInt32(dtdeal.Rows[0]["StateID"]);
                        ObjDealer.GSTPhoto = Convert.ToString(dtdeal.Rows[0]["GSTPhoto"]);
                        ObjDealer.GSTPhotobase64Image = "";
                        ObjDealer.VisitCard = Convert.ToString(dtdeal.Rows[0]["VisitCard"]); ;
                        ObjDealer.VisitCardbase64Image = "";
                        ObjDealer.CreateDate = "";
                        ObjDealer.UpdateDate = "";



                        if (dtdeal.Rows.Count > 0)
                        {
                            response.status = 0;
                            response.Message = "Success";
                            response.Data = ObjDealer;
                        }
                        else
                        {
                            response.status = 1;
                            response.Message = "Dealer not found.";
                            ObjDealer = retrundealer();
                            response.Data = ObjDealer;
                        }
                    }
                    else
                    {
                        response.status = 1;
                        response.Message = "Dealer does not exist.";
                        ObjDealer = retrundealer();
                        response.Data = ObjDealer;
                    }


                }
                else
                {
                    response.status = 0;
                    response.Message = "Record could not able to store, please try again.";
                    response.Data = blankarray;
                }
            }
            catch (Exception ex)
            {
                response.status = 1;
                response.Message = (("Error AddDealer: " + ex.Message) ?? "");
                response.Data = blankarray;
            }
            return response;
        }
        #endregion



        public string setimage(string TypeImage, string base64Image, string imagename)
        {
            try
            {
                string ImgName = imagename;
                if (!string.IsNullOrEmpty(base64Image))
                {
                    String path = HttpContext.Current.Server.MapPath("~/DealerImage/" + TypeImage); //Path
                    //Check if directory exist
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                        //If directory doesn't exist then Create it 
                    }
                    string imageName = ImgName + ".jpg";
                    string imgPath = Path.Combine(path, imageName);
                    if (Drawimage(base64Image, imgPath))
                    {
                        return imageName;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public bool Drawimage(string base64Image, string imgPath)
        {
            try
            {
                if (base64Image.Contains("data:image"))
                {
                    //Need To remove some header information at the beginning if image data contains
                    //ImageDataUrl = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD....";
                    //Otherwise, this will give an error.
                    //Remove everything in front of the DataUrl and including the first comma.
                    //ImageDataUrl = "9j/4AAQSkZJRgABAQAAAQABAAD...
                    base64Image = base64Image.Substring(base64Image.LastIndexOf(',') + 1);
                    // removing extra header information 
                }
                byte[] imageBytes = Convert.FromBase64String(base64Image);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        #endregion

    }
}
public class AllDataOrder
{
    public BA_tblOrder _BA_tblOrder { get; set; }
    public List<OrderFreeProduct> _BA_Free_product { get; set; }
    public List<OrderProductDetailData> _BA_Product_dtl { get; set; }

}
