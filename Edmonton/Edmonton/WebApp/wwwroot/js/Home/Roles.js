/// <reference path="../../types/jquery/index.d.ts" />
/// <reference path="../../types/jstree/index.d.ts" />
var Roles = (function () {
    function Roles() {
        if (Roles._instance) {
            throw new Error("Error: Instantiation failed: Use SingletonDemo.getInstance() instead of new.");
        }
        Roles._instance = this;
    }
    Roles.getInstance = function () {
        return Roles._instance;
    };
    Roles.prototype.init = function () {
        $('#jstree').jstree();
    };
    /*
    End:: Singleton implementation
    */
    Roles.prototype.callUser = function (value) {
        $('.right_pane').load("/Home/UserDetails/" + value + "?_=" + Math.round(Math.random() * 10000));
    };
    Roles.prototype.updateUser = function () {
        var id = $("#id").val();
        var name = $("#name").val();
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
                alert('Error: ' + xhr.responseText);
            }
        });
    };
    return Roles;
}());
/*
Start:: Singleton implementation
*/
Roles._instance = new Roles();
var roles = Roles.getInstance();
roles.init();
//# sourceMappingURL=file.js.map
//# sourceMappingURL=file1.js.map
$(function () {

});