import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../models/book';
import { SearchResult } from '../models/searchresult';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private http: HttpClient) { }

  public search(str: string, page: number | null): Observable<SearchResult[]> {

    let query = `?str=${encodeURIComponent(str)}`;
    if (page !== null || undefined) {
      query += `&pageNum=${page}`;
    }

    return this.http.get<SearchResult[]>(`https://localhost:7041/Book/Search` + query);
  }

  public getBookById(id: string): Observable<Book> {
    return this.http.get<Book>(`https://localhost:7041/Book/${id}`);
  }
}
