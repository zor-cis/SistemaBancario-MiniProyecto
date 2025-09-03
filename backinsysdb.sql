create database bakingsys;
use bakingsys;

CREATE TABLE client (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    documentTypeId INT UNIQUE NOT NULL,
    email VARCHAR(150) NOT NULL,
    phone_number BIGINT,
    dateOfBirth DATE NOT NULL,
    password VARCHAR(255) NOT NULL,
    age INT
);

CREATE INDEX client_email ON client(email);