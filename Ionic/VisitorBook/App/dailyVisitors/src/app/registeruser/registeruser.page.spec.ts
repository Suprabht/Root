import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';

import { RegisteruserPage } from './registeruser.page';

describe('RegisteruserPage', () => {
  let component: RegisteruserPage;
  let fixture: ComponentFixture<RegisteruserPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisteruserPage ],
      imports: [IonicModule.forRoot()]
    }).compileComponents();

    fixture = TestBed.createComponent(RegisteruserPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
