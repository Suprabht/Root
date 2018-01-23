Programs = function () { };
Programs.prototype.init = function () {
    var table = document.createElement('table');
    table.id = 'grid';
    $('#gridContainer').append(table);
    var div = document.createElement('div');
    div.id = 'gridPager';
    $('#gridContainer').append(div);
    var programCatagory = {};
    $.getJSON("/Home/ProgramCategory",
        function (data) {
            for (i = 0; i < data.rows.length; i++) {
                for (i = 0; i < data.rows.length; i++) {
                    programCatagory[data.rows[i].programCategoryId] = data.rows[i].programCategoryAbbreviation + " : " + data.rows[i].programCategoryName;
                };
            };
            $.getJSON("/Home/Program1",
                function (clientData) {
                    programs.loadGrid(clientData, programCatagory);
                });
        });
};
Programs.prototype.loadGrid = function (data, programCatagory) {
    var grid = $("#grid");
    //http://www.google.com/maps/place/49.46800006494457,17.11514008755796
    grid.jqGrid({
        colModel: [
            { label: 'Program Id', name: 'programId', index: 'programId', width: "110", editable: false, editrules: { required: true }, key: true },
            { label: 'Program Name', name: 'programName', index: 'programName', width: "210", editable: true, editrules: { required: true } },
            { label: 'Program Description', name: 'programDescription', index: 'programDescription', width: "210", editable: true, editrules: { required: true } },
            { label: 'Program Code', name: 'programCode', index: 'programCode', width: "150", editable: true, editrules: { required: true } },
            { label: 'Program Category Id', name: 'programCategoryId', index: 'programCategoryId', width: "150", editable: true, editrules: { required: true }, edittype: 'select', editoptions: { value: programCatagory } }
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
            url: "/Home/Program1",
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
                var program = {
                    programId: parseInt(data.id),
                    programName: data.programName,
                    programDescription: data.programDescription,
                    programCode: data.programCode,
                    programCategoryId: data.programCategoryId
                };
                return program;
            },
            afterSubmit: function (response, postdata) {
                programs.unLoadGrid();
                programs.init();
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Add Dialog
        {
            url: "/Home/Program1",
            closeAfterAdd: true,
            recreateForm: true,
            mtype: "PUT",
            checkOnUpdate: false,
            checkOnSubmit: false,
            clearAfterAdd: true,
            reloadAfterSubmit: false,
            afterSubmit: function (response, postdata) {
                programs.unLoadGrid();
                programs.init();
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Delete Dailog
        {
            url: "/Home/Program1",
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
                programs.unLoadGrid();
                programs.init();
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        });
}
Programs.prototype.unLoadGrid = function () {
    $('#grid').jqGrid("clearGridData");
    $('#grid').remove();
    $('#gridPager').remove();
    $('#gridContainer').empty();
}

var programs = new Programs();
programs.init();