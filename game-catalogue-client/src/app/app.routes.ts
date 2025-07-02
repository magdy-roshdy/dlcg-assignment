import { Routes } from '@angular/router';

import { GamesList } from './games-list/games-list';
import { GameEdit } from './game-edit/game-edit';

export const routes: Routes = [
    { path: 'games', component: GamesList },
    { path: 'games/:id/edit', component: GameEdit },
    { path: '', redirectTo: 'games', pathMatch: 'full' },
    { path: '**', redirectTo: 'games' }
];
