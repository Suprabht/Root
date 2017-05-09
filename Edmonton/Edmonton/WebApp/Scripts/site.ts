/// <reference path="types/jquery/index.d.ts" />
class Index {
    /*
    Start:: Singleton implementation
    */
    private static _instance: Index = new Index();

    constructor() {
        if (Index._instance) {
            throw new Error("Error: Instantiation failed: Use SingletonDemo.getInstance() instead of new.");
        }
        Index._instance = this;
    }

    public static getInstance(): Index {
        return Index._instance;
    }

    public init(): void {
        $("#logoff").click(function () {
            $("#formLogout").submit();
        });
    }
    /*
    End:: Singleton implementation
    */
}
var index = Index.getInstance();
index.init();