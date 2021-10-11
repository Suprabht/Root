import { Component, OnInit } from '@angular/core';
import { VisitorsdetailsService } from '../services/visitorsdetails.service';
import { VisitorDetailsTabPage } from '../visitorDetailsTab/visitorDetailsTab.page';
import { PhotoService } from '../services/photo.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Visitor } from '../models/visitor';
import { settings } from '../models/settings';
import { AlertController} from '@ionic/angular';
declare var html2pdf;

@Component({
  selector: 'app-visitorListingTab',
  templateUrl: 'visitorListingTab.page.html',
  styleUrls: ['visitorListingTab.page.scss']
})
export class VisitorListingTabPage implements OnInit {
  rows = [];
  loadingIndicator = true;
  reorderable = true;
  imageUrl:string;
  selectedVisitor:Visitor;
  //visitorDetailsData : Visitor[];
  //list:Visitor[];
  //li:any;
  //lis = [];
  detailList = [];
  constructor(public visitorService:VisitorsdetailsService,
     private visitorDetailsTab:VisitorDetailsTabPage,
     private photoService:PhotoService,
     private router:Router, 
     private activatedRouter:ActivatedRoute,
     private alertController: AlertController) {
     //this.visitorDetailsData = new Array<Visitor>();
  }
 
  ngOnInit() {
    this.selectedVisitor = new Visitor();
    this.imageUrl = settings.rootURL.replace("/api","");
    //this.detailList = this.visitorService.getVisitorDetails(settings.rootURL);
    this.visitorService.getVisitorDetailsSingleDay(settings.rootURL).subscribe(res => {
      this.detailList = res as Visitor[];
     /* this.detailList.forEach(e =>{
        console.log("testing"+e.loginDateTime);
      });*/ 
      
    });   
  }
  refresh()
  {
    this.detailList = [];
    //this.detailList = this.visitorService.getVisitorDetails(settings.rootURL);
    this.visitorService.getVisitorDetailsSingleDay(settings.rootURL).subscribe(res => {
      this.detailList = res as Visitor[];});   
  }
  logout(id:number)
  {    
    this.visitorService.logout(id, settings.rootURL).subscribe(response => {
      this.detailList = response as Visitor[];
    });
  }

  delete(id:number)
  {    
    this.visitorService.delete(id, settings.rootURL).subscribe(response => {
      this.detailList = response as Visitor[];
    });
  }

  getDetails(){
    this.visitorService.getVisitorDetails(settings.rootURL);
  }

  showDetails(visitor:Visitor){
    this.visitorService.showVisitorDetails(visitor,settings.rootURL);
  }

  printBadge(visitor:Visitor){
    this.selectedVisitor = visitor;
    const div = document.getElementById("printDiv");
    try{
      var option={
        margin:1,
        filename:Date.now().toString()+".pdf"
      }
      html2pdf().set(option).from(div).save();
    }
    catch(e){}
  }

  showConfirmAlert(id:number) {
    let alertConfirm = this.alertController.create({
      header: 'Delete Items',
      message: 'Are You Sure to delete this visitor entry?',
      buttons: [
        {
          text: 'No',
          role: 'cancel',
          handler: () => {
            console.log('No clicked');
          }
        },
        {
          text: 'Yes',
          handler: () => {
            this.delete(id)
          }
        }
      ]
    }).then(response => response.present());
  }
}
