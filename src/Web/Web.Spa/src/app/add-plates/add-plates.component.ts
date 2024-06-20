import { Component } from '@angular/core';
import { JsonPipe, NgFor, NgIf, CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommercialService } from '../services/commercial/commercial.service';
import { PaginationComponent } from '../pagination/pagination.component';
import { Plate } from '../../models/plate';

@Component({
  selector: 'app-add-plates',
  standalone: true,
  imports: [NgFor, NgIf, JsonPipe, ReactiveFormsModule, FormsModule, PaginationComponent, CommonModule],
  templateUrl: './add-plates.component.html',
  styleUrl: './add-plates.component.scss'
})
export class AddPlatesComponent {

  plateForm!: FormGroup;
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
      Numbers: new FormControl((''), [Validators.required, Validators.max(4), Validators.pattern("^[0-9]+$")]),
      Letters: new FormControl((''), [Validators.required, Validators.max(4), Validators.pattern("[a-zA-Z]+")]),
      SalePrice: new FormControl((''), [Validators.required, Validators.pattern("^-?[0-9][0-9\.]+$")]),
      PurchasePrice: new FormControl((''), [Validators.required, Validators.pattern("^-?[0-9][0-9\.]+$")])
    });

    this.getPlateCount();
    this.getPlates();
  }

  get reg(): any { return this.plateForm.get('Registration'); }
  get num(): any { return this.plateForm.get('Numbers'); }
  get let(): any { return this.plateForm.get('Letters'); }
  get sale(): any { return this.plateForm.get('SalePrice'); }
  get purchase(): any { return this.plateForm.get('PurchasePrice'); }

  getPlates() {
    this.commercialService.getPlates(this.currentPage, this.itemsPerPage).subscribe({
      next: (data) => {
        this.pagedResults = data.Result;
        this.populated = true;
      },
      error: err => console.log(err)
    })
  }

  getPlateCount() {
    this.commercialService.plateCount("unsold").subscribe({
      next: (data) => {
        this.plateCount = data;
        this.reset();
      },
      error: err => console.log(err)
    })
  }

  reset() {
    this.reg.reset();
    this.num.reset();
    this.let.reset();
    this.sale.reset();
    this.purchase.reset();
  }

  onPageChange(page: number) { 
    this.currentPage = page;
    this.getPlates();
  }

  addPlate() {

    const plate: Plate = {
      Registration: this.plateForm.value.Registration,
      Letters: this.plateForm.value.Letters,
      Numbers: this.plateForm.value.Numbers,
      SalePrice: this.plateForm.value.SalePrice,
      PurchasePrice: this.plateForm.value.PurchasePrice,
      Reserved: false,
      Sold: false,
      DateSold: null,
      PriceSoldFor: 0.0
    };


    this.commercialService.addPlate(plate).subscribe({
      next: (data) => {
        this.pagedResults.push(data);
      },
      error: err => console.log(err)
    })
  }

  addMarkup(price: number) : number {

    var markup = (price / 10 ) * 2;

    return markup + price;
  }

}
