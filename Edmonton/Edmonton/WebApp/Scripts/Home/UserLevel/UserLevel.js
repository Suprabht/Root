UserLevel = function () { };
UserLevel.prototype.init = function () {
    var table = document.createElement('table');
    table.id = 'grid';
    $('#gridContainer').append(table);
    var div = document.createElement('div');
    div.id = 'gridPager';
    $('#gridContainer').append(div);
    //var dataToLoad = userLevel.getData();
    //userLevel.loadGrid(dataToLoad);
    $.getJSON("/Home/UserLevel",
        function (data) {
            userLevel.loadGrid(data);
        });
};
UserLevel.prototype.getData = function () {
    $.getJSON("/Home/UserLevel",
        function (response) {
           // userLevel.loadGrid(data);
            //$('#grid').jqGrid('clearGridData');
            //$('#grid').jqGrid('setGridParam', { data: response });
            //$('#grid').trigger('reloadGrid');
            $('#grid')
            .jqGrid('setGridParam',
                {
                    datatype: 'local',
                    data: response
                })
                .trigger("reloadGrid");
        });

    //$.ajax({
    //    url: "/Home/UserLevel",
    //    async: false,
    //    dataType: 'json',
    //    success: function (response) {
    //        return response;
    //    }
    //});
    //return false;
}
UserLevel.prototype.loadGrid = function (data) {
    var grid = $("#grid");
    grid.jqGrid({
        colModel: [
            { label: 'User Level Id', name: 'userLevelId', index: 'userLevelId', width: "110", editable: false, editrules: { required: true }, key: true },
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
        closeAfterAdd: true,
        closeAfterEdit: true,
        ignoreCase: true
    });
    grid.navGrid('#gridPager',
        // the buttons to appear on the toolbar of the grid
        { edit: true, add: true, del: true, search: false, refresh: false, view: false, position: "left", cloneToTop: false },
        // options for the Edit Dialog
        {
            url: "/Home/UserLevel",
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
                var userLevelData = {
                    userLevelId: parseInt(data.id),
                    userLevelName: data.userLevelName,
                    userLevelDescription: data.userLevelDescription
                };
                return userLevelData;
            },
            afterSubmit: function (response, postdata) {
                // userLevel.unLoadGrid();
                // userLevel.init();
                //$(".ui-icon-closethick").trigger('click');
                userLevel.unLoadGrid();
                userLevel.init();         
                //var dataToLoad = userLevel.getData();
                //$("#grid").trigger("reloadGrid");
                //userLevel.getData();
                //$('#grid').jqGrid('setGridParam', { data: dataToLoad });
                //$('#grid').trigger('reloadGrid');
               return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Add Dialog
        {
            url: "/Home/UserLevel",
            closeAfterAdd: true,
            recreateForm: true,
            //modal: true,
            //jqModal: true,
            mtype: "PUT",
            checkOnUpdate: false,
            checkOnSubmit: false,
            closeAfterAdd: true,
            clearAfterAdd: true,
            reloadAfterSubmit: false,
            //serializeEditData: function (data) {
            //    var userLevelData = {
            //        userLevelId: data.id,
            //        userLevelName: data.userLevelName,
            //        userLevelDescription: data.userLevelDescription
            //    };
            //    return userLevelData;
            //},
            afterSubmit: function (response, postdata) {              
                userLevel.unLoadGrid();
                userLevel.init();  
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        },
        // options for the Delete Dailog
        {
            url: "/Home/UserLevel",
            mtype: "DELETE",
            editCaption: "The Edit Dialog",
            recreateForm: true,
            checkOnUpdate: false,
            checkOnSubmit: false,
            closeAfterDelete: true,
            reloadAfterSubmit: true,
            modal: true,
            jqModal: true,
            //serializeDelData: function (data) {

            //    var data = {

            //    };
            //    //alert(data);
            //    //var postData = { 'data': data };
            //    //return JSON.stringify({id:1});
            //    return data;
            //},
            afterSubmit: function (response, postdata) {
                
                userLevel.unLoadGrid();
                userLevel.init();
                $(".ui-icon-closethick").trigger('click');
                return true;
            },
            errorTextFormat: function (data1) {
                return 'Error: ' + data1.responseText;
            }
        });
};

UserLevel.prototype.unLoadGrid = function ()
{
    $('#grid').jqGrid("clearGridData");
    //$('#grid').jqGrid('GridDestroy');
    //$("#grid").jqGrid('GridUnload')
    //$.jgrid.gridUnload('#grid'); 
    //$('#grid').GridUnload();
    $('#grid').remove();
    $('#gridPager').remove();
    $('#gridContainer').empty();    
    //$('#gridContainer').html("");
}
var userLevel = new UserLevel();
userLevel.init();