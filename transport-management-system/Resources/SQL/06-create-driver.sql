CREATE TABLE if not EXISTS `driver` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(30) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `Pesel` varchar(11) DEFAULT NULL,
  `Salary` decimal(15,2) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB;