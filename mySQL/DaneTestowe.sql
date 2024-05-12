-- INSERT INTO users (login, mail, password_hash, user_role)
-- VALUES
-- 	("admin", "admin@gmail.com", "admin!80", 'admin'),
--     ("test", "test@gmail.com", "test?901", 'user');
--     
-- SELECT * FROM users;

-- INSERT INTO authors (first_name, last_name)
-- VALUES 
-- 	("Dan", "Simmons"),
--     ("Brandon", "Sanderson");
--     
-- SELECT * FROM authors;

-- INSERT INTO genres (name)
-- VALUES
-- 	("science-fiction"),
--     ("fantasy");
--     
-- SELECT * FROM genres;

-- INSERT INTO branches (name, address)
-- VALUES
-- 	("branch-1", "ul.b1"),
--     ("branch-2", "ul.b2");
--     
-- SELECT * FROM branches;

-- INSERT INTO books (title, author_id, genre_id, pages, publish_year, availability, branch_id)
-- VALUES
-- 	("Hyperion", 1, 1, 518, 1989, 'available', 1),
--     ("The Way of Kings", 2, 2, 1007, 2010, 'not available', 2);
--     
-- SELECT * FROM books;

INSERT INTO borrowed (user_id, book_id, borrow_date, planned_return_date, return_date)
VALUES
	(2, 1, NULL, NULL, NULL);
    
SELECT * FROM borrowed;