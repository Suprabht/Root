Atendance = function () { };
Atendance.prototype.init = function () {
    var table = document.createElement('table');
    table.id = 'grid';
    $('#gridContainer').append(table);
    var div = document.createElement('div');
    div.id = 'gridPager';
    $('#gridContainer').append(div);
    $.getJSON("/Home/employeeAttendance/",
        function (data) {
            atendance.loadGrid(data);
        });
};

Atendance.prototype.loadGrid = function (data) {
    var grid = $("#grid");
    //http://www.google.com/maps/place/49.46800006494457,17.11514008755796
    grid.jqGrid({
        colModel: [
            { label: 'Attendance Id', name: 'attendanceId', index: 'attendanceId', width: "110", editable: false, editrules: { required: true }, key: true },
            { label: 'AssignmentId', name: 'assignmentId', index: 'assignmentId', width: "210", editable: true, hidden: true, editrules: { required: true } },
            { label: 'Assignment Detail', name: 'assignmentDetail', index: 'assignmentDetail', width: "210", editable: true, editrules: { required: true } },            
            { label: 'User Name', name: 'userName', index: 'userName', width: "210", editable: true, editrules: { required: true } },
            { label: 'User Email', name: 'userEmail', index: 'userEmail', width: "210", editable: true, editrules: { required: true } },
            
            { label: 'Longitude', name: 'long', index: 'long', width: "150", editable: true, editrules: { required: true } },
            { label: 'Latitude', name: 'latt', index: 'latt', width: "150", editable: true, editrules: { required: true } },
            { label: 'Time', name: 'logTime', index: 'logTime', width: "150", editable: false, editrules: { required: true }},
            { label: 'Distance', name: 'distance', index: 'distance', width: "150", editable: false, editrules: { required: true }, summaryType: 'count', summaryTpl: '<button type="button">Approve!</button>' }
            
        ],
        pager: '#gridPager',
        regional: 'en',
        datatype: "jsonstring",
        datastr: data,
        jsonReader: { repeatitems: false },
        rowNum: data.records,
        //viewrecords: true,
        //caption: "Atendance",
        //height: "auto",
        //ignoreCase: true,
        //grouping: true,
        //groupingView: {
        //    groupField: ['assignmentDetail'],
        //    groupColumnShow: [false],
        //    groupSummary: [true],
        //    groupText: ['<b>{0}</b>'],
        //},
    });
}

Atendance.prototype.unLoadGrid = function () {
    $('#grid').jqGrid("clearGridData");
    $('#grid').remove();
    $('#gridPager').remove();
    $('#gridContainer').empty();
}

var atendance = new Atendance();
atendance.init();