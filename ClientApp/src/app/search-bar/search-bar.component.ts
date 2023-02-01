import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BookService } from '../services/book.service';
import { BookResult, SearchResult } from '../models/searchresult';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {
  @Input() searchText: string = "";

  public results: SearchResult[] = [];

  constructor(
    private bookService: BookService,
    private router: Router
  ) { }

  ngOnInit(): void { }

  onSearchInput(event: Event): void {
    if (this.searchText.length > 0) {
      this.bookService.search(this.searchText, 0).subscribe(res => this.results = res);
    }
  }

  public handleResultClick (resultId: string): void {
    this.router.navigate(['/book', resultId ]);
  }
}
