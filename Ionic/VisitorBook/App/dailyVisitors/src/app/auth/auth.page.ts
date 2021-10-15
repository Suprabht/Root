import { Component, OnInit } from '@angular/core';
import { AuthService } from './auth.service';
import { settings } from '../models/settings';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.page.html',
  styleUrls: ['./auth.page.scss'],
})
export class AuthPage implements OnInit {

  constructor(private authService:AuthService) { }

  ngOnInit() {}
  ngAfterViewInit(){}
  ionViewWillEnter()
  {
    this.authService.userEmail = "";
    this.authService.userPassword = "";
    settings.token = "";
    settings.userEmail = "";
    settings.userName = "";
  }
  ionViewDidEnter()
  {}
  onLogin(){
    this.authService.login(settings.rootURL);
  }

}
