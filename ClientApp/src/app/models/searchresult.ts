export type SearchResult = BookResult;

export interface BookResult {
  title: string,
  author: string,
  releaseDate: string,
  id: string,
  genres: GenreResult[],
  shortDescription: string,
  imgUrl?: string,
}

export interface GenreResult {
  id: number,
  text: string
}
