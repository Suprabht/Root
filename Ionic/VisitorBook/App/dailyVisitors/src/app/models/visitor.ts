export class Visitor  {
    visitorId: number;
    visitorName:string
    email: string;
    company:string;
    mobileNumber: number;
    adress: string;
    fromPlace: string;
    signature: string;
    picture:string;
    loginDateTime?:Date;
    logoutDateTime?:Date;
    isLogoutVisible:boolean;
    isDeleted:boolean;
    personInSdl: string;
    userId:Number;


    constructor();   
    constructor(object?: any) {
        this.visitorId = object && object.visitorId || null;
        this.visitorName = object && object.visitorName || "";
        this.email = object && object.visitorEmailId || "";
        this.company = object && object.company || "";
        this.mobileNumber = object && object.mobileNumber || null;
        this.adress = object && object.adress || "";
        this.fromPlace = object && object.fromPlace || null;
        this.signature = object && object.signature || "";
        this.loginDateTime = object && object.loginDateTime || null;
        this.logoutDateTime = object && object.logoutDateTime || null;
        this.isLogoutVisible = object && object.logoutTime || false;
        this.isDeleted = object && object.isDeleted || false;
        this.personInSdl = object && object.personInSdl || "";
        this.userId = object && object.userId || 0;
    }

    
}
