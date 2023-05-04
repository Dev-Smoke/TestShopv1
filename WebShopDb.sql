--create database TestShopv1

--use TestShopv1
--go  

CREATE TABLE Customer (
    Id int primary key not null identity(1,1),
    EmailAddress nvarChar(max) not null,
    Salutation nvarChar(10) not null,
    Firstname nvarChar(25) not null,
	Lastname nvarChar(25) not null,
	Street nvarChar(50) not null,
	ZipCode nvarChar(4) not null,
	City nvarChar(25) not null,
	PasswordHash nvarChar(max),
	PasswordSalt nvarChar(max)
);

CREATE TABLE [Order] (
    Id int primary key not null identity(1,1),
	CustomerId int REFERENCES Customer (Id) not null,
    TotalPriceBrutto money,
    OrderedOn date,
	PaidOn date,
	RecipientSalutation nvarChar(10),
    RecipientFirstname nvarChar(25),
	RecipientLastname nvarChar(25),
	RecipientStreet nvarChar(50),
	RecipientZipCode nvarChar(4),
	RecipientCity nvarChar(25)
);

CREATE TABLE Manufacturer (
	Id int primary key not null identity(1,1),
	[Name] nvarChar(25) not null
);

CREATE TABLE Category (
	Id int primary key not null identity(1,1),
	[Name] nvarChar(25) not null,
	TaxRate decimal 
);

CREATE TABLE Product (
    Id int primary key not null identity(1,1),
	[Name] nvarChar(50) not null,  
    UnitPriceNetto money not null,
	ImagePath nvarchar(max),
	[Description] nvarchar(max),
	CategoryId int REFERENCES Category (Id) not null,
	ManufacturerId int REFERENCES Manufacturer (Id) not null
);

--ALTER TABLE Product 
--ALTER COLUMN [Name] nvarchar(100) 
--drop database TestShopv1


CREATE TABLE OrderLine (
    Id int primary key identity(1,1) not null,
	OrderId int REFERENCES [Order] (Id) not null,
	ProductId int REFERENCES Product (Id) not null,    
    Quantity int not null,
	TotalPriceBrutto money,
	TaxRate decimal 
);

ALTER TABLE OrderLine
ALTER COLUMN TaxRate NUMERIC(5,2);

ALTER TABLE Category
ALTER COLUMN TaxRate NUMERIC(5,2);



-- Kunden
INSERT INTO Customer (Id, EmailAddress, Salutation, Firstname, Lastname, Street, ZipCode, City, PasswordHash, PasswordSalt)
VALUES (1, 'max.mustermann@example.com', 'Herr', 'Max', 'Mustermann', 'Musterstraße 1', '1010', 'Wien', 'abc123', 'xyz456'),
       (2, 'jane.doe@example.com', 'Frau', 'Jane', 'Doe', 'Beispielweg 2', '1020', 'Wien', 'def456', 'uvw789'),
       (3, 'peter.pan@example.com', 'Herr', 'Peter', 'Pan', 'Testgasse 3', '1030', 'Wien', 'ghi789', 'rst012'),
       (4, 'anna.mueller@example.com', 'Frau', 'Anna', 'Müller', 'Musterweg 4', '1040', 'Wien', 'jkl234', 'pqr345');

-- Hersteller
INSERT INTO Manufacturer ( Name)
VALUES ( 'Microsoft'),
       ( 'Apple'),
       ( 'Samsung'),
       ( 'Lenovo'),
       ( 'Dell');

-- Kategorien
INSERT INTO Category ( Name, TaxRate)
VALUES ( 'Laptops', 0.2),
       ( 'Smartphones', 0.2),
       ( 'Tablets', 0.2),
       ( 'Zubehör', 0.2),
       ( 'Software', 0.2);

-- Produkte
INSERT INTO Product ( Name, UnitPriceNetto, ImagePath, Description, CategoryId, ManufacturerId)
VALUES ( 'Microsoft Surface Laptop 4', 1199.00, '', 'Leistungsstarker Laptop mit Touchscreen', 1, 1),
       ( 'Apple iPhone 13', 799.00, '', 'Neues iPhone mit 5G-Unterstützung', 2, 2),
       ( 'Samsung Galaxy Tab S7+', 699.00, '', 'Hochwertiges Tablet mit 120 Hz Display', 3, 3),
       ( 'Lenovo IdeaPad Flex 5', 699.00, '', 'Flexibler Laptop mit Touchscreen', 1, 4),
       ( 'Dell XPS 13', 1399.00, '', 'Leistungsstarker Laptop mit InfinityEdge-Display', 1, 5),
       ( 'Apple MacBook Air M1', 1149.00, '', 'Dünnes und leichtes Notebook mit Apple Silicon', 1, 2),
       ( 'Microsoft Surface Pro 8', 1149.00, '', 'Leistungsstarkes Tablet mit Kickstand und Type Cover', 3, 1),
       ( 'Samsung Galaxy S21 Ultra', 1099.00, '', 'High-End-Smartphone mit starkem Prozessor', 2, 3),
	   ( 'Gaming-Maus', 59.99, '', 'Eine hochwertige Gaming-Maus mit vielen Funktionen.', 1, 3),
	   ( 'Gaming-Tastatur', 79.99, '', 'Eine hochwertige Gaming-Tastatur mit vielen Funktionen.', 1, 3);