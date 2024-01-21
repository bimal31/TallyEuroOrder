<%@ Page Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="FreeOrderOld.aspx.cs" Inherits="OrderApp.FreeOrderOld" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        <div class="col-md-2">
                            <asp:Label ID="lbldealercode" runat="server" Text="Dealer Code"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDealerCodeSearch" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Order Date:</label>
                        </div>
                        <div class="col-md-4">
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
                        <div class="col-md-4">
                            <asp:TextBox ID="txtdealernamesearch" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>

                        <div class="col-md-2">
                            <label>Sales Executive :</label>
                        </div>
                        <div class="col-md-4">
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
                            <label>Transport:</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txttransport" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Order Status:</label>
                        </div>
                        <div class="col-md-4">
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

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            From Scheme:
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtFromScheme" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            To Scheme:
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtToScheme" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-2">
                            Total Kgs:
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtTotalKgsF" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Free Total Kgs:
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtFreetotalkg" runat="server" Text="0" CssClass="form-control"></asp:TextBox>
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
                                <asp:GridView ID="gridEUROXTRA" runat="server" CssClass="myGridClass" AutoGenerateColumns="false" 
                                    OnRowDataBound="gridEUROXTRA_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="EURO XTRA">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("ProductPacking") %>'></asp:Label>
                                                <asp:Label ID="lblpkgboxnoEUROXTRA" runat="server">Nos</asp:Label>
                                                <asp:HiddenField ID="HdnProductId1" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId1" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck1" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos1" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType1" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG1" runat="server" Value='<%# Bind("TotalKG") %>' />
                                                <asp:HiddenField ID="hdEUROXTRA"  runat="server" /> 
                                                <asp:TextBox ID="txtEUROXTRA" runat="server"  CssClass="form-control" MaxLength="3"
                                                onchange=<%# "TotalNumber_hdEUROXTRA(" + Eval("ProductPck") + ", '" + Eval("PackingType") + "', this)"  %>
                                                onkeypress="return isNumber(event)"></asp:TextBox>

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
                                                <asp:Label ID="lblpkgboxnoeurowp" runat="server">Nos</asp:Label>
                                                <asp:HiddenField ID="HdnProductId2" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId2" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck2" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos2" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType2" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG2" runat="server" Value='<%# Bind("TotalKG") %>' />
                                                 <asp:TextBox ID="txteurowp" runat="server"  CssClass="form-control" MaxLength="3"
                                                 onkeypress="return isNumber(event)" 
                                                 onchange=<%# "TotalNumber_hdeurowp(" + Eval("ProductPck") + ", '" + Eval("PackingType") + "', this)"  %>></asp:TextBox>
                                            
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
                                                <asp:Label ID="lblpkgboxnoeuro2in1" runat="server">Nos</asp:Label>

                                                <asp:HiddenField ID="HdnProductId3" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId3" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck3" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos3" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType3" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG3" runat="server" Value='<%# Bind("TotalKG") %>' />
                                                  <asp:TextBox ID="txteuro2in1" runat="server"  CssClass="form-control" MaxLength="3" 
                                                      onkeypress="return isNumber(event)" 
                                                      onchange=<%# "TotalNumber_hdeuro2in1(" + Eval("ProductPck") + ", '" + Eval("PackingType") + "', this)"  %>></asp:TextBox>
                                              
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
                                                <asp:Label ID="lblpkgboxnoExtreme" runat="server">Nos</asp:Label>

                                                <asp:HiddenField ID="HdnProductId4" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId4" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck4" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos4" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType4" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG4" runat="server" Value='<%# Bind("TotalKG") %>' />
                                               <asp:TextBox ID="txtExtreme" runat="server" CssClass="form-control"  MaxLength="3"
                                                    onkeypress="return isNumber(event)" 
                                                   onchange=<%# "TotalNumber_hdExtreme(" + Eval("ProductPck") + ", '" + Eval("PackingType") + "', this)"  %>></asp:TextBox>
                                             
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
                                                <asp:Label ID="llblpkgboxnoEuroUltra" runat="server">Nos</asp:Label>
                                                <asp:HiddenField ID="HdnProductId5" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId5" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck5" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos5" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType5" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme5" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG5" runat="server" Value='<%# Bind("TotalKG") %>' />
                                                <asp:TextBox ID="txtEuroUltra" runat="server" CssClass="form-control"  MaxLength="3"
                                                       onkeypress="return isNumber(event)"
                                                       onchange=<%# "TotalNumber_hdEuroUltra(" + Eval("ProductPck") + ", '" + Eval("PackingType") + "', this)"  %>></asp:TextBox>
                                             
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
                                                <asp:Label ID="llblpkgboxnoEuroEWR" runat="server">Nos</asp:Label>
                                                <asp:HiddenField ID="HdnProductId8" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId8" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck8" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos8" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType8" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme8" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG8" runat="server" Value='<%# Bind("TotalKG") %>' />

                                                 <asp:TextBox ID="txtEuroEWR" runat="server" CssClass="form-control"  MaxLength="3"
                                                       onkeypress="return isNumber(event)" 
                                                      onchange=<%# "TotalNumber_hdEuroEWR(" + Eval("ProductPck") + ", '" + Eval("PackingType") + "', this)"  %>></asp:TextBox>
                                              

                                                <asp:HiddenField ID="hdEuroEWR" runat="server" />
                                                <asp:HiddenField ID="hdEuroEWRpcode" runat="server" />
                                                <br />
                                                <asp:DropDownList ID="dropschemeEuroEWR" Width="100%" runat="server"></asp:DropDownList>
                                                <asp:TextBox ID="txtschemeEuroEWR" MaxLength="160"  Font-Size="X-Small" runat="server" CssClass="form-control" Visible="true"></asp:TextBox>
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
                                                <asp:Label ID="lblpkgboxnoPVcGlue" runat="server">Nos</asp:Label>

                                                <asp:HiddenField ID="HdnProductId6" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId6" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck6" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos6" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType6" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme6" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG6" runat="server" Value='<%# Bind("TotalKG") %>' />

                                                  <asp:TextBox ID="txtPVcGlue" runat="server" CssClass="form-control"  MaxLength="3"
                                                       onkeypress="return isNumber(event)" 
                                                      onchange=<%# "TotalNumber_hdPVcGlue(" + Eval("ProductPck") + ", '" + Eval("PackingType") + "', this)"  %>></asp:TextBox>
                                              
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
                                                <asp:Label ID="lblpkgboxnoWoodStrong" runat="server">Nos</asp:Label>

                                                <asp:HiddenField ID="HdnProductId7" runat="server" Value='<%# Bind("ProductId") %>' />
                                                <asp:HiddenField ID="HdnProductPckId7" runat="server" Value='<%# Bind("ProductPckID") %>' />
                                                <asp:HiddenField ID="HdnProductPck7" runat="server" Value='<%# Bind("ProductPck") %>' />
                                                <asp:HiddenField ID="HdnPackingNos7" runat="server" Value='<%# Bind("PackingNos") %>' />
                                                <asp:HiddenField ID="HdnPackingType7" runat="server" Value='<%# Bind("PackingType") %>' />
                                                <asp:HiddenField ID="HdnIsScheme7" runat="server" Value='<%# Bind("IsScheme") %>' />
                                                <asp:HiddenField ID="HdnTotalProductPckKG7" runat="server" Value='<%# Bind("TotalKG") %>' />

                                                 <asp:TextBox ID="txtWoodStrong" runat="server" CssClass="form-control"  MaxLength="3" 
                                                     onkeypress="return isNumber(event)" 
                                                     onchange=<%# "TotalNumber_hdWoodStrong(" + Eval("ProductPck") + ", '" + Eval("PackingType") + "', this)"  %>></asp:TextBox>
                                                
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
                            <asp:TextBox ID="txtFreeKg" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-8">
                        </div>
                        <div class="col-md-1 pull-right;">
                            <asp:Button ID="btnSubmitOrder" runat="server" Text="Send" CssClass="btn btn-success" OnClick="btnSubmitOrder_Click" />
                            <%--<asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-dark" OnClick="btnClear_Click" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <asp:HiddenField ID="hdDelaerId" runat="server" />
    <asp:HiddenField ID="hdOrderId" runat="server" />
    <asp:HiddenField ID="hdFreeOrderId" runat="server" />
    <asp:HiddenField ID="hdTotalFreeKgCount" runat="server" />
    <asp:HiddenField ID="hdTotalKgCount" runat="server" />
    <asp:HiddenField ID="hdEditOrderId" runat="server" />

    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        <%--function TotalNumber(TotalKG, PackingType, this1) {
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
            var HdnTotalKgCount = $('#<%=hdTotalFreeKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }
            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);

            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);

            if (PackingType.toLowerCase() == 'gm') {
                TotalKgCount = TotalKgCount / 1000;
                HdnLastQty = HdnLastQty / 1000;
            }

            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            //if (TotalKgCount == 0) {
            //    FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            //}       
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=txtFreeKg.ClientID%>').val(round(FinalKgCount, 0));
            $('#<%=hdTotalFreeKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find('input:hidden').val($(this1).val());

            var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
            var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();

            //if (parseFloat(txtFreeKg) > parseFloat(txtFreetotalkg)) {
            //    alert("You can't add more Free kg than allowed kg.");
            //}
        }--%>

        $('#ContentPlaceHolder1_txtFromScheme').change(function () {

            if ($('#ContentPlaceHolder1_txtFromScheme').val() == "" || $('#ContentPlaceHolder1_txtToScheme').val() == "") {

                $('#ContentPlaceHolder1_txtFreetotalkg').val(0);
            }
            else {

              
                $('#ContentPlaceHolder1_txtFreetotalkg').val((($('#ContentPlaceHolder1_txtTotalKgsF').val() * $('#ContentPlaceHolder1_txtToScheme').val()) / $('#ContentPlaceHolder1_txtFromScheme').val()));
                var FreeTotalKg = Math.ceil($('#ContentPlaceHolder1_txtFreetotalkg').val());
                $('#ContentPlaceHolder1_txtFreetotalkg').val(FreeTotalKg);

                var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
                var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();

            }

        });

        $('#ContentPlaceHolder1_txtToScheme').change(function () {

            if ($('#ContentPlaceHolder1_txtFromScheme').val() == "" || $('#ContentPlaceHolder1_txtToScheme').val() == "") {

                $('#ContentPlaceHolder1_txtFreetotalkg').val(0);
            }
            else {

                $('#ContentPlaceHolder1_txtFreetotalkg').val((($('#ContentPlaceHolder1_txtTotalKgsF').val() * $('#ContentPlaceHolder1_txtToScheme').val()) / $('#ContentPlaceHolder1_txtFromScheme').val()));
                var FreetotalKg = Math.ceil($('#ContentPlaceHolder1_txtFreetotalkg').val());
                $('#ContentPlaceHolder1_txtFreetotalkg').val(FreetotalKg);

                var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
                var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();

                //if (parseFloat(txtFreeKg) > parseFloat(txtFreetotalkg)) {
                //    alert("You can't add more Free kg than allowed kg.");
                //}
            }
        });


        function round(value, exp) {
            if (typeof exp === 'undefined' || +exp === 0)
                return Math.round(value);

            value = +value;
            exp = +exp;

            if (isNaN(value) || !(typeof exp === 'number' && exp % 1 === 0))
                return NaN;

            // Shift
            value = value.toString().split('e');
            value = Math.round(+(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp)));

            // Shift back
            value = value.toString().split('e');
            return +(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp));
        }


        function TotalNumber_hdEUROXTRA(TotalKG, PackingType, this1) {
            // Box/Qty Value
            var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }

            // Last Box/Qty Value
            var HdnLastQty = $(this1).parent().find("[id*=hdEUROXTRA]").val();

            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }
            //else if (HdnLastQty > 0)
            //{
            //    HdnLastQty= HdnLastQty;
            //}

            // Total Value
            var HdnTotalKgCount = $('#<%=hdTotalFreeKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }
           
            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
           
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);


            if (PackingType.toLowerCase() == 'gram' || PackingType.toLowerCase() == 'gm') {
                TotalKgCount = TotalKgCount / 1000;
                HdnLastQty = HdnLastQty / 1000;
            }
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            //alert(HdnTotalKgCount);
            //alert(HdnLastQty);
            //alert(FinalKgCount);
            // if Box value is 0 then substract it with Last value
            if (TotalKgCount == 0) {
            //    FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            }
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=txtFreeKg.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalFreeKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find("[id*=hdEUROXTRA]").val($(this1).val());
            var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
            var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();
        }


        function TotalNumber_hdeurowp(TotalKG, PackingType, this1) {

            var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }

            // Last Box/Qty Value
            var HdnLastQty = $(this1).parent().find("[id*=hdeurowp]").val();

            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }

            // Total Value
            var HdnTotalKgCount = $('#<%=hdTotalFreeKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }

            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);


            if (PackingType.toLowerCase() == 'gram' || PackingType.toLowerCase() == 'gm') {
                TotalKgCount = TotalKgCount / 1000;
                HdnLastQty = HdnLastQty / 1000;
            }
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            //if (TotalKgCount == 0) {
            //    FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            //}
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=txtFreeKg.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalFreeKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find("[id*=hdeurowp]").val($(this1).val());
            var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
            var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();
           

        }

        function TotalNumber_hdeuro2in1(TotalKG, PackingType, this1) {


            var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }

            // Last Box/Qty Value
            var HdnLastQty = $(this1).parent().find("[id*=hdeuro2in1]").val();

            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }

            // Total Value
            var HdnTotalKgCount = $('#<%=hdTotalFreeKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }

            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);


            if (PackingType.toLowerCase() == 'gram' || PackingType.toLowerCase() == 'gm') {
                TotalKgCount = TotalKgCount / 1000;
                HdnLastQty = HdnLastQty / 1000;
            }
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            //if (TotalKgCount == 0) {
            //    FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            //}
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=txtFreeKg.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalFreeKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find("[id*=hdeuro2in1]").val($(this1).val());
            var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
            var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();

          
        }


        function TotalNumber_hdExtreme(TotalKG, PackingType, this1) {

             var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }

            // Last Box/Qty Value
            var HdnLastQty = $(this1).parent().find("[id*=hdExtreme]").val();

            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }

            // Total Value
            var HdnTotalKgCount = $('#<%=hdTotalFreeKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }

            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);


            if (PackingType.toLowerCase() == 'gram' || PackingType.toLowerCase() == 'gm') {
                TotalKgCount = TotalKgCount / 1000;
                HdnLastQty = HdnLastQty / 1000;
            }
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            //if (TotalKgCount == 0) {
            //    FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            //}
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=txtFreeKg.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalFreeKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find("[id*=hdExtreme]").val($(this1).val());
            var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
            var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();
         }

        function TotalNumber_hdEuroUltra(TotalKG, PackingType, this1) {

             var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }

            // Last Box/Qty Value
            var HdnLastQty = $(this1).parent().find("[id*=hdEuroUltra]").val();

            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }

            // Total Value
            var HdnTotalKgCount = $('#<%=hdTotalFreeKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }

            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);


            if (PackingType.toLowerCase() == 'gram' || PackingType.toLowerCase() == 'gm') {
                TotalKgCount = TotalKgCount / 1000;
                HdnLastQty = HdnLastQty / 1000;
            }
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            //if (TotalKgCount == 0) {
            //    FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            //}
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=txtFreeKg.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalFreeKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find("[id*=hdEuroUltra]").val($(this1).val());
            var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
            var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();

            
           }


        function TotalNumber_hdEuroEWR(TotalKG, PackingType, this1) {

              var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }

            // Last Box/Qty Value
            var HdnLastQty = $(this1).parent().find("[id*=hdEuroEWR]").val();

            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }

            // Total Value
            var HdnTotalKgCount = $('#<%=hdTotalFreeKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }

            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);


            if (PackingType.toLowerCase() == 'gram' || PackingType.toLowerCase() == 'gm') {
                TotalKgCount = TotalKgCount / 1000;
                HdnLastQty = HdnLastQty / 1000;
            }
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            //if (TotalKgCount == 0) {
            //    FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            //}
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=txtFreeKg.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalFreeKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find("[id*=hdEuroEWR]").val($(this1).val());
            var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
            var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();



        }

        function TotalNumber_hdPVcGlue(TotalKG, PackingType, this1) {
            // Box/Qty Value

               var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }

            // Last Box/Qty Value
            var HdnLastQty = $(this1).parent().find("[id*=hdPVcGlue]").val();

            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }

            // Total Value
            var HdnTotalKgCount = $('#<%=hdTotalFreeKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }

            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);


            if (PackingType.toLowerCase() == 'gram' || PackingType.toLowerCase() == 'gm') {
                TotalKgCount = TotalKgCount / 1000;
                HdnLastQty = HdnLastQty / 1000;
            }
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            //if (TotalKgCount == 0) {
            //    FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            //}
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=txtFreeKg.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalFreeKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find("[id*=hdPVcGlue]").val($(this1).val());
            var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
            var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();


 

        }

        function TotalNumber_hdWoodStrong(TotalKG, PackingType, this1) {

               var ProductQty = $(this1).val();
            if (ProductQty == '') {
                ProductQty = parseFloat(0);
            }

            // Last Box/Qty Value
            var HdnLastQty = $(this1).parent().find("[id*=hdWoodStrong]").val();

            if (HdnLastQty == '') {
                HdnLastQty = parseFloat(0);
            }

            // Total Value
            var HdnTotalKgCount = $('#<%=hdTotalFreeKgCount.ClientID%>').val();
            if (HdnTotalKgCount == '') {
                HdnTotalKgCount = parseFloat(0);
            }

            var TotalKgCount = 0;
            TotalKgCount = TotalKG * ProductQty;
            TotalKgCount = parseFloat(TotalKgCount);
            HdnLastQty = TotalKG * HdnLastQty;
            HdnLastQty = parseFloat(HdnLastQty);


            if (PackingType.toLowerCase() == 'gram' || PackingType.toLowerCase() == 'gm') {
                TotalKgCount = TotalKgCount / 1000;
                HdnLastQty = HdnLastQty / 1000;
            }
            var FinalKgCount = parseFloat(TotalKgCount) + parseFloat(HdnTotalKgCount);
            // if Box value is 0 then substract it with Last value
            //if (TotalKgCount == 0) {
            //    FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            //}
            FinalKgCount = parseFloat(FinalKgCount) - parseFloat(HdnLastQty);
            $('#<%=txtFreeKg.ClientID%>').val(FinalKgCount);
            $('#<%=hdTotalFreeKgCount.ClientID%>').val(FinalKgCount);
            $(this1).parent().find("[id*=hdWoodStrong]").val($(this1).val());
            var txtFreetotalkg = $('#<%=txtFreetotalkg.ClientID%>').val();
            var txtFreeKg = $('#<%=txtFreeKg.ClientID%>').val();
           
          
        }
      

       


  

 

    </script>
</asp:Content>
