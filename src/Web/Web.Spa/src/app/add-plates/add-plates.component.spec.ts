import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPlatesComponent } from './add-plates.component';

describe('AddPlatesComponent', () => {
  let component: AddPlatesComponent;
  let fixture: ComponentFixture<AddPlatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddPlatesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddPlatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
