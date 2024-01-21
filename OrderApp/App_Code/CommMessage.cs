using System.Collections.Generic;

public static class CommMessage
{
    public static string Recordcouldnotable = "Record could not able to store, please try again.";

    public static string coludnotchangestatus = "Couldn't able to change status, Please Try Again Later.";

    public static string CouldnotabletoDelete = "Could not able to Delete, Please Try Again.";
    public static string usernameareadyexist = "UserName is already exist. Please enter other UserName";

    public static string usernamevailable = "UserName is available";

    public static string orderstatusnotupdate = "Order Status not update.Plz  contact to office.";

    public static string selectstatename = "Please select state Name";

    public static string selectproductname = "Please select Product Name";

    public static string productnamealreadyexist = "ProductName is already exist. Please enter other ProductName";
    public static string ProductNameisavailable = "ProductName is available";


    public static string Countrynamealreadyexist = "Country Name is already exist. Please enter other Country Name";

    public static string SchemeNamealreadyexist = "Scheme Name is already exist. Please enter other Scheme Name";

        public static string UOMNamealreadyexist = "UOM Name is already exist. Please enter other UOM Name";

    


    public static string Inserted = "Data Inserted successfully.";

    public static string enterorderdate = "Please Enter Order Date";


    public static string Notfounddealer = "Could not find Dealer records";

    public static string SelectSalesExecutive = "Please select sales Executive";
    public static string enterfromdate = "Please Enter From Date";
    public static string entertodate = "Please Enter To Date";
    public static string Enterfromscheme = "Please Enter From Scheme";
    public static string Entertoscheme = "Please Enter To Scheme";
    public static string Entertpurchasekg = "Please Enter Purchase Kg";
    public static string Notmatchkg = "Total Free Kg value is not matching with Total value of product kg. Please check value and enter again.";


    public static string somethingwrong = "Something wrong Insert Order.";
    public static string enterprodqty = "Please enter the  product qty.";

    public static string OrderSave = "Order Save Successfully.";
    public static string URL = "";



    public static string DealerNotfound = "Dealer Not found.";

    public static string Dealerpayment = "Dealer Payment pending.";



    public static string OrderType_Order = "Order";
    public static string OrderType_withbillFreeScheme = "With Bill Free Scheme";
    public static string OrderType_FreeScheme = "Free Scheme";


    // free 
    public static string addWithBillFreeScheme = "Add With Bill Free Scheme";
    public static string EditWithBillFreeScheme = "Edit With Bill Free Scheme";
    public static string viewWithBillFreeScheme = "View With Bill Free Scheme";

    public static string freetotalkgnotmatch = "Free Total Kgs and Free Kg value must match";

    public static string totalkgandtotalvalue = "Total Kgs and Total value must match";

    // Order
    public static string addOrder = "Add Order";
    public static string EditOrder = "Edit Order";
    public static string viewOrder = "View Order";

    // dealer shcment 
    public static string addfreescheme = "Add Free Scheme";
    public static string Editfreescheme = "Edit Free Scheme";
    public static string viewfreescheme = "View Free Scheme";

    //List of valid image extensions
    public static List<string> _validImageExtensions = new List<string> { ".png", ".jpg", ".jpeg" };
    public static int _validImageSize = 2097152; //2MB => 2 * 1024 * 1024
    public static string _InvalidImageFile = "Please select valid .png, .jpg or .jpeg image file for {0}.";
    public static string _InvalidImageSize = "{0} image is too large. Maximum file size permitted is 2MB.";

    public static string StateErrorMessage = "Please select at least one state.";

    public static string Statenamealreadyexist = "State Name is already exist. Please enter other State Name";

    public static string RequiredProductPackingItems = "There always should be at least one active product packing item.";
}