import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Book } from '../models/book';
import { AddBookStatsModel } from '../models/bookstatsmodel';
import { SearchResult } from '../models/searchresult';
import { UserBookStatsModel } from '../models/userbookstats';
import { BookUpdateModel } from '../models/book-update-model';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  
  private readonly apiUrl: string = ''; // It should all go to the same URL as the frontend
  private readonly userStatsUrl: string = `${this.apiUrl}/UserBookStat`;

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

  public updateBookById(model: BookUpdateModel): Observable<Book> {
    return this.http.post<Book>(`${this.apiUrl}/Book/${model.id}`, model);
  }

  public addStats(model: AddBookStatsModel): Observable<AddBookStatsModel> {
    return this.http.post<AddBookStatsModel>(`${this.userStatsUrl}/Add`, model);
  }

  public getAllUserStats(): Observable<UserBookStatsModel[]> {
    return this.http.get<UserBookStatsModel[]>(`${this.userStatsUrl}/GetAllBookStatsForCurrentUser`);
  }

  public deleteStats(bookId: string): Observable<UserBookStatsModel> {
    return this.http.delete<UserBookStatsModel>(`${this.userStatsUrl}/DeleteStatsForBook?bookId=${bookId}`);
  }

}
