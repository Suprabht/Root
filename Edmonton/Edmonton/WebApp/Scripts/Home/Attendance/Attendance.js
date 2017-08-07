Atendance = function () { };
Atendance.prototype.init = function () {
    var table = document.createElement('table');
    table.id = 'grid';
    $('#gridContainer').append(table);
    var div = document.createElement('div');
    div.id = 'gridPager';
    $('#gridContainer').append(div);
    $.getJSON("/Home/Client",
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
            { label: 'User Name', name: 'userName', index: 'userName', width: "210", editable: true, editrules: { required: true } },
            { label: 'User Email', name: 'userEmail', index: 'userEmail', width: "210", editable: true, editrules: { required: true } },
            { label: 'Client Name', name: 'clientName', index: 'clientName', width: "350", editable: true, editrules: { required: true } },
            { label: 'Longitude', name: 'long', index: 'long', width: "150", editable: true, editrules: { required: true } },
            { label: 'Latitude', name: 'latt', index: 'latt', width: "150", editable: true, editrules: { required: true } },
            { label: 'Link', name: 'link', width: "150", editable: false, formatter: atendance.methodFormatter }
        ],
        pager: '#gridPager',
        regional: 'en',
        datatype: "jsonstring",
        datastr: data,
        jsonReader: { repeatitems: false },
        rowNum: data.records,
        viewrecords: true,
        caption: "Atendance",
        height: "auto",
        ignoreCase: true
    });
}
Atendance.prototype.methodFormatter = function (cellValue, options, rowObject) {

    return '<a href="' + rowObject.link + '" target="_blank" > Link </a>';
}
Atendance.prototype.unLoadGrid = function () {
    $('#grid').jqGrid("clearGridData");
    $('#grid').remove();
    $('#gridPager').remove();
    $('#gridContainer').empty();
}

var atendance = new Atendance();
atendance.init();