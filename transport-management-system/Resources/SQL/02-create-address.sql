CREATE TABLE if not EXISTS `address` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Country` varchar(50) DEFAULT NULL,
  `PostalCode` varchar(6) DEFAULT NULL,
  `City` varchar(50) DEFAULT NULL,
  `Street` varchar(50) DEFAULT NULL,
  `BuildingNumber` varchar(7) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB;