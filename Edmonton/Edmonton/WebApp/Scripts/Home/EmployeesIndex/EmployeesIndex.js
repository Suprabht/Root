Index = function () { };
Index.prototype.init = function () {
    $.getJSON("/Home/IndexDashBoardEmployes/",
        function (data) {           
            $("#pendingAcceptence").html(data.isPendingAccept);
            $("#clientName").html(data.clientName);
            $("#address").html(data.clientAddress);
            $("#time").html(data.assignmentDate);
        });
};
Index.prototype.buttonclick = function () {
    $(".acceptButton").attr("disabled", "disabled");
}
var index = new Index();
index.init();