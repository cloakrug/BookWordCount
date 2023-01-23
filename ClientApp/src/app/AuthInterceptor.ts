import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './services/auth.service';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private auth: AuthService,
    private router: Router
  ) { }

  protected protectedRoutePrefixes: string[] = [
    '/Book/',
    '/UserBookStat/Add'
  ];

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.isUrlProtected(req.url)) {
      console.log('route is protected. Adding token');
      const token = this.auth.getBearerToken();

      const request = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });

      return next.handle(request).pipe(tap(() => { },
        (err: any) => {
          if (err instanceof HttpErrorResponse) {
            if (err.status !== 401) {
              return;
            }
            this.router.navigate(['login']);
          }
        }));

    } else {
      console.log('route is NOT protected');
      return next.handle(req);
    }
  }

  public isUrlProtected(url: string): boolean {
    console.log('checking url:', url)
    return this.protectedRoutePrefixes.some(prefix => url.includes(prefix));
  }
}


