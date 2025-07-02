import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Game } from '../interfaces/game';
import { environment } from '../../environments/environment';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'game-details',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './game-details.html',
  styleUrls: ['./game-details.css']
})
export class GameDetails {
  @Input() game!: Game;

  private platformIconMap: Record<string, string> = {
    xmbox1: 'fab fa-xbox',
    xboxs: 'fab fa-xbox',
    ps4: 'fab fa-playstation',
    ps5: 'fab fa-playstation',
    pc: 'fas fa-desktop'
  };

  private iconTitleMap: Record<string, string> = {
    'fab fa-xbox': 'Xbox',
    'fab fa-playstation': 'PlayStation',
    'fas fa-desktop': 'PC'
  };

  get uniquePlatformIcons(): { icon: string; title: string }[] {
    const icons = this.game.platforms
      .split(',')
      .map(key => this.platformIconMap[key.trim()])
      .filter(Boolean);

    return Array.from(new Set(icons))
      .map(icon => ({
        icon,
        title: this.iconTitleMap[icon] || ''
      }));
  }

  get imageUrl(): string {
    return environment.baseUrl + this.game.imagePath;
  }
}
