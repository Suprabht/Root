import { Component, OnInit } from '@angular/core';
import { Office } from '../models/office';
import { settings } from '../models/settings';
import { RegisteruserService } from './registeruser.service';

@Component({
  selector: 'app-registeruser',
  templateUrl: './registeruser.page.html',
  styleUrls: ['./registeruser.page.scss'],
})
export class RegisteruserPage implements OnInit {

  constructor(private registeruserService:RegisteruserService) { }

  ngOnInit() {
    this.registeruserService.getUsersDisplayName(settings.rootURL);
    this.registeruserService.getOfficeName(settings.rootURL);
    /*var office = new Office();
    office.officeId = 0;
    office.officeName = "Please select a office";
    this.registeruserService.allOffice.push(office);*/
  }
  registeruser(){
    this.registeruserService.registeruser(settings.rootURL)
  }
}
