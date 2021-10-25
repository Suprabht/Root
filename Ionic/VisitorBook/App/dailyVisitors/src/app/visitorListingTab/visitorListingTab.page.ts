import { Component, OnInit } from '@angular/core';
import { VisitorsdetailsService } from '../services/visitorsdetails.service';
import { VisitorDetailsTabPage } from '../visitorDetailsTab/visitorDetailsTab.page';
import { PhotoService } from '../services/photo.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Visitor } from '../models/visitor';
import { settings } from '../models/settings';
import { AlertController} from '@ionic/angular';

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
  detailList = [];
  userName = "";
  constructor(public visitorService:VisitorsdetailsService,
     private visitorDetailsTab:VisitorDetailsTabPage,
     private photoService:PhotoService,
     private router:Router, 
     private activatedRouter:ActivatedRoute,
     private alertController: AlertController) {
  }
 
  ngOnInit() {
    this.selectedVisitor = new Visitor();
   // this.imageUrl = settings.rootURL.replace("/api","");
    this.imageUrl = settings.rootURL;
    this.userName = settings.userName;
    this.visitorService.getVisitorDetailsSingleDay(settings.rootURL, settings.token, settings.userId).subscribe(res => {
      this.detailList = res as Visitor[];
     /* this.detailList.forEach(e =>{
        console.log("testing"+e.loginDateTime);
      });*/ 
      
    });   
  }
  refresh()
  {
    this.detailList = [];
    this.visitorService.getVisitorDetailsSingleDay(settings.rootURL, settings.token, settings.userId).subscribe(res => {
      this.detailList = res as Visitor[];});   
  }
  logout(id:number)
  {    
    this.visitorService.logout(id, settings.rootURL, settings.token, settings.userId).subscribe(response => {
      this.detailList = response as Visitor[];
    });
  }

  delete(id:number)
  {    
    this.visitorService.delete(id, settings.rootURL, settings.token, settings.userId).subscribe(response => {
      this.detailList = response as Visitor[];
    });
  }

  getDetails(){
    this.visitorService.getVisitorDetailsSingleDay(settings.rootURL, settings.token, settings.userId);
  }

  showDetails(visitor:Visitor){
    this.visitorService.showVisitorDetails(visitor,settings.rootURL);
  }
  
  printBadgePDF(visitor:Visitor){
    this.visitorService.printBadgePDF(visitor.visitorId, settings.rootURL, settings.token).subscribe((data: Blob) => {
        var file = new Blob([data], { type: 'application/pdf' })
        var fileURL = URL.createObjectURL(file);
        // if you want to open PDF in new tab
        window.open(fileURL); 
        var a         = document.createElement('a');
        a.href        = fileURL; 
        a.target      = '_blank';
        a.download    = 'visitorBadge.pdf';
        document.body.appendChild(a);
        a.click();
      },
      (error) => {
        console.log('getPDF error: ',error);
      }
    );
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
