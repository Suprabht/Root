import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { report } from 'process';
import { settings } from '../models/settings';

@Injectable({
  providedIn: 'root'
})
export class RegisteruserService {
    public userName="";
    public email="";
    public password="";
    public confirmPassword="";
    public errorMessage="";
    constructor(private http:HttpClient, private router: Router) { }
    registeruser(rootURL){
        if((this.password!="")&&(this.confirmPassword!="")&&(this.email!="")&&(this.userName!=""))
        {
            if(this.validateEmail(this.email)){
                if(this.confirmPassword==this.password)
                {
                    if(this.validatePassword(this.password))
                    {
                        var registerModel={
                            "email": this.email,
                            "userName": this.userName,
                            "password":this.password
                        }
                        this.http.post(rootURL+'/Authentication/Register', registerModel).subscribe((resp:any)=>{
                            if(resp.result=="Error")
                            {
                                this.errorMessage = resp.text
                            }
                            else{
                                this.router.navigateByUrl('/auth');
                            }
                            }, (error) => {
                                this.errorMessage = "Registration unsuccessful!"
                            }); 
                    }
                    else{

                        this.errorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character"
                    }
                }
                else{

                    this.errorMessage = "Confirm password and Password doesnot match."
                }
            }
            else{

                this.errorMessage = "Please check the email Id."
            }
        }
        else{

            this.errorMessage = "All fields are mandatory please enter name, email id and passwords."
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

    validateEmail(mail) 
    {
        if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(mail))
        {
        return (true)
        }
        return (false)
    }
}