import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';
import { ExploreContainerComponentModule } from '../explore-container/explore-container.module';

import { VisitorListingTabPage } from './visitorListingTab.page';

describe('VisitorListingTabPage', () => {
  let component: VisitorListingTabPage;
  let fixture: ComponentFixture<VisitorListingTabPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [VisitorListingTabPage],
      imports: [IonicModule.forRoot(), ExploreContainerComponentModule]
    }).compileComponents();

    fixture = TestBed.createComponent(VisitorListingTabPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
