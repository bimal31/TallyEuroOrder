var p = 0;

Sys.Application.add_load(function () {
    $find("ReportViewer1").add_propertyChanged(viewerPropertyChanged);
});

function viewerPropertyChanged(sender, e) {    
    if (e.get_propertyName() == "isLoading") {
        if ($find("ReportViewer1").get_isLoading()) {
            // Do something when loading starts
        }
        else {           
            // Do something when loading stops
            PrintReport()
        }
    }
};

function PrintReport() {
    var viewerReference = $find("ReportViewer1");

    var stillonLoadState = viewerReference.get_isLoading();

    if (!stillonLoadState && p == 0) {
        var reportArea = viewerReference.get_reportAreaContentType();
        if (reportArea == Microsoft.Reporting.WebFormsClient.ReportAreaContent.ReportPage) {
            p++;

            var mywindow = window.open();                    

            mywindow.document.write('<html><head><title>' + document.title  + '</title>');
            mywindow.document.write('</head><body >');
            mywindow.document.write('<h1>' + document.title  + '</h1>');
            mywindow.document.write(document.getElementById('ReportViewer1_ctl09').innerHTML);
            mywindow.document.write('</body></html>');
            mywindow.document.close();
            mywindow.focus();                      
            mywindow.print();
            mywindow.close();

        }
    }
}