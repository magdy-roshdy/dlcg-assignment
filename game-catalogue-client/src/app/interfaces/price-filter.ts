export interface PriceFilter {
  key: string;
  min: number | null;
  max: number | null;
  selected: boolean;
}

export const PRICE_FILTERS: PriceFilter[] = [
  { key: 'free', min: null, max: null, selected: false },
  { key: 'lt5', min: null, max: 5, selected: false },
  { key: '5-20', min: 5, max: 20, selected: false },
  { key: '20-40', min: 20, max: 40, selected: false },
  { key: '40-60', min: 40, max: 60, selected: false },
  { key: 'gt60', min: 60, max: null, selected: false },
];