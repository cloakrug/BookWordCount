import { Injectable } from '@angular/core';
import { AuthenticatedResult, OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private oidcService: OidcSecurityService) { }

  isLoggedIn(): Observable<boolean> {
    return this.oidcService.isAuthenticated$
      .pipe(map((authenticatedRes: AuthenticatedResult): boolean => {
        return authenticatedRes.isAuthenticated;
      }));
  }
}
