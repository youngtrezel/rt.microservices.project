import { Component, Input, Output, EventEmitter } from '@angular/core';
import { JsonPipe, NgFor, NgClass, NgIf, CommonModule } from '@angular/common';

@Component({
  selector: 'app-pagination',
  standalone: true,
  imports: [NgFor, NgClass, NgIf, CommonModule],
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.scss'
})
export class PaginationComponent {
  @Input() totalItems: number | any;
  @Input() currentPage: number | any;
  @Input() itemsPerPage: number | any;
  @Output() onClick: EventEmitter<number> = new EventEmitter();
  totalPages = 0;
  pages: number[] = [];

  constructor() {
    
  }

  ngOnInit(): void {

    if(this.totalItems)
      this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
      this.pages = Array.from({ length: this.totalPages }, (_, i) => i + 1);

  }

  pageClicked(page: number) {
    this.onClick.emit(page);
  }

  previousPage() {
    this.onClick.emit(this.currentPage - 1);
  }

  nextPage() {
    this.onClick.emit(this.currentPage + 1);
  }


}
