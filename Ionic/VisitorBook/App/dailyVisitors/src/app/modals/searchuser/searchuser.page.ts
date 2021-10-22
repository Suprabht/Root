import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { User } from 'src/app/models/user';
import { VisitorsdetailsService } from 'src/app/services/visitorsdetails.service';

@Component({
  selector: 'app-searchuser',
  templateUrl: './searchuser.page.html',
  styleUrls: ['./searchuser.page.scss'],
})
export class SearchuserPage implements OnInit {
  usersList: User[];
  selectedUser: User;
  isEmployeeDropDownVisible = false;

  constructor(public modalController: ModalController,
    public visitorService:VisitorsdetailsService) { }

  ngOnInit() {
    this.selectedUser = new User;
  }

  filterUserData(event){
    this.usersList = this.visitorService.allUserList;
    this.selectedUser = new User;
    const val = event.target.value;
    if(val && val.trim() != '' && val.length>0)
    {
      this.isEmployeeDropDownVisible = true;
      this.usersList = this.usersList.filter((item:User)=>{
        return (item.displayName.toLowerCase().indexOf(val.toLowerCase())>-1);
      })
    }else{
      this.isEmployeeDropDownVisible = false;
    }
  }
  
  selectVal(user:User){
    this.selectedUser = user;
    this.isEmployeeDropDownVisible = false;
    console.log(this.selectedUser);
  }

  async closeModal() {
    const onClosedData: string = "Wrapped Up!";
    await this.modalController.dismiss(onClosedData);
  }

  async done(){
    this.visitorService.selectedUserEmail = this.selectedUser.email;
    this.closeModal();
  }
}
