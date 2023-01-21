import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SigninBannerComponent } from './signin-banner.component';

describe('SigninBannerComponent', () => {
  let component: SigninBannerComponent;
  let fixture: ComponentFixture<SigninBannerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SigninBannerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SigninBannerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
