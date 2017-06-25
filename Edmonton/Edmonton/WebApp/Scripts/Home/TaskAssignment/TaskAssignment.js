TaskAssignment = function () { };
TaskAssignment.prototype.init = function () {
    $.getJSON("/Home/GetAssignment",
        function (data) {
            taskAssignment.loadGrid(data);
        });
};
TaskAssignment.prototype.loadGrid = function (data) {
    var grid = $("#grid");
    grid.jqGrid({
        colModel: [
            { label: 'Assignment Id', name: 'assignmentId', index: 'assignmentId', width: "110", editable: false, editrules: { required: true } },
            { label: 'Assignment Date', name: 'assignmentDate', index: 'assignmentDate', width: "110", editable: true, editrules: { required: true } },
            { label: 'Client Id', name: 'clientId', index: 'clientId', width: "110", hidden: true, editable: true, editrules: { required: true } },
            { label: 'Client Name', name: 'clientName', index: 'clientName', width: "110", editable: true, editrules: { required: true } },
            { label: 'Client Address', name: 'clientAddress', index: 'clientAddress', width: "110", editable: true, editrules: { required: true } },
            { label: 'User Id', name: 'userId', index: 'userId', width: "110", hidden: true, editable: true, editrules: { required: true } },
            { label: 'User Name', name: 'userName', index: 'userName', width: "110", editable: true, editrules: { required: true } },
            { label: 'User Email', name: 'userEmail', index: 'userEmail', width: "110", editable: true, editrules: { required: true } },
        ],
        pager: '#gridPager',
        regional: 'en',
        datatype: "jsonstring",
        datastr: data,
        jsonReader: { repeatitems: false },
        rowNum: data.records,
        viewrecords: true,
        caption: "Assignment",
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
var taskAssignment = new TaskAssignment();
taskAssignment.init();