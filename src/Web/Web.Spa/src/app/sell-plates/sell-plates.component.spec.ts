import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellPlatesComponent } from './sell-plates.component';

describe('SellPlatesComponent', () => {
  let component: SellPlatesComponent;
  let fixture: ComponentFixture<SellPlatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SellPlatesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(SellPlatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
