import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.googleLibraryLoaded$().subscribe((res: boolean) => {
      if (res) {
        console.log('googleLibraryLoaded$ returned true');
        this.renderLoginButton();
      } else {
        console.log('googleLibraryLoaded$ returned false');
      }
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
