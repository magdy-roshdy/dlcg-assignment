import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PlatformOption } from '../interfaces/platform-options';

@Component({
  selector: 'games-platforms-filter',
  imports: [CommonModule, FormsModule],
  templateUrl: './games-platforms-filter.html',
  styleUrls: ['./games-platforms-filter.css']
})
export class GamesPlatformsFilter {
  @Input() options: PlatformOption[] = [];
  @Output() selectionChange = new EventEmitter<void>();

  onToggle(): void {
    this.selectionChange.emit();
  }
}
