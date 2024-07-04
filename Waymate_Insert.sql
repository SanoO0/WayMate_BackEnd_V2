USE WayMate;
GO

INSERT INTO users (userType, username, password, email, birthDate, isBanned, phoneNumber, lastName, firstName, gender, city)
VALUES
('admin', 'admin1', 'hashed_password_1', 'admin1@example.com', '1980-04-15', 0, '+32470123456', 'Vermeulen', 'Jean', 'Male', 'Bruxelles'),
('driver', 'driver1', 'hashed_password_2', 'driver1@example.com', '1990-09-25', 0, '+32470234567', 'Dupont', 'Luc', 'Male', 'Anvers'),
('user', 'user1', 'hashed_password_3', 'user1@example.com', '1985-11-13', 0, '+32470345678', 'Peeters', 'Marie', 'Female', 'Liège'),
('passenger', 'passenger1', 'hashed_password_4', 'passenger1@example.com', '2000-07-19', 0, '+32470456789', 'Janssens', 'Klaas', 'Male', 'Gand'),
('admin', 'admin2', 'hashed_password_5', 'admin2@example.com', '1975-02-10', 0, '+32470567890', 'Lemmens', 'Sophie', 'Female', 'Namur'),
('driver', 'driver2', 'hashed_password_6', 'driver2@example.com', '1992-05-22', 0, '+32470678901', 'Smets', 'Tom', 'Male', 'Charleroi'),
('user', 'user2', 'hashed_password_7', 'user2@example.com', '1988-08-30', 0, '+32470789012', 'Claes', 'Els', 'Female', 'Louvain'),
('passenger', 'passenger2', 'hashed_password_8', 'passenger2@example.com', '1995-12-05', 0, '+32470890123', 'Goossens', 'Jan', 'Male', 'Mons'),
('admin', 'admin3', 'hashed_password_9', 'admin3@example.com', '1982-03-28', 0, '+32470901234', 'Maes', 'Lea', 'Female', 'Hasselt'),
('driver', 'driver3', 'hashed_password_10', 'driver3@example.com', '1986-06-14', 0, '+32471012345', 'Willems', 'Pieter', 'Male', 'Bruges');
GO

INSERT INTO trip (idDriver, smoke, price, luggage, petFriendly, date, driverMessage, airConditioning, cityStartingPoint, cityDestination, plateNumber, brand, model)
VALUES
(2, 0, 25.5, 1, 0, '2024-07-05 09:00:00', 'Départ à l heure, soyez ponctuel.', 1, 'Anvers', 'Bruxelles', '1-ABC-123', 'Volkswagen', 'Golf'),
(6, 1, 30.0, 1, 1, '2024-07-05 14:00:00', 'Prévoyez une pause déjeuner.', 0, 'Charleroi', 'Liège', '2-DEF-456', 'Ford', 'Fiesta'),
(2, 0, 15.0, 0, 1, '2024-07-06 10:00:00', 'Pas d animaux, svp.', 1, 'Anvers', 'Gand', '1-GHI-789', 'BMW', '320i'),
(6, 1, 40.0, 1, 0, '2024-07-06 08:00:00', 'Fumeur autorisé.', 1, 'Charleroi', 'Namur', '2-JKL-012', 'Audi', 'A4'),
(10, 0, 50.0, 1, 1, '2024-07-07 07:00:00', 'Climatisation disponible.', 1, 'Bruges', 'Bruxelles', '3-MNO-345', 'Mercedes', 'C-Class'),
(2, 0, 20.0, 1, 0, '2024-07-07 12:00:00', 'Voyage tranquille.', 0, 'Anvers', 'Mons', '1-PQR-678', 'Toyota', 'Corolla'),
(6, 1, 35.0, 0, 1, '2024-07-08 15:00:00', 'Animal de compagnie accepté.', 1, 'Charleroi', 'Louvain', '2-STU-901', 'Renault', 'Clio'),
(2, 0, 45.0, 1, 0, '2024-07-08 11:00:00', 'Longue distance.', 1, 'Anvers', 'Hasselt', '1-VWX-234', 'Peugeot', '308'),
(6, 1, 25.0, 1, 1, '2024-07-09 13:00:00', 'Arrivée à temps.', 1, 'Charleroi', 'Mons', '2-YZA-567', 'Citroen', 'C4'),
(10, 0, 55.0, 0, 0, '2024-07-10 06:00:00', 'Voyage express.', 1, 'Bruges', 'Gand', '3-BCD-890', 'Opel', 'Astra');
GO

INSERT INTO booking (date, reservedSeats, idPassenger, idTrip)
VALUES
('2024-07-05 08:00:00', 1, 4, 1),
('2024-07-05 13:00:00', 2, 8, 2),
('2024-07-06 09:30:00', 1, 4, 3),
('2024-07-06 07:00:00', 1, 8, 4),
('2024-07-07 06:30:00', 3, 4, 5),
('2024-07-07 11:00:00', 2, 8, 6),
('2024-07-08 14:30:00', 1, 4, 7),
('2024-07-08 10:00:00', 1, 8, 8),
('2024-07-09 12:30:00', 2, 4, 9),
('2024-07-10 05:30:00', 1, 8, 10);
GO
