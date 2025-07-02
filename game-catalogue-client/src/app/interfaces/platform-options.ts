export interface PlatformOption {
  key: string;
  caption: string;
  selected: boolean
}

export const PLATFORM_OPTIONS: PlatformOption[] = [
  { key: 'xbox1', caption: 'Xbox One', selected: false },
  { key: 'xboxs', caption: 'Xbox Series S', selected: false },
  { key: 'ps4', caption: 'PlayStation 4', selected: false },
  { key: 'ps5', caption: 'PlayStation 5', selected: false },
  { key: 'pc', caption: 'PC', selected: false }
];
