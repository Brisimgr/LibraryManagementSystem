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
