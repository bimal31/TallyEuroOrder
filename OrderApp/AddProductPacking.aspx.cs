using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class AddProductPacking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    Session["dtItems"] = null;

                    ConfigureItemsDatatable();

                    GetStateList();
                    GetSchemeList();
                    GetProductsList();
                    GetUOMList();

                    if (Request.QueryString["q"] != null)
                    {
                        string strKey = Convert.ToString(Request.QueryString["q"]);
                        Common cmn = new Common();
                        strKey = cmn.Decrypt(strKey);

                        Int32 ProductPackingId = Convert.ToInt32(strKey);

                        DataTable _dt = new DataTable();

                        BA_tblProductPacking ObjDealer = new BA_tblProductPacking();
                        ObjDealer.ProductPckID = Convert.ToString(ProductPackingId);
                        ObjDealer.GET_RECORDS_FROM_tblProductPacking(ref _dt);

                        if (_dt != null)
                        {
                            txtProductPack.Text = Convert.ToString(_dt.Rows[0]["ProductPck"]);
                            drpProductName.SelectedValue = Convert.ToString(_dt.Rows[0]["ProductID"]);
                            txtPackingNos.Text = Convert.ToString(_dt.Rows[0]["PackingNos"]);
                            txtDetails.Text = Convert.ToString(_dt.Rows[0]["ProductPckDetails"]);
                            drpPackingType.Text = Convert.ToString(_dt.Rows[0]["PackingType"]);
                            chkScheme.Checked = Convert.ToBoolean(_dt.Rows[0]["IsScheme"]);

                            hdProductPackingId.Value = Convert.ToString(_dt.Rows[0]["ProductPckID"]);

                            lblProductName.InnerText = drpProductName.SelectedItem.ToString() + " ";
                            lblProductPacking.InnerText = txtProductPack.Text + "X" + txtPackingNos.Text + " ";
                            lblSchemeName.InnerText = "";
                            lblProductCode.InnerText = "(" + txtProductCode.Text + ")";

                            drpuom.SelectedItem.Text = Convert.ToString(_dt.Rows[0]["UOM"]);

                            DataTable _dtItems = new DataTable();

                            BA_tblProductPackingStateScheme objProdPackingItems = new BA_tblProductPackingStateScheme();
                            objProdPackingItems.ProductPckID = Convert.ToInt16(hdProductPackingId.Value);
                            objProdPackingItems.GET_RECORDS_FROM_tblProductPackingStateScheme_By_ProdPackId(ref _dtItems);

                            if (_dtItems != null)
                                Session["dtItems"] = _dtItems;
                            else
                                ConfigureItemsDatatable();
                        }
                    }

                    BindActiveItemsGrid();
                    BindDeleteItemsGrid();
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateMainForm())
                {
                    BA_tblProductPacking ObjProductPack = new BA_tblProductPacking();

                    Common Cmn = new Common();

                    ObjProductPack.ProductID = drpProductName.SelectedValue;
                    ObjProductPack.ProductPck = Convert.ToInt32(txtProductPack.Text);
                    ObjProductPack.PackingNos = Convert.ToInt32(txtPackingNos.Text);
                    ObjProductPack.ProductPckDetails = txtDetails.Text;
                    ObjProductPack.PackingType = drpPackingType.SelectedValue;
                    ObjProductPack.IsScheme = chkScheme.Checked;
                    ObjProductPack.Isdeleted = false;
                    ObjProductPack.CreateBy = Convert.ToInt32(Session["UserId"]);
                    ObjProductPack.UpdateBy = Convert.ToInt32(Session["UserId"]);

                    ObjProductPack.UOM = Convert.ToString(drpuom.SelectedItem);

                    DataTable dtItems = (DataTable)Session["dtItems"];

                    if (dtItems != null && dtItems.Rows.Count > 0)
                    {
                        StringBuilder sBuilder = new StringBuilder();
                        sBuilder.Append("<Data>\n");

                        foreach (DataRow row in dtItems.Rows)
                        {
                            sBuilder.Append(
                                "<Item>\n" +
                                    "<SrNo>" + Convert.ToString(row["SrNo"]) + "</SrNo>\n" +
                                    "<StateID>" + Convert.ToString(row["StateID"]) + "</StateID>\n" +
                                    "<SchemeID>" + Convert.ToString(row["SchemeID"]) + "</SchemeID>\n" +
                                    "<ProductCode>" + Convert.ToString(row["ProductCode"]) + "</ProductCode>\n" +
                                    "<isDeleted>" + Convert.ToString(row["isDeleted"]) + "</isDeleted>\n" +
                                "</Item>\n");
                        }

                        sBuilder.Append("</Data>");

                        ObjProductPack.XmlData = sBuilder.ToString();
                    }

                    bool output = false;

                    if (hdProductPackingId.Value == "")
                        output = ObjProductPack.INSERT_tblProductPacking();
                    else
                    {
                        ObjProductPack.ProductPckID = hdProductPackingId.Value;
                        output = ObjProductPack.UPDATE_tblProductPacking();
                    }

                    if (output)
                        Response.Redirect("ProductPacking.aspx", false);
                    else
                        lblErrorMessage.Text = CommMessage.Recordcouldnotable;
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        //protected void btnClear_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        txtProductPack.Text = "";
        //        txtPackingNos.Text = "";
        //        txtDetails.Text = "";

        //        lblProductName.InnerText = "";
        //        lblProductPacking.InnerText = "";
        //        lblSchemeName.InnerText = "";
        //        lblProductCode.InnerText = "";

        //        Session["dtItems"] = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        BA_ErrorLog ObjError = new BA_ErrorLog();
        //        ObjError.INSERT_ErrorLog(ex);
        //    }
        //}

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductPacking.aspx", false);
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateAddItem())
                {
                    DataTable dtItems = (DataTable)Session["dtItems"];

                    foreach (ListItem item in drpStateName.Items)
                    {
                        if (item.Selected)
                        {
                            DataRow newRow = dtItems.NewRow();
                            newRow["SrNo"] = "0";
                            newRow["StateID"] = item.Value;
                            newRow["StateName"] = item.Text;
                            newRow["SchemeID"] =
                                drpScheme.SelectedValue == null || drpScheme.SelectedValue == "" ? "0" : drpScheme.SelectedValue;
                            newRow["SchemeName"] = drpScheme.SelectedItem;
                            newRow["ProductCode"] = txtProductCode.Text;
                            newRow["isDeleted"] = false;

                            dtItems.Rows.Add(newRow);
                        }
                    }

                    Session["dtItems"] = dtItems;

                    drpScheme.SelectedValue = "0";
                    txtProductCode.Text = "";

                    BindActiveItemsGrid();
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void gridActiveList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditValue")
                {
                    string itemId = Convert.ToString(e.CommandArgument);

                    Common cmn = new Common();

                }

                if (e.CommandName == "DeleteValue")
                {
                    //long itemId= Convert.ToInt32(e.CommandArgument);

                    //BA_tblDealer ObjDealer = new BA_tblDealer();
                    //ObjDealer.DealerID = Convert.ToInt32(itemId);

                    //bool output = ObjDealer.DELETE_RECORDS_FROM_tblDealer();

                    //if (output)
                    //{
                    //    BindActiveItemsGrid();
                    //    BindDeleteItemsGrid();
                    //}
                    //else
                    //    lblErrorMessage.Text = CommMessage.CouldnotabletoDelete;
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void gridActiveList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtItems = (DataTable)Session["dtItems"];

                DataRow[] rows = dtItems.Select("isDeleted = false");

                DataTable dtActiveItems = dtItems.Clone();

                if (rows != null && rows.Length > 0)
                    dtActiveItems = rows.CopyToDataTable();

                gridActiveList.PageIndex = e.NewPageIndex;
                gridActiveList.DataSource = dtActiveItems;
                gridActiveList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void gridActiveList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridActiveList.EditIndex = e.NewEditIndex;

            BindActiveItemsGrid();

            GridViewRow row = gridActiveList.Rows[e.NewEditIndex];

            lblSchemeName.InnerText = " " + row.Cells[1].Text;
            lblProductCode.InnerText = " (" + (row.Cells[2].FindControl("txtEditProductCode") as TextBox).Text + ")";
        }

        protected void OnUpdateGridViewItem(object sender, EventArgs e)
        {
            Button btnUpdate = (Button)(sender);

            string recordId = Convert.ToString(btnUpdate.CommandArgument);

            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;

            string stateId = (row.Cells[2].FindControl("hdnStateId") as HiddenField).Value;
            string productCode = (row.Cells[2].FindControl("txtEditProductCode") as TextBox).Text;

            DataTable dtItems = (DataTable)Session["dtItems"];

            DataRow updateRow =
                dtItems.Select("isDeleted = false AND SrNo = " + recordId + " AND StateID = " + stateId).FirstOrDefault();

            if (updateRow != null)
                updateRow["ProductCode"] = productCode;

            Session["dtItems"] = dtItems;

            gridActiveList.EditIndex = -1;

            BindActiveItemsGrid();

            lblSchemeName.InnerText = "";
            lblProductCode.InnerText = "";
        }

        protected void OnDeleteGridViewItem(object sender, EventArgs e)
        {
            Button btnUpdate = (Button)(sender);

            string recordId = Convert.ToString(btnUpdate.CommandArgument);

            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;

            string stateId = (row.Cells[2].FindControl("hdnStateId") as HiddenField).Value;

            DataTable dtItems = (DataTable)Session["dtItems"];

            DataRow updateRow = dtItems.Select("SrNo = " + recordId + " AND StateID = " + stateId).FirstOrDefault();

            if (updateRow != null)
                updateRow["isDeleted"] = true;

            Session["dtItems"] = dtItems;

            BindActiveItemsGrid();
            BindDeleteItemsGrid();
        }

        protected void gridActiveList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridActiveList.EditIndex = -1;

            BindActiveItemsGrid();

            lblSchemeName.InnerText = "";
            lblProductCode.InnerText = "";
        }

        protected void CustomValidateProductCode_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                var productCode = txtProductCode.Text.Trim();

                if (productCode != "")
                {
                    DataTable dtItems = (DataTable)Session["dtItems"];

                    DataTable _dtDBItems = new DataTable();

                    BA_tblProductPackingStateScheme objProdPackingItems = new BA_tblProductPackingStateScheme();
                    objProdPackingItems.SchemeProductCode = productCode;
                    objProdPackingItems.SchemeIdData =
                        drpScheme.SelectedValue == null || drpScheme.SelectedValue == "" ? 0 : Convert.ToInt32(drpScheme.SelectedValue);

                    foreach (ListItem item in drpStateName.Items)
                    {
                        if (item.Selected)
                        {
                            objProdPackingItems.state_id = Convert.ToInt32(item.Value);
                            objProdPackingItems.CHECK_EXISTING_tblProductPackingStateScheme(ref _dtDBItems);

                            if (_dtDBItems != null && _dtDBItems.Rows.Count > 0)
                            {
                                args.IsValid = false;

                                break;
                            }

                            if (dtItems != null && dtItems.Rows.Count > 0)
                            {
                                DataRow[] rows = dtItems.Select("StateID = " + item.Value + " AND ProductCode = '" + productCode + "'");

                                if (rows != null && rows.Length > 0)
                                {
                                    args.IsValid = false;

                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                args.IsValid = false;

                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        private void GetProductsList()
        {
            DataTable dt = new DataTable();
            BA_tblProduct ObjProduct = new BA_tblProduct();

            ObjProduct.SELECT_ALL_tblProduct(ref dt);

            drpProductName.DataSource = dt;
            drpProductName.DataTextField = "ProductName";
            drpProductName.DataValueField = "ProductId";
            drpProductName.DataBind();

            drpProductName.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        public void GetStateList()
        {
            DataTable dt = new DataTable();
            BA_States ObjBStates = new BA_States();

            ObjBStates.SELECT_ALL_States(ref dt);

            drpStateName.DataSource = dt;
            drpStateName.DataTextField = "state_name";
            drpStateName.DataValueField = "state_id";
            drpStateName.DataBind();
            //drpStateName.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        private void GetUOMList()
        {
            BA_tblUOM objUOM = new BA_tblUOM();

            DataTable dt = new DataTable();
            objUOM.SELECT_ALL_tbluom(ref dt);

            drpuom.DataSource = dt;
            drpuom.DataTextField = "UOMName";
            drpuom.DataValueField = "UOMId";
            drpuom.DataBind();

            //drpUOM.Items.Insert(0, new ListItem("No UOM", "0"));
        }
        private void GetSchemeList()
        {
            BA_tblScheme objScheme = new BA_tblScheme();

            DataTable dt = new DataTable();
            objScheme.SELECT_ALL_tblScheme(ref dt);

            drpScheme.DataSource = dt;
            drpScheme.DataTextField = "SchemeName";
            drpScheme.DataValueField = "SchemeId";
            drpScheme.DataBind();

            drpScheme.Items.Insert(0, new ListItem("No Scheme", "0"));
        }

        private bool ValidateAddItem()
        {
            if (drpStateName.SelectedValue == "")
            {
                lblStateValidation.Text = CommMessage.StateErrorMessage;

                return false;
            }

            if (!Page.IsValid)
                return false;

            lblStateValidation.Text = "";

            return true;
        }

        private void BindActiveItemsGrid()
        {
            try
            {
                DataTable dtItems = (DataTable)Session["dtItems"];

                DataRow[] rows = dtItems.Select("isDeleted = false");

                DataTable dtActiveItems = dtItems.Clone();

                if (rows != null && rows.Length > 0)
                    dtActiveItems = rows.CopyToDataTable();

                gridActiveList.DataSource = dtActiveItems;
                gridActiveList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        private void BindDeleteItemsGrid()
        {
            try
            {
                DataTable dtItems = (DataTable)Session["dtItems"];

                DataRow[] rows = dtItems.Select("isDeleted = true");

                DataTable dtDeletedItems = dtItems.Clone();

                if (rows != null && rows.Length > 0)
                    dtDeletedItems = rows.CopyToDataTable();

                gridDeletedList.DataSource = dtDeletedItems;
                gridDeletedList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        private void ConfigureItemsDatatable()
        {
            DataTable dtItems = new DataTable();

            if (dtItems == null || dtItems.Rows.Count == 0)
            {
                DataColumn[] activeCols = new DataColumn[] {
                        new DataColumn("SrNo",typeof(long)),
                        new DataColumn("StateID",typeof(int)),
                        new DataColumn("StateName",typeof(string)),
                        new DataColumn("SchemeID",typeof(string)),
                        new DataColumn("SchemeName",typeof(string)),
                        new DataColumn("ProductCode",typeof(string)),
                        new DataColumn("isDeleted",typeof(bool))
                    };

                dtItems.Columns.AddRange(activeCols);

                Session["dtItems"] = dtItems;
            }
        }

        private bool ValidateMainForm()
        {
            if (drpProductName.SelectedValue == "0")
            {
                lblErrorMessage.Text = CommMessage.selectproductname;

                return false;
            }

            if (Session["dtItems"] != null)
            {
                DataTable dtItems = (DataTable)Session["dtItems"];

                if (dtItems == null || dtItems.Rows.Count == 0)
                {
                    lblErrorMessage.Text = CommMessage.RequiredProductPackingItems;

                    return false;
                }
            }
            else
            {
                lblErrorMessage.Text = CommMessage.RequiredProductPackingItems;

                return false;
            }

            return true;
        }
    }
}