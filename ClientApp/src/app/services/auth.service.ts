import { EventEmitter, Injectable, NgZone } from '@angular/core';
import { CredentialResponse } from 'google-one-tap';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public googleLibraryLoadedDataSource: BehaviorSubject<boolean> = new BehaviorSubject(false);
  public credentialResponse$: EventEmitter<boolean> = new EventEmitter<boolean>();
  public tokenDataSource: BehaviorSubject<string> = new BehaviorSubject("");
  public demoMode$: BehaviorSubject<boolean> = new BehaviorSubject(false);

  constructor(private zone: NgZone) {
    this.googleLibraryLoaded$().subscribe((loaded: boolean) => {
      if (loaded) {
        this.tokenDataSource.next(this.getBearerToken());
      }
    });
  }

  handleCredentialResponse(response: CredentialResponse) {
    this.zone.run(() => {
      // Decoding  JWT token...
      let decodedToken: any | null = null;
      try {
        this.setBearerToken(response.credential);
        decodedToken = JSON.parse(atob(response?.credential.split('.')[1]));
      } catch (e) {
        console.error('Error while trying to decode token', e);
      }
      console.log('decodedToken', decodedToken);

      this.credentialResponse$.emit(this.isTokenNull(decodedToken));
    });
  }

  public isLoggedIn(): Observable<boolean> {
    return this.tokenDataSource.pipe(map(this.isTokenNull));
  }

  public isTokenNull(token: string): boolean {
    return token !== null && token !== undefined && token !== ""
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

  public setDemoMode(mode: boolean) {
    this.demoMode$.next(mode);
  }
}
