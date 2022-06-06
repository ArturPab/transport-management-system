CREATE TABLE if not EXISTS `carStatus` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `StatusDescription` enum('available','busy','broken') DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB;