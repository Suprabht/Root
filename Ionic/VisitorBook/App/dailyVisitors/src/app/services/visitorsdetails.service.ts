import { Injectable, OnDestroy } from '@angular/core';
import { Form, FormGroup, NgForm } from '@angular/forms';
import { BehaviorSubject, Observable, Subject, throwError } from 'rxjs';
import { map, catchError} from 'rxjs/operators'
import { Visitor } from '../models/visitor';
import { User } from '../models/user';
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http"
import { AlertController} from '@ionic/angular';
import { FileTransfer, FileTransferObject } from '@ionic-native/file-transfer';
import { File } from '@ionic-native/file/ngx';

@Injectable({
  providedIn: 'root'
})
export class VisitorsdetailsService implements OnDestroy {
  allVisitorList = [];
  allUserList = [];
  selectedUserEmail = "";
  fetchingAllRecords = false;
  selectedVisitor:Visitor;
  
  public observableVisitorList = new Subject<Visitor[]>();
  public observableUserList  = new Subject<User[]>();
  private fileTransfer: FileTransferObject; 

  constructor( private http:HttpClient, 
    private alertController: AlertController, 
    //private transfer: FileTransfer, 
    private files: File
  ) {}

  downloadPDFVisitorDetails(id:number, rootURL, token){
   /*  const url = rootURL+'/VisitorDetails/visitorDetailsPDF?id=' + id;
    this.fileTransfer = FileTransfer.create(); 
    this.fileTransfer.download(url, this.files.externalRootDirectory + 'visitorDetails_'+Date.now()+'.pdf', true).then((entry) => {
    console.log('download complete: ' + entry.toURL());
  }, (error) => {
    // handle error
  });*/
   const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization':'Bearer ' + token
  });
    return this.http.get(rootURL+'/VisitorDetails/visitorDetailsPDF?id=' + id, {headers: headers, responseType: 'blob' as 'json' });
  }

  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }

  postVisitorDetails(formData, signature:string, photoBase64String:string, rootURL, token){
    var requestHeaders = new HttpHeaders().set('Authorization','Bearer ' + token);
    var visitor = new Visitor();
    visitor.visitorId = formData.visitorId ;
    visitor.visitorName = formData.visitorName;
    visitor.email = formData.email;
    visitor.mobileNumber = Number(formData.mobileNumber);
    visitor.adress = formData.adress;
    visitor.fromPlace = formData.fromPlace;
    visitor.company = formData.company
    visitor.signature = signature;
    visitor.picture = photoBase64String;
    visitor.loginDateTime = formData.loginDateTime;
    visitor.logoutDateTime = formData.logoutDateTime;
    visitor.isLogoutVisible = false;
    visitor.isDeleted = false;
    visitor.personInSdl = formData.personInSdl;
   return this.http.post(rootURL+'/VisitorDetails', visitor, {headers:requestHeaders});
  }

  getUsers(rootURL, token):User[]{
    //this.fetchingAllRecords = true;
    var requestHeaders = new HttpHeaders().set('Authorization','Bearer ' + token);
    this.http.get(rootURL+'/Users', {headers:requestHeaders}).subscribe((response) => {
      //console.log(response);
      this.allUserList = response as User[];
      this.observableUserList.next(response as User[]);
      return this.allUserList;
    });
    return this.allUserList;
  }

  getVisitorDetails(rootURL, token):Visitor[]{
    //console.log("Start");
    this.fetchingAllRecords = true;
    var requestHeaders = new HttpHeaders().set('Authorization','Bearer ' + token);
    this.http.get(rootURL+'/VisitorDetails', {headers:requestHeaders}).subscribe((response) => {
      //console.log(response);
      this.allVisitorList = response as Visitor[];
      this.fetchingAllRecords= false;
      this.observableVisitorList.next(response as Visitor[]);
      return this.allVisitorList;
    });
    return this.allVisitorList;
  }

  getVisitorDetailsSingleDay(rootURL, token){
    var requestHeaders = new HttpHeaders().set('Authorization','Bearer ' + token);
    return this.http.get(rootURL+'/VisitorDetails', {headers:requestHeaders});
  }

  logout(id:number, rootURL, token)
  {
    var requestHeaders = new HttpHeaders().set('Authorization','Bearer ' + token);
    return this.http.get(rootURL+'/VisitorDetails/logoutById?id=' + id, {headers:requestHeaders});
  }

  delete(id:number, rootURL, token)
  {
    var requestHeaders = new HttpHeaders().set('Authorization','Bearer ' + token);
    return this.http.delete(rootURL+'/VisitorDetails/' + id, {headers:requestHeaders});
  }

  sendEmailOfVisitorDetails(id:number, rootURL, emailId, token)
  { 
    var requestHeaders = new HttpHeaders().set('Authorization','Bearer ' + token);
    return this.http.get(rootURL+'/VisitorDetails/emailDetails?id=' + id + '&emailId=' + emailId, {headers:requestHeaders});
  }

  printBadgePDF(id:number, rootURL, token){
        const headers = new HttpHeaders({
          'Content-Type': 'application/json',
          'Accept': 'application/json',
          'Authorization':'Bearer ' + token
      });
      return this.http.get(rootURL+'/VisitorDetails/badgePDF?id=' + id, {headers: headers, responseType: 'blob' as 'json' });
  }

  downloadPDF(ids:number[], rootURL, token){
    var str = ids.toString().split(",").join("&visitorIds="); 
    const headers = new HttpHeaders({
          'Content-Type': 'application/json',
          'Accept': 'application/json',
          'Authorization':'Bearer ' + token
      });
    return this.http.get(rootURL+'/VisitorDetails/downloadPDF?visitorIds='+ str, {headers: headers, responseType: 'blob'  as 'json' });
  }

  downloadExcel(ids:number[], rootURL, token){
    var str = ids.toString().split(",").join("&visitorIds="); 
    const headers = new HttpHeaders({
      'Content-Type': 'text/csv',
      'Accept': 'text/csv',
      'Authorization':'Bearer ' + token
      });
    return this.http.get(rootURL+'/VisitorDetails/downloadCSV?visitorIds='+ str, {headers: headers, responseType: 'blob'});
  }

  emailReport(ids:number[], rootURL, emailId, token)
  {
    var requestHeaders = new HttpHeaders().set('Authorization','Bearer ' + token);
    var str = ids.toString().split(",").join("&visitorIds="); 
    return this.http.get(rootURL+'/VisitorDetails/emailReport?visitorIds=' + str + '&emailId=' + emailId, {headers:requestHeaders});
  }

  async showVisitorDetails(visitor:Visitor, url:String){
    //url = url.replace("/api","");
    var message =`<table cellpadding=0 border=0>
      <tr>
        <td>Visitor Name</td>
        <td>${visitor.visitorName}</td>    
      </tr>
      <tr>
        <td>Email</td>
        <td>${visitor.email}</td>    
      </tr>
      <tr>
        <td>Mobile Number</td>
        <td>${visitor.mobileNumber}</td>    
      </tr>
      <tr>
        <td>Address</td>
        <td>${visitor.adress}</td>    
      </tr>
      <tr>
        <td>Form Place Name</td>
        <td>${visitor.fromPlace}</td>    
      </tr>
      <tr>
        <td>Log in Time</td>
        <td>${this.formatedTimestamp(visitor.loginDateTime)}</td>    
      </tr>
      <tr>
        <td>Log out Time</td>
        <td>${this.formatedTimestamp(visitor.logoutDateTime)}</td>    
      </tr>
      <tr>
        <td><img src="${url}/${visitor.signature}" /></td>
        <td><img src="${url}/${visitor.picture}" /></td>    
      </tr>
    </table>`;
    await this.alertController.create({
      cssClass: 'my-custom-class',
      header:"Visitor Details",
      message:message,
      buttons: ['OK']
    }).then(response => response.present());
  }

  formatedTimestamp (dateTime:Date) {
    if((dateTime != undefined) || (dateTime != null))
    {
      var date = dateTime.toString().split('T')[0];
      var time = dateTime.toString().split('T')[1];
      return `${date} ${time}`;
    }
    else{
      return "--";
    }
    
  }
}
