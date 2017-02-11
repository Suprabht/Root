import { Component } from '@angular/core';

@Component({
  selector: 'app',
  template: `
    <h1>Hello {{firstName}}</h1>
    <ngb-alert [dismissible]="true">I'm a dismissible alert :) </ngb-alert>
    <input type="text" id="firstName" [(ngModel)]="firstName"/>
    <my-component></my-component>
    `
})
export class AppComponent  {
    firstName: string = 'Angular';
}
