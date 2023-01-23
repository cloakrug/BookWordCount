import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { CredentialResponse } from 'google-one-tap';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(public oidcSecurityService: OidcSecurityService, public authService: AuthService) {
    this.oidcSecurityService.checkAuth().subscribe(({ isAuthenticated, userData, accessToken, idToken }) => {
      console.log('app authenticated', isAuthenticated);
      console.log('app userData', userData);
      console.log('app accessToken', accessToken);
      console.log('app idToken', idToken);
    });
  }
  
  ngOnInit() {
    // @ts-ignore
    window.onGoogleLibraryLoad = () => {
      console.log('Google\'s One-tap sign in script loaded!');

      // @ts-ignore
      google.accounts.id.initialize({
        // Ref: https://developers.google.com/identity/gsi/web/reference/js-reference#IdConfiguration
        client_id: '2187841631-i1nggnmlq66mepnhi12qnavkpcs91sko.apps.googleusercontent.com',
        callback: this.handleCredentialResponse.bind(this), // Whatever function you want to trigger...
        auto_select: false,
        cancel_on_tap_outside: false
      });

      this.authService.googleLibraryLoadedDataSource.next(true);  // TODO: refactor
    };
  }

  handleCredentialResponse(response: CredentialResponse) {
    this.authService.handleCredentialResponse(response);
  }

}
