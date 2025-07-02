import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Game } from '../interfaces/game';
import { GamesService } from '../services/games.service';
import { GameResponse } from '../interfaces/game-response';
import { NgbPagination, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import { GameDetails } from '../game-details/game-details';
import { PLATFORM_OPTIONS, PlatformOption } from '../interfaces/platform-options'
import { PriceFilter, PRICE_FILTERS } from '../interfaces/price-filter'
import { GamesPlatformsFilter } from '../games-platforms-filter/games-platforms-filter';
import { GamesPricesFilter } from '../games-prices-filter/games-prices-filter';

@Component({
  selector: 'app-games-list',
  standalone: true,
  imports: [FormsModule,
    CommonModule,
    NgbPagination,
    GameDetails,
    NgbAlertModule,
    GamesPlatformsFilter,
    GamesPricesFilter],
  templateUrl: './games-list.html',
  styleUrls: ['./games-list.css']
})
export class GamesList implements OnInit {
  platformFilters: PlatformOption[] = PLATFORM_OPTIONS;
  priceFilters: PriceFilter[] = PRICE_FILTERS;

  games: Game[] = [];
  page = 1;
  size = 4;
  totalGames = 0;
  isLoading = false;
  errorMessage: string | null = null;

  constructor(private gamesService: GamesService) { }

  ngOnInit(): void {
    this.loadGames();
  }

  loadGames(): void {
    this.isLoading = true;
    const selectedPlatforms = this.platformFilters
      .filter(p => p.selected)
      .map(p => p.key);

    const selectedPrices = this.priceFilters
      .filter(p => p.selected)
      .map(p => p.key);

    this.gamesService.getGames({
      platforms: selectedPlatforms,
      priceRanges: selectedPrices,
      page: this.page,
      size: this.size
    }).subscribe({
      next: (resp: GameResponse) => {
        this.games = resp.items;
        this.totalGames = resp.totalCount;
        this.isLoading = false;
      },
      error: err => {
        this.isLoading = false;
        this.errorMessage = 'Failed to load games. Please try again later.';
        console.error(err)
      }
    });
  }

  onPlatformChange(): void {
    this.page = 1;
    this.loadGames();
  }

  onPriceChange(): void {
    this.page = 1;
    this.loadGames();
  }

  onPageChange(newPage: number): void {
    this.page = newPage;
    this.loadGames();
  }
}