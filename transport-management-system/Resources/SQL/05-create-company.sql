CREATE TABLE if not EXISTS `company` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL,
  `Nip` varchar(10) DEFAULT NULL,
  `Regon` varchar(14) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB;