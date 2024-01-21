
var $tblrows = "";
var orderdata = [], arrOrderProductDetail = [];
var totalPurchaseKg = 0;
var counterProddtl = 1, arrOrderProductDetailscounter = 1;
var counter = 1;
var productdatalist = [];
$(document).ready(function () {

    if ($("#ContentPlaceHolder1_editorderid").val() != "") 
	{
	    GetBrandCategory();
        arrOrderProductDetail = [];
        $tblrows = $("#tblitemScheme tbody tr");
       
        GetProductData();
        purchageonchange();

        //alert($("#ContentPlaceHolder1_isview").val());
        if ($("#ContentPlaceHolder1_isview").val() == "1") 
		{
            $("#addrow").hide();
            $("#ContentPlaceHolder1_txtPurchaseKg").attr("disabled", "disabled");
            $("#addrow").attr("disabled", "disabled");
            $("#ibtnDel").attr("disabled", "disabled");
            $(".purchasekg").attr("disabled", "disabled");
            $(".FromScheme").attr("disabled", "disabled");
            $(".ToScheme").attr("disabled", "disabled");
            $(".newdata2").attr("disabled", "disabled");
        }
    }
    else {
        arrOrderProductDetail = []
        GetBrandCategory();
        $tblrows = $("#tblitemScheme tbody tr");
        loadFirsttrProductItem();
    }
});


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
            alert("Enter Purchase Kg ...!");
            isblankcolumn = true;
            return
        }
        else if ($(this).find("td").eq(2).find(":text").val() == "") {
            $(this).find("td").eq(2).find(":text").focus();
            alert("Enter From Scheme ...!");
            isblankcolumn = true;
        }
        else if ($(this).find("td").eq(3).find(":text").val() == "") {
            $(this).find("td").eq(3).find(":text").focus();
            alert("Enter To Scheme ...!");
            isblankcolumn = true;
        }
    })
    if (isblankcolumn == false) {
        counter++;
        var newRow = $("<tr>");
        var cols = "";
        cols += '<td ><select id="drpitem" onchange="selectionitem(this)" class="form-control item newdata' + counter + '"></select><input type="hidden" id="tempcounter" class="tempcounter" value=' + index + ' ><input type="hidden" id="tempdrpid" class="tempdrpid" value=""></td>';

        cols += '<td ><input type="text" id="purchasekg" onkeypress="return isNumber(event)" name="txtPurchaseKg" class="form-control purchasekg" /> </td>';
        cols += '<td ><input type="text" id="FromScheme" onkeypress="return isNumber(event)" name="txtFromScheme" class="form-control FromScheme" /></td>';
        cols += '<td ><input type="text"  id="ToScheme"  onkeypress="return isNumber(event)" name="txtToScheme" class="form-control ToScheme" /></td>';

        cols += '<td ><input type="text" id="TotalFreeKg" onkeypress="return isNumber(event)"  name="txtTotalFreeKgs" class="form-control TotalFreeKg" disabled/></td>';
        cols += ' <td style="display:none;"> <input type="text" id="hdProdSrno" name="txtTotalFreeKgs" class="hdProdSrno" value=' + counter + '> </td>';

        cols += '<td ><button type="button" id="btnfreemodel" class="btn btn-info btnfreemodel" >Free </button></td>';
        cols += '<td > <input type="button" class="ibtnDel btn btn-danger" value="Delete"></td>';

        newRow.append(cols);
        $("table.order-list").append(newRow);

        DynamicGetBrandCategory("newdata" + counter);
    }
    $tblrows = $("#tblitemScheme tbody tr");

    purchageonchange();
    index++;

});

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
            var hdProdSrno = $(this).find("td").find(".TotalFreeKg").val();
            freecount = parseInt(parseInt(freecount) + parseInt(hdProdSrno));

        });
        $("#ContentPlaceHolder1_lblTotal").val(freecount);
    }
    else {
        return false;
    }
});
$("#tblOrderProductDetail tbody").on("click", ".ibtnDelProdRows", function (event) {
    if (confirm("Are you sure you want to delete this?")) {
        var trcounter = $(this).closest("tr").find("td").find(".arrOrderProductDetailscounterdata").val();
        //alert(trcounter);
        $(this).closest("tr").remove();
        for (var i = 0; i < arrOrderProductDetail.length; i++) {
            if (arrOrderProductDetail[i].arrOrderProductDetailscounterdata == trcounter) // delete index
            {
                //arrOrderProductDetail[i].remove();
                arrOrderProductDetail.splice(i, 1);
                i--;
            }
        }
        popuplistfreekgsum();
    }
    else {
        return false;
    }
});
var index = 0;
function loadFirsttrProductItem() {
    var newRow = $("<tr>");
    var cols = "";

    cols += '<td ><select class="form-control item" onchange="selectionitem(this)" id="drpitem"> </select><input type="hidden" id="tempcounter" class="tempcounter" value=' + index + ' ><input type="hidden" id="tempdrpid" class="tempdrpid" value="" ></td>';
    cols += '<td > <input type="text" onkeypress="return isNumber(event)" id="purchasekg" name="txtPurchaseKg" class="form-control  purchasekg" /></td>';
    cols += '<td > <input type="text" onkeypress="return isNumber(event)" id="FromScheme" name="txtFromScheme" class="form-control FromScheme" /></td>';
    cols += '<td ><input type="text" onkeypress="return isNumber(event)" id="ToScheme" name="txtToScheme" class="form-control ToScheme" /></td>';
    cols += '<td > <input type="text" onkeypress="return isNumber(event)" onchange="TotalFreeKg();" id="TotalFreeKg" name="txtTotalFreeKgs" class="form-control TotalFreeKg" disabled />';
    cols += '</td><td  style="display: none;"><input type="text" id="hdProdSrno" name="txtTotalFreeKgs" class="hdProdSrno" value="1"></td>';
    cols += '<td ><a class="deleteRow"></a><button type="button" id="btnfreemodel" class="btn btn-info btnfreemodel">Free </button></td>';
    cols += ' <td><input type="button" class="ibtnDel btn btn-danger " value="Delete"></td>';
    newRow.append(cols);
    $("table.order-list").append(newRow);
    $tblrows = $("#tblitemScheme tbody tr");
    purchageonchange();
    if ($("#ContentPlaceHolder1_isview").val() == "1") {
        $(".purchasekg").attr("disabled", "disabled");
        $(".FromScheme").attr("disabled", "disabled");
        $(".ToScheme").attr("disabled", "disabled");
    }
    else {
        $(".purchasekg").removeAttr("disabled", "disabled");
        $(".FromScheme").removeAttr("disabled", "disabled");
        $(".ToScheme").removeAttr("disabled", "disabled");
    }
    index++;
}






function makearraytable(item, FromScheme, ToScheme, PurchaseK, TotalFreeKg, hdProdSrno, index)
 {

    var data = {};
    data.item = item;
    data.FromScheme = FromScheme;
    data.ToScheme = ToScheme;
    data.PurchaseK = PurchaseK;
    data.TotalFreeKg = TotalFreeKg;
    data.PckTotalKg = $("#ContentPlaceHolder1_txtPurchaseKg").val();
    data.hdProdSrno = hdProdSrno;
    data.index = index;
    orderdata.push(data);
}

function selectionitem(thisval) {
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
}


function selectionitemFreeitem(thisval) {
    var selected = $(thisval).val();
    $("#tblOrderProductDetail tbody tr").each(function () {
        var old = $(this).find("#ProductPckID").val();
        if (selected == old) {
            $(thisval).val('');
            alert("Already exist");
            return;
        }
    });
   
}
//$(".item").on('change', function () {
//    alert("ere");
//    var selectitem = this.value;
//    $("table.order-list tbody tr").each(function () {
//        var old = $(this).find("td").find(".item option:selected").val();
//        alert(selectitem + " " + old)
//        if (selectitem == old) {
//            alert("selected");
//            $tblrow.find('.item').val($(this).find("td").find('.tempdrpid').val());
//            return;
//        }
//    });
//    $("#tempdrpid").val($tblrow.find('.item option:selected').val());
//});


function purchageonchange() {
    var isshow = false;
    var selected = false;
    $tblrows.each(function (index)
    {
        //exist array orderdata

        var $tblrow = $(this);
        // alert($tblrow);
        var first = 0;
        if (first == 0) {

        }
        first++;
        $tblrow.find('.purchasekg').on('change', function (event)
        {
                //var qty = $tblrow.find('.purchasekg').val();
                var Purchase = 0;
                if ($tblrow.find('.purchasekg').val() != '' && $tblrow.find('.ToScheme').val() != '' && $tblrow.find('.FromScheme').val() != '') {

                    Purchase = Math.ceil(((parseFloat($tblrow.find('.purchasekg').val()) * parseFloat($tblrow.find('.ToScheme').val())) / parseFloat($tblrow.find('.FromScheme').val())));
                    $tblrow.find('.TotalFreeKg').val(Purchase);
                    getordertabledata();
                    freecount = 0;
                    $("table.order-list tbody tr").each(function () {
                        var hdProdSrno = $(this).find("td").find(".TotalFreeKg").val();
                        freecount = parseInt(parseInt(freecount) + parseInt(hdProdSrno));

                    });
                    $("#ContentPlaceHolder1_lblTotal").val(freecount);
                }
            
        });
        $tblrow.find('.FromScheme').on('change', function (event)
        {
            //var qty = $tblrow.find('.purchasekg').val();
            var Purchase = 0;
            if ($tblrow.find('.purchasekg').val() != '' && $tblrow.find('.ToScheme').val() != '' && $tblrow.find('.FromScheme').val() != '') {

                Purchase = Math.ceil(((parseFloat($tblrow.find('.purchasekg').val()) * parseFloat($tblrow.find('.ToScheme').val())) / parseFloat($tblrow.find('.FromScheme').val())));

                $tblrow.find('.TotalFreeKg').val(Purchase);

                getordertabledata();
                freecount = 0;
                $("table.order-list tbody tr").each(function () {
                    var hdProdSrno = $(this).find("td").find(".TotalFreeKg").val();
                    freecount = parseInt(parseInt(freecount) + parseInt(hdProdSrno));

                });
                $("#ContentPlaceHolder1_lblTotal").val(freecount);
            }

        });
        $tblrow.find('.ToScheme').on('change', function (event) {
            //var qty = $tblrow.find('.purchasekg').val();
            var Purchase = 0;
            if ($tblrow.find('.purchasekg').val() != '' && $tblrow.find('.ToScheme').val() != '' && $tblrow.find('.FromScheme').val() != '') {

                Purchase = Math.ceil(((parseFloat($tblrow.find('.purchasekg').val()) * parseFloat($tblrow.find('.ToScheme').val())) / parseFloat($tblrow.find('.FromScheme').val())));

                $tblrow.find('.TotalFreeKg').val(Purchase);

                getordertabledata();

                freecount = 0;
                $("table.order-list tbody tr").each(function () {
                    var hdProdSrno = $(this).find("td").find(".TotalFreeKg").val();
                    freecount = parseInt(parseInt(freecount) + parseInt(hdProdSrno));

                });
                $("#ContentPlaceHolder1_lblTotal").val(freecount);

            }
        });

        $tblrow.find(".btnfreemodel").on("click", function myfunction() {
            $("#package").text('');
            if ($("#ContentPlaceHolder1_txtDealerCodeSearch").val() == "") {
                alert("Enter Dealer Code after you Added Row ...!");
                $("#ContentPlaceHolder1_txtDealerCodeSearch").focus();
                return;
            }
            getProductPacking($tblrow.find('.item option:selected').val());
            if ($tblrow.find('.item option:selected').val() == "") {
                $("#modelfreeitem").modal("hide");
                alert("Select Product Item ...!");
                return;

            }
            else {
                $("#itemname").text($tblrow.find('.item option:selected').text());
                $("#modeltitle").text($tblrow.find('.item option:selected').text());
                $("#Productid").val($tblrow.find('.item option:selected').val());
                $("#popuplistfreekg").val('');
                $("#popuplistfreekg").attr('disabled', 'disabled');
                if ($tblrow.find('.Producttotalfreekg').val() == "") {
                    alert("Total Free Kg Value blank ...!");
                    return;
                }
                $("#Producttotalfreekg").text($tblrow.find('.TotalFreeKg').val())
                // alert($tblrow.find('.hdProdSrno').val())
                $(".srnoProd").val($tblrow.find('.hdProdSrno').val());

                $("#tblOrderProductDetail tbody").html('');

                $("#modelfreeitem").modal("show");

                for (var i = 0; i < arrOrderProductDetail.length; i++) {
                    if (arrOrderProductDetail[i].srno == $("#srnoProd").val()) // delete index
                    {
					    fillOrderProductDetails(arrOrderProductDetail[i].PKG, arrOrderProductDetail[i].QTY, arrOrderProductDetail[i].PackingType, arrOrderProductDetail[i].PackingNos, arrOrderProductDetail[i].isscheme, arrOrderProductDetail[i].srno, arrOrderProductDetail[i].arrOrderProductDetailscounterdata, arrOrderProductDetail[i].PckTotalKg, arrOrderProductDetail[i].Schemetext, arrOrderProductDetail[i].ProductPckID);
                    }
                }

            }

        });
    });
}
var freecount = 0;

function productdatalist() {

}

$(".SaveOrderProductDetail").on("click", function myfunction() {
    if ($("#drppkg").val() == "") {
        alert("Select KG ...!");
        return;
    }
    if ($("#qty").val() == "") {
        alert("Select qty ...!");
        return;
    }
	//alert($("#dropschemeId option:selected").text());
    //alert($("#dropschemeId option:selected").val());
    //return false;
    $tblProdRows = $("#tblOrderProductDetail tbody tr");
    var newRow = $("<tr>");
    var cols = "";
    counterProddtl++;
    cols += '<td class="col-sm-3"><label  id="pkg" class="pkg">' + $("#drppkg option:selected").text() + ' (' + $("#package").text() + ')' + '</label>';
    cols += '<input style="display:none" type="text" id="ProductPckID" value=' + $("#drppkg option:selected").val() + ' class="ProductPckID" ></td>';

    cols += '<td class="col-sm-2"><label  id="qty" class="qty">' + $("#qty").val() + '</label></td>';
    cols += '<td  class="col-sm-1"  style="display:none"> <input type="text" id="srnoProddtl" class="srnoProddtl" value=' + $(".srnoProd").val() + '></td>';
    cols += '<td  class="col-sm-1"  style="display:none">';
    cols += '<input type="text" id="arrOrderProductDetailscounterdata" class="arrOrderProductDetailscounterdata" value=' + arrOrderProductDetailscounter + '>';
    cols += '<input type="text" id="packingnos" class="packingnos" value=' + $("#drppkg option:selected").attr("packingnos") + '>';
    cols += '<input type="text" id="totalkg" class="totalkg" value=' + $("#drppkg option:selected").attr("totalkg") + '>';
    cols += '<input type="text" id="ProductPck" class="ProductPck" value=' + $("#drppkg option:selected").attr("ProductPck") + '>';
    cols += '<input type="text" id="PackingType" class="PackingType" value=' + $("#drppkg option:selected").attr("PackingType") + '>';
    cols += '<input type="text" id="isscheme" class="isscheme" value=' + $("#drppkg option:selected").attr("isscheme") + '>';
    cols += '<input type="text" id="Schemetextdata" class="Schemetextdata" value=' + $("#dropschemeId option:selected").text() + '>';
    cols += '<input type="text" id="SchemeId" class="SchemeId" value=' + $("#dropschemeId option:selected").val() + '>';
    cols += ' </td>';
    cols += '<td class="col-sm-2"><label  id="NOS" class="NOS">NOS</label></td>';
    var totalkgcount = 0, totalfree = 0;
    
    if ($("#package").text() == "gm") {
        totalkgcount = parseFloat($("#drppkg option:selected").text()) / 1000;
        totalfree = parseFloat(totalkgcount) * parseFloat($("#qty").val());
    }
    else {
        totalfree = parseFloat($("#drppkg option:selected").text()) * parseFloat($("#qty").val());
    }

    cols += '<td class="col-sm-2"><label  id="overalltotalfreekg" class="overalltotalfreekg">' + parseFloat(totalfree) + '</label></td>';
    cols += '<td  class="col-sm-1"> <input type="button" class="ibtnDelProdRows btn btn-danger "  value="Delete"></td>';

    newRow.append(cols);

    arrOrderProductDetailscounter++;
    $("#tblOrderProductDetail tbody").append(newRow);
    clearpopupdatadtl();
    popuplistfreekgsum();

});


function prddtlmodelclose() {
    if ($("#Producttotalfreekg").text() != $("#popuplistfreekg").val()) {
        alert("Free Product Kg And Added list free kg not equilize  ...!");
        return;
    }

    $("#modelfreeitem").modal("hide");

    SaveClose();
}
function fillOrderProductDetails(pkg, qty, packingtype, packingnos, isscheme, srno, arrOrderProductDetailscounterdata, PckTotalKg, 
			Schemetextdata, ProductPckId, SchemeId) {

			

    var newRow = $("<tr>");
    var cols = "";
    counterProddtl++;

    cols += '<td class="col-sm-2"><label  id="pkg" class="pkg">' + pkg + ' ' + ' (' + packingtype + ')' + '</label>';
    cols += '<input style="display:none" type="text" id="ProductPckID" class="ProductPckID" value=' + ProductPckId + '></td>';

    cols += '<td class="col-sm-2"><label  id="qty" class="qty">' + qty + '</label></td>';
    cols += '<td  class="col-sm-1"  style="display:none"> <input type="text" id="srnoProddtl" class="srnoProddtl" value=' + srno + '></td>';
    cols += '<td  class="col-sm-1"  style="display:none">';
    cols += '<input type="text" id="arrOrderProductDetailscounterdata" class="arrOrderProductDetailscounterdata" value=' + arrOrderProductDetailscounterdata + '>';
    cols += '<input type="text" id="packingnos" class="packingnos" value=' + packingnos + '>';
    cols += '<input type="text" id="totalkg" class="totalkg" value=' + PckTotalKg + '>';
    cols += '<input type="text" id="ProductPck" class="ProductPck" value=' + pkg + '>';
    cols += '<input type="text" id="PackingType" class="PackingType" value=' + packingtype + '>';
    cols += '<input type="text" id="isscheme" class="isscheme" value=' + isscheme + '>';
    cols += '<input type="text" id="Schemetextdata" class="Schemetextdata" value=' + Schemetextdata + '>';
    cols += '<input type="text" id="SchemeId" class="SchemeId" value=' + SchemeId + '>';
    cols += ' </td>';
    cols += '<td class="col-sm-2"><label  id="NOS" class="NOS">NOS</label></td>';
    var totalkgcount = 0, totalfree = 0;

    if (packingtype == "gm") {
        totalkgcount = parseFloat(pkg) / 1000;
        totalfree = parseFloat(totalkgcount) * parseFloat(qty);
    }
    else {
        totalfree = parseFloat(pkg) * parseFloat(qty);
    }

    cols += '<td class="col-sm-2"><label  id="overalltotalfreekg" class="overalltotalfreekg">' + parseFloat(totalfree) + '</label></td>';
    cols += '<td  class="col-sm-1"> <input type="button" class="ibtnDelProdRows btn btn-danger "  value="Delete"></td>';

    newRow.append(cols); $("#tblOrderProductDetail tbody").append(newRow);
    clearpopupdatadtl();
    popuplistfreekgsum();
}

function clearpopupdatadtl() {
    $("#drppkg").val('');
    $("#qty").val('');
    $("#Schemetext").val('');
    $("#totalfreekg").text('');
    $("#package").val('');
    $("#popuplistfreekg").val('');
   // alert('sdfs');
    //$("#dropschemeId").select2("val", "");
    $(".drpscheme option").remove();

}
function popuplistfreekgsum() 
{
    var popuplistfreekgsum = 0;
    $("#tblOrderProductDetail tbody tr").each(function () {
        var totalfreekg = $(this).find("td").find("#overalltotalfreekg").text();
        popuplistfreekgsum = parseFloat(parseFloat(popuplistfreekgsum) + parseFloat(totalfreekg));
    });
    $("#popuplistfreekg").val(parseFloat(popuplistfreekgsum));
}


function SaveClose() 
{

    var srid = $("#srnoProd").val();

    for (var i = 0; i < arrOrderProductDetail.length; i++) {
        if (arrOrderProductDetail[i].srno == srid) // delete index
        {
            //arrOrderProductDetail[i].remove();
            arrOrderProductDetail.splice(i, 1);
            i--;
        }
    }

    $("#tblOrderProductDetail tbody tr").each(function ()
    {
        var checkblankdata = true;
        var SingleArrOrderProductDetail = {};

        SingleArrOrderProductDetail.PKG = $(this).find("td").find(".ProductPck").val();
        SingleArrOrderProductDetail.QTY = $(this).find("td").find(".qty").text();
        SingleArrOrderProductDetail.srno = srid;
        SingleArrOrderProductDetail.arrOrderProductDetailscounterdata = $(this).find("td").find(".arrOrderProductDetailscounterdata").val();
        SingleArrOrderProductDetail.Productid = $(".Productid").val();

        SingleArrOrderProductDetail.ProductPckID = $(this).find("td").find(".ProductPckID").val();
        SingleArrOrderProductDetail.PckTotalKg = $("#totalkg").val();
        SingleArrOrderProductDetail.PackingNos = $(this).find("td").find(".packingnos").val();
        SingleArrOrderProductDetail.isscheme = $(this).find("td").find(".isscheme").val();
        SingleArrOrderProductDetail.PackingType = $(this).find("td").find(".PackingType").val();
        SingleArrOrderProductDetail.Schemetext = $(this).find("td").find("#Schemetextdata").val();
        SingleArrOrderProductDetail.SchemeId = $(this).find("td").find("#SchemeId").val();

        SingleArrOrderProductDetail.overalltotalfreekg = $(this).find("td").find("#overalltotalfreekg").val();
        arrOrderProductDetail.push(SingleArrOrderProductDetail);

    });
    $("#tblOrderProductDetail tbody").html('');
}

function getordertabledata() {
    orderdata = [];
    totalPurchaseKg = 0;
    var index = 0;
    $("table.order-list tbody tr").each(function () {

        var ddlval = $(this).find("td").eq(0).find("select option:selected").val();
        var purchasekg = $(this).find("td").eq(1).find(":text").val();
        var FromScheme = $(this).find("td").eq(2).find(":text").val();
        var ToScheme = $(this).find("td").eq(3).find(":text").val();
        var TotalFreeKg = $(this).find("td").eq(4).find(":text").val();
        var hdProdSrno = $(this).find("td").find(".hdProdSrno").val();
        totalPurchaseKg = parseInt(parseInt(totalPurchaseKg) + parseInt(TotalFreeKg));
        makearraytable(ddlval, FromScheme, ToScheme, purchasekg, TotalFreeKg, hdProdSrno, index);
        index++;

    });
}


function GetBrandCategory() {
    $.ajax({
        type: "POST",
        url: "AddDealerOrderScheme.aspx/GetProcustlist",
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
function getProductPacking(ids) {
    $.ajax({
        type: "POST",
        url: "AddDealerOrderScheme.aspx/GetProductPacking",
        data: '{id:' + ids + ' }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            $(".drpProductPacking").html($("<option></option>").val('').html('---select---'));
            $.each(jQuery.parseJSON(data.d), function (key, item) {
                $(".drpProductPacking").append($("<option value=" + item.ProductPckID + " PackingNos=" + item.PackingNos + " PackingType=" + item.PackingType + " ProductPck=" + item.ProductPck + " TotalKG=" + item.TotalKG + " IsScheme=" + item.IsScheme + " >" + item.ProductPck + "</option>"));
                //$(".drpProductPacking").append($("<option></option>").val(item.ProductPckID).html(item.ProductPck + " (" + item.PackingType + ") "));
            });
        },
        failure: function () {
            alert("Failed!");
        }
    });
}

$("#drppkg").on('change', function myfunction() {
    ///var isscheme = $("#drppkg option:selected").attr("isscheme");
    $(".drpscheme option").remove();
   var Stateid = $("#ContentPlaceHolder1_hdstate").val();
    $("#package").text($("#drppkg option:selected").attr("PackingType"));
    var ProductPckId = ($("#drppkg option:selected").attr("value"));
    $.ajax({
        type: "POST",
        url: "AddDealerOrderScheme.aspx/GetProductPackingScheme",
        data: '{ProductPckId:' + ProductPckId + ', Stateid : ' + Stateid + ' }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            //$(".drpscheme").html($("<option></option>").val('').html('---select---'));
            $.each(jQuery.parseJSON(data.d), function (key, item) {
                $(".drpscheme").append($("<option value=" + item.SchemeId + " >" + item.SchemeName + "</option>"));
            });
        },
        failure: function () {
            alert("Failed!");
        }
    });
    
});


function GetProductData() {
    $.ajax({
        type: "POST",
        url: "AddDealerOrderScheme.aspx/GetProcustlist",
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
            GetSelectOrderFreeProduct($("#ContentPlaceHolder1_editorderid").val());
            GetSelectOrderProductDetails($("#ContentPlaceHolder1_editorderid").val());
        },
        failure: function () {
            alert("Failed!");
        }
    });
}

function DynamicGetBrandCategory(drpname) {
    $.ajax({
        type: "POST",
        url: "AddDealerOrderScheme.aspx/GetProcustlist",
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

$("#btnfreemodel").on("click", function myfunction() {
    $("#package").text('');
    $("#modelfreeitem").modal("show");
});



//Dynamic Table add or remove

function calculateRow(row) {
    var price = +row.find('input[name^="price"]').val();

}

function calculateGrandTotal() {
    var grandTotal = 0;
    $("table.order-list").find('input[name^="price"]').each(function () {
        grandTotal += +$(this).val();
    });
    $("#grandtotal").text(grandTotal.toFixed(2));
}
function sortOn(property) {
    return function (a, b) {
        if (a[property] < b[property]) {
            return -1;
        } else if (a[property] > b[property]) {
            return 1;
        } else {
            return 0;
        }
    }
}

$("#btnSubmitOrder").on("click", function myfunction() {
    //Product free item compare
    var freecount = 0, purchasecount = 0;

	var Stateid = $("#ContentPlaceHolder1_hdstate").val();
	
    $("table.order-list tbody tr").each(function () {
        var hdProdSrno = $(this).find("td").find(".TotalFreeKg").val();
        var purchasecount1 = $(this).find("td").find(".purchasekg").val();
        freecount = parseInt(parseInt(freecount) + parseInt(hdProdSrno));
        purchasecount = parseInt(parseInt(purchasecount) + parseInt(purchasecount1));

    });
    
    if (parseFloat(purchasecount) != parseFloat($("#ContentPlaceHolder1_txtPurchaseKg").val())) {
        alert("List Product Purchase Kg not equal to Purchase Kg  ...!");
        $("#ContentPlaceHolder1_txtPurchaseKg").focus();
        return;
    }

    if (parseFloat(freecount) != parseFloat($("#ContentPlaceHolder1_lblTotal").val())) {
        alert("Product Free Kg not equal to Total free Please check   ...!");
        $("#ContentPlaceHolder1_lblTotal").focus();
        return;
    }

    //END

    //////Free buton data compare arrOrderProductDetail[i].PKG, arrOrderProductDetail[i].QTY
    var isalldatafill = false;
   
    if (arrOrderProductDetail.length==0) 
	{
        alert("Enter Free Item Data ...!");
        return;
    }
    $("table.order-list tbody tr").each(function () {
        
        //alert($(this).find('td').find('.TotalFreeKg').val());
        var totalkgcount = 0, totalfree = 0;
        arrOrderProductDetail.sort(sortOn("srno"));
        for (var i = 0; i < arrOrderProductDetail.length; i++) {
            if (arrOrderProductDetail[i].srno == $(this).find('td').find('.hdProdSrno').val()) {
                if (arrOrderProductDetail[i].PackingType == "gm") {
                    totalkgcount = parseFloat(arrOrderProductDetail[i].PKG) / 1000;
                    totalfree += parseFloat(totalkgcount) * parseFloat(arrOrderProductDetail[i].QTY);
                }
                else {
                    totalfree += parseFloat(arrOrderProductDetail[i].PKG) * parseFloat(arrOrderProductDetail[i].QTY);
                   
                }
              
               
            }
        }
      //  alert(totalfree +"  "+ $(this).find('td').find('.TotalFreeKg').val());
        if (totalfree != $(this).find('td').find('.TotalFreeKg').val()) {
            isalldatafill = true;
            getProductPacking($(this).find('.item option:selected').val());
            $("#itemname").text( $(this).find('td').find('.item option:selected').text());
            $("#modeltitle").text($(this).find('td').find('.item option:selected').text());
            $("#Productid").val($(this).find('td').find('.item option:selected').val());
            $("#popuplistfreekg").val('');
            $("#popuplistfreekg").attr('disabled', 'disabled');
          
            $("#Producttotalfreekg").text($(this).find('td').find('.TotalFreeKg').val())
            // alert($tblrow.find('.hdProdSrno').val())
            $(".srnoProd").val($(this).find('td').find('.hdProdSrno').val());

            $("#tblOrderProductDetail tbody").html('');

            $("#modelfreeitem").modal("show");

            for (var i = 0; i < arrOrderProductDetail.length; i++) {
                if (arrOrderProductDetail[i].srno == $("#srnoProd").val()) // delete index
                {
				    fillOrderProductDetails(arrOrderProductDetail[i].PKG, arrOrderProductDetail[i].QTY, arrOrderProductDetail[i].PackingType,
					 arrOrderProductDetail[i].PackingNos, arrOrderProductDetail[i].isscheme, arrOrderProductDetail[i].srno, 
					 arrOrderProductDetail[i].arrOrderProductDetailscounterdata, arrOrderProductDetail[i].PckTotalKg,
					  arrOrderProductDetail[i].Schemetext, arrOrderProductDetail[i].ProductPckID, arrOrderProductDetail[i].SchemeId);

                }
            }

            return;
        }
    });
   
    if (isalldatafill==true) {
        alert("Enter Free Product Data");
        return;
    }
    


    if (formvalidation() == true) {

        var pkg = 0;
        var TotalFreeKgs = 0;
        listvalidationfree();
		
        if ($("#ContentPlaceHolder1_txtPurchaseKg").val() == "") {
            alert("Enter Purchase kg ...!");
            $("#ContentPlaceHolder1_txtPurchaseKg").focus();
            return;
        }
        $("table.order-list tbody tr").each(function () {
            pkg = parseInt(parseInt(pkg) + parseInt($(this).find("td").find(".purchasekg").val()));

            if ($(this).find("td").eq(4).find(":text").val() != "") {
                TotalFreeKgs = parseInt(parseInt(TotalFreeKgs) + parseInt($(this).find("td").eq(4).find(":text").val()));
            }

        });
        //alert("#ContentPlaceHolder1_txtPurchaseKg" + " " + pkg);
        if (parseInt($("#ContentPlaceHolder1_txtPurchaseKg").val()) > parseInt(pkg)) {
            alert("Total Free Kg value is not matching with Total value of product kg. Please check value and enter again.");
            return;

        }
        else if (parseInt($("#ContentPlaceHolder1_txtPurchaseKg").val()) < parseInt(pkg)) {
            alert("Total Free Kg value is not matching with Total value of product kg. Please check value and enter again.");
            return;

        }
        else if (parseInt($("#ContentPlaceHolder1_txtPurchaseKg").val()) == parseInt(pkg)) {
            var ReturnId = 0;
            var flg = "0";
            var Totalqty = $("#ContentPlaceHolder1_lblTotal").val();
            if (TotalFreeKg != "" && Totalqty != "") {
                if (TotalFreeKg == Totalqty) {
                    flg = "1";
                }
            }
            getordertabledata();

            var data = {};
            data.OrderType = "Free Scheme";
            data.DealerId = $("#ContentPlaceHolder1_hdDelaerId").val();
            data.ParentOrderId = "0";
            data.Transport = $("#ContentPlaceHolder1_txttransport").val();
            data.Other = $("#ContentPlaceHolder1_txtOther").val();
            data.POP = $("#ContentPlaceHolder1_txtPOP").val();
            data.SiteDelivery = $("#ContentPlaceHolder1_txtsitedelivery").val();
            data.OrderStatus = $("#ContentPlaceHolder1_drpOrderStatus").val();
            data.PurchaseDurationFromDate = $("#ContentPlaceHolder1_txtFromDate").val();
            data.PurchaseDurationToDate = $("#ContentPlaceHolder1_txtToDate").val();
            data.PurchaseKgs = $("#ContentPlaceHolder1_txtPurchaseKg").val();
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
                url: "AddDealerOrderScheme.aspx/SaveData",
                data: "{Stateid: " + Stateid + ", data:'" + JSON.stringify(data) + "',productFreedata:'" + JSON.stringify(orderdata) + "',OrderProductDetails:'" + JSON.stringify(arrOrderProductDetail) + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {

                    if (data.d != "") {
                        alert("Order Save Successfully.");

                        window.location.replace("DealerOrderSchemeList.aspx");
                    }

                },
                failure: function () {
                    alert("Something wrong Insert Order.");
                }
            });
        }
        else {
            // alert("false");
        }
    }
});

function listvalidationfree() {
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
            alert("Enter Purchase Kg ...!");
            isblankcolumn = true;
            return
        }
        else if ($(this).find("td").eq(2).find(":text").val() == "") {
            $(this).find("td").eq(2).find(":text").focus();
            alert("Enter From Scheme ...!");
            isblankcolumn = true;
        }
        else if ($(this).find("td").eq(3).find(":text").val() == "") {
            $(this).find("td").eq(3).find(":text").focus();
            alert("Enter To Scheme ...!");
            isblankcolumn = true;
        }
    })
}
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
    else if ($("#ContentPlaceHolder1_txtFromDate").val() == "" || $("#ContentPlaceHolder1_txtFromDate").val() == undefined) {
        alert("Please Enter From Date");
        $("ContentPlaceHolder1_txtFromDate").focus();
        return isvalid;
    }
    else if ($("#ContentPlaceHolder1_txtToDate").val() == "" || $("#ContentPlaceHolder1_txtToDate").val() == undefined) {
        alert("Please Enter To Date");
        $("ContentPlaceHolder1_txtToDate").focus();
        return isvalid;
    }
   
    else {
        isvalid = true;
        return true;
    }
}

//END

//Get Data From free prodcut  id
function GetSelectOrderFreeProduct(orderid) {

    //alert(JSON.stringify(productdatalist));
    $.ajax({
        type: "POST",
        url: "AddDealerOrderScheme.aspx/GetSelectOrderFreeProduct",
        data: "{orderid:'" + orderid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            index = 0;
            //Main Table Make 
            $.each(jQuery.parseJSON(data.d), function (key, item) {
                var newRow = $("<tr>");
                var cols = "";

                cols += '<td ><select id="drpitem" onchange="selectionitem(this)" class="form-control item newdata' + counter + '"></select>';
                cols += '  <input type="hidden" id="tempdrpid" class="tempdrpid" value=""><input type="hidden" id="tempcounter" class="tempcounter" value=' + index + ' ></td>';

                cols += '<td ><input type="text" id="purchasekg" onkeypress="return isNumber(event)" name="txtPurchaseKg" value=' + item.AnnualPurchasQty + ' class="form-control purchasekg" /> </td>';
                cols += '<td ><input type="text" id="FromScheme" onkeypress="return isNumber(event)" name="txtFromScheme" value=' + item.FreeSchemeFrom + ' class="form-control FromScheme" /></td>';
                cols += '<td ><input type="text"  id="ToScheme"  onkeypress="return isNumber(event)" name="txtToScheme" value=' + item.FreeSchemeTO + '  class="form-control ToScheme" /></td>';

                cols += '<td ><input type="text" id="TotalFreeKg" onkeypress="return isNumber(event)"  name="txtTotalFreeKgs" class="form-control TotalFreeKg EditTotalFreeKg' + counter + '" disabled/></td>';
                cols += ' <td style="display:none;"> <input type="text" id="hdProdSrno" name="txtTotalFreeKgs" class="hdProdSrno" value=' + item.OrderSrNo + '> </td>';

                cols += '<td ><button type="button" id="btnfreemodel" class="btn btn-info btnfreemodel" >Free </button></td>';

                cols += '<td  > <input type="button" class="ibtnDel btn btn-danger" value="Delete"></td>';

                newRow.append(cols);

                $("table.order-list").append(newRow);
                // DynamicGetBrandCategory("newdata" + counter);
                //console.log('DynamicGetBrandCategory1212');

                var Purchase = Math.ceil(parseFloat(item.AnnualPurchasQty) * parseFloat(item.FreeSchemeTO) / parseFloat(item.FreeSchemeFrom));
                //alert(Purchase);
                $(".EditTotalFreeKg" + counter).val(Purchase);
                var totalPurchaseKg = 0;

                makearraytable(item.ProductId, item.FreeSchemeFrom, item.FreeSchemeTO, item.AnnualPurchasQty, totalPurchaseKg, item.OrderSrNo, index);
                index++;
                $tblrows = $("#tblitemScheme tbody tr");
                purchageonchange();

                $(".newdata" + counter).html($("<option></option>").val('').html('---select---'));
                for (var i = 0; i < productdatalist.length; i++) {
                    $(".newdata" + counter).append($("<option></option>").val(productdatalist[i].ProductId).html(productdatalist[i].ProductName));
                }
                $(".newdata" + counter).val(item.ProductId);
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


function GetSelectOrderProductDetails(orderid) {
    $.ajax({
        type: "POST",
        url: "AddDealerOrderScheme.aspx/GetOrderProductDetails",
        data: "{orderid:'" + orderid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $.each(jQuery.parseJSON(data.d), function (key, item) {
                var SingleArrOrderProductDetail = {};
                SingleArrOrderProductDetail.PKG = item.ProductPck;
                SingleArrOrderProductDetail.QTY = item.ProductQty;
                SingleArrOrderProductDetail.srno = item.PDtlSrno;
                SingleArrOrderProductDetail.arrOrderProductDetailscounterdata = item.FreeOrderSRNO;
                SingleArrOrderProductDetail.Productid = item.ProductId;
                SingleArrOrderProductDetail.ProductPckID = item.ProductPckID;
                SingleArrOrderProductDetail.PckTotalKg = item.PckTotalKg;
                SingleArrOrderProductDetail.PackingNos = item.PackingNos;
                SingleArrOrderProductDetail.isscheme = item.IsScheme;
                SingleArrOrderProductDetail.Schemetext = item.Scheme;
                SingleArrOrderProductDetail.PackingType = item.PackingType;
                SingleArrOrderProductDetail.overalltotalfreekg = item.TotalKg;
				 SingleArrOrderProductDetail.SchemeId = item.SchemeId;
                SingleArrOrderProductDetail.ProductCode = item.ProductCode;
                arrOrderProductDetail.push(SingleArrOrderProductDetail);

            });
            viewmodeoredit();
        },
        failure: function () {
            alert("Failed!");
        }
    });
}


function viewmodeoredit() {
    if ($("#ContentPlaceHolder1_isview").val() == "1") {
        $("#ContentPlaceHolder1_txtPurchaseKg").attr("disabled", "disabled");
        $(".purchasekg").attr("disabled", "disabled");
        $(".FromScheme").attr("disabled", "disabled");
        $(".ToScheme").attr("disabled", "disabled");
        $(".ibtnDel").hide();
        $(".item").attr("disabled", "disabled");
        $(".SaveOrderProductDetail").hide();
        $(".ibtnDelProdRows").hide();
        $("#btnSubmitOrder").hide();
        $("#ContentPlaceHolder1_txtDealerCodeSearch").attr("disabled", "disabled");
        $("ContentPlaceHolder1_drpsSalesExe").attr("disabled", "disabled");

    }
    else {
        $(".purchasekg").removeAttr("disabled", "disabled");
        $(".FromScheme").removeAttr("disabled", "disabled");
        $(".ToScheme").removeAttr("disabled", "disabled");
        $(".ibtnDel").removeAttr("disabled", "disabled");
        $(".item").removeAttr("disabled", "disabled");
        $(".SaveOrderProductDetail").removeAttr("disabled", "disabled");
        $(".ibtnDelProdRows").removeAttr("disabled", "disabled");
        $("#btnSubmitOrder").removeAttr("disabled", "disabled");
        $("#ContentPlaceHolder1_txtDealerCodeSearch").removeAttr("disabled", "disabled");

    }
}