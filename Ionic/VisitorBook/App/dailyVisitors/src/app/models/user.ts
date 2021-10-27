export class User  {
    userId: number;
    empowerUserId:string;
    displayName:string;
    email: string;
    dateOfBirth?:Date;
    dateOfJoining?:Date;
    insertDateTime?:Date;
    identityName:string;
    firstName:string;
    lastName:string;
    officeId: number;

    constructor();   
    constructor(object?: any) {
        this.userId = object && object.userId || null;
        this.empowerUserId = object && object.empowerUserId || "";
        this.email = object && object.visitorEmailId || "";
        this.displayName = object && object.displayName || "";
        this.dateOfBirth = object && object.dateOfBirth || null;
        this.dateOfJoining = object && object.dateOfJoining || null;
        this.insertDateTime = object && object.insertDateTime || null;
        this.identityName = object && object.identityName || "";
        this.firstName = object && object.firstName || "";
        this.lastName = object && object.lastName || "";
        this.officeId = object && object.officeId || null;
        
    }
    
}
