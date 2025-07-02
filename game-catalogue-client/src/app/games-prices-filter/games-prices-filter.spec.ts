import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GamesPricesFilter } from './games-prices-filter';

describe('GamesPricesFilter', () => {
  let component: GamesPricesFilter;
  let fixture: ComponentFixture<GamesPricesFilter>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GamesPricesFilter]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GamesPricesFilter);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
