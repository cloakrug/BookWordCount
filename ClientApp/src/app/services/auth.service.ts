import { Injectable } from '@angular/core';
import { AuthenticatedResult, OidcSecurityService } from 'angular-auth-oidc-client';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public googleLibraryLoadedDataSource: BehaviorSubject<boolean> = new BehaviorSubject(false);

  constructor(private oidcService: OidcSecurityService) { }

  public isLoggedIn(): Observable<boolean> {
    return this.oidcService.isAuthenticated$
      .pipe(map((authenticatedRes: AuthenticatedResult): boolean => {
        return authenticatedRes.isAuthenticated;
      }));
  }

  public googleLibraryLoaded(): boolean {
    // @ts-ignore
    return typeof google === 'object';
  }

  public googleLibraryLoaded$(): Observable<boolean> {
    return this.googleLibraryLoadedDataSource;  
  }

  public getBearerToken(): string {
    return localStorage.getItem("token") ?? "";
  }

  public setBearerToken(token: string) {
    localStorage.setItem("token", token)
  }

  public signOutExternal() {
    console.log("removing token");
    localStorage.removeItem("token");
  }
}
