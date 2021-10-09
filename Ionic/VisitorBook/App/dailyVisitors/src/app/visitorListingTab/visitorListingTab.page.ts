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
  //abc = '';
  visitorDetailsData : Visitor[];
  list:Visitor[];
  li:any;
  lis = [];
  detailList = [];
  constructor(public visitorService:VisitorsdetailsService,
     private visitorDetailsTab:VisitorDetailsTabPage,
     private photoService:PhotoService,
     private router:Router, 
     private activatedRouter:ActivatedRoute,
     private alertController: AlertController) {
     this.visitorDetailsData = new Array<Visitor>();
  }
 
  ngOnInit() {
    this.imageUrl = settings.rootURL.replace("/api","");
    this.visitorService.getVisitorDetails(settings.rootURL).subscribe(res => {
      this.detailList = res as Visitor[];
     /* this.detailList.forEach(e =>{
        console.log("testing"+e.loginDateTime);
      });
      */
    });    
  }
  refresh()
  {
    this.visitorService.getVisitorDetails(settings.rootURL).subscribe(res => {
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

  showConfirmAlert(id:number) {
    let alertConfirm = this.alertController.create({
      header: 'Delete Items',
      message: 'Are You Sure to delete this itemss?',
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
