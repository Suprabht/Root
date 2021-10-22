import { Component, OnInit, ViewChild, AfterViewInit, OnDestroy } from '@angular/core';
import { VisitorsdetailsService } from '../services/visitorsdetails.service';
import { Visitor} from '../models/visitor';
import { SignaturePad } from 'angular2-signaturepad/signature-pad'
import { PhotoService } from '../services/photo.service';
import { Camera,} from '@ionic-native/camera/ngx';
import { empty } from 'rxjs';
import { NavParams, NavController, AlertController, ModalController, PopoverController} from '@ionic/angular';
import { VisitorListingTabPage } from '../visitorListingTab/visitorListingTab.page';
import { Form, FormControl, FormGroup, NgForm, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';
import { settings } from '../models/settings';
import { VisitorDetailsPage } from '../modals/visitor-details/visitor-details.page';
import { SearchuserPage } from '../modals/searchuser/searchuser.page';
import { User } from '../models/user';


declare var html2pdf;

@Component({
  selector: 'app-visitorDetailsTab',
  templateUrl: 'visitorDetailsTab.page.html',
  styleUrls: ['visitorDetailsTab.page.scss']
})
export class VisitorDetailsTabPage implements OnInit,OnDestroy,AfterViewInit {
  @ViewChild ('signatureCanvas', {static: true}) signaturePad: SignaturePad;
  photos = this.photoService.photos;
  visitor :Visitor; 
  visitorList:Visitor[];
  signatureMaskHidden = false;
  isEmployee = false;
  userName = "";
  usersList: User[];
  selectedUser: User;
  isEmployeeDropDownVisible = false;
  //#region properties
  get visitorName(){
    return this.form.get("visitorName");
  }
  get email(){
    return this.form.get("email");
  }
  get mobileNumber(){
    return this.form.get("mobileNumber");
  }
  get adress(){
    return this.form.get("adress");
  }
  get fromPlace(){
    return this.form.get("fromPlace");
  }
  get company(){
    return this.form.get("company");
  }
  get personInSdl(){
    return this.form.get("personInSdl");
  }
  //#endregion
  public errorMessages = {
    visitorName: [
      {type:"required", message:"Name is required"},
      {type:"maxlength", message:"Name can not be more than 100 charecter"}
    ],
    email: [
      {type:"required", message:"Email is required"},
      {type:"pattern", message:"Please enter a valid email id."}
    ],
    mobileNumber: [
      {type:"required", message:"Mobile No is required"},
      {type:"pattern", message:"Please enter a valid mobile no"}
    ],
    adress: [
      {type:"required", message:"Address is required"},
      {type:"maxlength", message:"Address can not be more than 100 charecter"}
    ],
    fromPlace: [
      {type:"required", message:"Place is required"},
      {type:"maxlength", message:"Place can not be more than 100 charecter"}
    ],
    company: [
      {type:"required", message:"Company is required"},
      {type:"maxlength", message:"Place can not be more than 100 charecter"}
    ],
    personVisitingInRWS: [
      {type:"required", message:"Person's name to be visited in RWS is required"},
      {type:"maxlength", message:"Place can not be more than 100 charecter"}
    ]
  }
  

  form = this.formBuilder.group({
        visitorName: ["",[Validators.required, Validators.maxLength(100)]],
        email: ["",[Validators.required, Validators.email]],
        mobileNumber: ["",[Validators.required, Validators.pattern("[0-9]{10}")]],
        adress: ["",[Validators.required, Validators.maxLength(100)]],
        fromPlace: ["",[Validators.required, Validators.maxLength(100)]],
        company: ["",[Validators.required, Validators.maxLength(100)]],
        personInSdl: ["",[Validators.required, Validators.maxLength(100)]]
  });

  constructor(public visitorService:VisitorsdetailsService, 
    private http:HttpClient,
    public photoService: PhotoService,
    private camera:Camera,
    public navParams: NavParams,
    public navCtrl:NavController,
    private router:Router, 
    public modalController: ModalController,
    private activatedRouter:ActivatedRoute,
    private formBuilder:FormBuilder,
    private alertController: AlertController,
    private popover:PopoverController) {
    this.isEmployeeDropDownVisible = false
    this.visitor = new Visitor();
    this.selectedUser = new User();
    this.visitorService.observableUserList.subscribe(users => {
      this.usersList = users;
    })
  }

  public signatureImage:string;

  public signaturePadOptions: Object = { 
    'maxwidth':1,
    'minWidth': 1,
    'canvasWidth': 330,
    'canvasHeight': 230
  };

//#region methords
  drawStart() {}
 
  drawComplete() {
    // will be notified of szimek/signature_pad's onEnd event
    this.signatureImage = this.signaturePad.toDataURL();
    this.visitor.signature=this.signatureImage;  
  }

  hideImage()
  {
    this.signatureMaskHidden = true;
  }
/*
  drawClear(form:NgForm):void
  {
    this.signaturePad.clear();
    this.signatureMaskHidden = false;
  }*/

  resetForm(form?:NgForm){
    this.signatureMaskHidden = false;
    this.signaturePad.clear();
    this.photoService.photos.pop();
    if(form!=null)
      form.reset();
  }

  getCamera()
  {
    this.camera.getPicture({
      sourceType: this.camera.PictureSourceType.CAMERA,
      destinationType:this.camera.DestinationType.FILE_URI
    }).then((res)=>{}).catch(e=>{})
  }

  ngOnInit() {
    this.visitorService.getVisitorDetails(settings.rootURL, settings.token);
    this.userName = settings.userName;
    /*this.visitorService.getVisitorDetails(settings.rootURL).subscribe(res => {
      this.visitorService.allVisitorList = res as Visitor[];
    });   */
    this.visitorService.getUsers(settings.rootURL, settings.token);
  }

  ngOnDestroy() {}

  ngAfterViewInit(){}

  opencamera()
  {
    this.photoService.photos.pop();
    this.photoService.addNewToGallery()
  }

  onSubmit(form?:NgForm){ 
    if(this.signatureImage == undefined)
    {
      this.showAlert("Please take a signature");
    }
    else
    {      
      if(this.photos.length>0 )
      {
        var signatureString = this.signatureImage.replace("data:image/png;base64,","");
        this.getBase64ImageFromURL(this.photos[0].webviewPath).subscribe(base64data => {
          this.visitorService.postVisitorDetails(form.value, signatureString, base64data, settings.rootURL, settings.token).subscribe(
            res => {
              this.resetForm(form);
              var details = res as Visitor;
              this.visitorService.selectedVisitor = details;
              this.openModal();
            },
            err=>{console.log(err)}
          );
        });
      }
      else
      {
        this.showAlert("Please take a photo");
      }      
    }    
  }

  getBase64ImageFromURL(url: string) {
    return Observable.create((observer: Observer<string>) => {
      let img = new Image();
      img.crossOrigin = 'Anonymous';
      img.src = url;
      if (!img.complete) {
        img.onload = () => {
          observer.next(this.getBase64Image(img));
          observer.complete();
        };
        img.onerror = (err) => {
          observer.error(err);
        };
      } else {
        observer.next(this.getBase64Image(img));
        observer.complete();
      }
    });
  }

  getBase64Image(img: HTMLImageElement) {
    var canvas = document.createElement("canvas");
    canvas.width = img.width;
    canvas.height = img.height;
    var ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0);
    var dataURL = canvas.toDataURL("image/png");
    return dataURL.replace(/^data:image\/(png|jpg);base64,/, "");
  }

  async showAlert(message:string){
    await this.alertController.create({
      cssClass: 'my-custom-class',
      header:"Validation Failed!!",
      message:message,
      buttons: ['OK']
    }).then(response => response.present());
  }

  downloadPDF()
  {
    const div = document.getElementById("div1");
    var option={
      margin:1,
      filename:Date.now().toString()+".pdf"
    }
    try{
      html2pdf().set(option).from(div).save();
    }
    catch(e){}
  }

  segmentChanged(event){
    if(event.detail.value == "true")
    {
      this.isEmployee = true;
      this.isEmployeeDropDownVisible = false;
      this.selectedUser = new User();
    }
    else{
      this.isEmployee = false;
    }
    //console.log(this.isEmployee);
  }

  async openModal() {
    const modal = await this.modalController.create({
      component: VisitorDetailsPage,
      componentProps: {}
    });

    modal.onDidDismiss().then((dataReturned) => {
      if (dataReturned !== null) {
        //this.dataReturned = dataReturned.data;
        //alert('Modal Sent Data :'+ dataReturned);
      }
    });

    return await modal.present();
  }

  async openSelectUser() {
    const modal = await this.modalController.create({
      component: SearchuserPage,
      componentProps: {}
    });

    modal.onDidDismiss().then((dataReturned) => {
      if (dataReturned !== null) {
        //this.dataReturned = dataReturned.data;
        //alert('Modal Sent Data :'+ dataReturned);
      }
    });

    return await modal.present();
  }
//#endregion  
  loginUser(){
    var visitorUser= new Visitor;
    visitorUser.visitorId = undefined;
    visitorUser.visitorName = this.selectedUser.displayName;
    visitorUser.email = this.selectedUser.email;
    visitorUser.mobileNumber = Number("0000000000");
    visitorUser.adress = "RWS";
    visitorUser.fromPlace = "N/A";
    visitorUser.company = "RWS"
    visitorUser.signature = "None";
    visitorUser.picture = "None";
    visitorUser.loginDateTime = undefined;
    visitorUser.logoutDateTime = undefined;
    visitorUser.personInSdl = "N/A";
    this.visitorService.postVisitorDetails(visitorUser, "None", "None", settings.rootURL, settings.token).subscribe(
      res => {
        var details = res as Visitor;
        this.visitorService.selectedVisitor = details;
        this.isEmployeeDropDownVisible = false;
        this.selectedUser = new User();
        this.openModal();
      },
      err=>{console.log(err)}
    );
  }
  selectVal(user:User){
    this.selectedUser = user;
    this.isEmployeeDropDownVisible = false;
    console.log(this.selectedUser);
  }

  filterUserData(event){
    this.usersList = this.visitorService.allUserList;
    this.selectedUser = new User;
    const val = event.target.value;
    if(val && val.trim() != '' && val.length>0)
    {
      this.isEmployeeDropDownVisible = true;
      this.usersList = this.usersList.filter((item:User)=>{
        return (item.displayName.toLowerCase().indexOf(val.toLowerCase())>-1);
      })
    }else{
      this.isEmployeeDropDownVisible = false;
    }
  }

}

