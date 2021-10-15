import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { settings } from '../models/settings';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

 public userEmail="";
 public userPassword="";
 public errorMessage="";
 
 private _userIsAuthenticated = false;

 get userAuthenticated(){
   
   return this._userIsAuthenticated;
 }

  constructor(private http:HttpClient, private router: Router) { }

  login(rootURL){
    if((this.userEmail!="")&&(this.userPassword!=""))
    {
      if(this.validateEmail(this.userEmail))
      {
        var user ={
          "email": this.userEmail,
          "password":this.userPassword
         } 
         this.http.post(rootURL+'/Authentication/Login', user).subscribe((resp:any)=>{
           
            settings.token = resp.token;
            settings.userEmail = resp.userEmail;
            settings.userName = resp.userName;
            this._userIsAuthenticated = true;
            this.router.navigateByUrl('/tabs/visitorDetailsTab');
         }, (error) => {
          this.errorMessage = "Login failed! Please check the credential!"
        });
      }
      else{
        this.userEmail="";
        this.errorMessage = "Please enter a valid email id!"
      }
    }
    else{
      this.errorMessage = "Please enter email id and password!"
    }
  }
  validateEmail(mail) 
  {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(mail))
    {
      return (true)
    }
      return (false)
  }
  logout(){
    settings.token = "";
    settings.userEmail = "";
    settings.userName = "";
    this.userEmail="";
    this.userPassword="";
    this._userIsAuthenticated = false;
    this.router.navigateByUrl('/auth');
  }
}
