/// <reference path="../../types/jquery/index.d.ts" />
class Login {
    /*
    Start:: Singleton implementation
    */
    private static _instance: Login = new Login();

    constructor() {
        if (Login._instance) {
            throw new Error("Error: Instantiation failed: Use SingletonDemo.getInstance() instead of new.");
        }
        Login._instance = this;
    }

    public static getInstance(): Login {
        return Login._instance;
    }

    public init(): void {
        $(".login").effect("shake", { direction: "up", times: 3, distance: 8 }, 200);
        $(".login").effect("shake", { direction: "up", times: 2, distance: 5 }, 300);
        $(".login").effect("shake", { direction: "up", times: 2, distance: 2 }, 100);
        
    }
    /*
    End:: Singleton implementation
    */
}
var login = Login.getInstance();
login.init();