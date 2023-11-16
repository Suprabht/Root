import { Component } from '@angular/core';
import { Visitor } from '../models/visitor';
import { settings } from '../models/settings';
import { VisitorsdetailsService } from '../services/visitorsdetails.service';
//import { CapacitordownloadService } from '../services/capacitordownload.service';

@Component({
  selector: 'app-reportsTab',
  templateUrl: 'reportsTab.page.html',
  styleUrls: ['reportsTab.page.scss']
})
export class ReportsTabPage {
  visitor:Visitor;
  detailList: Visitor[];
  imageUrl:string;
  selectedVisitor:Visitor;
  userName = "";
  constructor(public visitorService:VisitorsdetailsService) {
    this.visitor = new Visitor();
    this.visitorService.observableVisitorList.subscribe(visitor => {
      //console.log("changed....");
      this.detailList = visitor;
    })
  }
  ngOnInit() {
    this.selectedVisitor = new Visitor();
    //this.imageUrl = settings.rootURL.replace("/api","");
    this.imageUrl = settings.rootURL;
    if(!this.visitorService.fetchingAllRecords)
    {
      this.detailList = this.visitorService.getVisitorDetails(settings.rootURL, settings.token, settings.userId) as Visitor[];
    }
    this.userName = settings.userName;
  }

  keyPressSearch(event, controlName:string)
  {
    //console.log(event);
    this.detailList = this.visitorService.allVisitorList;
    var valueToTest = event.target.value.toLowerCase();
    if(controlName === "visitorName")
    {
      this.detailList = this.visitorService.allVisitorList.filter(visitor => visitor.visitorName.toLowerCase().includes(valueToTest));
    }
    if(controlName === "email")
    {
      this.detailList = this.visitorService.allVisitorList.filter(visitor => visitor.email.toLowerCase().includes(valueToTest));
    }
    if(controlName === "mobileNumber")
    {
      this.detailList = this.visitorService.allVisitorList.filter(visitor => visitor.mobileNumber.toLowerCase().includes(valueToTest));
    }
    if(controlName === "loginDateTime")
    {
      this.detailList = this.visitorService.allVisitorList.filter(visitor => visitor.loginDateTime.toLowerCase().includes(valueToTest));
    }
    if(controlName === "fromPlace")
    {
      this.detailList = this.visitorService.allVisitorList.filter(visitor => visitor.fromPlace.toLowerCase().includes(valueToTest));
    }
    if(controlName === "company")
    {
      this.detailList = this.visitorService.allVisitorList.filter(visitor => visitor.company.toLowerCase().includes(valueToTest));
    }
    if(controlName === "personVisitingInRWS")
    {
      this.detailList = this.visitorService.allVisitorList.filter(visitor => visitor.personVisitingInRWS.toLowerCase().includes(valueToTest));
    }
    //console.log(event, event.keyCode, event.keyIdentifier);
    //console.log(this.visitor);
    console.log(this.detailList.length);
  }

  reset()
  {
    this.visitor = new Visitor();
    this.detailList = this.visitorService.allVisitorList;
  }
  
  submit()
  {
    this.detailList = this.visitorService.allVisitorList.filter(visitor => 
      visitor.visitorName.toLowerCase().includes(this.visitor.visitorName) &&
      visitor.email.toLowerCase().includes(this.visitor.email) &&
      visitor.mobileNumber.toLowerCase().includes(this.visitor.mobileNumber) &&
      visitor.loginDateTime.toLowerCase().includes(this.visitor.loginDateTime) &&
      visitor.fromPlace.toLowerCase().includes(this.visitor.fromPlace) &&
      visitor.company.toLowerCase().includes(this.visitor.company) &&
      visitor.personInSdl.toLowerCase().includes(this.visitor.personInSdl)
      );
  }

  refresh()
  {
    //this.capacitordownloadService.download();
    
    this.selectedVisitor = new Visitor();
    if(!this.visitorService.fetchingAllRecords)
    {
      this.detailList = this.visitorService.getVisitorDetails(settings.rootURL, settings.token, settings.userId) as Visitor[];
    }/**/
  }

  sendEmailOfVisitorDetails(id:number){
    //this.visitorService.sendEmailOfVisitorDetails(id, settings.rootURL);
    this.visitorService.sendEmailOfVisitorDetails(id, settings.rootURL, settings.userEmail, settings.token).subscribe(response => {
      //this.detailList = response as Visitor[];
      console.log(response);
    });
  }

  downloadPDFVisitorDetails(visitor:Visitor){
    this.visitorService.downloadPDFVisitorDetails(visitor.visitorId, settings.rootURL, settings.token);
    /*this.visitorService.downloadPDFVisitorDetails(visitor.visitorId, settings.rootURL, settings.token).subscribe((data: Blob) => {
        var file = new Blob([data], { type: 'application/pdf' })
        var fileURL = URL.createObjectURL(file);
        // if you want to open PDF in new tab
        window.open(fileURL); 
        var a         = document.createElement('a');
        a.href        = fileURL; 
        //a.target      = '_blank';
        a.download    = 'visitorDetails_'+Date.now()+'.pdf';
        document.body.appendChild(a);
        a.click();
      },
      (error) => {
        console.log('getPDF error: ',error);
      }
    );*/

  }

  downloadExcel(){
      let visitorIds = new Array<number>();
      this.detailList.forEach(element => {
        visitorIds.push(element.visitorId);
      });
      this.visitorService.downloadExcel(visitorIds, settings.rootURL, settings.token).subscribe((response) => {
        // @ts-ignore
        let file = new Blob([response], {type: 'text/csv'});
        let downloadUrl = URL.createObjectURL(file);
        let a = document.createElement('a');
        a.href = downloadUrl;
        a.download = 'report_'+Date.now()+'.csv';// you can take a custom name as well as provide by server
    
        // start download
        a.click();
    // after certain amount of time remove this object!!!
    setTimeout( ()=> {
            URL.revokeObjectURL(downloadUrl);
        }, 100);
    });
  }

  downloadPDF(){
      let visitorIds = new Array<number>();
      this.detailList.forEach(element => {
        visitorIds.push(element.visitorId);
      });
      this.visitorService.downloadPDF(visitorIds, settings.rootURL, settings.token).subscribe((response) => {
        // @ts-ignore
        let file = new Blob([response], {type: 'application/pdf'});
        let downloadUrl = URL.createObjectURL(file);
        let a = document.createElement('a');
        a.href = downloadUrl;
        a.download = 'reportPDF_'+Date.now()+'.pdf';// you can take a custom name as well as provide by server
    
        // start download
        a.click();
    // after certain amount of time remove this object!!!
    setTimeout( ()=> {
            URL.revokeObjectURL(downloadUrl);
        }, 100);
    });
  }

  emailReport(){
    let visitorIds = new Array<number>();
      this.detailList.forEach(element => {
        visitorIds.push(element.visitorId);
      });
    this.visitorService.emailReport(visitorIds, settings.rootURL, settings.userEmail, settings.token).subscribe(response => {
      console.log(response);
    });
  }
}
