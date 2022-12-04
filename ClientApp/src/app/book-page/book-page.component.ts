import { Component, OnInit } from '@angular/core';
import { Book } from '../models/book';
import { BookService } from '../services/book.service';

@Component({
  selector: 'book-page',
  templateUrl: './book-page.component.html',
  styleUrls: ['./book-page.component.css']
})
export class BookPageComponent implements OnInit {

  public book: Book | null = null;

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    if (!this.book) {
      this.bookService.getBookById()
        .subscribe((res: Book) => {

        });
    }
  }

}
