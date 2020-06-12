import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class AppHttpInterceptor implements HttpInterceptor {
    constructor(private toasterService: ToastrService) {}
    private options = { positionClass: 'toast-top-right' };

    intercept(
        req: HttpRequest<any>,
        next: HttpHandler
      ): Observable<HttpEvent<any>> {

        return next.handle(req).pipe(
            catchError((err: any) => {
                if (err instanceof HttpErrorResponse) {
                    this.toasterService.error('HTTP request failed', '', this.options);
                } else {
                    this.toasterService.error('Something went wrong', '', this.options);
                }
                return of(err);
            }));
      }
}
