// src/app/services/games.service.ts

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Game } from '../interfaces/game';
import { GameFilter } from '../interfaces/game-filter';
import { GameResponse } from '../interfaces/game-response';

@Injectable({
  providedIn: 'root'
})
export class GamesService {
  private readonly baseUrl = `${environment.baseUrl}/api/games`;

  constructor(private http: HttpClient) { }

  getGames(filter?: GameFilter): Observable<GameResponse> {
    let params = new HttpParams();
    if (filter?.platforms?.length) {
      params = params.set('platforms', filter.platforms.join(','));
    }
    if (filter?.priceRanges?.length) {
      params = params.set('prices', filter.priceRanges.join(','));
    }
    if (filter?.page != null) {
      params = params.set('page', filter.page.toString());
    }
    if (filter?.size != null) {
      params = params.set('size', filter.size.toString());
    }
    return this.http.get<GameResponse>(this.baseUrl, { params });
  }

  getGameById(id: number): Observable<Game> {
    return this.http.get<Game>(`${environment.baseUrl}/api/games/${id}`);
  }

  editGame(game: Game): Observable<Game> {
    return this.http.put<Game>(`${this.baseUrl}/${game.id}`, game);
  }

  updateGameForm(form: FormData): Observable<Game> {
    const id = form.get('Id');
    return this.http.put<Game>(`${this.baseUrl}/${id}`, form);
  }
}
