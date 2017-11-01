/// <reference path="../../types/jquery/index.d.ts" />
/// <reference path="../../types/jstree/index.d.ts" />
var Roles = (function () {
    function Roles() {
        if (Roles.instance) {
            throw new Error("Error: Instantiation failed: Use SingletonDemo.getInstance() instead of new.");
        }
        Roles.instance = this;
    }
    Roles.getInstance = function () {
        return Roles.instance;
    };
    Roles.prototype.init = function (userId, roleId) {
        $(".left_pane").load("/Home/RolesTree/?_=" + Math.round(Math.random() * 10000), function () {
            $("#jstree").jstree("destroy");
            $("#jstree").jstree().on("ready.jstree", function (e, data) {
                $(".jstree-anchor").bind("click", function () {
                    roles.addUser($(this).parent().attr("roleId"));
                });
                if (userId === "") {
                    $(".jstree-anchor").first().click();
                }
                else {
                    //alert(userId);
                    $(".roleli[roleId=" + roleId + "]").children().first().click();
                    $(".userli[userId=" + userId + "]").children()[1].click();
                }
            });
        });
    };
    /*
    End:: Singleton implementation
    */
    Roles.prototype.callUser = function (value) {
        $(".right_pane").load("/Home/UserDetails/" + value + "?_=" + Math.round(Math.random() * 10000), function () {
            $("#tabs").tabs();
        });
    };
    Roles.prototype.addUser = function (value) {
        $(".right_pane").load("/Home/AddUser/" + value + "?_=" + Math.round(Math.random() * 10000));
    };
    Roles.prototype.addUserToDb = function () {
        var id = $("#id").val();
        var name = $("#name").val();
        var secondName = $("#secondName").val();
        var email = $("#email").val();
        var phone = $("#phone").val();
        var alternateEmail = $("#alternateEmail").val();
        var address = $("#address").val();
        var alternetPhone = $("#alternetPhone").val();
        var bloodGroup = $("#bloodGroup").val();
        var roleId = $("#roleId").val();
        $.ajax({
            type: "POST",
            url: "/Home/AddUserDetails",
            dataType: "json",
            data: {
                Id: id,
                Name: name,
                SecondName: secondName,
                Email: email,
                Phone: phone,
                AlternateEmail: alternateEmail,
                Address: address,
                AlternetPhone: alternetPhone,
                BloodGroup: bloodGroup,
                RoleId: roleId
            },
            cache: false,
            success: function (data) {
                $("#alertDiv").show().html("<strong>Success!</strong> User has been added.");
                roles.init(data.userId, data.roleId);
            },
            error: function (xhr, ajaxOptions, error) {
                alert(xhr.status);
                alert("Error: " + xhr.responseText);
            }
        });
    };
    Roles.prototype.updateUser = function () {
        var id = $("#id").val();
        var name = $("#name").val();
        var secondName = $("#secondName").val();
        var email = $("#email").val();
        var phone = $("#phone").val();
        var alternateEmail = $("#alternateEmail").val();
        var address = $("#address").val();
        var alternetPhone = $("#alternetPhone").val();
        var bloodGroup = $("#bloodGroup").val();
        $.ajax({
            type: "POST",
            url: "/Home/UpdateUserDetails",
            dataType: "json",
            data: {
                Id: id,
                Name: name,
                SecondName: secondName,
                Email: email,
                Phone: phone,
                AlternateEmail: alternateEmail,
                Address: address,
                AlternetPhone: alternetPhone,
                BloodGroup: bloodGroup
            },
            cache: false,
            success: function (data) {
                $("#alertDiv").show().html("<strong>Success!</strong> User has been updated.");
            },
            error: function (xhr, ajaxOptions, error) {
                alert(xhr.status);
                alert("Error: " + xhr.responseText);
            }
        });
    };
    Roles.prototype.deleteUser = function () {
        var id = $("#id").val();
        //var name = $("#name").val();
        //var secondName = $("#secondName").val();
        //var email = $("#email").val();
        //var phone = $("#phone").val();
        //var alternateEmail = $("#alternateEmail").val();
        //var address = $("#address").val();
        //var alternetPhone = $("#alternetPhone").val();
        //var bloodGroup = $("#bloodGroup").val();
        //var roleId = $("#roleId").val();
        $.ajax({
            type: "POST",
            url: "/Home/DeleteUserDetails",
            dataType: "json",
            data: {
                Id: id
            },
            cache: false,
            success: function (data) {
                $("#alertDiv").show().html("<strong>Success!</strong> User has been delete.");
                roles.init("", "");
            },
            error: function (xhr, ajaxOptions, error) {
                alert(xhr.status);
                alert("Error: " + xhr.responseText);
            }
        });
    };
    return Roles;
}());
/*
Start:: Singleton implementation
*/
Roles.instance = new Roles();
var roles = Roles.getInstance();
roles.init("", "");
//# sourceMappingURL=file.js.map
//# sourceMappingURL=file1.js.map
$(function () {

});