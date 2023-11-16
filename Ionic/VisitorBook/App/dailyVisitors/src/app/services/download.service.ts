import { Injectable } from '@angular/core';
import { FileTransfer, FileTransferObject } from '@ionic-native/file-transfer/';
import { File } from "@ionic-native/file/ngx";
import { AlertController, Platform } from '@ionic/angular';
import { from as fromPromise, Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { DocumentViewer } from "@ionic-native/document-viewer/ngx";

@Injectable({
    providedIn: 'root'
})
export class DownloadService {

    constructor(
        private file: File,
        private platform: Platform,
        private alertCtrl: AlertController,
        private transfer: FileTransferObject,
        private documentViewer: DocumentViewer
    ) { }

    public downloadFile(fileName: string, url: string, token: string): Observable<any> {
        return fromPromise(this.performDownload(fileName, url, token));
    }

    protected async performDownload(fileName: string, url: string, token: string) {
        // We added this check since this is only intended to work on devices and emulators 
        if (!this.platform.is('cordova')) {
            console.warn('Cannot download in local environment!');
            return;
        }
        var fileTransfer = new FileTransferObject();
        let uri = encodeURI(url);
        let path = await this.getDownloadPath();
        return fileTransfer.download(
            uri,
            path + fileName
        ).then(
            result => {
                this.showAlert(true, fileName);
                let docUrl = result.toURL();
                this.documentViewer.viewDocument(docUrl, this.getMimetype(fileName), {})
            },
            error => {
                this.showAlert(false, fileName);
                console.log("Error: " + error);
            }
        )
    }

    public async getDownloadPath() {
        if (this.platform.is('ios')) {
            return this.file.documentsDirectory;
        }

        // To be able to save files on Android, we first need to ask the user for permission. 
        // We do not let the download proceed until they grant access
        /* await this.androidPermissions.checkPermission(this.androidPermissions.PERMISSION.WRITE_EXTERNAL_STORAGE).then(
             result => {
                 if (!result.hasPermission) {
                     return this.androidPermissions.requestPermission(this.androidPermissions.PERMISSION.WRITE_EXTERNAL_STORAGE);
                 }
             }
         );*/

        return this.file.externalRootDirectory + "/Download/";
    }

    public showAlert(hasPassed: boolean, fileName: string) {
        let title = hasPassed ? "Download complete!" : "Download failed!";

        let subTitle = hasPassed ? `Successfully downloaded ${fileName}.` : `There was a problem while downloading ${fileName}`;

        const alert = this.alertCtrl.create({
            message: title,
            subHeader: subTitle,
            buttons: ['OK']
        }).then(alert => alert.present());
    }

    private getMimetype(name) {
        if (name.indexOf('.pdf') >= 0) {
            return 'application/pdf'
        } else if (name.indexOf('.csv') >= 0) {
            return 'text/csv'
        } else if (name.indexOf('.png') >= 0) {
            return 'image/png'
        } else if (name.indexOf('.mp4') >= 0) {
            return 'video/mp4'
        }
    }
}
