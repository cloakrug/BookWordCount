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
    //if (this.searchText.length > 0) {
    //  this.bookService.search(this.searchText, 0).subscribe(res => this.results = res);
    //}

    this.results = [
      {
        title: "The Road",
        id: "2134",
        shortDescription: "depressing...",
        imgUrl: "https://wepiruwenrg",
        releaseYear: "1984",
        author: "Cormac McCarthy",
        genres: [
          {
            id: 1,
            text: "Action"
          }
        ]
      },
      {
        title: "Kite runner",
        id: "5678",
        shortDescription: "just a boy and his kite",
        imgUrl: "https://wepiruwenrg",
        releaseYear: "2001",
        author: "Hosseini",
        genres: [
          {
            id: 1,
            text: "Action"
          },
          {
            id: 2,
            text: "Historical Fiction"
          }
        ]
      },
      {
        title: "1984",
        id: "1579",
        shortDescription: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum".substr(0, 200),
        imgUrl: "https://wepiruwenrg",
        releaseYear: "1950",
        author: "George Orwell",
        genres: [
          {
            id: 4,
            text: "Science Fiction"
          }
        ]
      }
    ]
  }

  public handleResultClick (resultId: string): void {
    this.router.navigate(['/book', resultId ]);
  }
}
