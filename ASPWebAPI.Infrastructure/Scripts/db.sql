CREATE DATABASE PETCENTER;
USE PETCENTER;

CREATE SCHEMA roles;

CREATE SCHEMA adoption;

CREATE SCHEMA auth;

CREATE TABLE roles.Volunteer (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Role VARCHAR(50),
    StartDate DATETIME NOT NULL,
    Email VARCHAR(100)
);

CREATE TABLE auth.[User] (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Email VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role VARCHAR(50) NOT NULL
);

CREATE TABLE roles.Adopter (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Phone VARCHAR(50),   
);

CREATE TABLE adoption.Pet (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Species VARCHAR(50) NOT NULL,
    Age INT NOT NULL,
    IsAdopted BIT NOT NULL,
    PhotoUrl VARCHAR(255),
    Description TEXT,
    VolunteerId INT,
    CONSTRAINT FK_Pet_Volunteer FOREIGN KEY (VolunteerId) REFERENCES roles.Volunteer(Id)
);

CREATE TABLE adoption.AdoptionRequest (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PetId INT NOT NULL,
    AdopterId INT NOT NULL,
    RequestDate DATETIME NOT NULL,
	AdoptionDate DATETIME,
    Status VARCHAR(50) NOT NULL,
    CONSTRAINT FK_AdoptionRequest_Pet FOREIGN KEY (PetId) REFERENCES adoption.Pet(Id),
    CONSTRAINT FK_AdoptionRequest_Adopter FOREIGN KEY (AdopterId) REFERENCES roles.Adopter(Id)
);

-- foreign keys
CREATE INDEX IX_AdoptionRequest_AdopterId ON adoption.AdoptionRequest(AdopterId);
CREATE INDEX IX_AdoptionRequest_PetId ON adoption.AdoptionRequest(PetId);
CREATE INDEX IX_Pet_VolunteerId ON adoption.Pet(VolunteerId);

-- frequently filtered columns
CREATE INDEX IX_Pet_IsAdopted ON adoption.Pet(isAdopted);
CREATE INDEX IX_AdoptionRequest_Status ON adoption.AdoptionRequest(Status);
