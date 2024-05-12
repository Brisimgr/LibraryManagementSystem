CREATE TRIGGER on_user_insert
BEFORE INSERT ON users
FOR EACH ROW
SET NEW.user_role = 'user';