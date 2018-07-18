﻿CREATE PROCEDURE [dbo].[InsertDriverInfo]
	@userId int,
	@carId int,
	@drivingLicencePicFront varbinary(Max),
	@drivingLicencePicBack varbinary(Max),
	@knowledgeOfLanguages float
AS
	INSERT INTO Drivers(UserId, CarId,DrivingLicencePicFront,DrivingLicencePicBack,KnowledgeOfLanguages)
	Values(@userId, @carId, @drivingLicencePicFront, @drivingLicencePicBack, @knowledgeOfLanguages)
RETURN 0
