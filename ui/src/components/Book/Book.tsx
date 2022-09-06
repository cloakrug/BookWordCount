import React from 'react'
import SearchBar from '../SearchBar/SearchBar'

export default function Book(props: any) {
    const book = props.book;

    return (
        <>
            <h1>book page </h1>
            <h1>{book.title}</h1>
            <h6>{book.releaseDate}</h6>
            <h6>{book.genre} • {book.majorGenre }</h6>
            <p>{book.description}</p>
            /*<img src={book.imageUrl} alt={ `${ book.title } book cover` } ></img>*/
        </>
    )
}