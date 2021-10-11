import { Component, OnInit, ViewChild, AfterViewInit, OnDestroy } from '@angular/core';
import { VisitorsdetailsService } from '../services/visitorsdetails.service';
import { Visitor} from '../models/visitor';
import { SignaturePad } from 'angular2-signaturepad/signature-pad'
import { PhotoService } from '../services/photo.service';
import { Camera,} from '@ionic-native/camera/ngx';
import { empty } from 'rxjs';
import { NavParams, NavController, AlertController} from '@ionic/angular';
import { VisitorListingTabPage } from '../visitorListingTab/visitorListingTab.page';
import { Form, FormControl, FormGroup, NgForm, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable, Observer } from 'rxjs';
import { settings } from '../models/settings';
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
  get personVisitingInRWS(){
    return this.form.get("personVisitingInRWS");
  }

  form = this.formBuilder.group({
        visitorName: ["",[Validators.required, Validators.maxLength(100)]],
        email: ["",[Validators.required, Validators.email]],
        mobileNumber: ["",[Validators.required, Validators.pattern("[0-9]{10}")]],
        adress: ["",[Validators.required, Validators.maxLength(100)]],
        fromPlace: ["",[Validators.required, Validators.maxLength(100)]],
        company: ["",[Validators.required, Validators.maxLength(100)]],
        personVisitingInRWS: ["",[Validators.required, Validators.maxLength(100)]]
  });

  constructor(public visitorService:VisitorsdetailsService, 
    private http:HttpClient,
    public photoService: PhotoService,
    private camera:Camera,
    public navParams: NavParams,
    public navCtrl:NavController,
    private router:Router, 
    private activatedRouter:ActivatedRoute,
    private formBuilder:FormBuilder,
    private alertController: AlertController) {

    this.visitor = new Visitor();
  }
  
  public signatureImage:string;

  public signaturePadOptions: Object = { 
    'maxwidth':1,
    'minWidth': 1,
    'canvasWidth': 330,
    'canvasHeight': 230
  };

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
  drawClear(form:NgForm):void
  {
    this.signaturePad.clear();
    this.signatureMaskHidden = false;
  }

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
    this.visitorService.getVisitorDetails(settings.rootURL);
    /*this.visitorService.getVisitorDetails(settings.rootURL).subscribe(res => {
      this.visitorService.allVisitorList = res as Visitor[];
    });   */
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
          this.visitorService.postVisitorDetails(form.value, signatureString, base64data, settings.rootURL).subscribe(
            res => {
              this.resetForm(form);
              var details = res as Visitor;
              this.visitorService.showVisitorDetails(details,settings.rootURL);
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

}

