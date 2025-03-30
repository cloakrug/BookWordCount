import { Component, OnInit } from '@angular/core';
import { Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'signin-banner',
  templateUrl: './signin-banner.component.html',
  styleUrls: ['./signin-banner.component.scss']
})
export class SigninBannerComponent implements OnInit {
  private routerSnapshot: RouterStateSnapshot;

  constructor(private router: Router, private authService: AuthService) {
    this.routerSnapshot = router.routerState.snapshot;
  }

  ngOnInit(): void {
    console.log(' in SigninBannerComponent')
  }

  onLoginClick(): void {
    console.log(' in onLoginClick')
    this.router.navigate(['/login'], { queryParams: { returnUrl: this.routerSnapshot.url } });
  }

  enterDemoMode(): void{
    console.log('entering demo mode...')
    this.authService.setDemoMode(true); 
  }
}

