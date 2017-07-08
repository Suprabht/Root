TaskAssignment = function () { };
TaskAssignment.prototype.init = function () {
    var table = document.createElement('table');
    table.id = 'grid';
    $('#gridContainer').append(table);
    var div = document.createElement('div');
    div.id = 'gridPager';
    $('#gridContainer').append(div);
    var clients = {};
    var users = {};
    $.getJSON("/Home/Client",
        function (data) {
            for (i = 0; i < data.rows.length; i++) {
                clients[data.rows[i].clientId] = data.rows[i].clientName + " | " + data.rows[i].clientAddress.substring(0, 20);
            };

            $.getJSON("/Home/GetUsers",
                function (usersList) {
                    for (i = 0; i < usersList.rows.length; i++) {
                        users[usersList.rows[i].id] = usersList.rows[i].name + " | " + usersList.rows[i].email.substring(0, 20);
                    }
                    $.getJSON("/Home/TaskAssignment",
                        function (data) {
                            taskAssignment.loadGrid(data, clients, users);
                        });
                });
           
        });  
};
TaskAssignment.prototype.loadGrid = function (data, clients, users) {

    var grid = $("#grid");
    grid.jqGrid({
        colModel: [
            { label: 'Assignment Id', name: 'assignmentId', index: 'assignmentId', width: "110", editable: false, editrules: { required: true } },
            { label: 'Assignment Date', name: 'assignmentDate', index: 'assignmentDate', width: "110", editable: true, editrules: { required: true }, editoptions: { dataInit: function (elem) { $(elem).datepicker(); } } },
            { label: 'Client', name: 'clientName', index: 'clientName', width: "110", editable: true, editrules: { required: true }, edittype: 'select', editoptions: { value: clients }},
            { label: 'User Id', name: 'userName', index: 'userName', width: "110", editable: true, editrules: { required: true }, edittype: 'select', editoptions: { value: users } }
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
            url: "/Home/TaskAssignment",
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
                var taskAssignmentData = {
                    assignmentId: parseInt(data.id),
                    assignmentDate: data.assignmentDate,
                    clientId: data.clientId,
                    userId: data.userId
                };
                return taskAssignmentData;
            },
            afterSubmit: function (response, postdata) {
                taskAssignment.unLoadGrid();
                taskAssignment.init();
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Add Dialog
        {
            url: "/Home/TaskAssignment",
            closeAfterAdd: true,
            recreateForm: true,
            mtype: "PUT",
            checkOnUpdate: false,
            checkOnSubmit: false,
            clearAfterAdd: true,
            reloadAfterSubmit: false,
            afterSubmit: function (response, postdata) {
                taskAssignment.unLoadGrid();
                taskAssignment.init();
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Delete Dailog
        {
            url: "/Home/TaskAssignment",
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
                taskAssignment.unLoadGrid();
                taskAssignment.init();
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        });
}
TaskAssignment.prototype.unLoadGrid = function () {
    $('#grid').jqGrid("clearGridData");
    $('#grid').remove();
    $('#gridPager').remove();
    $('#gridContainer').empty();
}
var taskAssignment = new TaskAssignment();
taskAssignment.init();