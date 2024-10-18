import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAstronautsComponent } from './manage-astronauts.component';

describe('ManageAstronautsComponent', () => {
  let component: ManageAstronautsComponent;
  let fixture: ComponentFixture<ManageAstronautsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ManageAstronautsComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ManageAstronautsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
