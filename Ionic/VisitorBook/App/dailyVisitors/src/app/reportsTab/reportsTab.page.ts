import { Component } from '@angular/core';
import { Visitor } from '../models/visitor';
import { settings } from '../models/settings';
import { VisitorsdetailsService } from '../services/visitorsdetails.service';
declare var html2pdf;

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
  constructor(public visitorService:VisitorsdetailsService,) {
    this.visitor = new Visitor();
    this.visitorService.observableVisitorList.subscribe(visitor => {
      console.log("changed....");
      this.detailList = visitor;
    })
  }
  ngOnInit() {
    this.selectedVisitor = new Visitor();
    this.imageUrl = settings.rootURL.replace("/api","");
    if(!this.visitorService.fetchingAllRecords)
    {
      this.detailList = this.visitorService.getVisitorDetails(settings.rootURL) as Visitor[];
    }
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
      visitor.personVisitingInRWS.toLowerCase().includes(this.visitor.personVisitingInRWS)
      );
  }

  refresh()
  {
    this.selectedVisitor = new Visitor();
    if(!this.visitorService.fetchingAllRecords)
    {
      this.detailList = this.visitorService.getVisitorDetails(settings.rootURL) as Visitor[];
    }
  }
  printVisitorDetails(visitor:Visitor){
    this.selectedVisitor = visitor;
    
    const div = document.getElementById("printVisitorDetails");
    try{
      var option={
        margin:10,
        filename:Date.now().toString()+".pdf"
      }
      html2pdf().set(option).from(div).save();
    }
    catch(e){}
  }
  sendEmailOfVisitorDetails(id:number){
    this.visitorService.sendEmailOfVisitorDetails(id, settings.rootURL).subscribe(response => {
      this.detailList = response as Visitor[];
    });
  }
}
