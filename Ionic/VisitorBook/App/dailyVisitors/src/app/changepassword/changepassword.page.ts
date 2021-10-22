import { Component, OnInit } from '@angular/core';
import { settings } from '../models/settings';
import { ChangepasswordService } from './changepassword.service';

@Component({
  selector: 'app-changepassword',
  templateUrl: './changepassword.page.html',
  styleUrls: ['./changepassword.page.scss'],
})
export class ChangepasswordPage implements OnInit {

  constructor(private changepasswordService:ChangepasswordService) { }

  ngOnInit() {
  }
  ionViewWillEnter()
  {
    this.changepasswordService.email = settings.userEmail;
    this.changepasswordService.oldPassword = "";
    this.changepasswordService.newPassword = "";
    this.changepasswordService.errorMessage = "";
  }
  ionViewDidEnter()
  {}
  changePassword(){
    this.changepasswordService.changePassword(settings.rootURL);
  }
}
