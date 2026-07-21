import { Component, computed, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BreederContextService } from '../../core/services/breeder-context';
import { LitterResponse, LitterStatus } from '../../core/models/litter.model';
import { LitterService } from '../../core/services/litter.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-litter-list',
  standalone: true,
  imports: [FormsModule, DatePipe],
  templateUrl: './litter-list.html',
})
export class LitterListComponent {
  breederIdInput = '';
  statuses: (LitterStatus | '')[] = ['', 'Draft', 'Submitted', 'Approved', 'Published'];
  selectedStatus: LitterStatus | '' = '';

  items = signal<LitterResponse[]>([]);
  totalCount = signal(0);
  pageNumber = signal(1);
  pageSize = signal(10);
  errorMessage = signal<string | null>(null);
  isLoading = signal(false);
  publishingId = signal<string | null>(null);

  constructor(
    private litterService: LitterService,
    private breederContext: BreederContextService,
  ) {}

  onBreederIdChange() {
    this.breederContext.setBreederId(this.breederIdInput);
  }

  loadLitters() {
    if (!this.breederIdInput) {
      this.errorMessage.set('Enter Breeder ID.');
      return;
    }

    this.isLoading.set(true);
    this.errorMessage.set(null);

    this.litterService
      .getLitters({
        status: this.selectedStatus || undefined,
        pageNumber: this.pageNumber(),
        pageSize: this.pageSize(),
      })
      .subscribe({
        next: (res) => {
          this.items.set(res.items);
          this.totalCount.set(res.totalCount);
          this.isLoading.set(false);
        },
        error: (err) => {
          this.errorMessage.set(err.error?.message ?? 'Data loading error.');
          this.isLoading.set(false);
        },
      });
  }

  publish(litterId: string) {
    this.publishingId.set(litterId);
    this.errorMessage.set(null);

    this.litterService.publish(litterId).subscribe({
      next: () => {
        this.publishingId.set(null);
        this.loadLitters();
      },
      error: (err) => {
        this.errorMessage.set(err.error?.message ?? 'Publishing error.');
        this.publishingId.set(null);
      },
    });
  }

  totalPages = computed(() => Math.ceil(this.totalCount() / this.pageSize()) || 1);

  nextPage() {
    const maxPage = Math.ceil(this.totalCount() / this.pageSize());
    if (this.pageNumber() < maxPage) {
      this.pageNumber.update((p) => p + 1);
      this.loadLitters();
    }
  }

  prevPage() {
    if (this.pageNumber() > 1) {
      this.pageNumber.update((p) => p - 1);
      this.loadLitters();
    }
  }
}
