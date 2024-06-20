import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderedPlatesComponent } from './ordered-plates.component';

describe('OrderedPlatesComponent', () => {
  let component: OrderedPlatesComponent;
  let fixture: ComponentFixture<OrderedPlatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrderedPlatesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OrderedPlatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
