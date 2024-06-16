CREATE DATABASE libraryDB;

USE libraryDB;

CREATE TABLE users (
	user_id INT PRIMARY KEY AUTO_INCREMENT,
    login VARCHAR(50) UNIQUE NOT NULL,
    mail VARCHAR(50) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL, 
    user_role ENUM('admin', 'user')
);

ALTER TABLE users
ADD CONSTRAINT check_password_complexity
CHECK (
	LENGTH(password_hash) >= 8 AND
    REGEXP_LIKE(password_hash, '[0-9]') AND
    REGEXP_LIKE(password_hash, '[^a-zA-Z0-9]')
);

CREATE TABLE branches (
	branch_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL,
    address VARCHAR(100) NOT NULL
);

CREATE TABLE genres (
	genre_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL
);

CREATE TABLE authors (
	author_id INT PRIMARY KEY AUTO_INCREMENT,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL
);

CREATE TABLE books (
	book_id INT PRIMARY KEY AUTO_INCREMENT,
    title VARCHAR(50) NOT NULL,
    author_id INT,
    FOREIGN KEY(author_id) REFERENCES authors(author_id),
    genre_id INT,
    FOREIGN KEY(genre_id) REFERENCES genres(genre_id),
    pages INT NOT NULL,
    publish_year INT,
    availability ENUM('available', 'not available'),
    branch_id INT,
    FOREIGN KEY(branch_id) REFERENCES branches(branch_id)
);

CREATE TABLE borrowed (
	borrowed_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    FOREIGN KEY(user_id) REFERENCES users(user_id),
    book_id INT,
    FOREIGN KEY(book_id) REFERENCES books(book_id),
    borrow_date DATE,
    planned_return_date DATE,
    return_date DATE,
    charge INT NOT NULL DEFAULT 0
);

INSERT INTO users (login, mail, password_hash, user_role)
VALUES
	("admin", "admin@gmail.com", "admin!80", 'admin'),
    ("test", "test@gmail.com", "test?901", 'user');
    
-- Funkcja, ktora sprawdza dostepnosc danej pozycji
DELIMITER $$
CREATE FUNCTION is_book_available(id INT) RETURNS BOOLEAN
DETERMINISTIC
READS SQL DATA
BEGIN
	DECLARE available_status BOOLEAN;
    
    SELECT EXISTS (
		SELECT 1
        FROM books
        WHERE book_id = id AND availability = 'available'
    ) INTO available_status;
    
    RETURN available_status;
END $$
DELIMITER ;

-- Trigger, ktory sprawdza czy zostala ustawiona data wypozyczenia ksiazki podczas jej dodawania,
-- jezeli nie to ustawia date wypozyczenia na dzisiejsza.
-- Dodatkowo sprawdza czy zostala ustawiona planowana data zwrotu i jezeli nie to ustawia ja
-- na 30 dni po wypozyczeniu
-- Zmienia takze status wypozyczonej ksiazki na niedostepny
DELIMITER $$
CREATE TRIGGER on_borrowed_insert
BEFORE INSERT ON borrowed
FOR EACH ROW
BEGIN
	IF (NOT is_book_available(NEW.book_id)) THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Book is not available for borrowing';
    END IF;

	IF (NEW.borrow_date IS NULL) THEN
		SET NEW.borrow_date = CURDATE();
	END IF;
    
    IF (NEW.planned_return_date IS NULL) THEN
		SET NEW.planned_return_date = DATE_ADD(NEW.borrow_date, INTERVAL 30 DAY);
	END IF;
    
    UPDATE books
    SET availability = 'not available' 
    WHERE book_id = NEW.book_id;
END $$
DELIMITER ;

-- Trigger, ktory sprawdza czy ksiazka zostala zwrocona, jezeli tak to aktualizuje jej status 
DELIMITER $$
CREATE TRIGGER on_borrowed_update
AFTER UPDATE ON borrowed
FOR EACH ROW
BEGIN
	IF (NEW.return_date IS NOT NULL) THEN
		UPDATE books
        SET availability = 'available'
        WHERE book_id = NEW.book_id;
    END IF;
END $$
DELIMITER ;

SET GLOBAL event_scheduler = ON;

-- Wydarzenie, ktore codziennie sprawdza czy nie zostala przekroczona planowana data zwrotu
-- jezeli tak, nalicza oplate
DELIMITER $$
CREATE EVENT check_overdue_books
ON SCHEDULE EVERY 1 DAY
DO
BEGIN
	UPDATE borrowed
    SET charge = charge + 10
    WHERE planned_return_date < CURDATE() AND return_date IS NULL;
END $$
DELIMITER ;

CREATE TRIGGER on_user_insert
BEFORE INSERT ON users
FOR EACH ROW
SET NEW.user_role = 'user';

DELIMITER $$
CREATE PROCEDURE find_books(
	IN search_option VARCHAR(20),
    IN search_crit VARCHAR(50)
)
BEGIN
	IF (search_option = 'author') THEN
		SELECT * 
        FROM book_details
        WHERE author LIKE CONCAT('%', search_crit, '%');
    ELSEIF (search_option = 'title') THEN
		SELECT * 
        FROM book_details
        WHERE title LIKE CONCAT('%', search_crit, '%');
    ELSEIF (search_option = 'genre') THEN
		SELECT * 
        FROM book_details
        WHERE genre LIKE CONCAT('%', search_crit, '%');
    ELSE
		SELECT 'Invalid search option';
    END IF;
END $$
DELIMITER ;

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

-- Widok do wywswietlania szczegolow wypozyczen
CREATE VIEW borrowed_details AS
SELECT  bor.borrowed_id,
		u.login AS user,
		b.title AS book,
        bor.borrow_date, bor.planned_return_date, bor.return_date, bor.charge
FROM borrowed bor
INNER JOIN users u ON bor.user_id = u.user_id
INNER JOIN books b ON bor.book_id = b.book_id;

INSERT INTO authors (first_name, last_name) VALUES
('J.K.', 'Rowling'),
('George', 'Orwell'),
('Harper', 'Lee'),
('J.R.R.', 'Tolkien'),
('Jane', 'Austen'),
('Ernest', 'Hemingway'),
('F. Scott', 'Fitzgerald'),
('Leo', 'Tolstoy'),
('Mark', 'Twain'),
('Charles', 'Dickens'),
('Brandon', 'Sanderson'),
('Dan', 'Simmons');

INSERT INTO genres (name) VALUES
('Fiction'),
('Mystery'),
('Thriller'),
('Romance'),
('Science Fiction'),
('Fantasy'),
('Horror'),
('Biography'),
('History'),
('Self-Help'),
('Business'),
('Travel'),
('Poetry'),
('Drama'),
('Cookbooks');

INSERT INTO branches (name, address) VALUES
('Main Library', '123 Main Street, Cityville'),
('Downtown Branch', '456 Center Avenue, Downtown'),
('Westside Branch', '789 Elm Street, Westside'),
('East End Branch', '101 Oak Avenue, East End'),
('Riverside Branch', '202 Maple Drive, Riverside');

INSERT INTO books (title, author_id, genre_id, pages, publish_year, availability, branch_id) VALUES
('Harry Potter and the Philosopher\'s Stone', 1, 6, 320, 1997, 'available', 1),
('1984', 2, 1, 328, 1949, 'available', 2),
('To Kill a Mockingbird', 3, 1, 281, 1960, 'available', 3),
('The Great Gatsby', 7, 1, 180, 1925, 'available', 4),
('The Catcher in the Rye', 6, 1, 277, 1951, 'available', 5),
('Pride and Prejudice', 5, 4, 432, 1813, 'available', 1),
('The Lord of the Rings', 4, 6, 1178, 1954, 'available', 2),
('The Adventures of Huckleberry Finn', 9, 1, 366, 1884, 'available', 3),
('War and Peace', 8, 9, 1392, 1869, 'available', 4),
('Great Expectations', 10, 1, 544, 1861, 'available', 5),
('Mistborn: The Final Empire', 11, 6, 541, 2006, 'available', 1),
('The Way of Kings', 11, 6, 1007, 2010, 'available', 2),
('Words of Radiance', 11, 6, 1001, 2014, 'available', 3),
('Oathbringer', 11, 6, 1248, 2017, 'available', 4),
('The Alloy of Law', 11, 6, 541, 2011, 'available', 5),
('Elantris', 11, 6, 615, 2005, 'available', 1),
('Warbreaker', 11, 6, 592, 2009, 'available', 2),
('The Well of Ascension', 11, 6, 541, 2007, 'available', 3),
('The Hero of Ages', 11, 6, 541, 2008, 'available', 4),
('The Bands of Mourning', 11, 6, 541, 2016, 'available', 5),
('Hyperion', 12, 6, 482, 1989, 'available', 1),
('The Fall of Hyperion', 12, 6, 517, 1990, 'available', 2),
('Endymion', 12, 6, 579, 1996, 'available', 3),
('The Rise of Endymion', 12, 6, 709, 1997, 'available', 4),
('Ilium', 12, 6, 672, 2003, 'available', 5),
('Olympos', 12, 6, 688, 2005, 'available', 1),
('Carrion Comfort', 12, 1, 800, 1989, 'available', 2),
('The Terror', 12, 7, 960, 2007, 'available', 3),
('Drood', 12, 14, 777, 2009, 'available', 4),
('Black Hills', 12, 7, 560, 2010, 'available', 5);

INSERT INTO borrowed (user_id, book_id, borrow_date, planned_return_date, return_date)
VALUES
(2, 1, NULL, NULL, NULL);


