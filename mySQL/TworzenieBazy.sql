CREATE DATABASE libraryDB;

USE libraryDB;

CREATE TABLE users (
	user_id INT PRIMARY KEY AUTO_INCREMENT,
    login VARCHAR(50) UNIQUE NOT NULL,
    mail VARCHAR(50) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL, -- widzialem ze jest funkcja PASSWORD(), jak jej uzyc?
    user_role ENUM('admin', 'user')
);

ALTER TABLE users
ADD CONSTRAINT check_password_complexity
CHECK (
	LENGTH(password_hash) >= 8 AND
    REGEXP_LIKE(password_hash, '[0-9]') AND
    REGEXP_LIKE(password_hash, '[^a-zA-Z0-9]')
);

-- SELECT * FROM users;

CREATE TABLE branches (
	branch_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL,
    address VARCHAR(100) NOT NULL
);

-- SELECT * FROM branches;

CREATE TABLE genres (
	genre_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL
);

-- SELECT * FROM genres;

CREATE TABLE authors (
	author_id INT PRIMARY KEY AUTO_INCREMENT,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL
);

-- SELECT * FROM authors;

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

-- SELECT * FROM books;

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

-- SELECT * FROM borrowed;

