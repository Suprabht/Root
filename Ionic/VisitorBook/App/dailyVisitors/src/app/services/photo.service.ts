import { Injectable, ViewChild } from '@angular/core';
import {
  Plugins, CameraResultType, Capacitor, FilesystemDirectory,
  CameraPhoto, CameraSource
} from '@capacitor/core';
const { Camera, Filesystem, Storage } = Plugins;
import { Platform } from '@ionic/angular';
import { SignaturePad } from 'angular2-signaturepad/signature-pad'
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PhotoService {
  public photos: Photo[] = [];
  private content = new BehaviorSubject<string>("Default Subject");
  public share = this.content.asObservable();
  updateData(any) {
    this.content.next(any);
  }
  constructor() { }
  public async addNewToGallery() {
    const capturedPhoto = await Camera.getPhoto({
      resultType: CameraResultType.Uri,
      source: CameraSource.Camera,
      quality: 100
    });

    this.photos.unshift({
      filepath: "soon...",
      webviewPath: capturedPhoto.webPath
    });
    
  }
}

interface Photo {
  filepath: string;
  webviewPath: string;
  base64?: string;
}
