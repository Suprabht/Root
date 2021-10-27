import { Component, OnInit } from '@angular/core';
import { Office } from '../models/office';
import { settings } from '../models/settings';
import { VisitorsdetailsService } from '../services/visitorsdetails.service';
import { RegisteruserService } from './registeruser.service';

@Component({
  selector: 'app-registeruser',
  templateUrl: './registeruser.page.html',
  styleUrls: ['./registeruser.page.scss'],
})
export class RegisteruserPage implements OnInit {

  constructor(private registeruserService:RegisteruserService,public visitorService:VisitorsdetailsService, ) { }

  ngOnInit() {
    this.registeruserService.getUsersDisplayName(settings.rootURL);
    this.registeruserService.getOfficeName(settings.rootURL);
    this.visitorService.getRWSUsers(settings.rootURL);
  }
  registeruser(){
    this.registeruserService.registeruser(settings.rootURL)
  }
}
