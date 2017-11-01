LeavePlanner = function () { };
LeavePlanner.prototype.init = function () {
    $.getJSON("/Home/LeaveCalendar/?t=" + Math.random(),
        function (data) {
            $("#leave-calendar").zabuto_calendar({
                data: data.calendarDates
            });

            var table = document.createElement('table');
            table.id = 'grid';
            $('#gridContainer').append(table);
            var div = document.createElement('div');
            div.id = 'gridPager';
            $('#gridContainer').append(div);
            $.getJSON("/Home/Leave?t=" + Math.random(),
                function (data) {
                    leavePlanner.loadGrid(data);
                });

        });
   
};
LeavePlanner.prototype.loadGrid = function (data) {
    var grid = $("#grid");
    grid.jqGrid({
        colModel: [
            { label: 'Leave Id', name: 'leaveId', index: 'leaveId', width: "110", editable: false, editrules: { required: true }, key: true, hidden: true },
            {
                label: 'Leave Date', name: 'leaveDate', index: 'leaveDate', width: "210", editable: true, editrules: { required: true }, formatter: 'date',
                formatoptions: {
                    srcformat: 'Y/m/d H:i',
                    newformat: 'd/m/Y'
                }
            }
          //  { label: 'Program Description', name: 'programDescription', index: 'programDescription', width: "350", editable: true, editrules: { required: true } }
        ],
        onSelectRow: function (id) {            
            jQuery('#grid').jqGrid('editRow', id, true, leavePlanner.pickdates);               
        },
        pager: '#gridPager',
        regional: 'en',
        datatype: "jsonstring",
        datastr: data,
        jsonReader: { repeatitems: false },
        rowNum: data.records,
        viewrecords: true,
        caption: "Leave",
        height: "auto",
        ignoreCase: true
    });
    grid.navGrid('#gridPager',
        // the buttons to appear on the toolbar of the grid
        { edit: false, add: true, del: true, search: false, refresh: false, view: false, position: "left", cloneToTop: false },
        // options for the Edit Dialog
        {
            url: "/Home/Leave",
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
                leavePlanner.unLoadGrid();
                leavePlanner.init();
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Add Dialog
        {
            url: "/Home/Leave",
            closeAfterAdd: true,
            recreateForm: true,
            mtype: "PUT",
            checkOnUpdate: false,
            checkOnSubmit: false,
            closeAfterAdd: true,
            clearAfterAdd: true,
            reloadAfterSubmit: false,
            afterSubmit: function (response, postdata) {
                leavePlanner.unLoadGrid();
                leavePlanner.init();
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Delete Dailog
        {
            url: "/Home/Leave",
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
                leavePlanner.unLoadGrid();
                leavePlanner.init();
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        });
}
LeavePlanner.prototype.unLoadGrid = function () {
    $('#grid').jqGrid("clearGridData");
    $('#grid').remove();
    $('#gridPager').remove();
    $('#gridContainer').empty();
}
LeavePlanner.prototype.pickdates = function (id) {
    jQuery("#" + id + "_sdate", "#grid").datepicker({ dateFormat: "d/m/Y" });
}
var leavePlanner = new LeavePlanner();
leavePlanner.init();