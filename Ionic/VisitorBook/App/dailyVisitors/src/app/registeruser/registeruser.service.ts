import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { report } from 'process';
import { settings } from '../models/settings';
import { User } from '../models/user';
import { Office } from '../models/office';

@Injectable({
    providedIn: 'root'
})
export class RegisteruserService {
    public userName = "";
    public email = "";
    public password = "";
    public confirmPassword = "";
    public officeId;
    public errorMessage = "";
    public allUserDisplayName: string[];
    public allOffice: Office[];

    constructor(private http: HttpClient, private router: Router) {

    }

    registeruser(rootURL) {
        if (this.userName.trim().length > 4) {
            if (this.allUserDisplayName.some(names => names.toLocaleLowerCase() === this.userName.toLocaleLowerCase())) {
                this.errorMessage = "Please choose another name. This name already exists.";
            }
            else {
                if ((this.password != "") && (this.confirmPassword != "") && (this.email != "") && (this.userName != "")) {
                    if (this.validateEmail(this.email)) {
                        if (this.confirmPassword == this.password) {
                            if (this.validatePassword(this.password)) {
                                var registerModel = {
                                    "email": this.email,
                                    "userName": this.userName,
                                    "password": this.password,
                                    "officeId": this.officeId
                                }
                                debugger;
                                this.http.post(rootURL + '/Authentication/Register', registerModel).subscribe((resp: any) => {
                                    if (resp.result == "Error") {
                                        this.errorMessage = resp.massage
                                    }
                                    else {
                                        this.router.navigateByUrl('/auth');
                                    }
                                }, (error) => {
                                    this.errorMessage = error.error.massage
                                });
                            }
                            else {

                                this.errorMessage = "Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character"
                            }
                        }
                        else {

                            this.errorMessage = "Confirm password and Password doesnot match."
                        }
                    }
                    else {

                        this.errorMessage = "Please check the email Id."
                    }
                }
                else {

                    this.errorMessage = "All fields are mandatory please enter name, email id and passwords."
                }
            }
        }
        else {

            this.errorMessage = "User name should be greater than 4 characters."
        }
    }

    checkName() {
        if (this.userName.trim().length > 4) {
            if (this.allUserDisplayName.some(names => names.toLocaleLowerCase() === this.userName.toLocaleLowerCase())) {
                this.errorMessage = "Please choose another name. This name already exists.";
            }
            else {
                this.errorMessage = "";
            }
        }
        else {

            this.errorMessage = "User name should be greater than 4 characters."
        }
    }

    getUsersDisplayName(rootURL): String[] {
        this.http.get(rootURL + '/Users/GetUsersDisplayName').subscribe((response) => {
            this.allUserDisplayName = response as string[];
            console.log(this.allUserDisplayName);
            return this.allUserDisplayName;
        });
        return this.allUserDisplayName;
    }

    getOfficeName(rootURL): Office[] {
        this.http.get(rootURL + '/Users/GetOffice').subscribe((response) => {
            //console.log(response);
            this.allOffice = response as Office[];
            this.officeId = this.allOffice[0].officeId;
            /*var office = new Office();
            office.officeId = 0;
            office.officeName = "Please select a office";
            this.allOffice.push(office);*/
            return this.allOffice;
        });
        return this.allOffice;
    }

    validatePassword(password) {
        //Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character
        if (/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/.test(password)) {
            return (true)
        }
        return (false)
    }

    validateEmail(mail) {
        if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(mail)) {
            return (true)
        }
        return (false)
    }
}