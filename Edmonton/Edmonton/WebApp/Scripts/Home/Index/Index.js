Index = function () { };
Index.prototype.init = function () {
    $.getJSON("/Home/IndexDashBoard/",
        function (data) {
            $("#accepted").html(data.isAccepted);
            $("#pendingApproval").html(data.pendingApproval);
            $("#pendingAcceptence").html(data.pendingAcceptence);
        });
};
Index.prototype.unLoadGrid = function () {
   
}
var index = new Index();
index.init();