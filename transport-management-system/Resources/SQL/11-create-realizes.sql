CREATE TABLE if not EXISTS `realizes` (
  `OrderId` int NOT NULL AUTO_INCREMENT,
  `CarId` int DEFAULT NULL,
  CONSTRAINT `fk_car` FOREIGN KEY (`CarId`) REFERENCES `car` (`Id`),
  CONSTRAINT `fk_order` FOREIGN KEY (`OrderId`) REFERENCES `order` (`Id`)
) ENGINE=InnoDB;