import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { SnackbarService } from '../services/snackbar.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private snackbarService: SnackbarService,
    private router: Router,
    private zone: NgZone
  ) { }

  ngOnInit(): void {
    this.authService.googleLibraryLoaded$().subscribe((res: boolean) => {
      if (res) {
        console.log('googleLibraryLoaded$ returned true');
        this.renderLoginButton();
      } else {
        console.log('googleLibraryLoaded$ returned false');
      }
    });

    this.authService.credentialResponse$.subscribe((res: boolean) => {
      //this.zone.run(() => {
        if (res) {
          // get return url from query parameters or default to home page
          const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
          this.router.navigateByUrl(returnUrl);
        } else {
          this.snackbarService.openSnackBar('Login failed', 'OK');
        }
      //});
    });
  }

  public renderLoginButton() {
    // @ts-ignore
    google.accounts.id.renderButton(
      // @ts-ignore 
      document.getElementById('loginWithGoogleBtn'),
      { theme: 'outline', size: 'large', width: '100%' }
    );
  }
}
