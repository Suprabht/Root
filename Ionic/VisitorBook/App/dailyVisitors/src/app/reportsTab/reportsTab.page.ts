import { Component } from '@angular/core';
import { Visitor } from '../models/visitor';
import { settings } from '../models/settings';
import { VisitorsdetailsService } from '../services/visitorsdetails.service';

@Component({
  selector: 'app-reportsTab',
  templateUrl: 'reportsTab.page.html',
  styleUrls: ['reportsTab.page.scss']
})
export class ReportsTabPage {
  visitor:Visitor;
  allVisitorList = [];
  detailList = [];
  imageUrl:string;
  selectedVisitor:Visitor;
  constructor(public visitorService:VisitorsdetailsService,) {
    this.visitor = new Visitor();
  }
  ngOnInit() {
    this.selectedVisitor = new Visitor();
    this.imageUrl = settings.rootURL.replace("/api","");
    this.visitorService.getVisitorDetails(settings.rootURL).subscribe(res => {
      this.allVisitorList = res as Visitor[];
      this.detailList = this.allVisitorList;
      console.log(this.detailList.length);
    });   
  }
  keyPressSearch(event, controlName:string)
  {
    //console.log(event);
    this.detailList = this.allVisitorList;
    var valueToTest = event.target.value.toLowerCase();
    if(controlName === "visitorName")
    {
      this.detailList = this.allVisitorList.filter(visitor => visitor.visitorName.toLowerCase().includes(valueToTest));
    }
    if(controlName === "email")
    {
      this.detailList = this.allVisitorList.filter(visitor => visitor.email.toLowerCase().includes(valueToTest));
    }
    if(controlName === "mobileNumber")
    {
      this.detailList = this.allVisitorList.filter(visitor => visitor.mobileNumber.toLowerCase().includes(valueToTest));
    }
    if(controlName === "loginDateTime")
    {
      this.detailList = this.allVisitorList.filter(visitor => visitor.loginDateTime.toLowerCase().includes(valueToTest));
    }
    if(controlName === "fromPlace")
    {
      this.detailList = this.allVisitorList.filter(visitor => visitor.fromPlace.toLowerCase().includes(valueToTest));
    }
    if(controlName === "company")
    {
      this.detailList = this.allVisitorList.filter(visitor => visitor.company.toLowerCase().includes(valueToTest));
    }
    if(controlName === "personVisitingInRWS")
    {
      this.detailList = this.allVisitorList.filter(visitor => visitor.personVisitingInRWS.toLowerCase().includes(valueToTest));
    }
    //console.log(event, event.keyCode, event.keyIdentifier);
    //console.log(this.visitor);
  }

  reset()
  {
    this.visitor = new Visitor();
    this.detailList = this.allVisitorList;
  }
  
  submit()
  {
    this.detailList = this.allVisitorList.filter(visitor => 
      visitor.visitorName.toLowerCase().includes(this.visitor.visitorName) &&
      visitor.email.toLowerCase().includes(this.visitor.email) &&
      visitor.mobileNumber.toLowerCase().includes(this.visitor.mobileNumber) &&
      visitor.loginDateTime.toLowerCase().includes(this.visitor.loginDateTime) &&
      visitor.fromPlace.toLowerCase().includes(this.visitor.fromPlace) &&
      visitor.company.toLowerCase().includes(this.visitor.company) &&
      visitor.personVisitingInRWS.toLowerCase().includes(this.visitor.personVisitingInRWS)
      );
  }

}
