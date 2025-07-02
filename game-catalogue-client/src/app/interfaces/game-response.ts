import { Game } from "./game";

export interface GameResponse {
  items: Game[];
  totalCount: number;
}