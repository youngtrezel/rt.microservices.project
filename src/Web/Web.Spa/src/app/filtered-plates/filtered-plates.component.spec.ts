import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilteredPlatesComponent } from './filtered-plates.component';

describe('FilteredPlatesComponent', () => {
  let component: FilteredPlatesComponent;
  let fixture: ComponentFixture<FilteredPlatesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FilteredPlatesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FilteredPlatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
