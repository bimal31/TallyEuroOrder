<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintOrder.aspx.cs" Inherits="OrderApp.PrintOrder" MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Script/jquery-1.9.1.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>View Order </h3>
            </div>
            
        </div>
        <center>
            <asp:Label ID="lblErrorMessage"  ForeColor="Red"  runat="server"></asp:Label>
         </center>
        <div class="panel-body">
            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lbldealercode" runat="server" Text="Dealer Code :"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtDealerCodeSearch" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtDealerCodeSearch_TextChanged"></asp:TextBox>
                            <%--<asp:LinkButton ID="lnkdealerInfo" runat="server">View Dealer Information</asp:LinkButton>--%>
                            <a id="lnkdealerInfo" href="#">View Dealer Information</a>
                        </div>
                        <div class="col-md-2">
                            <input type="button" id="btnAddDealer" runat="server" class="btn btn-info btn-rounded" value="Add Dealer" />
                        </div>
                        <div class="col-md-2">
                            <label>Order Date :</label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>Sales Executive :</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="drpsSalesExe" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                            <label>Order Status :</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="drpOrderStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                <asp:ListItem Text="Cancel" Value="Cancel"></asp:ListItem>
                                <asp:ListItem Text="Factory" Value="Factory"></asp:ListItem>
                                <asp:ListItem Text="Dispatch" Value="Dispatch"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            <label>Transport :</label>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox ID="txttransport" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>
                </div>
            </div>

            <div id="divAddOrder" runat="server">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-2">
                                <label>EURO XTRA</label>
                                <asp:GridView ID="gridEUROXTRA" runat="server" AutoGenerateColumns="false"  OnRowDataBound="gridEUROXTRA_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PKG.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:HiddenField ID="HdnProductId1" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId1" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck1" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos1" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType1" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme1" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG1" runat="server" Value='<%# Bind("TotalKG") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.(Box)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEUROXTRA" runat="server" CssClass="form-control" MaxLength="3" onkeypress="return isNumber(event)" onchange='<%# "TotalNumber(" + Eval("TotalKG") + ", this)"  %>'></asp:TextBox>
                                                <asp:HiddenField ID="hdEUROXTRA" runat="server" />
                                                <br />
                                                <asp:Label ID="lblschEUROXTRA" runat="server" Text="Scheme" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtschemeEUROXTRA" runat="server" CssClass="form-control" MaxLength="100" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-2">
                                <label>EURO WP</label>
                                <asp:GridView ID="grdEUROWP" runat="server" AutoGenerateColumns="false"  OnRowDataBound="grdEUROWP_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PKG.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:HiddenField ID="HdnProductId2" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId2" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck2" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos2" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType2" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme2" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG2" runat="server" Value='<%# Bind("TotalKG") %>' />


                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.(Box)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txteurowp" runat="server" CssClass="form-control" MaxLength="3" onkeypress="return isNumber(event)" onchange='<%# "TotalNumber(" + Eval("TotalKG") + ", this)"  %>'></asp:TextBox>
                                                <asp:HiddenField ID="hdeurowp" runat="server" />
                                                <br />
                                                <asp:Label ID="lblschEUROWP" runat="server" Text="Scheme" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtschemeEUROWP" runat="server" CssClass="form-control" MaxLength="100" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-2">
                                <label>EURO 2 IN 1</label>
                                <asp:GridView ID="grdeuro2in1" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdeuro2in1_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PKG.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:HiddenField ID="HdnProductId3" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId3" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck3" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos3" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType3" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme3" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG3" runat="server" Value='<%# Bind("TotalKG") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.(Box)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txteuro2in1" runat="server" CssClass="form-control" MaxLength="3" onkeypress="return isNumber(event)" onchange='<%# "TotalNumber(" + Eval("TotalKG") + ", this)"  %>'></asp:TextBox>
                                                <asp:HiddenField ID="hdeuro2in1" runat="server" />
                                                <br />
                                                <asp:Label ID="lblscheuro2in1" runat="server" Text="Scheme" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtschemeeuro2in1" runat="server" CssClass="form-control" MaxLength="100" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-2">
                                <label>EXTREME 3</label>
                                <asp:GridView ID="grdExtreme" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdExtreme_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PKG.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:HiddenField ID="HdnProductId4" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId4" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck4" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos4" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType4" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme4" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG4" runat="server" Value='<%# Bind("TotalKG") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.(Box)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtExtreme" runat="server" CssClass="form-control" MaxLength="3" onkeypress="return isNumber(event)" onchange='<%# "TotalNumber(" + Eval("TotalKG") + ", this)"  %>'></asp:TextBox>
                                                <asp:HiddenField ID="hdExtreme" runat="server" />
                                                <br />
                                                <asp:Label ID="lblschExtreme" runat="server" Text="Scheme" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtschemeExtreme" runat="server" CssClass="form-control" MaxLength="100" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-2">
                                <label>EURO ULTRA</label>
                                <asp:GridView ID="grdEuroUltra" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdEuroUltra_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PKG.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:HiddenField ID="HdnProductId5" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId5" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck5" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos5" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType5" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme5" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG5" runat="server" Value='<%# Bind("TotalKG") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.(Box)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEuroUltra" runat="server" CssClass="form-control" MaxLength="3" onkeypress="return isNumber(event)" onchange='<%# "TotalNumber(" + Eval("TotalKG") + ", this)"  %>'></asp:TextBox>
                                                <asp:HiddenField ID="hdEuroUltra" runat="server" />
                                                <br />
                                                <asp:Label ID="lblschEuroUltra" runat="server" Text="Scheme" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtschemeEuroUltra" runat="server" CssClass="form-control" MaxLength="100" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-2">
                                <label>PVC GLUE</label>
                                <asp:GridView ID="GrdPvcGlue" runat="server" AutoGenerateColumns="false" OnRowDataBound="GrdPvcGlue_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PKG.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:HiddenField ID="HdnProductId6" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId6" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck6" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos6" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType6" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme6" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG6" runat="server" Value='<%# Bind("TotalKG") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.(Box)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPVcGlue" runat="server" CssClass="form-control" MaxLength="3" onkeypress="return isNumber(event)" onchange='<%# "TotalNumber(" + Eval("TotalKG") + ", this)"  %>'></asp:TextBox>
                                                <asp:HiddenField ID="hdPVcGlue" runat="server" />
                                                <br />
                                                <asp:Label ID="lblschPvcGlue" runat="server" Text="Scheme" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtschemePvcGlue" runat="server" CssClass="form-control" MaxLength="100" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                                <label>WOOD STRONG</label>
                                <asp:GridView ID="grdWoodStrong" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdWoodStrong_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PKG.">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:HiddenField ID="HdnProductId7" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId7" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck7" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos7" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType7" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme7" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG7" runat="server" Value='<%# Bind("TotalKG") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY.(Box)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtWoodStrong" runat="server" CssClass="form-control" MaxLength="3" onkeypress="return isNumber(event)" onchange='<%# "TotalNumber(" + Eval("TotalKG") + ", this)"  %>'></asp:TextBox>
                                                <asp:HiddenField ID="hdWoodStrong" runat="server" />
                                                <br />
                                                <asp:Label ID="lblschWoodStrong" runat="server" Text="Scheme" Visible="false"></asp:Label>
                                                <asp:TextBox ID="txtschemeWoodStrong" runat="server" CssClass="form-control" MaxLength="100" Visible="false"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-10"></div>

                            <div class="col-md-2 pull-right">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12" id="divOtherDetails" runat="server">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">OTHER :</div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtOther" runat="server" TextMode="MultiLine" Style="width: 100%; height: 100px;">
                            </asp:TextBox>
                        </div>

                        <div class="col-md-1">POP :</div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtPOP" runat="server" TextMode="MultiLine" Style="width: 100%; height: 100px;">
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            <label>Total :</label>
                        </div>
                        <div class="col-md-2">
                            <asp:TextBox ID="lblTotal" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-7">
                        </div>
                        <div class="col-md-2 pull-right;">
                            <%--<asp:Button ID="btnSubmitOrder" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSubmitOrder_Click" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-dark" OnClick="btnClear_Click" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hdDelaerId" runat="server" />
    <asp:HiddenField ID="hdOrderId" runat="server" />
    <asp:HiddenField ID="hdEditOrderId" runat="server" />
    <asp:HiddenField ID="hdTotalKgCount" runat="server" />

    <div class="modal fade" id="DealerModal" tabindex="-1" role="dialog" aria-labelledby="ModalTitle"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="ModalTitle"></h4>
                </div>
                <div class="modal-body">

                    <div class="form-group">
                        <div class="row" id="divdealercode">
                            <div class="col-lg-12">
                                <label runat="server">Dealer Code</label>
                                <asp:TextBox ID="txtDealerCode" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hdDealerId" runat="server" />
                                <%--<asp:RequiredFieldValidator ID="reqDealerCode" runat="server" ControlToValidate="txtDealerCode" ErrorMessage="Plese Enter Dealer Code." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <label>Dealer Name</label>
                                <asp:TextBox ID="txtDealerName" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDealerName" ErrorMessage="Plese Enter Dealer Name." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>ContactName</label>
                                <asp:TextBox ID="txtContactName" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>Address</label>
                                <asp:TextBox ID="txtAddress" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>Area</label>
                                <asp:TextBox ID="txtArea" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtArea" ErrorMessage="Plese Enter Area." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>Pincode</label>
                                <asp:TextBox ID="txtpincode" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>Phone No</label>
                                <asp:TextBox ID="txtPhoneNo" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPhoneNo" ErrorMessage="Plese Enter PhoneNo." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <label>GST</label>
                                <asp:TextBox ID="txtGST" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtGST" ErrorMessage="Plese Enter GST." ValidationGroup="RequireValidation"></asp:RequiredFieldValidator>
                            </div>
                        </div>


                    </div>
                </div>
                <div class="modal-footer">
                    <%--<asp:Button ID="btnSaveDealer" OnClick="btnSaveDealer_Click" Text="Save" runat="server" Class="btn btn-primary" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>--%>
                </div>
            </div>
        </div>
    </div>




    <script type="text/javascript">
        $(document).ready(function () {
            $('#lnkdealerInfo').click(function () {
                if ($("#ContentPlaceHolder1_txtDealerCodeSearch").val() != "") {
                    $('#divdealercode').css("display", "block");
                    $('#ModalTitle').text('View Dealer');
                    $('#ContentPlaceHolder1_btnSaveDealer').hide()
                    $('#ContentPlaceHolder1_txtDealerCode').attr('readonly', true);
                    $('#ContentPlaceHolder1_txtDealerName').attr('readonly', true);
                    $('#ContentPlaceHolder1_txtAddress').attr('readonly', true);
                    $('#ContentPlaceHolder1_txtArea').attr('readonly', true);
                    $('#ContentPlaceHolder1_txtPhoneNo').attr('readonly', true);
                    $('#ContentPlaceHolder1_txtGST').attr('readonly', true);
                    $('#ContentPlaceHolder1_txtTransport').attr('readonly', true);

                    $('#ContentPlaceHolder1_txtContactName').attr('readonly', true);
                    $('#ContentPlaceHolder1_txtpincode').attr('readonly', true);

                    $('#DealerModal').modal('show');
                }
                else {
                    alert("Please enter the dealer code.");
                    return false;
                }

            });
        });

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function TotalNumber(TotalKG, this1) {
            // Box/Qty Value
            var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }

            // Last Box/Qty Value
            var HdnLastQty = $(this1).parent().find('input:hidden').val();
            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }

            // Total Value
            var HdnTotalKgCount = $('#<%=hdTotalKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }
            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);

            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);

            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            //if (TotalKgCount == 0) {
            //    FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            //}       
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=lblTotal.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find('input:hidden').val($(this1).val());


        }
    </script>
</asp:Content>
