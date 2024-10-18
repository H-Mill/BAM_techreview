import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { LoadingSpinnerService } from '../services/loading-spinner/loading-spinner.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  constructor(private loadingSpinnerService: LoadingSpinnerService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Show the loading spinner before the request starts
    this.loadingSpinnerService.show();

    return next.handle(req).pipe(
      // Hide the loading spinner after the request completes (success or error)
      finalize(() => this.loadingSpinnerService.hide())
    );
  }
}
