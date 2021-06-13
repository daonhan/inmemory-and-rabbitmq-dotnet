import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ToastrService } from 'ngx-toastr';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
    constructor(private toastrService: ToastrService) {}

    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            catchError((err) => {
                let error = err.error;
                error = { errors: [] };

                if (err.status === 0) {
                    error.errors.push('Server unreachable.')
                    this.toastrService.error('Server unreachable.');
                }

                if (err.status === 500) {
                    error.errors.push('Server could not process your request successfully.')
                    this.toastrService.error('Server could not process your request successfully.')
                }

                if (err.status === 400) {
                    error.errors = err.error.errors.Messages;
                }

                return throwError(error);
            })
        );
    }
}