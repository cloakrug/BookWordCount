export type SearchResult = BookResult;

export interface BookResult {
  title: string,
  author: string,
  releaseYear: string,
  id: string,
  genres: GenreResult[],
  shortDescription: string,
  imgUrl?: string,
}

export interface GenreResult {
  id: number,
  text: string
}
