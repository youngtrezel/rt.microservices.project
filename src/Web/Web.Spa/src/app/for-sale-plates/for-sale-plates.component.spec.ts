import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForSalePlatesComponent } from './for-sale-plates.component';

describe('ForSalePlatesComponent', () => {
  let component: ForSalePlatesComponent;
  let fixture: ComponentFixture<ForSalePlatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ForSalePlatesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ForSalePlatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
