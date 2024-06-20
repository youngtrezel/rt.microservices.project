import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservePlatesComponent } from './reserve-plates.component';

describe('ReservePlatesComponent', () => {
  let component: ReservePlatesComponent;
  let fixture: ComponentFixture<ReservePlatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservePlatesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReservePlatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
