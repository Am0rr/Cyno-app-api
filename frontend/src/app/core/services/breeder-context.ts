import { Injectable, signal } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class BreederContextService {
  private _breederId = signal<string>('');

  get breederId(): string {
    return this._breederId();
  }

  setBreederId(id: string): void {
    this._breederId.set(id);
  }
}
