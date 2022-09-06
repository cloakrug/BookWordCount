import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import Book from '../Book/Book';
import SearchBar from '../SearchBar/SearchBar'

export default function BookPage(props: any) {

    let { bookId } = useParams();

    // TODO: types
    const [book, setBook] = useState(null);

    console.log("loading book...")

    useEffect(() => {
        // GET request using fetch inside useEffect React hook
        fetch(`https://localhost:7041/Book/${bookId}`, {
            headers: {
                'Access-Control-Allow-Origin': '*'
            }
        })
            .then(response => response.json())
            .then(data => setBook(data));

        // empty dependency array means this effect will only run once (like componentDidMount in classes)
    }, []);

    return (
        <>
            <SearchBar />
            <h2> book here: </h2>
            {!isNullOrUndefined(book) && <Book book={book} />}
        </>
    )
}

function isNullOrUndefined(obj: any) {
    return obj === null || obj === undefined;
}