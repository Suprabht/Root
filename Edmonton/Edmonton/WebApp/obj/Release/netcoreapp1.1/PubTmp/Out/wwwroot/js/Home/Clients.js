Clients = function () { };
Clients.prototype.init = function () {
    var table = document.createElement('table');
    table.id = 'grid';
    $('#gridContainer').append(table);
    var div = document.createElement('div');
    div.id = 'gridPager';
    $('#gridContainer').append(div);
    $.getJSON("/Home/Client",
        function (data) {
            clients.loadGrid(data);
        });
};
Clients.prototype.loadGrid = function (data) {
    var grid = $("#grid");
    //http://www.google.com/maps/place/49.46800006494457,17.11514008755796
    grid.jqGrid({
        colModel: [
            { label: 'Client Id', name: 'clientId', index: 'clientId', width: "110", editable: false, editrules: { required: true }, key: true  },
            { label: 'Client Name', name: 'clientName', index: 'clientName', width: "210", editable: true, editrules: { required: true } },
            { label: 'Client Address', name: 'clientAddress', index: 'clientAddress', width: "350", editable: true, editrules: { required: true } },
            { label: 'Longitude', name: 'long', index: 'long', width: "150", editable: true, editrules: { required: true } },
            { label: 'Latitude', name: 'latt', index: 'latt', width: "150", editable: true, editrules: { required: true } },
            { label: 'Link', name: 'link', width: "150", editable: false, formatter: clients.methodFormatter }
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
            url: "/Home/Client",
            editCaption: "The Edit Dialog",
            recreateForm: true,
            checkOnUpdate: false,
            checkOnSubmit: false,
            closeAfterEdit: true,
            reloadAfterSubmit: true,
            mtype: "POST",
            modal: true,
            jqModal: true,
            serializeEditData: function (data) {
                var clientDetailsData = {
                    clientId: parseInt(data.id),
                    clientName: data.clientName,
                    clientAddress: data.clientAddress,
                    long: data.long,
                    latt: data.latt
                };
                return clientDetailsData;
            },
            afterSubmit: function (response, postdata) {
                clients.unLoadGrid();
                clients.init();
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Add Dialog
        {
            url: "/Home/Client",
            closeAfterAdd: true,
            recreateForm: true,
            mtype: "PUT",
            checkOnUpdate: false,
            checkOnSubmit: false,
            clearAfterAdd: true,
            reloadAfterSubmit: false,
            afterSubmit: function (response, postdata) {
                clients.unLoadGrid();
                clients.init();
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Delete Dailog
        {
            url: "/Home/Client",
            mtype: "DELETE",
            editCaption: "The Edit Dialog",
            recreateForm: true,
            checkOnUpdate: false,
            checkOnSubmit: false,
            closeAfterDelete: true,
            reloadAfterSubmit: true,
            modal: true,
            jqModal: true,
            afterSubmit: function (response, postdata) {
                clients.unLoadGrid();
                clients.init();
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        });
}
Clients.prototype.methodFormatter = function (cellValue, options, rowObject) {
    
    return '<a href="' + rowObject.link +'" target="_blank" > Link </a>';
}
Clients.prototype.unLoadGrid = function () {
    $('#grid').jqGrid("clearGridData");
    $('#grid').remove();
    $('#gridPager').remove();
    $('#gridContainer').empty();
}

var clients = new Clients();
clients.init();