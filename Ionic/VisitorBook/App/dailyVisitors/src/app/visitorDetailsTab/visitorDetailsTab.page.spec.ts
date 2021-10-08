import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';
import { ExploreContainerComponentModule } from '../explore-container/explore-container.module';

import { VisitorDetailsTabPage } from './visitorDetailsTab.page';

describe('visitorDetailsTabPage', () => {
  let component: VisitorDetailsTabPage;
  let fixture: ComponentFixture<VisitorDetailsTabPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [VisitorDetailsTabPage],
      imports: [IonicModule.forRoot(), ExploreContainerComponentModule]
    }).compileComponents();

    fixture = TestBed.createComponent(VisitorDetailsTabPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
