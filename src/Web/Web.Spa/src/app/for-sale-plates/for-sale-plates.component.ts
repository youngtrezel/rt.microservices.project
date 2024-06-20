import { Component } from '@angular/core';
import { JsonPipe, NgFor, NgIf, CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommercialService } from '../services/commercial/commercial.service';
import { PaginationComponent } from '../pagination/pagination.component';
import { Plate } from '../../models/plate';

@Component({
  selector: 'app-for-sale-plates',
  standalone: true,
  imports: [NgFor, NgIf, JsonPipe, ReactiveFormsModule, FormsModule, PaginationComponent, CommonModule],
  templateUrl: './for-sale-plates.component.html',
  styleUrl: './for-sale-plates.component.scss'
})
export class ForSalePlatesComponent {

  plateForm!: FormGroup;
  pagedResults: Plate[];
  currentPage: number = 1;
  itemsPerPage: number = 20;
  populated: boolean = false;

  constructor(private formBuilder: FormBuilder, private commercialService: CommercialService) {
    this.pagedResults = [];
  }

  ngOnInit() : void {
    this.plateForm = this.formBuilder.group({
      Search: new FormControl((''), [Validators.required]),     
    });
    this.getPlates();
  }

  getPlates() {
    this.commercialService.getFilteredUnsoldPlates(this.plateForm.value.Search, this.currentPage, this.itemsPerPage).subscribe({
      next: (data) => {
        this.pagedResults = data.Result;
        this.populated = true;
      },
      error: err => console.log(err)
    })
  }

}
