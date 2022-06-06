CREATE TABLE if not EXISTS `performs` (
  `OrderId` int NOT NULL AUTO_INCREMENT,
  `DriverId` int DEFAULT NULL,
  CONSTRAINT `fk_driver` FOREIGN KEY (`DriverId`) REFERENCES `driver` (`Id`),
  CONSTRAINT `fk_order2` FOREIGN KEY (`OrderId`) REFERENCES `order` (`Id`)
) ENGINE=InnoDB;