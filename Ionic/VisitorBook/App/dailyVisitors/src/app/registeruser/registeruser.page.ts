import { Component, OnInit } from '@angular/core';
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
  }
  registeruser(){
    this.registeruserService.registeruser(settings.rootURL)
  }
}
