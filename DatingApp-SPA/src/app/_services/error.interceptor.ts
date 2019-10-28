import { Injectable } from '@angular/core';
import { HttpInterceptor } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    intercept(
        // tslint:disable-next-line: quotemark
        req: import("@angular/common/http").HttpRequest<any> , 
        // tslint:disable-next-line: quotemark
        next: import("@angular/common/http").HttpHandler
        // tslint:disable-next-line: quotemark
        ): import("rxjs").Observable<import("@angular/common/http").HttpEvent<any>> {
        // tslint:disable-next-line: quotemark
            return next.handle(req).pipe(
                catchError(error => {
                    if ( error.status === 401) {
                        return throwError(error.statusText )
                    }
                    if ( error instanceof HttpErrorResponse){
                        const applicationError = error.headers.get('Application-Error');
                        if(applicationError) {
                            return throwError(applicationError)
                        }
                        const serverError = error.error;
                        let modelStateErrors = '';
                        if (serverError.errors && typeof serverError.errors === 'object') {
                            for (const key in serverError.errors) {
                                if (serverError.errors[key]) {
                                    modelStateErrors += serverError.errors[key] + '/n' ;
                                }
                            }
                        }
                        return throwError(modelStateErrors || serverError || 'Server Error');
                    }
                })
            );
    }

}

export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
// tslint:disable-next-line: eofline
};