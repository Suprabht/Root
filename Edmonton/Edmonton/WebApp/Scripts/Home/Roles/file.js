/// <reference path="../../types/jquery/index.d.ts" />
/// <reference path="../../types/jstree/index.d.ts" />
var Roles = (function () {
    function Roles() {
        /*
        End:: Singleton implementation
        */
        this._score = 0;
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
    Roles.prototype.setScore = function (value) {
        this._score = value;
    };
    Roles.prototype.getScore = function () {
        alert(this._score);
        return this._score;
    };
    Roles.prototype.addPoints = function (value) {
        this._score += value;
    };
    Roles.prototype.removePoints = function (value) {
        this._score -= value;
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