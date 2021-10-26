export class Office  {
    officeId: number;
    empOfficeId:number;
    officeName:string;
    countryId: number;
    address: string;
    city:string;
    state:string;
    zipCode:string;
    oUName:string;
    active:boolean;
    countryName:string;


    constructor();   
    constructor(object?: any) {
        this.officeId = object && object.officeId || null;
        this.empOfficeId = object && object.empOfficeId || "";
        this.officeName = object && object.officeName || "";
        this.countryId = object && object.countryId || "";
        this.address = object && object.address || null;
        this.city = object && object.city || null;
        this.state = object && object.state || null;
        this.zipCode = object && object.zipCode || null;
        this.oUName = object && object.oUName || "";
        this.active = object && object.active || "";
        this.countryName = object && object.countryName || "";
    }
    
}
