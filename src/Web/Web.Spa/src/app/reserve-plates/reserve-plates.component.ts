import { Component } from '@angular/core';
import { JsonPipe, NgFor, NgIf, CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommercialService } from '../services/commercial/commercial.service';
import { PaginationComponent } from '../pagination/pagination.component';
import { Plate } from '../../models/plate';

@Component({
  selector: 'app-reserve-plates',
  standalone: true,
  imports: [NgFor, NgIf, JsonPipe, ReactiveFormsModule, FormsModule, PaginationComponent, CommonModule],
  templateUrl: './reserve-plates.component.html',
  styleUrl: './reserve-plates.component.scss'
})
export class ReservePlatesComponent {

  plateForm!: FormGroup;
  plateForm2!: FormGroup;
  pagedResults: Plate[];
  plateCount: any = 500;
  currentPage: number = 1;
  itemsPerPage: number = 20;
  totalItems: number = 0;
  populated: boolean = false;

  constructor(private formBuilder: FormBuilder, private commercialService: CommercialService) {
    this.pagedResults = [];
  }



  ngOnInit() : void {
    this.plateForm = this.formBuilder.group({
      Registration: new FormControl((''), [Validators.required]),     
    });
    this.plateForm2 = this.formBuilder.group({
      Registration2: new FormControl((''), [Validators.required]),     
    });

    this.getPlateCount();
    this.getPlates();
  }

  get reg(): any { return this.plateForm.get('Registration'); }
  get reg2(): any { return this.plateForm.get('Registration2'); }

  getPlates() {
    this.commercialService.getUnreservedPlates(this.currentPage, this.itemsPerPage).subscribe({
      next: (data) => {
        this.pagedResults = data.Result;
        this.populated = true;
        this.reg.reset();
        this.reg2.reset();
      },
      error: err => console.log(err)
    })
  }

  getPlateCount() {
    this.commercialService.plateCount("unreserved").subscribe({
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

  reservePlate() {

    var reg = this.plateForm.value.Registration;

    this.commercialService.reservePlate(reg).subscribe({
      next: (data) => {
        this.getPlates();
        
      },
      error: err => console.log(err)
    })
  }

  unreservePlate() {

    var reg = this.plateForm.value.Registration2;

    this.commercialService.unreservePlate(reg).subscribe({
      next: (data) => {
        this.getPlates();     
      },
      error: err => console.log(err)
    })
  }

}
