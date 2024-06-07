USE libraryDB;

-- UPDATE books
-- SET books.genre_id = 5
-- WHERE books.author_id = 12;

-- SELECT * FROM genres;

-- SELECT * FROM book_details;

-- SELECT * FROM borrowed_details;

-- INSERT INTO users (login, mail, password_hash, user_role)
-- VALUES
-- 	("test", "test@gmail.com", "test", 'admin');
--     
-- SELECT * FROM users;

-- INSERT INTO borrowed (user_id, book_id, borrow_date, planned_return_date, return_date)
-- VALUES
-- 	(2, 2, NULL, NULL, NULL);
--     
-- SELECT * FROM borrowed;

-- UPDATE borrowed
-- SET return_date = "2024-05-20"
-- WHERE borrowed_id = 1;

UPDATE books
SET genre_id = 6
WHERE book_id = 32;

SELECT * FROM books;

-- CALL find_books('author', 'D');


