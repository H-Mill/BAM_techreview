import { Component } from '@angular/core';
import { LoadingSpinnerService } from '../services/loading-spinner/loading-spinner.service';

@Component({
  selector: 'app-loading-spinner',
  templateUrl: './loading-spinner.component.html',
  styleUrl: './loading-spinner.component.scss'
})
export class LoadingSpinnerComponent {
  loading$ = this.loadingSpinnerService.loading$; // Track loading state
  constructor(private loadingSpinnerService: LoadingSpinnerService) { }
}
