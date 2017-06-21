ProgramDetails = function () { };
ProgramDetails.prototype.init = function () {
    $.getJSON("/Home/GetPrograms",
        function (data) {
            programDetails.loadGrid(data);
        });

    //var data = {
    //    "page": "1",
    //    "records": "3",
    //    "rows": [
    //        { "id": "83123a", Name: "Name 1", "PackageCode": "83123a" },
    //        { "id": "83432a", Name: "Name 3", "PackageCode": "83432a" },
    //        { "id": "83566a", Name: "Name 2", "PackageCode": "83566a" }
    //    ]
    //};

    //this.loadGrid(data);
};
ProgramDetails.prototype.loadGrid = function (data) {
    var grid = $("#grid");
    grid.jqGrid({
        colModel: [
            { label: 'Program Id', name: 'programId', index: 'programId', width: "110", editable: false, editrules: { required: true } },
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
var programDetails = new ProgramDetails();
programDetails.init();