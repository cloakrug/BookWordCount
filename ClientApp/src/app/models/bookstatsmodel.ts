export interface BookStatsModel {
  UserId?: string,
  BookId: string,
  wordCount?: number,
  pageCount?: number,
  difficulty?: number,
  durationInSeconds?: number
}
