CREATE ROLE admin_role;
CREATE ROLE user_role;

GRANT ALL PRIVILEGES ON libraryDB.* TO admin_role;
GRANT UPDATE ON libraryDB.borrowed TO user_role;

GRANT admin_role TO 'admin'@'%';

SELECT login FROM users WHERE user_role = 'admin';
SELECT login FROM users WHERE user_role = 'user';
