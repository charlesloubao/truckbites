USE truckbites;

-- Insert fake food trucks
INSERT INTO FoodTrucks (DisplayName, Description, CreatedAt, UpdatedAt)
VALUES ('Taco Fiesta', 'Authentic Mexican street tacos and burritos with a modern twist', NOW(), NOW()),
       ('Burger Boss', 'Gourmet burgers made with locally sourced ingredients', NOW(), NOW()),
       ('Pizza Paradise', 'Wood-fired artisan pizzas on the go', NOW(), NOW()),
       ('Sushi on Wheels', 'Fresh sushi and Japanese fusion cuisine', NOW(), NOW()),
       ('BBQ Kings', 'Slow-smoked meats and classic BBQ favorites', NOW(), NOW()),
       ('Veggie Vibes', 'Plant-based comfort food that everyone will love', NOW(), NOW()),
       ('Waffle Wonderland', 'Sweet and savory waffles for any time of day', NOW(), NOW()),
       ('Thai Street Kitchen', 'Bold flavors from the streets of Bangkok', NOW(), NOW());

-- Insert menu items for Taco Fiesta (FoodTruckId = 1)
INSERT INTO FoodTruckMenuItems (FoodTruckId, Name, Price, IsSoldOut, CreatedAt, UpdatedAt)
VALUES (1, 'Carne Asada Taco', 4.50, 0, NOW(), NOW()),
       (1, 'Al Pastor Taco', 4.50, 0, NOW(), NOW()),
       (1, 'Fish Taco', 5.00, 0, NOW(), NOW()),
       (1, 'California Burrito', 9.99, 0, NOW(), NOW()),
       (1, 'Chips and Guacamole', 6.50, 0, NOW(), NOW()),
       (1, 'Churros', 4.00, 0, NOW(), NOW());

-- Insert menu items for Burger Boss (FoodTruckId = 2)
INSERT INTO FoodTruckMenuItems (FoodTruckId, Name, Price, IsSoldOut, CreatedAt, UpdatedAt)
VALUES (2, 'Classic Cheeseburger', 10.99, 0, NOW(), NOW()),
       (2, 'Bacon BBQ Burger', 12.99, 0, NOW(), NOW()),
       (2, 'Mushroom Swiss Burger', 11.99, 0, NOW(), NOW()),
       (2, 'Veggie Burger', 10.50, 0, NOW(), NOW()),
       (2, 'Truffle Fries', 6.99, 0, NOW(), NOW()),
       (2, 'Onion Rings', 5.99, 0, NOW(), NOW()),
       (2, 'Milkshake', 5.50, 1, NOW(), NOW());

-- Insert menu items for Pizza Paradise (FoodTruckId = 3)
INSERT INTO FoodTruckMenuItems (FoodTruckId, Name, Price, IsSoldOut, CreatedAt, UpdatedAt)
VALUES (3, 'Margherita Pizza', 11.00, 0, NOW(), NOW()),
       (3, 'Pepperoni Pizza', 12.50, 0, NOW(), NOW()),
       (3, 'BBQ Chicken Pizza', 13.50, 0, NOW(), NOW()),
       (3, 'Vegetarian Pizza', 12.00, 0, NOW(), NOW()),
       (3, 'Garlic Knots', 4.50, 0, NOW(), NOW()),
       (3, 'Caesar Salad', 7.00, 0, NOW(), NOW());

-- Insert menu items for Sushi on Wheels (FoodTruckId = 4)
INSERT INTO FoodTruckMenuItems (FoodTruckId, Name, Price, IsSoldOut, CreatedAt, UpdatedAt)
VALUES (4, 'California Roll', 8.50, 0, NOW(), NOW()),
       (4, 'Spicy Tuna Roll', 9.50, 0, NOW(), NOW()),
       (4, 'Dragon Roll', 12.00, 0, NOW(), NOW()),
       (4, 'Salmon Nigiri (2pc)', 6.00, 0, NOW(), NOW()),
       (4, 'Edamame', 4.50, 0, NOW(), NOW()),
       (4, 'Miso Soup', 3.50, 0, NOW(), NOW());

-- Insert menu items for BBQ Kings (FoodTruckId = 5)
INSERT INTO FoodTruckMenuItems (FoodTruckId, Name, Price, IsSoldOut, CreatedAt, UpdatedAt)
VALUES (5, 'Pulled Pork Sandwich', 9.99, 0, NOW(), NOW()),
       (5, 'Beef Brisket Plate', 15.99, 0, NOW(), NOW()),
       (5, 'Baby Back Ribs (Half Rack)', 16.50, 0, NOW(), NOW()),
       (5, 'Smoked Chicken Quarter', 10.50, 0, NOW(), NOW()),
       (5, 'Mac and Cheese', 5.50, 0, NOW(), NOW()),
       (5, 'Coleslaw', 3.50, 0, NOW(), NOW()),
       (5, 'Cornbread', 2.50, 0, NOW(), NOW());

-- Insert menu items for Veggie Vibes (FoodTruckId = 6)
INSERT INTO FoodTruckMenuItems (FoodTruckId, Name, Price, IsSoldOut, CreatedAt, UpdatedAt)
VALUES (6, 'Beyond Burger', 11.50, 0, NOW(), NOW()),
       (6, 'Falafel Wrap', 9.00, 0, NOW(), NOW()),
       (6, 'Buddha Bowl', 10.50, 0, NOW(), NOW()),
       (6, 'Cauliflower Wings', 8.50, 0, NOW(), NOW()),
       (6, 'Sweet Potato Fries', 5.00, 0, NOW(), NOW()),
       (6, 'Green Smoothie', 6.50, 0, NOW(), NOW());

-- Insert menu items for Waffle Wonderland (FoodTruckId = 7)
INSERT INTO FoodTruckMenuItems (FoodTruckId, Name, Price, IsSoldOut, CreatedAt, UpdatedAt)
VALUES (7, 'Classic Belgian Waffle', 7.50, 0, NOW(), NOW()),
       (7, 'Chicken and Waffles', 12.99, 0, NOW(), NOW()),
       (7, 'Strawberry Nutella Waffle', 9.50, 0, NOW(), NOW()),
       (7, 'Bacon Cheddar Waffle', 10.50, 0, NOW(), NOW()),
       (7, 'Waffle Sundae', 8.50, 0, NOW(), NOW()),
       (7, 'Fresh Fruit Bowl', 6.00, 0, NOW(), NOW());

-- Insert menu items for Thai Street Kitchen (FoodTruckId = 8)
INSERT INTO FoodTruckMenuItems (FoodTruckId, Name, Price, IsSoldOut, CreatedAt, UpdatedAt)
VALUES (8, 'Pad Thai', 11.50, 0, NOW(), NOW()),
       (8, 'Green Curry', 12.00, 0, NOW(), NOW()),
       (8, 'Tom Yum Soup', 8.50, 0, NOW(), NOW()),
       (8, 'Spring Rolls (4pc)', 6.50, 0, NOW(), NOW()),
       (8, 'Mango Sticky Rice', 7.00, 0, NOW(), NOW()),
       (8, 'Thai Iced Tea', 4.50, 0, NOW(), NOW());