/// <reference path="../../types/jquery/index.d.ts" />
/// <reference path="../../types/jstree/index.d.ts" />

class Roles {
    /*
    Start:: Singleton implementation
    */
    private static _instance: Roles = new Roles();    

    constructor() {
        if (Roles._instance) {
            throw new Error("Error: Instantiation failed: Use SingletonDemo.getInstance() instead of new.");
        }
        Roles._instance = this;
    }

    public static getInstance(): Roles {
        return Roles._instance;
    }

    public init(): void {   
        $('#jstree').jstree();
    }
    /*
    End:: Singleton implementation
    */

    public callUser(value: string): void {
        
        $('.right_pane').load("/Home/UserDetails/" + value + "?_=" + Math.round(Math.random() * 10000));
    }

    public updateUser(): void {
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
                Id:id,
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
    }
    
}
var roles = Roles.getInstance();
roles.init();