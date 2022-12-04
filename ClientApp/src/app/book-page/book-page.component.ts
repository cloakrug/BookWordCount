import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Book } from '../models/book';
import { BookService } from '../services/book.service';

@Component({
  selector: 'book-page',
  templateUrl: './book-page.component.html',
  styleUrls: ['./book-page.component.css']
})
export class BookPageComponent implements OnInit {

  public book: Book | null = null;
  public errorGettingBook: boolean = false;

  constructor(
    private bookService: BookService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const bookId: string = params['id'];

      if (!bookId) {
        this.handleInvalidUrl();
        return;
      }

      this.bookService.getBookById(bookId)
        .subscribe(
          (res: Book) => this.book = res,
          (err: HttpErrorResponse) => this.handleGetBookError(err)
        );
    });
  }

  handleGetBookError(err: HttpErrorResponse): void {
    this.errorGettingBook = true;
  }

  handleInvalidUrl(): void {
    throw new Error('Method not implemented.');
  }
}
