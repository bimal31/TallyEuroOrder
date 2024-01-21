var $tblrows = "";
var orderdata = [], arrOrderProductDetail = [];
var totalPurchaseKg = 0;
var counterProddtl = 1, arrOrderProductDetailscounter = 1;
var counter = 1;
var productdatalist = [];
var index = 0;
var ProductPckIDEdit = "";
var ProductPackingschemeEdit = "";
$(document).ready(function () {

    if ($("#ContentPlaceHolder1_editorderid").val() != "") {
        GetBrandCategory();
        arrOrderProductDetail = [];
        $tblrows = $("#tblitemScheme tbody tr");
        GetProductData();

    }
    else {
        arrOrderProductDetail = []
        $tblrows = $("#tblitemScheme tbody tr");
        loadFirsttrProductItem();
        GetBrandCategory();
    }
});

//region item drodown fill
function GetBrandCategory() {
    $.ajax({
        type: "POST",
        url: "AddOrder.aspx/GetProcustlist",
        data: '{name: "abc" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $("#drpitem").html($("<option></option>").val('').html('---select---'));
            $.each(jQuery.parseJSON(data.d), function (key, item) {
                $("#drpitem").append($("<option></option>").val(item.ProductId).html(item.ProductName));
            });
        },
        failure: function () {
            alert("Failed!");
        }
    });
}
//endregion 


//region Productdata 
function GetProductData() {
    $.ajax({
        type: "POST",
        url: "AddOrder.aspx/GetProcustlist",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(jQuery.parseJSON(data.d), function (key, item) {
                var Product = {};
                Product.ProductId = item.ProductId;
                Product.ProductName = item.ProductName;
                productdatalist.push(Product);

            });
            GetSelectOrderProductDetails($("#ContentPlaceHolder1_editorderid").val());
        },
        failure: function () {
            alert("Failed!");
        }
    });
}
//endregion 


//region 
function GetSelectOrderProductDetails(orderid) {
    $.ajax({
        type: "POST",
        url: "AddOrder.aspx/GetOrderProductDetails",
        data: "{orderid:'" + orderid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            index = 0;
            $.each(jQuery.parseJSON(data.d), function (key, item) {
                var newRow = $("<tr>");
                var cols = "";
                cols += '<td ><select class="form-control  newdata' + counter + '" onchange="selectionitem(this,' + counter + ')" id="drpitem"> </select><input type="hidden" id="tempcounter" class="tempcounter" value=' + index + ' ><input type="hidden" id="tempdrpid" class="tempdrpid" value="" ></td>';
                cols += '<td ><select class="form-control ProductPacking' + counter + '"  id="drpProductPacking" onchange="selectionitemQty(this,' + counter + ')" > </select><input type="hidden" id="hdProductPackingid' + counter +'" class="tempdrpid" value="" ></td>';
                cols += '<td > <input type="text" onkeypress="return isNumber(event)" id="txtQty" name="txtQty" class="form-control txtQty" value=' + item.ProductQty + ' /></td>';
                cols += '<td ><select class="form-control ProductPackingscheme' + counter + '"  id="drpProductPackingscheme"> </select><input type="hidden" id="hdProductPackingschemeid' + counter +'" class="tempdrpid" value="" ></td>';
                cols += '<td > <input type="text" onkeypress="return isNumber(event)" id="ItemTotalKg" name="ItemTotalKg" class="form-control ItemTotalKg" value=' + item.TotalKg + ' disabled />';
                cols += '</td><td  style="display: none;"><input type="text" id="hdProdSrno" name="txtTotal" class="hdProdSrno" value=' + item.OrderSrNo + '></td>';
                cols += ' <td><input type="button" class="ibtnDel btn btn-danger " value="Delete"></td>';

                newRow.append(cols);

                $("table.order-list").append(newRow);
               
                makearraytable(item.ProductID, item.ProductPckID, item.ProductPck, item.PackingNos, item.PackingType,
                    item.PckTotalKg, item.ProductQty, item.SchemeId, item.ProductCode,
                    item.TotalKg, item.OrderSrNo, index);
                index++;
                $tblrows = $("#tblitemScheme tbody tr");
               

                $(".newdata" + counter).html($("<option></option>").val('').html('---select---'));
                for (var i = 0; i < productdatalist.length; i++) {
                    $(".newdata" + counter).append($("<option></option>").val(productdatalist[i].ProductId).html(productdatalist[i].ProductName));
                }
                $(".ProductPacking" + counter).closest("td").find("#hdProductPackingid" + counter).val(item.ProductPckID);
                $(".ProductPackingscheme" + counter).closest("td").find("#hdProductPackingschemeid" + counter).val(item.ProductPckID);
                
                $(".newdata" + counter).val(item.ProductId).trigger('change');
                $("#tempdrpid").val(item.ProductId);
                $(".newdata" + counter).closest("td").find("#tempdrpid").val(item.ProductId);
               
                
                counter++;
            });
            viewmodeoredit();

        },
        failure: function () {
            alert("Failed!");
        }
    });
}
//endregion 

//region new row add time item drop down fill
function DynamicGetBrandCategory(drpname) {
    $.ajax({
        type: "POST",
        url: "AddOrder.aspx/GetProcustlist",
        data: '{name: "abc" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $("." + drpname + "").html($("<option></option>").val('').html('---select---'));
            $.each(jQuery.parseJSON(data.d), function (key, item) {
                $("." + drpname + "").append($("<option></option>").val(item.ProductId).html(item.ProductName));
            });
        },
        failure: function () {
            alert("Failed!");
        }
    });
}
//endregion

//region first row add in page
function loadFirsttrProductItem() {
    var newRow = $("<tr>");
    var cols = "";

    cols += '<td ><select class="form-control  newdata' + counter + '" onchange="selectionitem(this,' + counter + ')" id="drpitem"> </select><input type="hidden" id="tempcounter" class="tempcounter" value=' + index + ' ><input type="hidden" id="tempdrpid" class="tempdrpid" value="" ></td>';
    cols += '<td ><select class="form-control ProductPacking' + counter + '"  id="drpProductPacking" onchange="selectionitemQty(this,' + counter + ')" > </select></td>';
    cols += '<td > <input type="text" onkeypress="return isNumber(event)" id="txtQty" name="txtQty" class="form-control txtQty" /></td>';
    cols += '<td ><select class="form-control ProductPackingscheme' + counter + '"  id="drpProductPackingscheme"> </select></td>';
    cols += '<td > <input type="text" onkeypress="return isNumber(event)" id="ItemTotalKg" name="ItemTotalKg" class="form-control ItemTotalKg" disabled />';
    cols += '</td><td  style="display: none;"><input type="text" id="hdProdSrno" name="txtTotal" class="hdProdSrno" value="1"></td>';
    cols += ' <td><input type="button" class="ibtnDel btn btn-danger " value="Delete"></td>';
    newRow.append(cols);

    $("table.order-list").append(newRow);
    $tblrows = $("#tblitemScheme tbody tr");
    purchageonchange();
    if ($("#ContentPlaceHolder1_isview").val() == "1") {
        $(".txtQty").attr("disabled", "disabled");
    }
    else {
        $(".txtQty").removeAttr("disabled", "disabled");
    }
    index++;
}
//endregion 

//add row wis total qty calc
function purchageonchange() {
    var isshow = false;
    var selected = false;

    $tblrows.each(function (index) {
        var $tblrow = $(this);
        var first = 0;
        if (first == 0) {
        }
        first++;
        $tblrow.find('.txtQty').on('change', function (event) {
            if ($tblrow.find('.txtQty').val() != '') {
                Purchase = (parseFloat($tblrow.find('.txtQty').val()) * parseFloat($tblrow.find("#drpProductPacking option:selected").attr("TotalKG")));
                $tblrow.find('.ItemTotalKg').val(Purchase);
                getordertabledata();
                freecount = 0;
                $("table.order-list tbody tr").each(function () {
                    var hdProdSrno = $(this).find("td").find(".ItemTotalKg").val();
                    freecount = (parseFloat(freecount) + parseFloat(hdProdSrno));
                });
                $("#ContentPlaceHolder1_lblTotal").val(freecount);
            }

        });
        $tblrow.find('#drpProductPacking').on('change', function (event) {
            var Purchase = 0;
            if ($tblrow.find('.txtQty').val() != '') {
                Purchase = (parseFloat($tblrow.find('.txtQty').val()) * parseFloat($tblrow.find("#drpProductPacking option:selected").attr("TotalKG")));
                $tblrow.find('.ItemTotalKg').val(Purchase);
                getordertabledata();
                freecount = 0;
                $("table.order-list tbody tr").each(function () {
                    var hdProdSrno = $(this).find("td").find(".ItemTotalKg").val();
                    freecount = (parseFloat(freecount) + parseFloat(hdProdSrno));
                });
                $("#ContentPlaceHolder1_lblTotal").val(freecount);
            }

        });
    });
}
//endregion

//region ordertabeldata  
function getordertabledata() {
    orderdata = [];
    totalPurchaseKg = 0;
    var index = 0;
    $("table.order-list tbody tr").each(function () {

        var ProductID = $(this).find("td").eq(0).find("select option:selected").val();
        var ProductPckID = $(this).find("td").eq(1).find("select option:selected").val();

        var ProductPck = $(this).find("td").eq(1).find("select option:selected").attr("ProductPck");
        var PackingNos = $(this).find("td").eq(1).find("select option:selected").attr("PackingNos");
        var PackingType = $(this).find("td").eq(1).find("select option:selected").attr("PackingType");
        var PckTotalKg = $(this).find("td").eq(1).find("select option:selected").attr("TotalKG");

        var Qty = $(this).find("td").eq(2).find(":text").val();
        var SchemeId = $(this).find("td").eq(3).find("select option:selected").val();
        var ProductCode = $(this).find("td").eq(3).find("select option:selected").attr("ProductCode");
        var TotalKg = $(this).find("td").eq(4).find(":text").val();
        var hdProdSrno = $(this).find("td").find(".hdProdSrno").val();
        makearraytable(ProductID, ProductPckID, ProductPck, PackingNos, PackingType, PckTotalKg, Qty, SchemeId, ProductCode,
            TotalKg, hdProdSrno, index);
        index++;

    });
}

function makearraytable(ProductID, ProductPckID, ProductPck, PackingNos, PackingType, PckTotalKg, Qty, SchemeId, ProductCode,
    TotalKg, hdProdSrno, index) {

    var data = {};
    data.ProductID = ProductID;
    data.ProductPckID = ProductPckID;
    data.ProductPck = ProductPck;
    data.PackingNos = PackingNos;

    data.PackingType = PackingType;
    data.PckTotalKg = PckTotalKg;
    data.Qty = Qty;

    data.SchemeId = SchemeId;
    data.ProductCode = ProductCode;
    data.TotalKg = TotalKg;
    data.hdProdSrno = hdProdSrno;
    data.index = index;
    orderdata.push(data);
}
//region ordertabeldata  

//region select item wise data productpacking class call
function selectionitem(thisval, drppos) {

    var row = $(thisval).closest("tr");
    var selected = $(thisval).val();
    var tempindex = $(row).find("#tempcounter").val();
    $("table.order-list tbody tr").each(function () {
        var old = $(this).find("td").find(".item option:selected").val();
        var oldindex = $(this).find("#tempcounter").val();
        if (selected == old && tempindex != oldindex) {
            $(thisval).val($(row).find('.tempdrpid').val());
            return;
        }
    });
    $(row).find('#tempdrpid').val($(thisval).val());
    getProductPacking(selected, drppos);
}
//endregion 

// region product packing  drop fill
function getProductPacking(ids, drppos) {
    $.ajax({
        type: "POST",
        url: "AddOrder.aspx/GetProductPacking",
        data: '{id:' + ids + ' }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $(".ProductPacking" + drppos + "").html($("<option></option>").val('').html('---select---'));
            $.each(jQuery.parseJSON(data.d), function (key, item) {
                $(".ProductPacking" + drppos + "").append($("<option value=" + item.ProductPckID + " PackingNos=" + item.PackingNos + " PackingType=" + item.PackingType + " ProductPck=" + item.ProductPck + " TotalKG=" + item.TotalKG + " IsScheme=" + item.IsScheme + " >" + item.ProductPck + '-' + item.PackingType + "</option>"));
            });
            
            if ($("#ContentPlaceHolder1_editorderid").val() != "")
            {
                $(".ProductPacking" + drppos).val($(".ProductPacking" + drppos).closest("td").find("#hdProductPackingid" + drppos).val());
                
                $(".ProductPacking" + drppos).trigger('change');

                $(".ProductPackingscheme" + drppos).val($(".ProductPackingscheme" + drppos).closest("td").find("#hdProductPackingschemeid" + drppos).val())

                    //$(".ProductPackingscheme" + counter).val(item.SchemeId);
                purchageonchange();
            }
        },
        failure: function () {
            alert("Failed!");
        }

    });

}
//endregion product packing


// region schem drop down fill item wise
function selectionitemQty(thisval, drppos) {
    $(".ProductPackingscheme" + drppos + " option").remove();
    var ids = $(thisval).val();
    var Stateid = $("#ContentPlaceHolder1_hdstate").val();
    $.ajax({
        type: "POST",
        url: "AddOrder.aspx/GetProductPackingScheme",
        data: '{ProductPckId:' + ids + ', Stateid : ' + Stateid + ' }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(jQuery.parseJSON(data.d), function (key, item) {
                $(".ProductPackingscheme" + drppos + "").append($("<option value=" + item.SchemeId + " >" + item.schemeProductCode + "</option>"));
            });
            purchageonchange();
        },
        failure: function () {
            alert("Failed!");
        }
    });
}

//region add row new item 
$("#addrow").on("click", function () {
    var isProselect = false;

    if ($("#ContentPlaceHolder1_txtDealerCodeSearch").val() == "") {
        alert("Enter Dealer Code after you Added Row ...!");
        $("#ContentPlaceHolder1_txtDealerCodeSearch").focus();
        return;
    }
    var isblankcolumn = false;


    $("table.order-list tbody tr").each(function () {
        var ddlval = $(this).find("td").eq(0).find("select option:selected").val();
        if (ddlval == "") {
            $(this).find("td").eq(0).find(":text").focus();
            alert("Select Item ...!");
            isblankcolumn = true;
            return
        }
        if ($(this).find("td").eq(1).find(":text").val() == "") {
            $(this).find("td").eq(1).find(":text").focus();
            alert("Enter Qty ...!");
            isblankcolumn = true;
            return
        }

    })
    if (isblankcolumn == false) {
        counter++;
        var newRow = $("<tr>");
        var cols = "";
        cols += '<td ><select class="form-control  newdata' + counter + '" onchange="selectionitem(this,' + counter + ')" id="drpitem"> </select><input type="hidden" id="tempcounter" class="tempcounter" value=' + index + ' ><input type="hidden" id="tempdrpid" class="tempdrpid" value="" ></td>';
        cols += '<td ><select class="form-control ProductPacking' + counter + '"  id="drpProductPacking" onchange="selectionitemQty(this,' + counter + ')" > </select></td>';
        cols += '<td > <input type="text" onkeypress="return isNumber(event)" id="txtQty" name="txtQty" class="form-control txtQty" /></td>';
        cols += '<td ><select class="form-control ProductPackingscheme' + counter + '"  id="drpProductPackingscheme"> </select></td>';
        cols += '<td > <input type="text" onkeypress="return isNumber(event)" id="ItemTotalKg" name="ItemTotalKg" class="form-control ItemTotalKg" disabled />';
        cols += '</td><td  style="display: none;"><input type="text" id="hdProdSrno" name="txtTotal" class="hdProdSrno" value="1"></td>';
        cols += ' <td><input type="button" class="ibtnDel btn btn-danger " value="Delete"></td>';
        newRow.append(cols);

        $("table.order-list").append(newRow);
        DynamicGetBrandCategory("newdata" + counter);
    }
    $tblrows = $("#tblitemScheme tbody tr");

    purchageonchange();
    index++;

});
//endregion 

//region  delete row item table
$("table.order-list").on("click", ".ibtnDel", function (event) {
    if (confirm("Are you sure you want to delete this?")) {
        if ($("#tblitemScheme tbody tr").length == 1) {
            alert("At least one row in Product list");
            return;
        }
        var serialno = $(this).closest("tr").find(".hdProdSrno").val();
        $(this).closest("tr").remove();
        for (var i = 0; i < orderdata.length; i++) {
            if (orderdata[i].srno == serialno) // delete index
            {
                //arrOrderProductDetail[i].remove();
                orderdata.splice(i, 1);
                i--;
            }
        }

        for (var j = 0; j < arrOrderProductDetail.length; j++) {
            if (arrOrderProductDetail[j].srno == serialno) // delete index
            {
                arrOrderProductDetail.splice(j, 1);
                j--;
            }
        }
        freecount = 0;
        $("table.order-list tbody tr").each(function () {
            var hdProdSrno = $(this).find("td").find(".ItemTotalKg").val();
            freecount = (parseInt(freecount) + parseInt(hdProdSrno));
        });
        $("#ContentPlaceHolder1_lblTotal").val(freecount);
    }
    else {
        return false;
    }
});
//end region


// region button save click event 

$("#btnSubmitOrder").on("click", function myfunction() {
    //Product free item compare
    var freecount = 0, purchasecount = 0;

    var Stateid = $("#ContentPlaceHolder1_hdstate").val();

    if (formvalidation() == true) {

        var pkg = 0;
        var TotalFreeKgs = 0;


        var ReturnId = 0;
        var flg = "0";
        var Totalqty = $("#ContentPlaceHolder1_lblTotal").val();
        //if (TotalFreeKg != "" && Totalqty != "") {
        //    if (TotalFreeKg == Totalqty) {
        //        flg = "1";
        //    }
        //}
        getordertabledata();

        var data = {};
        data.OrderType = "Order";
        data.DealerId = $("#ContentPlaceHolder1_hdDelaerId").val();
        data.ParentOrderId = "0";
        data.Transport = $("#ContentPlaceHolder1_txttransport").val();
        data.Other = $("#ContentPlaceHolder1_txtOther").val();
        data.POP = $("#ContentPlaceHolder1_txtPOP").val();
        data.SiteDelivery = $("#ContentPlaceHolder1_txtsitedelivery").val();
        data.OrderStatus = $("#ContentPlaceHolder1_drpOrderStatus").val();
        data.TotalKgGm = $("#ContentPlaceHolder1_lblTotal").val();
        var editid = $("#ContentPlaceHolder1_editorderid").val();
        if (editid == "") {
            data.OrderID = 0;
        }
        else {
            data.OrderID = editid;
        }
        data.SalesId = $("#ContentPlaceHolder1_drpsSalesExe").val();
        data.IsFree = false;
        $.ajax({
            type: "POST",
            url: "AddOrder.aspx/SaveData",
            data: "{Stateid: " + Stateid + ", data:'" + JSON.stringify(data) + "',OrderProductDetails:'" + JSON.stringify(orderdata) + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                if (data.d != "") {
                    alert("Order Save Successfully.");

                    window.location.replace("OrderList.aspx");
                }

            },
            failure: function () {
                alert("Something wrong Insert Order.");
            }
        });

    }
});

// endregion 

//region form validation 
function formvalidation() {
    var isvalid = false;
    //alert($("#ContentPlaceHolder1_hdDelaerId").val());
    if ($("#ContentPlaceHolder1_hdDelaerId").val() == "" || $("#ContentPlaceHolder1_hdDelaerId").val() == undefined) {
        alert("Could not find Dealer records");
        $("#ContentPlaceHolder1_txtDealerCodeSearch").focus();
        return isvalid;
    }
    else if ($("#ContentPlaceHolder1_drpsSalesExe option:selected").val() == "0" || ($("#ContentPlaceHolder1_drpsSalesExe").val() == undefined)) {
        alert("Please select sales Executive");
        $("ContentPlaceHolder1_drpsSalesExe").focus();
        return isvalid;
    }
    else {
        isvalid = true;
        return true;
    }
}

//endregion 


// region view 
function viewmodeoredit() {
    if ($("#ContentPlaceHolder1_isview").val() == "1") {

        $("#addrow").hide();
        $("#addrow").attr("disabled", "disabled");


        $(".ibtnDel").hide();
        $(".item").attr("disabled", "disabled");
        $("#btnSubmitOrder").hide();
        $("#ContentPlaceHolder1_txtDealerCodeSearch").attr("disabled", "disabled");
        $("ContentPlaceHolder1_drpsSalesExe").attr("disabled", "disabled");

    }
    else {
        $(".ibtnDel").removeAttr("disabled", "disabled");
        $(".item").removeAttr("disabled", "disabled");
        $("#btnSubmitOrder").removeAttr("disabled", "disabled");
        $("#ContentPlaceHolder1_txtDealerCodeSearch").removeAttr("disabled", "disabled");

    }
}
//ednregion