INSERT INTO users (login, mail, password_hash, user_role)
VALUES
	("admin", "admin@gmail.com", "admin!80", 'admin'),
    ("test", "test@gmail.com", "test?901", 'user');
USE libraryDB;
SELECT * FROM users;

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
('Brandon', 'Sanderson');



INSERT INTO authors (first_name, last_name) VALUES
('Dan', 'Simmons');


SELECT * FROM authors;

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
  
SELECT * FROM genres;

INSERT INTO branches (name, address) VALUES
('Main Library', '123 Main Street, Cityville'),
('Downtown Branch', '456 Center Avenue, Downtown'),
('Westside Branch', '789 Elm Street, Westside'),
('East End Branch', '101 Oak Avenue, East End'),
('Riverside Branch', '202 Maple Drive, Riverside');

SELECT * FROM branches;

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
('Great Expectations', 10, 1, 544, 1861, 'available', 5);   

INSERT INTO books (title, author_id, genre_id, pages, publish_year, availability, branch_id) VALUES
('Mistborn: The Final Empire', 11, 6, 541, 2006, 'available', 1),
('The Way of Kings', 11, 6, 1007, 2010, 'available', 2),
('Words of Radiance', 11, 6, 1001, 2014, 'available', 3),
('Oathbringer', 11, 6, 1248, 2017, 'available', 4),
('The Alloy of Law', 11, 6, 541, 2011, 'available', 5),
('Elantris', 11, 6, 615, 2005, 'available', 1),
('Warbreaker', 11, 6, 592, 2009, 'available', 2),
('The Well of Ascension', 11, 6, 541, 2007, 'available', 3),
('The Hero of Ages', 11, 6, 541, 2008, 'available', 4),
('The Bands of Mourning', 11, 6, 541, 2016, 'available', 5);

INSERT INTO books (title, author_id, genre_id, pages, publish_year, availability, branch_id) VALUES
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

SELECT * FROM books;

INSERT INTO borrowed (user_id, book_id, borrow_date, planned_return_date, return_date)
VALUES
	(2, 1, NULL, NULL, NULL);
    
SELECT * FROM borrowed;