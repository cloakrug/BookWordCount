import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import './index.css';
import App from './App';
import AboutPage from './components/AboutPage/AboutPage';
import BookPage from './components/BookPage/BookPage';
import NotFoundPage from './components/NotFoundPage/NotFoundPage';
import BrowsePage from './components/Browse/BrowsePage';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
    <React.StrictMode>
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<App />}>
                    <Route path="" element={<BrowsePage />} />
                    <Route path=":bookId" element={<BookPage />} />
                    <Route path="about" element={<AboutPage />} />
                    <Route path="book" element={<BookPage />} />
                    <Route path="*" element={<NotFoundPage />} /> 
                </Route>
            </Routes>
        </BrowserRouter>
    </React.StrictMode>
);
