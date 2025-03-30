import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Book } from '../models/book';
import { AuthService } from '../services/auth.service';
import { BookService } from '../services/book.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BookUpdateModel } from '../models/book-update-model';
import { SnackbarService } from '../services/snackbar.service';
import { BehaviorSubject, Subscription, combineLatest } from 'rxjs';

@Component({
  selector: 'book-page',
  templateUrl: './book-page.component.html',
  styleUrls: ['./book-page.component.scss']
})
export class BookPageComponent implements OnInit {

  public book: Book | null = null;
  public editBookMode: boolean = false;
  public errorGettingBook: boolean = false;
  public displayLoginBanner: boolean = false;

  public editBookForm: FormGroup;
  public isAdmin$: BehaviorSubject<boolean>;

  private latestDisplayBannerSubscription: Subscription | undefined;

  constructor(
    private bookService: BookService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private snackbarService: SnackbarService,
    private fb: FormBuilder,
    private http: HttpClient
  ) {
    this.editBookForm = this.fb.group({
      title: [''],
      description: ['']
    });
    this.isAdmin$ = this.authService.isAdmin$;
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const bookId: string = params['id'];

      if (!bookId) {
        this.handleInvalidUrl();
        return;
      }

      this.bookService.getBookById(bookId)
        .subscribe(
          (res: Book) => {
            this.book = res;
          },
          (err: HttpErrorResponse) => this.handleGetBookError(err)
        );
    });

    this.authService.isLoggedIn().subscribe(isLoggedIn => {
      console.log('in BookPageComponent, got isLoggedIn: ' + isLoggedIn);
      this.displayLoginBanner = !isLoggedIn;
    });

    // if logged in or in demo mode, dont show the banner
    this.latestDisplayBannerSubscription = combineLatest(this.authService.isLoggedIn(), this.authService.demoMode$).subscribe(([isLoggedIn, demoMode]) => {
      this.displayLoginBanner = !isLoggedIn && !demoMode;
    });
  }

  onEditClick() {
    this.editBookForm.get("title")?.setValue(this.book?.title);
    this.editBookForm.get("description")?.setValue(this.book?.description);
    this.editBookMode = true;
  }

  onEditBookSubmit() {
    if (this.book === null) return;

    const updateModel: BookUpdateModel = {
      id: this.book.id,
      title: this.editBookForm.get("title")?.value,
      description: this.editBookForm.get("description")?.value
    };

    this.bookService.updateBookById(updateModel).subscribe(res => {
      this.editBookMode = false;
      this.snackbarService.openSnackBar("Description successfully saved!");
    });
  }

  onExitEditMode() {
    this.editBookMode = false;
  }

  handleGetBookError(err: HttpErrorResponse): void {
    this.errorGettingBook = true;
  }

  handleInvalidUrl(): void {
    throw new Error('Method not implemented.');
  }

  ngOnDestroy() {
    if (this.latestDisplayBannerSubscription) {
      this.latestDisplayBannerSubscription.unsubscribe();
    }
  }
}
