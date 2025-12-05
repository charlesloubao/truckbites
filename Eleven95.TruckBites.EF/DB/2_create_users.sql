-- 1. Create the Entity Framework user for migrations
-- Replace 'PlaceYourStrongPasswordHere' with a real, complex password.
CREATE USER 'truckbites_ef'@'%' IDENTIFIED BY 'PlaceYourStrongPasswordHere';

-- 2. Grant its permission permissions - EF user can do anything in the db
GRANT ALL PRIVILEGES ON truckbites.* TO 'truckbites_ef';
FLUSH PRIVILEGES;

-- 3. Apply the privileges
FLUSH PRIVILEGES;

-- 4. Create the app user
-- Replace 'PlaceYourStrongPasswordHere' with a real, complex password.
CREATE USER 'truckbites_app'@'%' IDENTIFIED BY 'PlaceYourStrongPasswordHere';

-- 5. Grant ONLY CRUD permissions
-- SELECT = Read
-- INSERT = Create
-- UPDATE = Update
-- DELETE = Delete
-- EXECUTE = Execute Stored Procedures
GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE ON truckbites.* TO 'truckbites_app'@'%';

-- 6. Apply the privileges
FLUSH PRIVILEGES;