<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProductPacking.aspx.cs" Inherits="OrderApp.AddProductPacking"
    MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        div.btn-group, div.btn-group > button.multiselect {
            width: 100%;
            background-color: #fff;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>Add Product Packing</h3>
            </div>

            <div class="form-group pull-right">
                <asp:Button ID="btnback" runat="server" Text="Back To List" CssClass="btn btn-info btn-rounded" OnClick="btnback_Click" />
            </div>
        </div>

        <center>
            <asp:Label ID="lblErrorMessage" ForeColor="Red" runat="server"></asp:Label>
        </center>

        <div class="panel-body">
            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>Product Name</label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="drpProductName" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hdProductPackingId" runat="server" />
                        </div>
                        <div class="col-md-2">
                            <label>Product Packing</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtProductPack" CssClass="form-control" runat="server" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqProductPack" runat="server" ControlToValidate="txtProductPack" 
                                ForeColor="Red" ErrorMessage="Plese Enter Product Packing." ValidationGroup="RequireValidation"
                               
                                ></asp:RequiredFieldValidator>
                            
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>Packing Number</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtPackingNos" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqPackingNos" runat="server" ControlToValidate="txtPackingNos" ForeColor="Red" ErrorMessage="Plese Enter Packing Number." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-2">
                            <label>Details</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDetails" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>Packing Type</label>
                        </div>

                        <div class="col-md-2">
                            <asp:DropDownList ID="drpPackingType" runat="server" CssClass="form-control">
                                <asp:ListItem Text="kg" Value="kg"></asp:ListItem>
                                <asp:ListItem Text="gm" Value="gm"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            <label>UOM</label>
                        </div>

                        <div class="col-md-2">
                            <asp:DropDownList ID="drpuom" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-3">
                            <asp:CheckBox ID="chkScheme" runat="server" Text="State or Scheme Wise" />
                        </div>
                    </div>
                </div>
            </div>

            <asp:UpdatePanel runat="server" ID="UpdatePanelItems">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-2">
                                    <label>State</label>
                                </div>

                                <div class="col-md-4">
                                    <asp:ListBox ID="drpStateName" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                    <div class="clearfix"></div>

                                    <asp:Label ID="lblStateValidation" runat="server"></asp:Label>
                                </div>

                                <div class="col-md-2">
                                    <label>Scheme</label>
                                </div>

                                <div class="col-md-4">
                                    <asp:DropDownList ID="drpScheme" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-2">
                                    <label>Product Code</label>
                                </div>

                                <div class="col-md-4">
                                    <asp:TextBox ID="txtProductCode" runat="server" CssClass="form-control"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="RequiredProductCode" runat="server" ControlToValidate="txtProductCode"
                                        ErrorMessage="Plese Enter Product Code." ValidationGroup="RequireItemsValidation"></asp:RequiredFieldValidator>

                                    <div class="clearfix"></div>

                                    <asp:CustomValidator ID="CustomValidateProductCode" runat="server" ControlToValidate="txtProductCode"
                                        ErrorMessage="Product code is already exists. Duplicate Product Code is not allowed."
                                        ValidationGroup="RequireItemsValidation" OnServerValidate="CustomValidateProductCode_ServerValidate"></asp:CustomValidator>
                                </div>

                                <div class="col-md-6">
                                    <lable id="lblProductName" runat="server"></lable>
                                    <label id="lblProductPacking" runat="server"></label>
                                    <label id="lblSchemeName" runat="server"></label>
                                    <label id="lblProductCode" runat="server"></label>
                                </div>

                                <div class="col-md-2">
                                    <asp:Button ID="btnAddItem" runat="server" Text="Add Item" CssClass="btn btn-primary" ValidationGroup="RequireItemsValidation"
                                        CausesValidation="true" OnClick="btnAddItem_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <ul class="nav nav-tabs">
                            <li class="nav-item"><a class="nav-link active" data-toggle="tab" href="#ActiveList">Active List</a></li>
                            <li class="nav-item"><a class="nav-link" data-toggle="tab" href="#DeletedList">Deleted List</a></li>
                        </ul>

                        <div class="tab-content">
                            <div id="ActiveList" class="tab-pane fade show active p-t-10">
                                <asp:GridView ID="gridActiveList" runat="server" ShowHeaderWhenEmpty="true" Width="100%" CssClass="mygrdContent"
                                    EmptyDataText="No records found." PagerStyle-CssClass="pager" HeaderStyle-CssClass="header"
                                    RowStyle-CssClass="rows" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                    DataKeyNames="SrNo"
                                    OnPageIndexChanging="gridActiveList_PageIndexChanging"
                                    OnRowEditing="gridActiveList_RowEditing"
                                    OnRowCancelingEdit="gridActiveList_RowCancelingEdit">
                                    <Columns>
                                        <asp:BoundField DataField="StateName" HeaderText="State" ReadOnly="true" />
                                        <asp:BoundField DataField="SchemeName" HeaderText="Scheme" ReadOnly="true" />
                                        <%--<asp:BoundField DataField="ProductCode" HeaderText="Product Code" />--%>

                                        <asp:TemplateField HeaderText="Product Code">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnStateId" runat="server" Value='<%#Eval("StateID") %>' />
                                                <asp:Literal runat="server" ID="ltProductCode" Text='<%#Eval("ProductCode") %>'></asp:Literal>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:HiddenField ID="hdnStateId" runat="server" Value='<%#Eval("StateID") %>' />
                                                <asp:TextBox runat="server" ID="txtEditProductCode" Text='<%#Eval("ProductCode") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-success"
                                                    CommandName="Edit" CommandArgument='<%# Eval("SrNo") %>' />

                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger"
                                                    OnClick="OnDeleteGridViewItem" CommandArgument='<%# Eval("SrNo") %>' />
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success"
                                                    OnClick="OnUpdateGridViewItem" CommandArgument='<%# Eval("SrNo") %>' />

                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary"
                                                    CommandName="Cancel" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <PagerStyle CssClass="pagination-ys" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>
                            </div>

                            <div id="DeletedList" class="tab-pane fade p-t-10">
                                <asp:GridView ID="gridDeletedList" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No records found."
                                    Width="100%" CssClass="mygrdContent" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header"
                                    RowStyle-CssClass="rows" AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
                                    <Columns>
                                        <asp:BoundField DataField="StateName" HeaderText="State" />
                                        <asp:BoundField DataField="SchemeName" HeaderText="Scheme" />
                                        <asp:BoundField DataField="ProductCode" HeaderText="Product Code" />
                                    </Columns>

                                    <PagerStyle CssClass="pagination-ys" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="panel-footer p-t-20">
            <div class="form-group pull-right">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-success"
                    ValidationGroup="RequireValidation" CausesValidation="true" />

               <%-- <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-dark" />--%>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        $(function () {
            $('[id*=drpStateName]').multiselect({
                includeSelectAllOption: true,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
                dropRight: true
            });

            $('#<%=chkScheme.ClientID%>').on('change', function (e) {
                e.preventDefault();

                if (this.checked)
                    $('#<%=UpdatePanelItems.ClientID%>').css('display', 'block');
                else
                    $('#<%=UpdatePanelItems.ClientID%>').css('display', 'none');
            });

            $('#<%=drpProductName.ClientID%>').on('change', function () {
                var selectedValue = $(this).find('option:selected').text();

                if (this.value === "0")
                    $('#<%=lblProductName.ClientID%>').html('');
                else
                    $('#<%=lblProductName.ClientID%>').html(selectedValue + ' ');
            });

            $('#<%=txtProductPack.ClientID%>').on('keyup', function (e) {
                e.preventDefault();

                var value = this.value,
                    productNos = $('#<%=txtPackingNos.ClientID%>').val();

                $('#<%=lblProductPacking.ClientID%>').html(value + "X" + productNos + " ");
            });

            $('#<%=txtPackingNos.ClientID%>').on('keyup', function (e) {
                e.preventDefault();

                var productNos = this.value,
                    productPack = $('#<%=txtProductPack.ClientID%>').val();

                $('#<%=lblProductPacking.ClientID%>').html(productPack + "X" + productNos + " ");
            });

            $('#<%=txtProductCode.ClientID%>').on('keyup', function (e) {
                e.preventDefault();

                var value = this.value;

                $('#<%=lblProductCode.ClientID%>').html("(" + value + ")");
            });

            $('#<%=drpScheme.ClientID%>').on('change', function () {
                var selectedValue = $(this).find('option:selected').text();

                if (this.value === "0")
                    $('#<%=lblSchemeName.ClientID%>').html('');
                else
                    $('#<%=lblSchemeName.ClientID%>').html(selectedValue + " ");
            });

            $('#<%=chkScheme.ClientID%>').trigger('change');
        });

        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('[id*=drpStateName]').multiselect({
                        includeSelectAllOption: true,
                        enableFiltering: true,
                        filterPlaceholder: 'Search',
                        enableCaseInsensitiveFiltering: true,
                        dropRight: true
                    });

                    $("[id*=drpStateName]").multiselect("deselectAll", false);
                }
            });
        };
    </script>
</asp:Content>
