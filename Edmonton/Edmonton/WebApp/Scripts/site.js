/// <reference path="types/jquery/index.d.ts" />
var Index = (function () {
    function Index() {
        if (Index._instance) {
            throw new Error("Error: Instantiation failed: Use SingletonDemo.getInstance() instead of new.");
        }
        Index._instance = this;
    }
    Index.getInstance = function () {
        return Index._instance;
    };
    Index.prototype.init = function () {
        $("#logoff").click(function () {
            $("#formLogout").submit();
        });
    };
    return Index;
}());
/*
Start:: Singleton implementation
*/
Index._instance = new Index();
var index = Index.getInstance();
index.init();
//# sourceMappingURL=site.js.map