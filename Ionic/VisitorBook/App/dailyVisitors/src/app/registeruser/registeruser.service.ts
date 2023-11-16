import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { report } from 'process';
import { settings } from '../models/settings';
import { User } from '../models/user';
import { Office } from '../models/office';
import { LoadingService } from '../services/loading.service';
import { ModalController } from '@ionic/angular';
import { SearchuserPage } from '../modals/searchuser/searchuser.page';

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
    public loading;

    constructor(private http: HttpClient, 
        private router: Router,
        public modalController: ModalController,
        public loadingControl: LoadingService) {
    }

    registeruser(rootURL) {
        if (this.userName.trim().length > 2) {
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
        this.loadingControl.present();
        this.http.get(rootURL + '/Users/GetUsersDisplayName').subscribe((response) => {
            this.allUserDisplayName = response as string[];
            this.loadingControl.dismiss();
            return this.allUserDisplayName;
        });
        return this.allUserDisplayName;
    }

    getOfficeName(rootURL): Office[] {
        //this.loadingControl.present();
        this.http.get(rootURL + '/Users/GetOffice').subscribe((response) => {
            this.allOffice = response as Office[];
            this.officeId = this.allOffice[0].officeId;
           // this.loadingControl.dismiss();
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

    async openSelectUser() {
        const modal = await this.modalController.create({
          component: SearchuserPage,
          componentProps: {}
        });
    
        modal.onDidDismiss().then((dataReturned) => {
          if (dataReturned !== null) {
            var x = dataReturned.data as User;
            console.log(dataReturned)
            this.userName = x.identityName;
            this.email = x.email;
            this.officeId = x.officeId;
            //this.dataReturned = dataReturned.data;
            //alert('Modal Sent Data :'+ dataReturned);
          }
        });
    
        return await modal.present();
      }
}