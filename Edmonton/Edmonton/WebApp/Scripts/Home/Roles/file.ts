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

    
    private _score: number = 0;
    public setScore(value: number): void {
        this._score = value;
    }

    public getScore(): number {
        alert(this._score)
        return this._score;
    }

    public addPoints(value: number): void {
        this._score += value;
    }

    public removePoints(value: number): void {
        this._score -= value;
    }
}
var roles = Roles.getInstance();
roles.init();