using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetUserList();
                }

                if (Convert.ToString(Session["UserType"]) != "Admin")
                {
                    btnAdd.Visible = false;
                    divUserTypeList.Visible = false;
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("AddUser.aspx", false);
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdUserList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditValue")
                {
                    Int32 UserId = Convert.ToInt32(e.CommandArgument);

                    Common cmn = new Common();
                    string strEncryptValue = cmn.Encrypt(Convert.ToString(UserId));

                    Response.Redirect("AddUser.aspx?q=" + strEncryptValue, false);
                }

                if (e.CommandName == "DeleteValue")
                {
                    Int32 UserId = Convert.ToInt32(e.CommandArgument);

                    BA_tblUser ObjUser = new BA_tblUser();

                    ObjUser.UserID = Convert.ToInt32(UserId);
                    bool output = ObjUser.DELETE_RECORDS_FROM_tblUser();

                    if (output == true)
                    {
                        GetUserList();
                    }
                    else
                    {
                        lblErrorMessage.Text = CommMessage.CouldnotabletoDelete;
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdUserList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (Convert.ToString(Session["UserType"]) != "Admin")
                    {
                        grdUserList.Columns[8].Visible = false;
                    }
                    else
                    {
                        //if (e.Row.Cells[0].Text == Convert.ToString(Session["UserName"]))
                        //{
                        //}
                        //else
                        //{
                        //    Button btnEdit = e.Row.FindControl("btnEdit") as Button;
                        //    btnEdit.Visible = false;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void GetUserList()
        {
            DataTable dt = new DataTable();
            BA_tblUser objUser = new BA_tblUser();
            try
            {
                if (Convert.ToString(Session["UserType"]) != "Admin")
                {
                    objUser.UserName = txtSearch.Text;
                    objUser.UserID = Convert.ToInt32(Session["UserId"]);
                    objUser.GET_RECORDS_FROM_tblUser(ref dt);
                }
                else
                {
                    objUser.UserName = txtSearch.Text;
                    objUser.UserType = Convert.ToString(ddlUserType1.SelectedValue);
                    objUser.SELECT_ALL_tblUser(ref dt);
                }


                grdUserList.DataSource = dt;
                grdUserList.DataBind();

                Session["dtUser"] = dt;
                ViewState["sortdr"] = "Asc";
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
            
        }

        protected void grdUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtUser = new DataTable();
                dtUser = Session["dtUser"] as DataTable;

                grdUserList.PageIndex = e.NewPageIndex;
                grdUserList.DataSource = dtUser;
                grdUserList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void ddlUserType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetUserList();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdUserList_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtrslt = (DataTable)Session["dtUser"];
            if (dtrslt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["sortdr"] = "Desc";
                }
                else
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["sortdr"] = "Asc";
                }
                grdUserList.DataSource = dtrslt;
                grdUserList.DataBind();


            }

        }


        protected void txtSearch_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GetUserList();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

    }
}