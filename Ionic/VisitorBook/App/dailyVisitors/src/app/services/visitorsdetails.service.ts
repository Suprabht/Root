import { Injectable, OnDestroy } from '@angular/core';
import { Form, FormGroup, NgForm } from '@angular/forms';
import { BehaviorSubject, Observable, Subject, throwError } from 'rxjs';
import { map, catchError} from 'rxjs/operators'
import { Visitor } from '../models/visitor';
import {HttpClient, HttpHeaders} from "@angular/common/http"
import { AlertController} from '@ionic/angular';
@Injectable({
  providedIn: 'root'
})
export class VisitorsdetailsService implements OnDestroy {
  constructor( private http:HttpClient,
    private alertController: AlertController) {}
  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }
  postVisitorDetails(formData, signature:string, photoBase64String:string, rootURL){
    var visitor = new Visitor();
    visitor.visitorId = formData.visitorId ;
    visitor.visitorName = formData.visitorName;
    visitor.email = formData.email;
    visitor.mobileNumber = Number(formData.mobileNumber);
    visitor.adress = formData.adress;
    visitor.fromPlace = formData.fromPlace;
    visitor.signature = signature;
    visitor.picture = photoBase64String;
    visitor.loginDateTime = formData.loginDateTime;
    visitor.logoutDateTime = formData.logoutDateTime;
    visitor.isLogoutVisible = false;
    visitor.isDeleted = false;
   return this.http.post(rootURL+'/VisitorDetails', visitor);
  }

  getVisitorDetails(rootURL){
    return this.http.get(rootURL+'/VisitorDetails');
  }

  logout(id:number, rootURL)
  {
    return this.http.get(rootURL+'/VisitorDetails/logoutById?id=' + id);
  }

  delete(id:number, rootURL)
  {
    return this.http.delete(rootURL+'/VisitorDetails/' + id);
  }

  async showVisitorDetails(visitor:Visitor, url:String){
    url = url.replace("/api","");
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