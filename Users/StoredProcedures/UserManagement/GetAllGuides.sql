﻿CREATE PROCEDURE [dbo].[GetAllGuides]

AS

Select Id,FirstName,LastName,DateOfBirth,Email,PhoneNumber,Picture, UserName, [Password], Gender, KnowledgeOfLanguages,EducationGrade,Profession,WorkExperience,Rating,NumberOfAppraisers
from Users join Guide on Users.Id=Guide.UserId

RETURN 0
