import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { report } from 'process';
import { settings } from '../models/settings';

@Injectable({
  providedIn: 'root'
})
export class ChangepasswordService {
    public email="";
    public oldPassword="";
    public newPassword="";
    public errorMessage="";
    constructor(private http:HttpClient, private router: Router) { }
    changePassword(rootURL){
        if((this.oldPassword!="")&&(this.newPassword!=""))
        {
            if(this.validatePassword(this.newPassword))
            {
                var usermodel={
                    "email": this.email,
                    "oldPassword":this.oldPassword,
                    "newPassword":this.newPassword
                   }
                   this.http.post(rootURL+'/Authentication/ChangePassword', usermodel).subscribe((resp:any)=>{
                    if(resp.result=="Error")
                    {
                        this.errorMessage = resp.text
                    }
                    else{
                        this.router.navigateByUrl('/auth');
                    }
                    }, (error) => {
                        this.errorMessage = "Password can not be changed!"
                    }); 
            }
            else{

                this.errorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character"
            }
        }
        else{
            this.errorMessage = "Please enter passwords properly!"
        }
    }

    validatePassword(password) 
    {
        //Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character
        if (/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/.test(password))
        {
        return (true)
        }
        return (false)
    }
}