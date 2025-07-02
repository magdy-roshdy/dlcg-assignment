import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { GamesService } from '../services/games.service';
import { environment } from '../../environments/environment';
import { PLATFORM_OPTIONS, PlatformOption } from '../interfaces/platform-options'

@Component({
  selector: 'game-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './game-edit.html',
  styleUrls: ['./game-edit.css']
})
export class GameEdit implements OnInit {
  gameForm!: FormGroup;
  private gameId!: number;
  selectedFile?: File;
  platformOptions = PLATFORM_OPTIONS;
  imageBaseUrl: string | undefined;

  constructor(
    private fb: FormBuilder,
    private gamesService: GamesService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.gameId = Number(this.route.snapshot.paramMap.get('id'));
    this.imageBaseUrl = environment.baseUrl;

    // build form
    const platformsGroup = this.fb.group(
      this.platformOptions.reduce((acc, opt) => {
        acc[opt.key] = [false];
        return acc;
      }, {} as Record<string, any>)
    );

    this.gameForm = this.fb.group({
      name: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(0)]],
      imagePath: [''],
      platforms: platformsGroup
    });

    // load existing game data
    this.gamesService.getGameById(this.gameId).subscribe({
      next: game => {
        this.gameForm.patchValue({
          name: game.name,
          price: game.price,
          imagePath: game.imagePath
        });
        const selected = game.platforms.split(',');
        selected.forEach(key => {
          if (platformsGroup.controls[key]) {
            platformsGroup.controls[key].setValue(true);
          }
        });
      },
      error: () => this.router.navigate(['/games'])
    });
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.selectedFile = input.files?.[0];
  }

  onSubmit(): void {
    if (!this.gameForm.valid) return;

    //reconstruct platforms
    const sel = Object.entries(this.gameForm.value.platforms)
      .filter(([_, v]) => v)
      .map(([k]) => k)
      .join(',');

    //build FormData
    const formData = new FormData();
    formData.append('Id', this.gameId.toString());
    formData.append('Name', this.gameForm.value.name);
    formData.append('Price', this.gameForm.value.price.toString());
    formData.append('Platforms', sel);
    if (this.selectedFile) {
      formData.append('ImageFile', this.selectedFile);
    }

    this.gamesService.updateGameForm(formData).subscribe({
      next: () => this.router.navigate(['/games']),
      error: err => console.error('Edit failed', err)
    });
  }

  onCancel(): void {
    this.router.navigate(['/games']);
  }
}
