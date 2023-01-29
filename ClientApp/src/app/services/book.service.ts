import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../models/book';
import { AddBookStatsModel } from '../models/bookstatsmodel';
import { SearchResult } from '../models/searchresult';
import { UserBookStatsModel } from '../models/userbookstats';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  private readonly apiUrl: string = 'https://localhost:7041';

  constructor(private http: HttpClient) { }

  public search(str: string, page: number | null): Observable<SearchResult[]> {
    let query = `?str=${encodeURIComponent(str)}`;
    if (page !== null || undefined) {
      query += `&pageNum=${page}`;
    }
    return this.http.get<SearchResult[]>(`${this.apiUrl}/Book/Search` + query);
  }

  public getBookById(id: string): Observable<Book> {
    return this.http.get<Book>(`${this.apiUrl}/Book/${id}`);
  }

  public addStats(model: AddBookStatsModel): Observable<AddBookStatsModel> {
    return this.http.post<AddBookStatsModel>(`${this.apiUrl}/UserBookStat/Add`, model);
  }

  public getAllUserStats(): Observable<UserBookStatsModel[]> {
    return this.http.get<UserBookStatsModel[]>(`${this.apiUrl}/UserBookStat/GetAllBookStatsForCurrentUser`);
  }

}
