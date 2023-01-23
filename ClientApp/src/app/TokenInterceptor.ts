import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './services/auth.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private auth: AuthService) { }

  protected protectedRoutePrefixes: string[] = [
    '/Book/',
    '/UserBookStat/Add'
  ];

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.isUrlProtected(req.url)) {
      console.log('route is protected. Adding token');
      const token = this.auth.getBearerToken();

      // TODO: if token is null, redirect to login

      const authReq = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });

      return next.handle(authReq);
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


