import { Component } from '@angular/core';
import { JsonPipe, NgFor, NgIf, CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MarketingService } from '../services/marketing/marketing.service';
import { PaginationComponent } from '../pagination/pagination.component';
import { Plate } from '../../models/plate';

@Component({
  selector: 'app-ordered-plates',
  standalone: true,
  imports: [NgFor, NgIf, JsonPipe, ReactiveFormsModule, FormsModule, PaginationComponent, CommonModule],
  templateUrl: './ordered-plates.component.html',
  styleUrl: './ordered-plates.component.scss'
})
export class OrderedPlatesComponent {

  plateForm!: FormGroup;
  pagedResults: Plate[];
  plateCount: any = 500;
  currentPage: number = 1;
  itemsPerPage: number = 20;
  totalItems: number = 0;
  populated: boolean = false;

  constructor(private formBuilder: FormBuilder, private marketingService: MarketingService) {
    this.pagedResults = [];
  }

  ngOnInit() : void {
    this.getPlateCount();
    this.getPlates();
  }

  getPlates() {
    this.marketingService.getPlates(this.currentPage, this.itemsPerPage).subscribe({
      next: (data) => {
        this.pagedResults = data.Result;
        this.populated = true;
      },
      error: err => console.log(err)
    })
  }

  getPlateCount() {
    this.marketingService.plateCount("unsold").subscribe({
      next: (data) => {
        this.plateCount = data;
      },
      error: err => console.log(err)
    })
  }
  onPageChange(page: number) { 
    this.currentPage = page;
    this.getPlates();
  }
}
