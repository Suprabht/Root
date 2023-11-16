import { Injectable, OnDestroy } from '@angular/core';
import { Form, FormGroup, NgForm } from '@angular/forms';
import { from as fromPromise, BehaviorSubject, Observable, Subject, throwError } from 'rxjs';
import { map, catchError} from 'rxjs/operators'
import { Visitor } from '../models/visitor';
import { User } from '../models/user';
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http"
import { AlertController, Platform} from '@ionic/angular';
import { CapacitordownloadService } from './capacitordownload.service';
import { DownloadService } from './download.service';
//import { FileTransfer, FileTransferObject } from '@ionic-native/file-transfer';
//import { File } from '@ionic-native/file/ngx';

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
 // private fileTransfer: FileTransferObject; 

  constructor( private http:HttpClient, 
    private platform: Platform,
    private alertController: AlertController, 
    private downloadService:DownloadService,
    private capacitordownloadService:CapacitordownloadService
   // private file: File,
    //    private alertCtrl: AlertController
  ) {}

  downloadPDFVisitorDetails(id:number, rootURL, token){
   /*  const url = rootURL+'/VisitorDetails/visitorDetailsPDF?id=' + id;
    this.fileTransfer = FileTransfer.create(); 
    this.fileTransfer.download(url, this.files.externalRootDirectory + 'visitorDetails_'+Date.now()+'.pdf', true).then((entry) => {
    console.log('download complete: ' + entry.toURL());
  }, (error) => {
    // handle error
  });*/
  /*
   const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': 'application/json',
      'Authorization':'Bearer ' + token
  });
    return this.http.get(rootURL+'/VisitorDetails/visitorDetailsPDF?id=' + id, {headers: headers, responseType: 'blob' as 'json' });*/
    if (this.platform.is('cordova')) {
      this.downloadService.downloadFile('visitorDetails_' + Date.now() + '.pdf', rootURL + '/VisitorDetails/visitorDetailsPDF?id=' + id, token).subscribe();
      //this.capacitordownloadService.download();
    }
    else {
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': 'Bearer ' + token
      });
      return this.http.get(rootURL + '/VisitorDetails/visitorDetailsPDF?id=' + id, { headers: headers, responseType: 'blob' as 'json' })
        .subscribe((data: Blob) => {
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
      );
    }
  }

  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }

  postVisitorDetails(formData, signature:string, photoBase64String:string, rootURL, token, userId){
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
    visitor.userId = userId;
   return this.http.post(rootURL+'/VisitorDetails', visitor, {headers:requestHeaders});
  }

  getRWSUsers(rootURL):User[]{
    //this.fetchingAllRecords = true;
    //var requestHeaders = new HttpHeaders().set('Authorization','Bearer ' + token);
    this.http.get(rootURL+'/Users/GetRWSUsers').subscribe((response) => {
      //console.log(response);
      this.allUserList = response as User[];
      this.observableUserList.next(response as User[]);
      return this.allUserList;
    });
    return this.allUserList;
  }

  getVisitorDetails(rootURL, token, userId):Visitor[]{
    //console.log("Start");
    this.fetchingAllRecords = true;
    var requestHeaders = new HttpHeaders().set('Authorization','Bearer ' + token);
    this.http.get(rootURL+'/VisitorDetails?userId=' + userId, {headers:requestHeaders}).subscribe((response) => {
      //console.log(response);
      this.allVisitorList = response as Visitor[];
      this.fetchingAllRecords= false;
      this.observableVisitorList.next(response as Visitor[]);
      return this.allVisitorList;
    });
    return this.allVisitorList;
  }

  getVisitorDetailsSingleDay(rootURL, token, userId){
    var requestHeaders = new HttpHeaders().set('Authorization','Bearer ' + token);
    return this.http.get(rootURL+'/VisitorDetails?userId=' + userId, {headers:requestHeaders});
  }

  logout(id:number, rootURL, token, userId)
  {
    var requestHeaders = new HttpHeaders().set('Authorization','Bearer ' + token);
    return this.http.get(rootURL+'/VisitorDetails/logoutById?id=' + id +'&userId=' + userId, {headers:requestHeaders});
  }

  delete(id:number, rootURL, token, userId)
  {
    var requestHeaders = new HttpHeaders().set('Authorization','Bearer ' + token);
    return this.http.delete(rootURL+'/VisitorDetails/' + id +'/' + userId, {headers:requestHeaders});
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

/*

  public downloadFile(fileName: string, url: string, token: string): Observable<any> {
    return fromPromise(this.performDownload(fileName, url, token));
    //return Observable.fromPromise()
  }

  protected async performDownload(fileName: string, url: string, token: string){
      // We added this check since this is only intended to work on devices and emulators 
      if (!this.platform.is('cordova')) {
          console.warn('Cannot download in local environment!');
          return;
      }
      const fileTransfer: FileTransferObject = FileTransfer.create();
      //const fileTransfer: FileTransferObject = this.transfer.create();

      //let uri = encodeURI(url);
      let uri = encodeURI("https://apivisitor.azurewebsites.net/StaticFiles/visitorBadge123.pdf");
      let path = await this.getDownloadPath();

     // let fullFileName = fileName + '.' + fileExtension;

  // Depending on your needs, you might want to use some form of authentication for your API endpoints
  // In this case, we are using bearer tokens
    const headers = new HttpHeaders({
          'Content-Type': 'application/json',
          'Accept': 'application/json',
          'Authorization':'Bearer ' + token
      });
      return fileTransfer.download(
          uri,
          path + fileName,
          false
      ).then(
          result => {
              this.showAlert(true, fileName);
          },
          error => {
              this.showAlert(false, fileName);
          }
      )
  }

  public async getDownloadPath() {
      if (this.platform.is('ios')) {
          return this.file.documentsDirectory;
      }

    // To be able to save files on Android, we first need to ask the user for permission. 
    // We do not let the download proceed until they grant access
      

      return this.file.externalRootDirectory + "/Download/";
  }

  public showAlert(hasPassed: boolean, fileName: string) {
      let title = hasPassed ? "Download complete!" : "Download failed!";

      let subTitle = hasPassed ? `Successfully downloaded ${fileName}.` : `There was a problem while downloading ${fileName}`;

      const alert = this.alertCtrl.create({
          message: title,
          subHeader: subTitle,
          buttons: ['OK']
      }).then(alert=>alert.present());
  }
*/
}
