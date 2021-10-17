var contactListFn = function () {
    var Config = {
        apiURL: "https://localhost:44369/api", 
        dataGridContactList: null,
        records: 0,
    };

    var infrastructure = {
        LoadContactList: function () {  
            Config.dataGridContactList = $('#ContactList_DataTable').DataTable({ 
                serverSide: false,
                paging: false,
                stateSave: false,  
                processing: false,
                scrollX: true,
                scrollY: "500px",
                lengthChange: false,
                info: true,
                searching: false,
                bDestroy: false,
                ajax: {
                    url: Config.apiURL + "/ContactDetails",
                    type: "GET",  
                    cache: false,
                    dataType: "json",
                    timeout: 10000, // Ajax call timeout in milliseconds (180 seconds)
                    dataSrc: function (json) {
                        // This function is used to check any error that might be caused by Ajax call                        
                        if (json == undefined) {
                            // An error occurred when calling API to get data for grid
                            alert(json.responseText);
                            return false;
                        }
                        else {
                            // Data has been successfully retrieved via API   
                            return json;
                        } 
                    },
                    error: function (data) {
                        alert("Error: An unexpected error occurred. It might be caused by timeout."); 
                    }
                },
                //drawCallback: function (oSettings) {  
                //    var json = oSettings.json;
                //    if (json.data == undefined) {
                //        return false;
                //    }
                //    else {  
                //    }
                //},                   
                columns: [                     
                    { orderable: false, name: "ContactID", data: "contactId"},
                    {
                        orderable: true, data: null,
                        render: function (data, type, row) {
                            nameAndTitle = data.contactName + ' <br/>' + data.jobTitle ; 
                            return nameAndTitle;
                        }
                    },
                    {
                        //TODO: Display value for this column
                        orderable: true, data: null,
                        render: function (data, type, row) {
                            return "";
                        }
                    },
                    { orderable: false, name: "Phone", data: "phone" },
                    { orderable: false, name: "Address", data: "address" },                    
                    { orderable: false, name: "EmailAddress", data: "emailAddress" }, 
                    {
                        orderable: false, data: null,
                        render: function (data, type, row) {                           
                            return contactListFn.ParseDateTimeOffset(data.lastDateContacted);
                        } 
                    }
                ],
                columnDefs: [
                    // Make ContactID column invisible but store ContactID in the data table so that the ContactID can be retrieved after clicking a row
                    {
                        "targets": [0],
                        "visible": false
                    },
                ],               
                fnInfoCallback: function (oSettings, iStart, iEnd, iMax, iTotal, sPre) {
                    // Customize entry info
                    return iEnd + " of " + iTotal + " entries";
                }, 
                order: [[2, "ASC"]] // ContactName
            });
        }, 
    }     

    // Click a row in the Contact List datagrid to send user to the Contact Detail page. 
    $(document).on('click', '#ContactList_DataTable tr', function () {
        var tr = $(this).closest('tr');
        var row = Config.dataGridContactList.row(tr);
        rowData = row.data(); 
        window.location = "/Contact/Index/" + rowData.contactId;         
    }); 

    // Click Create New Contact button to load the Contact Detail page
    $("#btnCreateNewContact").on("click", function () {        
          window.location = "/Contact"; 
    });
      
    return {
        init: function () {
            infrastructure.LoadContactList();
        },

        // Convert DateTimeOffset value to MMM DD, YYYY format
        ParseDateTimeOffset: function (dateTimeOffsetValue) {            
            var dateTimeValue = new Date(dateTimeOffsetValue);
            return dateTimeValue.toUTCString().split(' ')[2] + ' ' + dateTimeValue.toUTCString().split(' ')[1] + ', ' + dateTimeValue.toUTCString().split(' ')[3] 
        } 
    }
}();

$(document).ready(function () {
    contactListFn.init();   
});

