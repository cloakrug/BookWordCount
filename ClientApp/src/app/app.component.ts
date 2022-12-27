import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  constructor(public oidcSecurityService: OidcSecurityService) {
    this.oidcSecurityService.checkAuth().subscribe(({ isAuthenticated, userData, accessToken, idToken }) => {
      console.log('app authenticated', isAuthenticated);
      console.log('app userData', userData);
      console.log('app accessToken', accessToken);
      console.log('app idToken', idToken);
    });
  }
  
  ngOnInit() {

  }
}
