/// <reference path="../../types/jquery/index.d.ts" />
var Login = (function () {
    function Login() {
        if (Login._instance) {
            throw new Error("Error: Instantiation failed: Use SingletonDemo.getInstance() instead of new.");
        }
        Login._instance = this;
    }
    Login.getInstance = function () {
        return Login._instance;
    };
    Login.prototype.init = function () {
        $(".login").effect("shake", { direction: "up", times: 3, distance: 8 }, 200);
        $(".login").effect("shake", { direction: "up", times: 2, distance: 5 }, 300);
        $(".login").effect("shake", { direction: "up", times: 2, distance: 2 }, 100);
    };
    return Login;
}());
/*
Start:: Singleton implementation
*/
Login._instance = new Login();
var login = Login.getInstance();
login.init();
//# sourceMappingURL=login.js.map