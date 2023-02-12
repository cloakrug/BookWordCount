import { Component, OnInit } from '@angular/core';
import { Router, RouterStateSnapshot } from '@angular/router';

@Component({
  selector: 'signin-banner',
  templateUrl: './signin-banner.component.html',
  styleUrls: ['./signin-banner.component.scss']
})
export class SigninBannerComponent implements OnInit {
  private routerSnapshot: RouterStateSnapshot;

  constructor(private router: Router) {
    this.routerSnapshot = router.routerState.snapshot;
  }

  ngOnInit(): void {
    console.log(' in SigninBannerComponent')
  }

  onLoginClick(): void {
    console.log(' in onLoginClick')
    this.router.navigate(['/login'], { queryParams: { returnUrl: this.routerSnapshot.url } });
  }
}

