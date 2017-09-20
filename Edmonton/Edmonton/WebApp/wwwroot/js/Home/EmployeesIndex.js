Index = function () { };
Index.prototype.init = function () {
    var nextVisit = sessionStorage.getItem('nextVisit');
    if (nextVisit !== "true")
    {
        $(".pendingAcceptence").hide();
        $("nav").hide();
        $(".today-assignment").hide();
        $(".sheduleCall").hide();
        $(".emergencyCall").hide();
        sessionStorage.setItem('nextVisit', 'true');
    }
    else
    {
        $(".pendingAcceptence").show();
        $("nav").show();
        $(".presentVisit").hide();
    }
   

    $.getJSON("/Home/IndexDashBoardEmployes/",
        function (data) {           
            $("#pendingAcceptence").html(data.isPendingAccept);
            if (data.clientName !== "")
            {
                
                $("#clientName").html(data.clientName);
                $("#address").html(data.clientAddress);
                $("#time").html(data.assignmentDate);

                $(".today-assignment").show();
                $(".sheduleCall").show();
                
            }
            else {
              
                $(".emergencyCall").show();
            }
        });
};
Index.prototype.buttonclick = function () {
    $(".pendingAcceptence").show();
    $("nav").show();
    $(".presentVisit").hide();
}
Index.prototype.justLogin = function () {
    $(".pendingAcceptence").show();
    $("nav").show();
    $(".presentVisit").hide();
}
var index = new Index();
index.init();