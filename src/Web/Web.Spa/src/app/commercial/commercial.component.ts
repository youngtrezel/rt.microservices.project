import { Component } from '@angular/core';
import { JsonPipe, NgFor, NgIf, CommonModule } from '@angular/common';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AddPlatesComponent } from '../add-plates/add-plates.component';
import { ReservePlatesComponent } from '../reserve-plates/reserve-plates.component';
import { ForSalePlatesComponent } from '../for-sale-plates/for-sale-plates.component';

@Component({
  selector: 'app-commercial',
  standalone: true,
  imports: [ForSalePlatesComponent, ReservePlatesComponent, AddPlatesComponent, NgFor, NgIf, JsonPipe, ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './commercial.component.html',
  styleUrl: './commercial.component.scss'
})
export class CommercialComponent {

  add: boolean = true;
  reserve: boolean = false;
  sell: boolean = false;


  constructor() {
    
  }

  isSelectedPage(page: string) {

    if(page == "add") {
      this.add = true;
      this.sell = false;
      this.reserve = false;
    }
    if(page == "sell") {
      this.add = false;
      this.sell = true;
      this.reserve = false;
    }
    if(page == "reserve") {
      this.add = false;
      this.sell = false;
      this.reserve = true;
    }
    
  }




}
