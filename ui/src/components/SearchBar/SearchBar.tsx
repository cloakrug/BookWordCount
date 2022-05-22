import React, { useState } from 'react';
import "./SearchBar.css";


const SearchBar = () => {
    const [text, setText] = useState(0);

    const onSearchChange = (event: any) => setText(event.target.value);

    return (
        <input
            type="search"
            value={text}
            onChange={onSearchChange}
            placeholder="Search..."
            className="search-bar"
        />
    );
}

export default SearchBar;