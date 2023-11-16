import { HttpClient, HttpEventType } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Plugins, FilesystemDirectory, FilesystemEncoding } from '@capacitor/core';
import { FileOpener } from "@ionic-native/file-opener/ngx";
import { File } from "@ionic-native/file/ngx";
import { DocumentViewer } from "@ionic-native/document-viewer/ngx";
import { DocumentViewerOptions } from '@ionic-native/document-viewer';
//import { rejects } from 'assert';
//import { resolve } from 'dns';
const { Filesystem, Storage } = Plugins;
const FILE_KEY = "files";

@Injectable({
  providedIn: 'root'
})
export class CapacitordownloadService {
  downloadUrl='';
  myFiles = [];
  downloadProgress = 0;
  pdfUrl = "https://apivisitor.azurewebsites.net/StaticFiles/visitorBadge123.pdf";
  //constructor(){}
  constructor( private http: HttpClient,
    private file: File, 
    private documentViewer:DocumentViewer,
    private fileOpener:FileOpener) {
    this.loadFile();
  }

  async loadFile(){
    const videoList = await Storage.get({ key: FILE_KEY });
    this.myFiles = JSON.parse(videoList.value)||[];
    console.log("Loding File....");
  }

  private convertBlobToBase64 = (blob: Blob) => new Promise((resolve, rejects)=>{
    const reader = new FileReader;
    reader.onerror = rejects;
    reader.onload = () =>{
      resolve(reader.result);
    };
    reader.readAsDataURL(blob);
  });

  private getMimetype(name){
    if(name.indexOf('.pdf')>=0){
      return 'application/pdf'
    } else if(name.indexOf('.csv')>=0){
      return 'text/csv'
    } else if(name.indexOf('.png')>=0){
      return 'image/png'
    } else if(name.indexOf('.mp4')>=0){
      return 'video/mp4'
    }
  }

  download(url?){
    url = this.pdfUrl;
    this.downloadUrl = url? url: this.downloadUrl;
    console.log("download....");
    this.http.get(this.downloadUrl, {
      responseType: 'blob',
      reportProgress: true,
      observe: 'events'
    }).subscribe(async event => {
      console.log("subscribe....");
      if (event.type === HttpEventType.DownloadProgress){
        this.downloadProgress = Math.round((100 * event.loaded)/event.total);
      } else if (event.type === HttpEventType.Response){
        this.downloadProgress = 0;
        console.log(event);
       // console.log(await this.convertBlobToBase64(event.body) as string);
        const name = this.downloadUrl.substr(this.downloadUrl.lastIndexOf('/')+1);
        const base64 = await this.convertBlobToBase64(event.body) as string;
        console.log("Name" + name);

        const saveFile = await Filesystem.writeFile({
          path: name,
          data: base64,
          directory: FilesystemDirectory.Documents,
          encoding: FilesystemEncoding.UTF8
          //directory:this.file.documentsDirectory
        }).then((writeFileResult) => {
          console.log("Write File Result: " + writeFileResult)
          Filesystem.getUri({
              directory: FilesystemDirectory.Documents,
              path: name
          }).then((getUriResult) => {
              const path = getUriResult.uri;
              console.log("uri: " + path);
              const options: DocumentViewerOptions = {
                title: 'My PDF'
              }
              
              this.documentViewer.viewDocument('assets/visitorDetailsPDF.pdf', 'application/pdf', options)
              //this.documentViewer.viewDocument(path, 'application/pdf', {})
              /*this.fileOpener.open(path, 'application/pdf')
              .then(() => console.log('File is opened'))
              .catch(error => console.log('Error openening file: ', error));
*/
              this.myFiles.unshift(path);

              Storage.set({
                key:FILE_KEY,
                value:JSON.stringify(this.myFiles)
              })
          }, (error) => {
              console.log(error);
          });
        });
        //console.log("uri:" + saveFile.uri);
       // console.log("directory" + saveFile.directory);

      /*   const path = saveFile.uri;
        const mimeType = this.getMimetype(name);
        console.log(path, mimeType);
       this.fileOpener.open(path, mimeType)
        .then(()=> console.log('File is opened'))
        .catch(()=>console.log("Error opening file"));
*/
        
      }
    });
  }/**/
}
