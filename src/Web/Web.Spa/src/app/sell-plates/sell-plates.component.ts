import { Component } from '@angular/core';
import { JsonPipe, NgFor, NgIf, CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommercialService } from '../services/commercial/commercial.service';
import { PaginationComponent } from '../pagination/pagination.component';
import { Plate } from '../../models/plate';

@Component({
  selector: 'app-sell-plates',
  standalone: true,
  imports: [NgFor, NgIf, JsonPipe, ReactiveFormsModule, FormsModule, PaginationComponent, CommonModule],
  templateUrl: './sell-plates.component.html',
  styleUrl: './sell-plates.component.scss'
})
export class SellPlatesComponent {

  plateForm!: FormGroup;
  plateForm2!: FormGroup;
  plateForm3!: FormGroup;
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
    this.plateForm3 = this.formBuilder.group({
      Registration3: new FormControl((''), [Validators.required]),     
    });
  }

  sellPlate() {

  }

  applyDiscount() {

  }

  applyMoneyOff(){
    
  }


}
