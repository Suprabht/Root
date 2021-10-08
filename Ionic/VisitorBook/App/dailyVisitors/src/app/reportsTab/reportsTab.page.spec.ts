import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { IonicModule } from '@ionic/angular';
import { ExploreContainerComponentModule } from '../explore-container/explore-container.module';

import { ReportsTabPage } from './reportsTab.page';

describe('ReportsTabPage', () => {
  let component: ReportsTabPage;
  let fixture: ComponentFixture<ReportsTabPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ReportsTabPage],
      imports: [IonicModule.forRoot(), ExploreContainerComponentModule]
    }).compileComponents();

    fixture = TestBed.createComponent(ReportsTabPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
