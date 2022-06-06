
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
