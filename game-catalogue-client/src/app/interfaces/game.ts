export interface Game {
  id: number;
  name: string;
  price: number;
  addedOn: string;
  imagePath: string;
  lastModified?: string;
  platforms: string;
}