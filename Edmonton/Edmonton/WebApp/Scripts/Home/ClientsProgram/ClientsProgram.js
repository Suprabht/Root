ClientsProgram = function () { };
ClientsProgram.prototype.init = function () {
    var table = document.createElement('table');
    table.id = 'grid';
    $('#gridContainer').append(table);
    var div = document.createElement('div');
    div.id = 'gridPager';
    $('#gridContainer').append(div);
    var programs = {};
    var client = {};
    $.getJSON("/Home/Program1",
        function (data) {
            for (i = 0; i < data.rows.length; i++) {
                for (i = 0; i < data.rows.length; i++) {
                    programs[data.rows[i].programId] = data.rows[i].programCode + " : " + data.rows[i].programName + " - " + data.rows[i].programCategoryAbbreviation + " : " + data.rows[i].programCategoryName;
                };
            };
            $.getJSON("/Home/Client",
                function (data) {
                    for (i = 0; i < data.rows.length; i++) {
                        client[data.rows[i].clientId] = data.rows[i].clientName + " | " + data.rows[i].clientAddress.substring(0, 20);
                    };
                    $.getJSON("/Home/ClientsPrograms",
                        function (data) {
                            clientsProgram.loadGrid(data, client, programs);
                        });
            });
    });
};
ClientsProgram.prototype.loadGrid = function (data, client, programs) {
    var grid = $("#grid");
    grid.jqGrid({
        colModel: [
            { label: 'Id', name: 'clientDetailsProgramsId', index: 'clientDetailsProgramsId', width: "110", editable: false, editrules: { required: true }, key: true },
            { label: 'Client', name: 'clientId', index: 'clientId', width: "110", editable: true, editrules: { required: true }, edittype: 'select', editoptions: { value: client } },
            { label: 'Client Name', name: 'clientName', index: 'clientName', width: "210", editable: false, editrules: { required: true } },
            { label: 'Program Name', name: 'programName', index: 'programName', width: "350", editable: false, editrules: { required: true } },
            { label: 'Program', name: 'programId', index: 'programId', width: "150", editable: true, editrules: { required: true }, edittype: 'select', editoptions: { value: programs } },
            //{ label: 'Link', name: 'link', width: "150", editable: false, formatter: clients.methodFormatter }
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
            url: "/Home/ClientsPrograms",
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
                    clientDetailsProgramsId: parseInt(data.id),
                    clientId: data.clientId,
                    programId: data.programId
                };
                return clientDetailsData;
            },
            afterSubmit: function (response, postdata) {
                clientsProgram.unLoadGrid();
                clientsProgram.init();
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Add Dialog
        {
            url: "/Home/ClientsPrograms",
            closeAfterAdd: true,
            recreateForm: true,
            mtype: "PUT",
            checkOnUpdate: false,
            checkOnSubmit: false,
            clearAfterAdd: true,
            reloadAfterSubmit: false,
            afterSubmit: function (response, postdata) {
                clientsProgram.unLoadGrid();
                clientsProgram.init();
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Delete Dailog
        {
            url: "/Home/ClientsPrograms",
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
                clientsProgram.unLoadGrid();
                clientsProgram.init();
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        });
}
ClientsProgram.prototype.unLoadGrid = function () {
    $('#grid').jqGrid("clearGridData");
    $('#grid').remove();
    $('#gridPager').remove();
    $('#gridContainer').empty();
}

var clientsProgram = new ClientsProgram();
clientsProgram.init();