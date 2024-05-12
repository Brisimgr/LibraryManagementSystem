-- Widok do wyswietlania szczegolow ksiazek
CREATE VIEW book_details AS
SELECT  b.book_id, b.title, 
		CONCAT(a.first_name, " ", a.last_name) AS author,
        g.name AS genre,
        b.pages, b.publish_year, b.availability,
        br.name AS branch, br.address AS branch_address
FROM books b
INNER JOIN authors a ON b.author_id = a.author_id
INNER JOIN genres g ON b.genre_id = g.genre_id
INNER JOIN branches br ON b.branch_id = br.branch_id;

SELECT * FROM book_details;

-- Widok do wywswietlania szczegolow wypozyczen
CREATE VIEW borrowed_details AS
SELECT  bor.borrowed_id,
		u.login AS user,
		b.title AS book,
        bor.borrow_date, bor.planned_return_date, bor.return_date, bor.charge
FROM borrowed bor
INNER JOIN users u ON bor.user_id = u.user_id
INNER JOIN books b ON bor.book_id = b.book_id;

SELECT * FROM borrowed_details;