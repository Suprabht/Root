UserLevel = function () { };
UserLevel.prototype.init = function () {

    $.getJSON("/Home/GetUserLevel",
        function (data) {
            userLevel.loadGrid(data);
        });
};
UserLevel.prototype.loadGrid = function (data) {
    var grid = $("#grid");
    grid.jqGrid({
        colModel: [
            { label: 'User Level Id', name: 'userLevelId', index: 'userLevelId', width: "110", editable: false, editrules: { required: true } },
            { label: 'User Level Name', name: 'userLevelName', index: 'userLevelName', width: "210", editable: true, editrules: { required: true } },
            { label: 'User Level Description', name: 'userLevelDescription', index: 'userLevelDescription', width: "350", editable: true, editrules: { required: true } }
        ],
        pager: '#gridPager',
        regional: 'en',
        datatype: "jsonstring",
        datastr: data,
        jsonReader: { repeatitems: false },
        rowNum: data.records,
        viewrecords: true,
        caption: "User Level",
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

var userLevel = new UserLevel();
userLevel.init();