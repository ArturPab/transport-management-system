create database if not exists tms;

use tms

CREATE TABLE if not EXISTS `address` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Country` varchar(50) DEFAULT NULL,
  `PostalCode` varchar(6) DEFAULT NULL,
  `City` varchar(50) DEFAULT NULL,
  `Street` varchar(50) DEFAULT NULL,
  `BuildingNumber` varchar(7) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB;

CREATE TABLE if not EXISTS `carStatus` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `StatusDescription` enum('available','busy','broken') DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB;

CREATE TABLE if not EXISTS `car` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Mark` varchar(20) DEFAULT NULL,
  `Model` varchar(20) DEFAULT NULL,
  `Payload` float DEFAULT NULL,
  `ProductionYear` varchar(4) DEFAULT NULL,
  `Vin` varchar(17) DEFAULT NULL,
  `StatusId` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_carstatus` (`StatusId`),
  CONSTRAINT `fk_carstatus` FOREIGN KEY (`StatusId`) REFERENCES `CarStatus` (`Id`)
) ENGINE=InnoDB;


CREATE TABLE if not EXISTS `company` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL,
  `Nip` varchar(10) DEFAULT NULL,
  `Regon` varchar(14) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB;

CREATE TABLE if not EXISTS `driver` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(30) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `Pesel` varchar(11) DEFAULT NULL,
  `Salary` decimal(15,2) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB;

CREATE TABLE if not EXISTS `route` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RouteLength` float DEFAULT NULL,
  `FromAddressId` int DEFAULT NULL,
  `ToAddressId` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_from` FOREIGN KEY (`FromAddressId`) REFERENCES `address` (`Id`),
  CONSTRAINT `fk_to` FOREIGN KEY (`ToAddressId`) REFERENCES `address` (`Id`)
) ENGINE=InnoDB;

CREATE TABLE if not EXISTS `orderStatus` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `StatusDescription` enum('pending','in progress','done') DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB;

CREATE TABLE if not EXISTS `order` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RouteId` int DEFAULT NULL,
  `Created` datetime(6) DEFAULT NULL,
  `NumberOfCourses` int DEFAULT NULL,
  `Price` decimal(15,2) DEFAULT NULL,
  `CompanyId` int DEFAULT NULL,
  `StatusId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  CONSTRAINT `fk_company` FOREIGN KEY (`CompanyId`) REFERENCES `company` (`Id`),
  CONSTRAINT `fk_route` FOREIGN KEY (`RouteId`) REFERENCES `route` (`Id`),
  CONSTRAINT `fk_status` FOREIGN KEY (`StatusId`) REFERENCES `orderStatus` (`Id`)
) ENGINE=InnoDB;

CREATE TABLE if not EXISTS `performs` (
  `OrderId` int NOT NULL AUTO_INCREMENT,
  `DriverId` int DEFAULT NULL,
  CONSTRAINT `fk_driver` FOREIGN KEY (`DriverId`) REFERENCES `driver` (`Id`),
  CONSTRAINT `fk_order2` FOREIGN KEY (`OrderId`) REFERENCES `order` (`Id`)
) ENGINE=InnoDB;

CREATE TABLE if not EXISTS `realizes` (
  `OrderId` int NOT NULL AUTO_INCREMENT,
  `CarId` int DEFAULT NULL,
  CONSTRAINT `fk_car` FOREIGN KEY (`CarId`) REFERENCES `car` (`Id`),
  CONSTRAINT `fk_order` FOREIGN KEY (`OrderId`) REFERENCES `order` (`Id`)
) ENGINE=InnoDB;

delete from carStatus;
insert into carStatus values (1, 'available'), (2, 'busy'), (3, 'broken');

delete from orderStatus;
insert into orderStatus values (1, 'pending'), (2, 'in progress'), (3, 'done');



