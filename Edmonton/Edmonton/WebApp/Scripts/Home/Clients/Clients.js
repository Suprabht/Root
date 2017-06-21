Clients = function () { };
Clients.prototype.init = function () {

    $.getJSON("/Home/GetClients",
        function (data) {
            clients.loadGrid(data);
        });
};
Clients.prototype.loadGrid = function (data) {
    var grid = $("#grid");
    //http://www.google.com/maps/place/49.46800006494457,17.11514008755796
    grid.jqGrid({
        colModel: [
            { label: 'Client Id', name: 'clientId', index: 'clientId', width: "110", editable: false, editrules: { required: true } },
            { label: 'Client Name', name: 'clientName', index: 'clientName', width: "210", editable: true, editrules: { required: true } },
            { label: 'Client Address', name: 'clientAddress', index: 'clientAddress', width: "350", editable: true, editrules: { required: true } },
            { label: 'Longitude', name: 'long', index: 'long', width: "210", editable: true, editrules: { required: true } },
            { label: 'Latitude', name: 'latt', index: 'latt', width: "210", editable: true, editrules: { required: true } },
           // { label: 'Link', name: 'link', index: 'link', width: "210", editable: true, editrules: { required: true } }
            { label: 'Link', name: 'link', width: "200", editable: false, formatter: clients.methodFormatter }
        ],
        pager: '#gridPager',
        regional: 'en',
        datatype: "jsonstring",
        datastr: data,
        jsonReader: { repeatitems: false },
        rowNum: data.records,
        viewrecords: true,
        caption: "Client",
        height: "auto",
        ignoreCase: true
    });
    grid.navGrid('#gridPager',
        // the buttons to appear on the toolbar of the grid
        { edit: true, add: true, del: true, search: false, refresh: false, view: false, position: "left", cloneToTop: false },
        // options for the Edit Dialog
        {
            editCaption: "The Edit Dialog",
            recreateForm: true,
            checkOnUpdate: true,
            checkOnSubmit: true,
            closeAfterEdit: true,
            reloadAfterSubmit: true,
            ajaxEditOptions: { contentType: "application/json" },
            serializeEditData: function (data1) {
                alert(data1);
                //var postData = { 'data': data };
                //return JSON.stringify(postData);
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Add Dialog
        {
            closeAfterAdd: true,
            recreateForm: true,
            serializeAddData: function (data1) {
                alert(data1);
                //var postData = { 'data': data };
                //return JSON.stringify(postData);
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Delete Dailog
        {
            ajaxDelOptions: { contentType: "application/json" },
            serializeDelData: function (data1) {
                alert(data1);
                //var postData = { 'data': data };
                //return JSON.stringify(postData);
            },

            afterSubmit: function (response, postdata) {
                alert(response);
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        });
}
Clients.prototype.methodFormatter = function (cellValue, options, rowObject) {
    
    return '<a href="' + rowObject.link +'" target="_blank" > Link </a>';
}

var clients = new Clients();
clients.init();