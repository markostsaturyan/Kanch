﻿CREATE TABLE [dbo].[Photographer]
(
	[UserId] INT NOT NULL,
	[CameraId] INT NOT NULL, 
    [Profession] NVARCHAR(MAX) NOT NULL, 
    [KnowledgeOfLanguages] NVARCHAR(MAX) NOT NULL, 
    [WorkExperience] NVARCHAR(MAX) NOT NULL, 
    [HasDron] BIT NOT NULL, 
    [HasCameraStabilizator] BIT NOT NULL, 
    [HasGopro] BIT NOT NULL, 
    FOREIGN KEY (UserId) REFERENCES Users(Id),
	FOREIGN KEY (CameraId) REFERENCES Camera(Id)
)
