CREATE TABLE if not EXISTS `order` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL,
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