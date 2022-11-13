import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
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
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: "browse", component: BrowsePageComponent },
      { path: "about", component: AboutPageComponent },
      { path: "book/:id", component: BookPageComponent },
      { path: "", component: HomePageComponent, pathMatch: 'full' },
      { path: '**', component: NotFoundComponent }
    ])
  ],
  providers: [BookService],
  bootstrap: [AppComponent]
})
export class AppModule { }
