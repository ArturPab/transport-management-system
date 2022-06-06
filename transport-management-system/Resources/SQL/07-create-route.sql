CREATE TABLE if not EXISTS `route` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RouteLength` float DEFAULT NULL,
  `FromAddressId` int DEFAULT NULL,
  `ToAddressId` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  CONSTRAINT `fk_from` FOREIGN KEY (`FromAddressId`) REFERENCES `address` (`Id`),
  CONSTRAINT `fk_to` FOREIGN KEY (`ToAddressId`) REFERENCES `address` (`Id`)
) ENGINE=InnoDB;