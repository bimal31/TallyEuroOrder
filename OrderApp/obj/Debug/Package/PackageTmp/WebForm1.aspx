<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="OrderApp.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Button ID="ButtonAdd" runat="server"
                Text="Add New Row" OnClick="ButtonAdd_Click" />

            <asp:GridView ID="grvStudentDetails" runat="server"
                ShowFooter="True" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333"
                GridLines="None" OnRowDeleting="grvStudentDetails_RowDeleting" OnRowDataBound="OnRowDataBound">
                <Columns>

                    <asp:BoundField DataField="RowNumber" HeaderText="SNo" />
                    <asp:TemplateField HeaderText="Product">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlproduct" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Purchase Kg">
                        <ItemTemplate>
                            <asp:TextBox ID="txtpurchase" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From Scheme:">
                        <ItemTemplate>
                            <asp:TextBox ID="txtFromScheme" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="To Scheme:">
                        <ItemTemplate>
                            <asp:TextBox ID="txtToScheme" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>



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
                        <asp:Button ID="btnSaveDealer"  Text="Save" runat="server" Class="btn btn-primary" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>



    </form>
</body>
<script type="text/javascript">

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

</script>

</html>

