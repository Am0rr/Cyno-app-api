import { Component, signal } from '@angular/core';
import { LitterListComponent } from './components/litter-list/litter-list';

@Component({
  selector: 'app-root',
  imports: [LitterListComponent],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {
  protected readonly title = signal('frontend');
}
