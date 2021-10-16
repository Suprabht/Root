import { Component, OnInit } from '@angular/core';
import { VisitorsdetailsService } from 'src/app/services/visitorsdetails.service';
import { AlertController, ModalController, NavParams, ToastController } from '@ionic/angular';
import { settings } from 'src/app/models/settings';
import { Visitor } from 'src/app/models/visitor';

@Component({
  selector: 'app-visitor-details',
  templateUrl: './visitor-details.page.html',
  styleUrls: ['./visitor-details.page.scss'],
})
export class VisitorDetailsPage implements OnInit {
  imageUrl:string;
  constructor(public toastCtrl: ToastController, 
    public visitorService:VisitorsdetailsService, 
    private modalController: ModalController,
    private alertController: AlertController) { }

  logout(id:number)
  {    
    this.visitorService.logout(id, settings.rootURL,settings.token).subscribe(response => {
      var detailList = response as Visitor[];
      if(detailList.length>0)
      {
        //visitorService.selectedVisitor
        detailList.forEach((visitor)=>{
          if(visitor.visitorId == this.visitorService.selectedVisitor.visitorId)
          {
            this.visitorService.selectedVisitor = visitor;
          }
        });
        this.openToast("Logout Successfully.");
      }
    });
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

  delete(id:number)
  {    
    this.visitorService.delete(id, settings.rootURL, settings.token).subscribe(response => {
      var detailList = response as Visitor[];
      if(detailList.length>0)
      {
        this.openToast("Deleted Successfully.");
        this.closeModal();
      }
    });
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
  
  ngOnInit() {
    //this.imageUrl = settings.rootURL.replace("/api","");
    this.imageUrl = settings.rootURL;
  }

  async openToast(message:string) {
    const toast = await this.toastCtrl.create({
      color: 'dark',
      duration: 4000,
      message: message
    }).then(toast => {
      toast.present();
    });
  }
  async closeModal() {
    const onClosedData: string = "Wrapped Up!";
    await this.modalController.dismiss(onClosedData);
  }
}
