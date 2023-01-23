import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { BookPageComponent } from './book-page/book-page.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { SearchBarComponent } from './search-bar/search-bar.component';
import { AboutPageComponent } from './about-page/about-page.component';
import { BrowsePageComponent } from './browse-page/browse-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { NameComponentComponent } from './name-component/name-component.component';
import { BookService } from './services/book.service';
import { WordCountFormComponent } from './word-count-form/word-count-form.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatSliderModule } from '@angular/material/slider';
import { DifficultyColorDirective } from './directives/difficultyColor';
import { AuthModule, LogLevel } from 'angular-auth-oidc-client';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { LoginPageComponent } from './login-page/login-page.component';
import { AuthInterceptor } from './AuthInterceptor';
import { SigninBannerComponent } from './signin-banner/signin-banner.component';
import { SnackbarService } from './services/snackbar.service';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    BookPageComponent,
    NotFoundComponent,
    SearchBarComponent,
    AboutPageComponent,
    BrowsePageComponent,
    HomePageComponent,
    NameComponentComponent,
    WordCountFormComponent,
    DifficultyColorDirective,
    LoginPageComponent,
    SigninBannerComponent
  ],
  imports: [
    AuthModule.forRoot({
      config: { 
        authority: 'https://accounts.google.com',
        redirectUrl: window.location.origin,
        postLogoutRedirectUri: window.location.origin,
        clientId: '2187841631-i1nggnmlq66mepnhi12qnavkpcs91sko.apps.googleusercontent.com',
        scope: 'openid profile email offline_access',
        responseType: 'code',
        silentRenew: true,
        useRefreshToken: true,
        logLevel: LogLevel.Debug
      }
    }),
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatButtonModule,
    MatSliderModule,
    MatSnackBarModule,
    MatInputModule,
    MatCheckboxModule,
    RouterModule.forRoot([
      { path: "browse", component: BrowsePageComponent },
      { path: "about", component: AboutPageComponent },
      { path: "book/:id", component: BookPageComponent },
      { path: "login", component: LoginPageComponent },
      { path: "", component: HomePageComponent, pathMatch: 'full' },  // pathMatch: 'full' as PathMatch  https://stackoverflow.com/questions/73964138/angular-14-pathmatch-type-string-is-not-assignable-to-type-full-prefix
      { path: '**', component: NotFoundComponent }
    ]),
    BrowserAnimationsModule
  ],
  providers: [
    BookService,
    SnackbarService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
