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
    this.authService.userEmail = "suprabhatpaul@sdl.com";
    this.authService.userPassword = "Welcome@1234";
    this.authService.errorMessage = "";
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
