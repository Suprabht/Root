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
   

    $.getJSON("/Home/IndexDashBoardEmployes/?r=" + Math.random(),
        function (data) {           
            $("#pendingAcceptence").html(data.isPendingAccept);
            //if ((data.response.indexOf("Error") == -1) && (data.clientName !== ""))
            if ((data.clientName !== "") && (data.clientName != undefined))
            {                
                $("#clientName").html(data.clientName);
                $("#address").html(data.clientAddress);
                $("#time").html(data.assignmentDate);

                $(".today-assignment").show();
                $(".sheduleCall").show();
                index.assignmentId = data.assignmentId;
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
    $(".today-assignment").html("<span class='noAssignment'>No Assignment</span>");
    $.getJSON("/Home/EmergencyLogin/?r=" + Math.random(),
        function (data) {

        });
};
Index.prototype.sheduleCallbuttonclick = function () {
    $(".pendingAcceptence").show();
    $("nav").show();
    $(".presentVisit").hide();
    index.setIsActive();
};
Index.prototype.setIsActive = function () {
    var checkPosition = setInterval(function () {
        
    }, 5000)
    $.getJSON("/Home/ActivateLogin/" + index.assignmentId + "?r=" + Math.random(),
        function (data) {
            
        });
};
Index.prototype.justLogin = function () {
    $(".pendingAcceptence").show();
    $("nav").show();
    $(".presentVisit").hide();
};
Index.prototype.assignmentId = 0;
var index = new Index();
index.init();