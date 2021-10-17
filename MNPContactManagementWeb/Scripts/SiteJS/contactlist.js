var contactList = function () {
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
                    { orderable: true, name: "ContactName", data: "contactName" },
                    { orderable: true, name: "Address", data: "address" },
                    { orderable: false, name: "Phone", data: "phone" },
                    { orderable: false, name: "EmailAddress", data: "emailAddress" },
                    { orderable: false, name: "LastDateContacted", data: "lastDateContacted" } 
                ],
                columnDefs: [
                    { className: "text-left", "targets": 0 }
                ],               
                fnInfoCallback: function (oSettings, iStart, iEnd, iMax, iTotal, sPre) {
                    // Customize entry info
                    return iEnd + " of " + iTotal + " entries";
                }, 
                order: [[2, "ASC"]] // ContactName
            });
        },
    }     

    $("#btnCreateNewContact").on("click", function () {
        // 
          window.location = "/Contact"; 
    });
      
    return {
        init: function () {
            infrastructure.LoadContactList();
        } 
    }
}();

$(document).ready(function () {
    contactList.init();
});

