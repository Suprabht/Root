Index = function () { };
Index.prototype.init = function () {
    $.getJSON("/Home/IndexDashBoard/?r=" + Math.random(),
        function (data) {
            $("#accepted").html(data.isAccepted);
            $("#pendingApproval").html(data.pendingApproval);
            $("#pendingAcceptence").html(data.pendingAcceptence);
        });
    index.initGrid();
    index.initEmergencyCallGrid();
    //var refresh = window.setInterval(function () {
    //    index.unLoadGrid();
    //    index.initGrid();        
    //}, 1000);
};

Index.prototype.initGrid = function () {
    var table = document.createElement('table');
    table.id = 'grid';
    $('#gridContainer').append(table);
    var div = document.createElement('div');
    div.id = 'gridPager';
    $('#gridContainer').append(div);
    $.getJSON("/Home/ActiveLogin",
        function (data) {
            index.loadGrid(data);
        });
};

Index.prototype.loadGrid = function (data) {
    var grid = $("#grid");
    //http://www.google.com/maps/place/49.46800006494457,17.11514008755796
    grid.jqGrid({
        colModel: [
            { label: 'AssignmentId', name: 'assignmentId', index: 'assignmentId', width: "210", editable: true, hidden: true, editrules: { required: true } },
            { label: 'User Name', name: 'userName', index: 'userName', width: "210", editable: true, editrules: { required: true } },
            { label: 'User Email', name: 'userEmail', index: 'userEmail', width: "210", editable: true, editrules: { required: true } },
            { label: 'Client Name', name: 'clientName', index: 'clientName', width: "210", editable: true, editrules: { required: true } },
            { label: 'Client Address', name: 'clientAddress', index: 'clientAddress', width: "210", editable: true, editrules: { required: true } },
        ],
        pager: '#gridPager',
        regional: 'en',
        datatype: "jsonstring",
        datastr: data,
        jsonReader: { repeatitems: false },
        rowNum: data.records,
        viewrecords: true,
        caption: "Present Active Employees",
        height: "auto"
       
    });
}
Index.prototype.unLoadGrid = function () {
    $('#grid').jqGrid("clearGridData");
    $('#grid').remove();
    $('#gridPager').remove();
    $('#gridContainer').empty();   
}

Index.prototype.initEmergencyCallGrid = function () {
    var table = document.createElement('table');
    table.id = 'gridEC';
    $('#gridEmergencyCall').append(table);
    var div = document.createElement('div');
    div.id = 'gridPagerEC';
    $('#gridEmergencyCall').append(div);
    $.getJSON("/Home/ActiveEmergencyCall",
        function (data) {
            index.loadEmergencyCallGrid(data);
        });
};

Index.prototype.loadEmergencyCallGrid = function (data) {
    var grid = $("#gridEC");
    //http://www.google.com/maps/place/49.46800006494457,17.11514008755796
    grid.jqGrid({
        colModel: [
            { label: 'User Name', name: 'userName', index: 'userName', width: "210", editable: true, editrules: { required: true } },
            { label: 'User Email', name: 'userEmail', index: 'userEmail', width: "210", editable: true, editrules: { required: true } }
        ],
        pager: '#gridPagerEC',
        regional: 'en',
        datatype: "jsonstring",
        datastr: data,
        jsonReader: { repeatitems: false },
        rowNum: data.records,
        viewrecords: true,
        caption: "Present Employees on call",
        height: "auto"

    });
}
Index.prototype.unLoadEmergencyCallGrid = function () {
    $('#gridEC').jqGrid("clearGridData");
    $('#gridEC').remove();
    $('#gridPagerEC').remove();
    $('#gridEmergencyCall').empty();
}

Index.prototype.refreshGrid = function () {
    index.unLoadGrid();
    index.init();
    index.unLoadEmergencyCallGrid();
    index.initEmergencyCallGrid();
}
var index = new Index();
index.init();