﻿import { Component } from '@angular/core';

@Component({
    selector: 'app',
    template: `<h1>Hello {{firstName}}</h1> `
})
export class AppComponent {
    firstName: string = 'Angular';
}
