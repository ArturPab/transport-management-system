CREATE TABLE if not EXISTS `orderStatus` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `StatusDescription` enum('pending','in progress','done') DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB;