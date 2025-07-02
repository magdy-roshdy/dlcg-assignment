import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PriceFilter } from '../interfaces/price-filter';

@Component({
  selector: 'games-prices-filter',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './games-prices-filter.html',
  styleUrls: ['./games-prices-filter.css']
})
export class GamesPricesFilter {
  @Input() options: PriceFilter[] = [];
  @Output() selectionChange = new EventEmitter<void>();

  onToggle(): void {
    this.selectionChange.emit();
  }
}