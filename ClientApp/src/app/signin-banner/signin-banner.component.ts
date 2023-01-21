import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'signin-banner',
  templateUrl: './signin-banner.component.html',
  styleUrls: ['./signin-banner.component.css']
})
export class SigninBannerComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
    console.log(' in SigninBannerComponent')
  }

  onLoginClick(): void {
    console.log(' in onLoginLcik')
    this.router.navigateByUrl('/login');
  }
}
