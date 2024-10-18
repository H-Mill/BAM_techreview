import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAstronautComponent } from './manage-astronaut.component';

describe('ManageAstronautComponent', () => {
  let component: ManageAstronautComponent;
  let fixture: ComponentFixture<ManageAstronautComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ManageAstronautComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ManageAstronautComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
