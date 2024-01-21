using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FirstGridViewRow();
            }
        }

        private void FirstGridViewRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Product", typeof(string)));
            dt.Columns.Add(new DataColumn("TotalPurchaseKg", typeof(string)));
            dt.Columns.Add(new DataColumn("FromScheme", typeof(string)));
            dt.Columns.Add(new DataColumn("ToScheme", typeof(string)));


            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["Product"] = string.Empty;
            dr["TotalPurchaseKg"] = string.Empty;
            dr["FromScheme"] = string.Empty;
            dr["ToScheme"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            grvStudentDetails.DataSource = dt;
            grvStudentDetails.DataBind();
        }


        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row.
                DropDownList ddlproduct = (e.Row.FindControl("ddlproduct") as DropDownList);
                DataTable dt = new DataTable();
                BA_tblProduct objUser = new BA_tblProduct();
                objUser.SELECT_ALL_tblProduct(ref dt);


                ddlproduct.DataSource = dt;
                ddlproduct.DataTextField = "ProductName";
                ddlproduct.DataValueField = "ProductId";
                ddlproduct.DataBind();

                //Add Default Item in the DropDownList.
                ddlproduct.Items.Insert(0, new ListItem("Please select"));


            }
        }

        private void AddNewRow()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList ddlproduct =
                          (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("ddlproduct");

                        TextBox txtpurchase =
                          (TextBox)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("txtpurchase");
                        TextBox txtFromScheme =
                          (TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("txtFromScheme");
                        TextBox txtToScheme =
                          (TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("txtToScheme");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Product"] = ddlproduct.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["TotalPurchaseKg"] = txtpurchase.Text;
                        dtCurrentTable.Rows[i - 1]["FromScheme"] = txtFromScheme.Text;
                        dtCurrentTable.Rows[i - 1]["ToScheme"] = txtToScheme.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    grvStudentDetails.DataSource = dtCurrentTable;
                    grvStudentDetails.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        DropDownList ddlproduct =
                       (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("ddlproduct");

                        TextBox txtpurchase = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("txtpurchase");
                        TextBox txtFromScheme = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("txtFromScheme");
                        TextBox txtToScheme = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("txtToScheme");

                        ddlproduct.SelectedValue = dt.Rows[i]["Product"].ToString();
                        txtpurchase.Text = dt.Rows[i]["TotalPurchaseKg"].ToString();
                        txtFromScheme.Text = dt.Rows[i]["FromScheme"].ToString();
                        txtToScheme.Text = dt.Rows[i]["ToScheme"].ToString();


                        rowIndex++;
                    }
                }
            }
        }
        protected void grvStudentDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData();
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable"] = dt;
                    grvStudentDetails.DataSource = dt;
                    grvStudentDetails.DataBind();

                    for (int i = 0; i < grvStudentDetails.Rows.Count - 1; i++)
                    {
                        grvStudentDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    SetPreviousData();
                }
            }
        }

        private void SetRowData()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        DropDownList ddlproduct =
                     (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[1].FindControl("ddlproduct");

                        TextBox txtpurchase = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("txtpurchase");
                        TextBox txtFromScheme = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("txtFromScheme");
                        TextBox txtToScheme = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("txtToScheme");


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Product"] = ddlproduct.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["TotalPurchaseKg"] = txtpurchase.Text;
                        dtCurrentTable.Rows[i - 1]["FromScheme"] = txtFromScheme.Text;
                        dtCurrentTable.Rows[i - 1]["ToScheme"] = txtToScheme.Text;

                        rowIndex++;
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

        }
    }
}