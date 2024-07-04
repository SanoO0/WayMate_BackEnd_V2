CREATE DATABASE WayMate;
GO
USE WayMate;
GO
-- Creating the 'users' table
CREATE TABLE users (
    id INT IDENTITY PRIMARY KEY NOT NULL,
    userType VARCHAR(10) NOT NULL,
    username VARCHAR(20) NOT NULL,
    password VARCHAR(200) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    birthDate DATE NOT NULL,
    isBanned BIT NOT NULL DEFAULT 0,
    phoneNumber VARCHAR(16) NOT NULL,
    lastName VARCHAR(50),
    firstName VARCHAR(50),
    gender VARCHAR(20),
    city VARCHAR(50) NOT NULL
);
GO
-- Creating the 'trip' table
CREATE TABLE trip (
    id INT IDENTITY PRIMARY KEY NOT NULL,
    idDriver INT NOT NULL REFERENCES users(id),
    smoke BIT NOT NULL,
    price FLOAT NOT NULL,
    luggage BIT NOT NULL,
    petFriendly BIT NOT NULL,
    date DATETIME NOT NULL,
    driverMessage VARCHAR(200),
    airConditioning BIT NOT NULL,
    cityStartingPoint VARCHAR(50) NOT NULL,
    cityDestination VARCHAR(50) NOT NULL,
    plateNumber VARCHAR(50) NOT NULL,
    brand VARCHAR(50) NOT NULL,
    model VARCHAR(50) NOT NULL
);
GO
-- Creating the 'booking' table
CREATE TABLE booking (
    id INT IDENTITY PRIMARY KEY NOT NULL,
    date DATETIME NOT NULL,
    reservedSeats INT NOT NULL,
    idPassenger INT NOT NULL REFERENCES users(id),
    idTrip INT NOT NULL REFERENCES trip(id)
);
GO