import { Component } from '@angular/core';
import { OrderedPlatesComponent } from '../ordered-plates/ordered-plates.component';
import { FilteredPlatesComponent } from '../filtered-plates/filtered-plates.component';
import { SellPlatesComponent } from '../sell-plates/sell-plates.component';

import { JsonPipe, NgFor, NgIf, CommonModule } from '@angular/common';
@Component({
  selector: 'app-marketing',
  standalone: true,
  imports: [CommonModule, NgIf, OrderedPlatesComponent, FilteredPlatesComponent, SellPlatesComponent],
  templateUrl: './marketing.component.html',
  styleUrl: './marketing.component.scss'
})
export class MarketingComponent {

  ordered: boolean = true;
  filtered: boolean = false;
  sell: boolean = false;

  isSelectedPage(page: string) {

    if(page == "ordered") {
      this.ordered = true;
      this.filtered = false;
      this.sell = false;
    }
    if(page == "filtered") {
      this.ordered = false;
      this.filtered = true;
      this.sell = false;
    } 
    if(page == "sell") {
      this.sell = true;
      this.ordered = false;
      this.filtered = false;
    } 
  }

}
