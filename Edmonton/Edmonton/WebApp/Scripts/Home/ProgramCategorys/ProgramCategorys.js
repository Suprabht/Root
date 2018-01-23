ProgramCategorys = function () { };
ProgramCategorys.prototype.init = function () {
    var table = document.createElement('table');
    table.id = 'grid';
    $('#gridContainer').append(table);
    var div = document.createElement('div');
    div.id = 'gridPager';
    $('#gridContainer').append(div);
    $.getJSON("/Home/ProgramCategory",
        function (data) {
            programCategorys.loadGrid(data);
               
    });
};
ProgramCategorys.prototype.loadGrid = function (data) {
    var grid = $("#grid");
    //http://www.google.com/maps/place/49.46800006494457,17.11514008755796
    grid.jqGrid({
        colModel: [
            { label: 'Program Category Id', name: 'programCategoryId', index: 'programCategoryId', width: "150", editable: false, editrules: { required: false } },
            { label: 'Program Category Name', name: 'programCategoryName', index: 'programCategoryName', width: "210", editable: true, editrules: { required: true } },
            { label: 'Program Category Description', name: 'programCategoryDescription', index: 'programCategoryDescription', width: "210", editable: true, editrules: { required: true } },
            { label: 'Program Category Code', name: 'programCategoryCode', index: 'programCategoryCode', width: "150", editable: true, editrules: { required: true } },
            { label: 'Program Category Abbreviation', name: 'programCategoryAbbreviation', index: 'programCategoryAbbreviation', width: "150", editable: true, editrules: { required: true } }
        ],
        pager: '#gridPager',
        regional: 'en',
        datatype: "jsonstring",
        datastr: data,
        jsonReader: { repeatitems: false },
        rowNum: data.records,
        viewrecords: true,
        caption: "Program Category",
        height: "auto",
        ignoreCase: true
    });
    grid.navGrid('#gridPager',
        // the buttons to appear on the toolbar of the grid
        { edit: true, add: true, del: true, search: false, refresh: false, view: false, position: "left", cloneToTop: false },
        // options for the Edit Dialog
        {
            url: "/Home/ProgramCategory",
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
                var programCategory = {
                    programCategoryId: parseInt(data.id),
                    programCategoryCode: data.programCategoryCode,
                    programCategoryAbbreviation: data.programCategoryAbbreviation,
                    programCategoryName: data.programCategoryName,
                    programCategoryDescription: data.programCategoryDescription
                };
                return programCategory;
            },
            afterSubmit: function (response, postdata) {
                programCategorys.unLoadGrid();
                programCategorys.init();
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Add Dialog
        {
            url: "/Home/ProgramCategory",
            closeAfterAdd: true,
            recreateForm: true,
            mtype: "PUT",
            checkOnUpdate: false,
            checkOnSubmit: false,
            clearAfterAdd: true,
            reloadAfterSubmit: false,
            afterSubmit: function (response, postdata) {
                programCategorys.unLoadGrid();
                programCategorys.init();
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Delete Dailog
        {
            url: "/Home/ProgramCategory",
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
                programCategorys.unLoadGrid();
                programCategorys.init();
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        });
}
ProgramCategorys.prototype.unLoadGrid = function () {
    $('#grid').jqGrid("clearGridData");
    $('#grid').remove();
    $('#gridPager').remove();
    $('#gridContainer').empty();
}

var programCategorys = new ProgramCategorys();
programCategorys.init();