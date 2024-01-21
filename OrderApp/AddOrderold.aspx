<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddOrderold.aspx.cs" Inherits="OrderApp.AddOrderold" MasterPageFile="~/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Script/jquery-1.9.1.js"></script>

    <style>
        .myGridClass {
            width: 100%;
            /*this will be the color of the odd row*/
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #000;
            border-collapse: collapse;
            font-family: 'Montserrat', sans-serif;
        }

            .myGridClass td {
                padding: 2px;
                font-size: 12px;
                text-align: center;
                line-height: 22px;
                border: solid 1px #000;
                color: #000;
            }

            .myGridClass th {
                padding: 4px 2px;
                color: #fff;
                text-align: center;
                line-height: 25px;
                font-weight: 600;
                font-size: 10px;
                background: #153170;
                border-left: solid 1px #000;
            }

            .myGridClass .myAltRowClass {
                background: #fcfcfc repeat-x top;
            }

            .myGridClass .myPagerClass {
                background: #ca2f48;
            }

                .myGridClass .myPagerClass table {
                    margin: 5px 0;
                }

                .myGridClass .myPagerClass td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .myGridClass .myPagerClass a {
                    color: #666;
                    text-decoration: none;
                }

                    .myGridClass .myPagerClass a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading page-titles">
            <div class="form-group pull-left">
                <h3>
                    <asp:Label ID="lblheading" runat="server"></asp:Label>
                </h3>
            </div>
            <div class="form-group pull-right">
                <asp:Button ID="btnback" runat="server" Text="Back To List" CssClass="btn btn-info btn-rounded" OnClick="btnback_Click" />
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
                            <a id="lnkdealerInfo" href="#">View Dealer Information</a>
                        </div>
                        <div class="col-md-2">
                            <%--<input type="button" id="btnAddDealer" runat="server" class="btn btn-info btn-rounded" value="Add Dealer" />--%>
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
                            <label>Dealer Name:</label>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtdealernamesearch" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>

                        <div class="col-md-2">
                            <label>Sales Executive :</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="drpsSalesExe" runat="server" CssClass="form-control">
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
                        <div class="col-md-5">
                            <asp:TextBox ID="txttransport" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Order Status :</label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="drpOrderStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                <asp:ListItem Text="Factory" Value="Factory"></asp:ListItem>
                                <asp:ListItem Text="Dispatch Department" Value="Dispatch Department"></asp:ListItem>
                                <asp:ListItem Text="Dispatched" Value="Dispatched"></asp:ListItem>
                                <asp:ListItem Text="Cancel" Value="Cancel"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divAddOrder" runat="server">
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-2">
                                <%--<label>EURO XTRA</label>--%>
                                <asp:GridView ID="gridEUROXTRA" runat="server" CssClass="myGridClass" AutoGenerateColumns="false" OnRowDataBound="gridEUROXTRA_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="EURO XTRA">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:Label ID="lblpkgboxnoEUROXTRA" runat="server">BOX </asp:Label>
                                                <asp:HiddenField ID="HdnProductId1" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId1" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck1" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos1" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType1" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG1" runat="server" Value='<%# Bind("TotalKG") %>' />
                                                <asp:HiddenField ID="hdEUROXTRA"  runat="server" /> 
                                                <asp:TextBox ID="txtEUROXTRA" runat="server"  name="hiddenvalue" CssClass="form-control" MaxLength="3"
                                                     onkeypress="return isNumber(event)" 
                                                    onchange='<%# "TotalNumber_hdEUROXTRA(" + Eval("TotalKG") + ", this)"  %>'></asp:TextBox>

                                                 <asp:HiddenField ID="hdeuroxtrapcode" runat="server" />
                                                <br />
                                                <asp:DropDownList ID="dropschemeEUROXTRA" Width="100%" runat="server"></asp:DropDownList>
                                                 <%--<asp:TextBox ID="txtschemeEUROXTRA" runat="server" Font-Size="X-Small" CssClass="form-control" MaxLength="160"  Visible="true"></asp:TextBox>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-2">
                                <%-- <label>EURO WP</label>--%>
                                <asp:GridView ID="grdEUROWP" runat="server" CssClass="myGridClass" AutoGenerateColumns="false" OnRowDataBound="grdEUROWP_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="EURO WP">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:Label ID="lblpkgboxnoeurowp" runat="server">BOX</asp:Label>
                                                <asp:HiddenField ID="HdnProductId2" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId2" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck2" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos2" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType2" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG2" runat="server" Value='<%# Bind("TotalKG") %>' />
                                                <asp:TextBox ID="txteurowp" runat="server" CssClass="form-control" MaxLength="3" 
                                                    onkeypress="return isNumber(event)" onchange='<%# "TotalNumber_hdeurowp(" + Eval("TotalKG") + ", this)"  %>'></asp:TextBox>
                                                <asp:HiddenField ID="hdeurowp" runat="server" />
                                                <asp:HiddenField ID="hdeurowppcode" runat="server" />
                                                <br />
                                                <asp:DropDownList ID="dropschemeeurowp" Width="100%" runat="server"></asp:DropDownList>
                                                 <%--<asp:TextBox ID="txtschemeEUROWP" runat="server" Font-Size="X-Small" CssClass="form-control" MaxLength="160"  Visible="true"></asp:TextBox>--%>
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-2">

                                <asp:GridView ID="grdeuro2in1" runat="server" CssClass="myGridClass" AutoGenerateColumns="false" OnRowDataBound="grdeuro2in1_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="EURO HI STRONG">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:Label ID="lblpkgboxnoeuro2in1" runat="server">BOX</asp:Label>

                                                <asp:HiddenField ID="HdnProductId3" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId3" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck3" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos3" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType3" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG3" runat="server" Value='<%# Bind("TotalKG") %>' />
                                                <asp:TextBox ID="txteuro2in1" runat="server" CssClass="form-control" MaxLength="3" 
                                                 onkeypress="return isNumber(event)" onchange='<%# "TotalNumber_hdeuro2in1(" + Eval("TotalKG") + ", this)"  %>'></asp:TextBox>
                                                 <asp:HiddenField ID="hdeuro2in1" runat="server" />
                                                <br />
                                                <asp:DropDownList ID="dropschemeeuro2in1" Width="100%" runat="server"></asp:DropDownList>
                                                <%--<asp:TextBox ID="txtschemeeuro2in1" runat="server" Font-Size="X-Small" CssClass="form-control" MaxLength="160"  Visible="true"></asp:TextBox>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-2">

                                <asp:GridView ID="grdExtreme" runat="server" CssClass="myGridClass" AutoGenerateColumns="false" OnRowDataBound="grdExtreme_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="EXTREME 3">
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:Label ID="lblpkgboxnoExtreme" runat="server">BOX</asp:Label>

                                                <asp:HiddenField ID="HdnProductId4" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId4" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck4" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos4" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType4" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG4" runat="server" Value='<%# Bind("TotalKG") %>' />
                                                <asp:TextBox ID="txtExtreme" runat="server" CssClass="form-control" MaxLength="3" 
                                                    onkeypress="return isNumber(event)" onchange='<%# "TotalNumber_hdExtreme(" + Eval("TotalKG") + ", this)"  %>'></asp:TextBox>
                                                <asp:HiddenField ID="hdExtreme" runat="server" />
                                                 <asp:HiddenField ID="hdExtremepcode" runat="server" />
                                                <br />
                                                <%--    <asp:Label ID="lblschExtreme" runat="server" Text="Scheme" Visible="false"></asp:Label> --%>
                                                <asp:DropDownList ID="dropschemeExtreme" Width="100%" runat="server"></asp:DropDownList>
                                                <%--<asp:TextBox ID="txtschemeExtreme" runat="server" Font-Size="X-Small" CssClass="form-control" MaxLength="160"  Visible="true"></asp:TextBox>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-2">

                                <asp:GridView ID="grdEuroUltra" runat="server" CssClass="myGridClass" AutoGenerateColumns="false" OnRowDataBound="grdEuroUltra_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="EURO ULTRA">
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:Label ID="llblpkgboxnoEuroUltra" runat="server">BOX</asp:Label>
                                                <asp:HiddenField ID="HdnProductId5" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId5" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck5" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos5" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType5" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme5" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG5" runat="server" Value='<%# Bind("TotalKG") %>' />
                                                <asp:TextBox ID="txtEuroUltra" runat="server" CssClass="form-control" MaxLength="3" 
                                                    onkeypress="return isNumber(event)" onchange='<%# "TotalNumber_hdEuroUltra(" + Eval("TotalKG") + ", this)"  %>'></asp:TextBox>
                                                <asp:HiddenField ID="hdEuroUltra" runat="server" />
                                                 <asp:HiddenField ID="hdEuroUltrapcode" runat="server" />
                                                <br />
                                                
                                                <asp:DropDownList ID="dropschemeEuroUltra" Width="100%" runat="server"></asp:DropDownList>
                                                <%--<asp:TextBox ID="txtschemeEuroUltra" MaxLength="160" Font-Size="X-Small" runat="server" CssClass="form-control" Visible="true"></asp:TextBox>--%>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                                <br />

                                <asp:GridView ID="grdEuroEWR" runat="server" CssClass="myGridClass" AutoGenerateColumns="false" OnRowDataBound="grdEuroEWR_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="EURO EWR">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:Label ID="llblpkgboxnoEuroEWR" runat="server">BOX</asp:Label>
                                                <asp:HiddenField ID="HdnProductId8" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId8" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck8" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos8" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType8" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme8" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG8" runat="server" Value='<%# Bind("TotalKG") %>' />

                                                <asp:TextBox ID="txtEuroEWR" runat="server" CssClass="form-control" MaxLength="3" 
                                                    onkeypress="return isNumber(event)" onchange='<%# "TotalNumber_hdEuroEWR(" + Eval("TotalKG") + ", this)"  %>'></asp:TextBox>
                                                <asp:HiddenField ID="hdEuroEWR" runat="server" />
                                                <asp:HiddenField ID="hdEuroEWRpcode" runat="server" />
                                                <br />
                                                <asp:DropDownList ID="dropschemeEuroEWR" Width="100%" runat="server"></asp:DropDownList>
                                                <%--<asp:TextBox ID="txtschemeEuroEWR" MaxLength="160"  Font-Size="X-Small" runat="server" CssClass="form-control" Visible="true"></asp:TextBox>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <div class="col-md-2">

                                <asp:GridView ID="GrdPvcGlue" runat="server" CssClass="myGridClass" AutoGenerateColumns="false" OnRowDataBound="GrdPvcGlue_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PVC GLUE">
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" Width="45px" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:Label ID="lblpkgboxnoPVcGlue" runat="server">BOX</asp:Label>

                                                <asp:HiddenField ID="HdnProductId6" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId6" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck6" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos6" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType6" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme6" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG6" runat="server" Value='<%# Bind("TotalKG") %>' />

                                                <asp:TextBox ID="txtPVcGlue" runat="server" CssClass="form-control" MaxLength="3" 
                                                    onkeypress="return isNumber(event)" onchange='<%# "TotalNumber_hdPVcGlue(" + Eval("TotalKG") + ", this)"  %>'></asp:TextBox>
                                                <asp:HiddenField ID="hdPVcGlue" runat="server" />
                                                 <asp:HiddenField ID="hdPVcGluepcode" runat="server" />
                                                <br />
                                                <asp:DropDownList ID="dropschemePvcGlue" Width="100%" runat="server"></asp:DropDownList>
                                                <%--<asp:TextBox ID="txtschemePvcGlue" runat="server" Font-Size="X-Small" MaxLength="160" CssClass="form-control" Visible="true"></asp:TextBox>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                                <br />

                                <asp:GridView ID="grdWoodStrong" runat="server" CssClass="myGridClass" AutoGenerateColumns="false" OnRowDataBound="grdWoodStrong_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="WOOD STRONG">
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Width="45px" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:Label ID="lblpkgboxnoWoodStrong" runat="server">BOX</asp:Label>

                                                <asp:HiddenField ID="HdnProductId7" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId7" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck7" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos7" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType7" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme7" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG7" runat="server" Value='<%# Bind("TotalKG") %>' />

                                                <asp:TextBox ID="txtWoodStrong" runat="server" CssClass="form-control" MaxLength="3" 
                                                    onkeypress="return isNumber(event)" onchange='<%# "TotalNumber_hdWoodStrong(" + Eval("TotalKG") + ", this)"  %>'></asp:TextBox>
                                                <asp:HiddenField ID="hdWoodStrong" runat="server" />
                                                 <asp:HiddenField ID="hdWoodStrongpcode" runat="server" />
                                                <br />
                                                <asp:DropDownList ID="dropschemeWoodStrong" Width="100%" runat="server"></asp:DropDownList>
                                                <%--<asp:TextBox ID="txtschemeWoodStrong" runat="server" Font-Size="X-Small" CssClass="form-control" MaxLength="160"  Visible="true"></asp:TextBox>--%>
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

                        <div class="col-md-1">Site Delivery</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtsitedelivery" runat="server" TextMode="MultiLine" MaxLength="160" Style="width: 100%; height: 100px;">
                            </asp:TextBox>
                        </div>

                        <div class="col-md-1">Other :</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtOther" runat="server" TextMode="MultiLine" MaxLength="160" Style="width: 100%; height: 100px;">
                            </asp:TextBox>
                        </div>

                        <div class="col-md-1">Pop :</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtPOP" runat="server" TextMode="MultiLine" MaxLength="160" Style="width: 100%; height: 100px;">
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
                        <div class="col-md-8">
                        </div>
                        <div class="col-md-1 pull-right;">
                            <asp:Button ID="btnSubmitOrder" runat="server" Text="Send" CssClass="btn btn-success" OnClick="btnSubmitOrder_Click" />
                            <%-- <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-dark" OnClick="btnClear_Click" />--%>
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
                      <center>
                        <asp:Label ID="lblerr"  ForeColor="Red"  runat="server"></asp:Label>
                     </center>
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
                                <label>State</label>

                                <asp:DropDownList ID="drpStateName" runat="server" CssClass="form-control">
                                </asp:DropDownList>
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
                    <asp:Button ID="btnSaveDealer" OnClick="btnSaveDealer_Click" Text="Save" runat="server" Class="btn btn-primary" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>




    <script type="text/javascript">
        $(document).ready(function () {

          <%--  $("#<%=btnAddDealer.ClientID%>").click(function () {
                if ($('#ContentPlaceHolder1_txtDealerCodeSearch').val().trim() != '') {
                    alert("Please remove Dealer code. Then after add dealer.");
                    return false;
                }

                $('#divdealercode').css("display", "none");
                $('#ModalTitle').text("Add Dealer");
                $('#ContentPlaceHolder1_lblErrorMessage').text("");



                $('#ContentPlaceHolder1_txtDealerCode').val('');
                $('#ContentPlaceHolder1_txtDealerName').val('');
                $('#ContentPlaceHolder1_txtAddress').val('');
                $('#ContentPlaceHolder1_txtArea').val('');
                $('#ContentPlaceHolder1_txtPhoneNo').val('');
                $('#ContentPlaceHolder1_txtGST').val('');
                $('#ContentPlaceHolder1_txtTransport').val('');

                $('#ContentPlaceHolder1_txtContactName').val('');
                $('#ContentPlaceHolder1_txtpincode').val('');



                $('#ContentPlaceHolder1_txtDealerCode').attr('readonly', false);
                $('#ContentPlaceHolder1_txtDealerName').attr('readonly', false);
                $('#ContentPlaceHolder1_txtAddress').attr('readonly', false);
                $('#ContentPlaceHolder1_txtArea').attr('readonly', false);
                $('#ContentPlaceHolder1_txtPhoneNo').attr('readonly', false);
                $('#ContentPlaceHolder1_txtGST').attr('readonly', false);
                $('#ContentPlaceHolder1_txtTransport').attr('readonly', false);

                $('#ContentPlaceHolder1_txtContactName').attr('readonly', false);
                $('#ContentPlaceHolder1_txtpincode').attr('readonly', false);

                $('#ContentPlaceHolder1_btnSaveDealer').show();
                $('#DealerModal').modal('show');

            });--%>




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

        <%--function TotalNumber(TotalKG, this1) {
            // Box/Qty Value
            var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }
            var HdnLastQty = $(this1).parent().find('input[name=myhiddenname]').val();

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
            //alert(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);

        
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            if (TotalKgCount == 0) {
                FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            }       
        
            $('#<%=lblTotal.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find('input[name=myhiddenname]').val($(this1).val());
        
        }--%>


        function TotalNumber_hdEUROXTRA(TotalKG, this1)
        {
            // Box/Qty Value
            var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }
            var HdnLastQty = $(this1).parent().find("[id*=hdEUROXTRA]").val();
            //alert(HdnLastQty);
            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }
            //alert(HdnLastQty);
            var HdnTotalKgCount = $('#<%=hdTotalKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }
            var TotalKgCount = 0;

            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            //alert(TotalKgCount);

            
            //alert(HdnLastQty);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);
          //  alert(HdnLastQty);
                        
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
           // if (TotalKgCount == 0) {
               // FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
          //  }       
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=lblTotal.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find("[id*=hdEUROXTRA]").val($(this1).val());
        }

        function TotalNumber_hdeurowp(TotalKG, this1)
        {
            // Box/Qty Value
            var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }
            var HdnLastQty = $(this1).parent().find("[id*=hdeurowp]").val();
           // alert(HdnLastQty);
            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }
            var HdnTotalKgCount = $('#<%=hdTotalKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }
            var TotalKgCount = 0;


            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            //alert(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);

        
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            if (TotalKgCount == 0) {
               
            }       
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=lblTotal.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find("[id*=hdeurowp]").val($(this1).val());
        }

        function TotalNumber_hdeuro2in1(TotalKG, this1)
        {

             var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }
            var HdnLastQty = $(this1).parent().find("[id*=hdeuro2in1]").val();
           // alert(HdnLastQty);
            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }
            var HdnTotalKgCount = $('#<%=hdTotalKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }
            var TotalKgCount = 0;


            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            //alert(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);

        
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            if (TotalKgCount == 0) {
               // FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            }       
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=lblTotal.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find("[id*=hdeuro2in1]").val($(this1).val());


            
        }

        function TotalNumber_hdExtreme(TotalKG, this1)
        {
            // Box/Qty Value
            var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }
            var HdnLastQty = $(this1).parent().find("[id*=hdExtreme]").val();
           // alert(HdnLastQty);
            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }
            var HdnTotalKgCount = $('#<%=hdTotalKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }
            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            //alert(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);

        
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            if (TotalKgCount == 0) {
               
            }       
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=lblTotal.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find("[id*=hdExtreme]").val($(this1).val());
        }


 function TotalNumber_hdEuroUltra(TotalKG, this1)
        {
            // Box/Qty Value
            var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }
            var HdnLastQty = $(this1).parent().find("[id*=hdEuroUltra]").val();
           // alert(HdnLastQty);
            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }
            var HdnTotalKgCount = $('#<%=hdTotalKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }
            var TotalKgCount = 0;

            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            //alert(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);

        
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            if (TotalKgCount == 0) {
               
            }       
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=lblTotal.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalKgCount.ClientID%>').val(FinalKgCount);
     $(this1).parent().find("[id*=hdEuroUltra]").val($(this1).val());
        }


        function TotalNumber_hdEuroUltra(TotalKG, this1)
        {
            // Box/Qty Value
            var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }
            var HdnLastQty = $(this1).parent().find("[id*=hdEuroUltra]").val();
           // alert(HdnLastQty);
            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }
            var HdnTotalKgCount = $('#<%=hdTotalKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }
            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            //alert(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);

        
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            if (TotalKgCount == 0) {
                
            }       
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=lblTotal.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find("[id*=hdEuroUltra]").val($(this1).val());
        }

         function TotalNumber_hdEuroEWR(TotalKG, this1)
        {
            // Box/Qty Value
            var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }
            var HdnLastQty = $(this1).parent().find("[id*=hdEuroEWR]").val();
            //alert(HdnLastQty);
            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }
            var HdnTotalKgCount = $('#<%=hdTotalKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }
            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            //alert(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);

        
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            if (TotalKgCount == 0) {
                
            }       
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=lblTotal.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalKgCount.ClientID%>').val(FinalKgCount);
             $(this1).parent().find("[id*=hdEuroEWR]").val($(this1).val());
        }
       

   function TotalNumber_hdPVcGlue(TotalKG, this1)
        {
            // Box/Qty Value
            var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }
            var HdnLastQty = $(this1).parent().find("[id*=hdPVcGlue]").val();
           // alert(HdnLastQty);
            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }
            var HdnTotalKgCount = $('#<%=hdTotalKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }
            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            //alert(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);

        
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            if (TotalKgCount == 0) {
              
            }       
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=lblTotal.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalKgCount.ClientID%>').val(FinalKgCount);
       $(this1).parent().find("[id*=hdPVcGlue]").val($(this1).val());
        }


 function TotalNumber_hdWoodStrong(TotalKG, this1)
        {
            // Box/Qty Value
            var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }
            var HdnLastQty = $(this1).parent().find("[id*=hdWoodStrong]").val();
            //alert(HdnLastQty);
            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }
            var HdnTotalKgCount = $('#<%=hdTotalKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }
            var TotalKgCount = 0;;


            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            //alert(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);

        
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            if (TotalKgCount == 0) {
              
            }       
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=lblTotal.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalKgCount.ClientID%>').val(FinalKgCount);
             $(this1).parent().find("[id*=hdWoodStrong]").val($(this1).val());
        }


    </script>
</asp:Content>


