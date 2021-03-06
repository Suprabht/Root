﻿import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './../components/app.component';
import { MyComponentComponent } from "./../components/my-component.component";


@NgModule({
    imports: [BrowserModule, NgbModule.forRoot(), FormsModule],
    declarations: [AppComponent, MyComponentComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }
