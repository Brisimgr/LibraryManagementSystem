DELIMITER $$
CREATE PROCEDURE find_books(
	IN search_option VARCHAR(20),
    IN search_crit VARCHAR(50)
)
BEGIN
	IF search_option = 'author' THEN
		SELECT * 
        FROM book_details
        WHERE author LIKE CONCAT('%', search_crit, '%');
    ELSEIF search_option = 'title' THEN
		SELECT * 
        FROM book_details
        WHERE title LIKE CONCAT('%', search_crit, '%');
    ELSEIF search_option = 'genre' THEN
		SELECT * 
        FROM book_details
        WHERE genre LIKE CONCAT('%', search_crit, '%');
    ELSE
		SELECT 'Invalid search option';
    END IF;
END $$
DELIMITER ;