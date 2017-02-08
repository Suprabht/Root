import { Component } from '@angular/core';

@Component({
  selector: 'my-app',
  template: `<h1>Hello {{name}}</h1><ngb-alert [dismissible]="true">I'm a dismissible alert :) </ngb-alert>`,
})
export class AppComponent  { name = 'Angular'; }
