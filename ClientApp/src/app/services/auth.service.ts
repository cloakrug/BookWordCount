import { Injectable } from '@angular/core';
import { AuthenticatedResult, OidcSecurityService } from 'angular-auth-oidc-client';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public googleLibraryLoadedDataSource: BehaviorSubject<boolean> = new BehaviorSubject(false);
  public tokenDataSource: BehaviorSubject<string> = new BehaviorSubject("");

  constructor(private oidcService: OidcSecurityService) {
    this.googleLibraryLoaded$().subscribe((loaded: boolean) => {
      if (loaded) {
        this.tokenDataSource.next(this.getBearerToken());
      } 
    });
  }

  public isLoggedIn(): Observable<boolean> {
    return this.tokenDataSource.pipe(map(token => token !== null && token !== undefined && token !== "" ));
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
    this.tokenDataSource.next(token);
    localStorage.setItem("token", token)
  }

  public signOutExternal() {
    console.log("removing token");
    this.tokenDataSource.next("");
    localStorage.removeItem("token");
  }
}
