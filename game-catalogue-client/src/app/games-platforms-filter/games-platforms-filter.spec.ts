import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GamesPlatformsFilter } from './games-platforms-filter';

describe('GamesPlatformsFilter', () => {
  let component: GamesPlatformsFilter;
  let fixture: ComponentFixture<GamesPlatformsFilter>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GamesPlatformsFilter]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GamesPlatformsFilter);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
