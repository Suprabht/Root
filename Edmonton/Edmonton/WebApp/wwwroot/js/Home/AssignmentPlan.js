AssignmentPlan = function () { };
AssignmentPlan.prototype.init = function () {
    var table = document.createElement('table');
    table.id = 'grid';
    $('#gridContainer').append(table);
    var div = document.createElement('div');
    div.id = 'gridPager';
    $('#gridContainer').append(div);
    var clients = {};
    var users = {};   
    $.getJSON("/Home/AssignmentPlanRecords",
        function (data) {
            assignmentPlan.loadGrid(data, clients, users);
        });
                
};
AssignmentPlan.prototype.loadGrid = function (data, clients, users) {

    var grid = $("#grid");
    grid.jqGrid({

        afterInsertRow: function (id, currentData, jsondata) {
            var button = "<a class='gridbutton' data-id='" + id + "' href='#'>edit</a>";
            $(this).setCell(id, "edit", button);
        },
        colModel: [
            { label: 'Assignment Id', name: 'assignmentId', index: 'assignmentId', width: "110", editable: false, editrules: { required: true } },
            { label: 'Assignment Date', name: 'assignmentDate', index: 'assignmentDate', width: "110", editable: false },
            { label: 'Client', name: 'clientName', index: 'clientName', width: "110", editable: false },
            { label: 'Client Address', name: 'clientAddress', index: 'clientAddress', width: "110", editable: false },
            { label: 'Latitude', name: 'latt', index: 'latt', width: "110", editable: false },
            { label: 'Longitude', name: 'long', index: 'long', width: "110", editable: false },
            { label: 'Link', name: 'link', width: "150", editable: false, formatter: assignmentPlan.methodFormatter },
            { label: 'Edit', name: 'edit', index: 'edit', align: 'center', sortable: false, width: '40px' }
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
}
AssignmentPlan.prototype.methodFormatter = function (cellValue, options, rowObject) {

    return '<a href="' + rowObject.link + '" target="_blank" > Link </a>';
}
AssignmentPlan.prototype.unLoadGrid = function () {
    $('#grid').jqGrid("clearGridData");
    $('#grid').remove();
    $('#gridPager').remove();
    $('#gridContainer').empty();
}
var assignmentPlan = new AssignmentPlan();
assignmentPlan.init();