ProgramDetails = function () { };
ProgramDetails.prototype.init = function () {
    var table = document.createElement('table');
    table.id = 'grid';
    $('#gridContainer').append(table);
    var div = document.createElement('div');
    div.id = 'gridPager';
    $('#gridContainer').append(div);
    $.getJSON("/Home/Program",
        function (data) {
            programDetails.loadGrid(data);
        });
};
ProgramDetails.prototype.loadGrid = function (data) {
    var grid = $("#grid");
    grid.jqGrid({
        colModel: [
            { label: 'Program Id', name: 'programId', index: 'programId', width: "110", editable: false, editrules: { required: true }, key: true  },
            { label: 'Program Name', name: 'programName', index: 'programName', width: "210", editable: true, editrules: { required: true } },
            { label: 'Program Description', name: 'programDescription', index: 'programDescription', width: "350", editable: true, editrules: { required: true } }
        ],
        pager: '#gridPager',
        regional: 'en',
        datatype: "jsonstring",
        datastr: data,
        jsonReader: { repeatitems: false },
        rowNum: data.records,
        viewrecords: true,
        caption: "Program",
        height: "auto",
        ignoreCase: true
    });
    grid.navGrid('#gridPager',
        // the buttons to appear on the toolbar of the grid
        { edit: true, add: true, del: true, search: false, refresh: false, view: false, position: "left", cloneToTop: false },
        // options for the Edit Dialog
        {
            url: "/Home/Program",
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
                var programDetailsData = {
                    programId: parseInt(data.id),
                    programName: data.programName,
                    programDescription: data.programDescription
                };
                return programDetailsData;
            },
            afterSubmit: function (response, postdata) {
                programDetails.unLoadGrid();
                programDetails.init();
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Add Dialog
        {
            url: "/Home/Program",
            closeAfterAdd: true,
            recreateForm: true,
            mtype: "PUT",
            checkOnUpdate: false,
            checkOnSubmit: false,
            closeAfterAdd: true,
            clearAfterAdd: true,
            reloadAfterSubmit: false,
            afterSubmit: function (response, postdata) {
                programDetails.unLoadGrid();
                programDetails.init();
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Delete Dailog
        {
            url: "/Home/Program",
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

                programDetails.unLoadGrid();
                programDetails.init();
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        });
}
ProgramDetails.prototype.unLoadGrid = function () {
    $('#grid').jqGrid("clearGridData");
    $('#grid').remove();
    $('#gridPager').remove();
    $('#gridContainer').empty();
}
var programDetails = new ProgramDetails();
programDetails.init();