import { Component } from '@angular/core';
import { JsonPipe, NgFor, NgIf, CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MarketingService } from '../services/marketing/marketing.service';
import { PaginationComponent } from '../pagination/pagination.component';
import { Plate } from '../../models/plate';

@Component({
  selector: 'app-filtered-plates',
  standalone: true,
  imports: [NgFor, NgIf, JsonPipe, ReactiveFormsModule, FormsModule, PaginationComponent, CommonModule],
  templateUrl: './filtered-plates.component.html',
  styleUrl: './filtered-plates.component.scss'
})
export class FilteredPlatesComponent {

  plateForm!: FormGroup;
  pagedResults: Plate[];
  currentPage: number = 1;
  itemsPerPage: number = 20;
  populated: boolean = false;

  constructor(private formBuilder: FormBuilder, private marketingService: MarketingService) {
    this.pagedResults = [];
  }

  ngOnInit() : void {
    this.plateForm = this.formBuilder.group({
      Search: new FormControl((''), [Validators.required]),     
    });

  }

  getPlates() {
    this.marketingService.getFilteredPlates(this.plateForm.value.Search, this.currentPage, this.itemsPerPage).subscribe({
      next: (data) => {
        this.pagedResults = data.Result;
        this.populated = true;
      },
      error: err => console.log(err)
    })
  }


}
