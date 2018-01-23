/// <reference path="../../types/jquery/index.d.ts" />
/// <reference path="../../types/jstree/index.d.ts" />

class Roles {
    /*
    Start:: Singleton implementation
    */
    private static instance: Roles = new Roles();    

    constructor() {
        if (Roles.instance) {
            throw new Error("Error: Instantiation failed: Use SingletonDemo.getInstance() instead of new.");
        }
        Roles.instance = this;
    }

    public static getInstance(): Roles {
        return Roles.instance;
    }

    public init(userId:string, roleId:string): void {   
        $(".left_pane").load(`/Home/RolesTree/?_=${Math.round(Math.random() * 10000)}`, () => {
            $("#jstree").jstree("destroy");
           
            $("#jstree").jstree().on("ready.jstree", (e, data) => {
                $(".jstree-anchor").bind("click", function () {
                    roles.addUser($(this).parent().attr("roleId"));
                });
                if (userId === "") {
                    $(".jstree-anchor").first().click();
                } else {
                    //alert(userId);
                    $(`.roleli[roleId=${roleId}]`).children().first().click();
                    $(`.userli[userId=${userId}]`).children()[1].click();
                }
            });
        });
    }
    /*
    End:: Singleton implementation
    */

    public callUser(value: string): void {
        $(".right_pane").load(`/Home/UserDetails/${value}?_=${Math.round(Math.random() * 10000)}`, () => {
            $("#tabs").tabs();
        });
    }

    public addUser(value: string): void {
        $(".right_pane").load(`/Home/AddUser/${value}?_=${Math.round(Math.random() * 10000)}`);
    }

    public addUserToDb(): void {
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

        var middleName = $("#middleName").val();
        var employeeNumber = $("#employeeNumber").val();
        var code = $("#code").val();
        var compensationType = $("#compensationTypeSelect").val();
        var rate = $("#rate").val();
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
                RoleId: roleId,

                MiddleName: middleName,                
                EmployeeNumber: employeeNumber,
                Code: code,
                CompensationType: compensationType,
                Rate: rate
            },
            cache: false,
            success(data) {
                $("#alertDiv").show().html("<strong>Success!</strong> User has been added.");
                roles.init(data.userId, data.roleId);
            },
            error(xhr, ajaxOptions, error) {
                alert(xhr.status);
                alert(`Error: ${xhr.responseText}`);
            }
        });
    }

    public updateUser(): void {
        var id = $("#id").val();
        var name = $("#name").val();
        var secondName = $("#secondName").val();
        var email = $("#email").val();
        var phone = $("#phone").val();
        var alternateEmail = $("#alternateEmail").val();
        var address = $("#address").val();
        var alternetPhone = $("#alternetPhone").val();
        var bloodGroup = $("#bloodGroup").val();

        var middleName = $("#middleName").val();
        var employeeNumber = $("#employeeNumber").val();
        var code = $("#code").val();
        var compensationType = $("#compensationTypeSelect").val();
        var rate = $("#rate").val();
        $.ajax({
            type: "POST",
            url: "/Home/UpdateUserDetails",
            dataType: "json",
            data: {
                Id:id,
                Name: name,
                SecondName: secondName,
                Email: email,
                Phone: phone,
                AlternateEmail: alternateEmail,
                Address: address,
                AlternetPhone: alternetPhone,
                BloodGroup: bloodGroup,

                MiddleName: middleName,
                EmployeeNumber: employeeNumber,
                Code: code,
                CompensationType: compensationType,
                Rate: rate
            },
            cache: false,
            success(data) {
                $("#alertDiv").show().html("<strong>Success!</strong> User has been updated.");
                
            },
            error(xhr, ajaxOptions, error) {
                alert(xhr.status);
                alert(`Error: ${xhr.responseText}`);
            }
        });
    }

    public deleteUser(): void {
        var conf = confirm("Are you sure you want to delete!");
        if (conf == true) {
            var id = $("#id").val();
            $.ajax({
                type: "POST",
                url: "/Home/DeleteUserDetails",
                dataType: "json",
                data: {
                    Id: id
                },
                cache: false,
                success(data) {
                    $("#alertDiv").show().html("<strong>Success!</strong> User has been delete.");
                    roles.init("", "");
                },
                error(xhr, ajaxOptions, error) {
                    alert(xhr.status);
                    alert(`Error: ${xhr.responseText}`);
                }
            });
        }         
    }

    public resetPassword(email: string): void {
        var conf = confirm("Do you want to reset Password!");
        if (conf == true) {
           
            $.ajax({
                type: "POST",
                url: "/Account/ForgetPasswordByUserEmail",
                dataType: "json",
                data: {
                    userEmail: email
                },
                cache: false,
                success(data) {
                    $("#alertDiv").show().html("<strong>Success!</strong> User has been sent mail.");
                   // roles.init("", "");
                },
                error(xhr, ajaxOptions, error) {
                    alert(xhr.status);
                    alert(`Error: ${xhr.responseText}`);
                }
            });
        }
    }
}
var roles = Roles.getInstance();
roles.init("","");