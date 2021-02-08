CREATE DATABASE CorporateQnA;

CREATE TABLE [Categories](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL
 );

 --This table will be autogenereted using Identity
 CREATE TABLE [AspNetUsers](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[FullName] [nvarchar](max) NULL,
	[ProfileImage] [nvarchar](max) NULL,
	[Designation] [nvarchar](max) NULL,
	[Department] [nvarchar](max) NULL,
	[JobLocation] [nvarchar](max) NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](max) NOT NULL
);

CREATE TABLE [Questions](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Head] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CategoryId] [int] NOT NULL REFERENCES [Categories](Id),
	[AskedBy] [int] NOT NULL REFERENCES [AspNetUsers](Id),
	[AskedOn] [datetime] NOT NULL,
	[ViewedBy] [nvarchar](max) NULL,
	[VotedBy] [nvarchar](max) NULL,
	[ReportedBy] [nvarchar](max) NULL
);

CREATE TABLE [Answers](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY,
	[QuestionId] [int] NOT NULL REFERENCES [Questions](Id),
	[AnsweredBy] [int] NOT NULL REFERENCES [AspNetUsers](Id),
	[Answer] [nvarchar](max) NOT NULL,
	[IsBestSolution] [bit] NOT NULL,
	[LikedBy] [nvarchar](max) NULL,
	[DislikedBy] [nvarchar](max) NULL,
	[AnsweredOn] [datetime] NOT NULL
);

--Views

CREATE VIEW [QuestionView]
AS
SELECT [Questions].[Id],[Questions].[Head],[Questions].[Description],[CategoryId],
[AskedOn],[AskedBy],[ViewedBy],[VotedBy],[ReportedBy],(
SELECT [ProfileImage] FROM [AspNetUsers] AS [User] WHERE [User].[Id]=[AskedBy]
) AS [UserImage],(
SELECT TOP 1 [IsBestSolution] FROM Answers WHERE [QuestionId]=[Questions].[Id] AND [IsBestSolution]=1
) AS [IsResolved]
FROM [Questions];

CREATE VIEW [UserView]
AS
SELECT [AspNetUsers].[Id],[FullName],[ProfileImage],[JobLocation],[Designation],[Department], 
COUNT([AskedBy]) AS [QuestionsAsked],
(
SELECT COUNT([AnsweredBy])  
FROM [AspNetUsers] [Users]  INNER JOIN [Answers] ON [Users].[Id]=[Answers].[AnsweredBy]
WHERE [Users].[Id]=[AspNetUsers].[Id] 
)  AS [QuestionsAnswered] ,
(
SELECT COUNT([IsBestSolution])  
FROM [AspNetUsers] [Users]  INNER JOIN [Answers] ON [Users].[Id]=[Answers].[AnsweredBy]
WHERE [Users].[Id]=[AspNetUsers].[Id] and [IsBestSolution]=1 
) AS [QuestionsSolved]
FROM [AspNetUsers] LEFT JOIN
[Questions] ON [Questions].[AskedBy]=[AspNetUsers].[Id]
GROUP BY [AspNetUsers].[Id],[FullName],[ProfileImage],[JobLocation],[Designation],[Department];  


--Procedures
CREATE PROC [spGetAnswersDetailsByQuestionId]
@QuestionId [int]
AS
BEGIN
SELECT [Answers].[Id],[Answer],[AnsweredOn],[IsBestSolution],[Users].[FullName],[Users].[ProfileImage]
AS [UserImage],[LikedBy],[DislikedBy] FROM [Answers] INNER JOIN [AspNetUsers] [Users] ON 
[Users].[Id]=[Answers].[AnsweredBy]
WHERE [QuestionId]=@QuestionId
END

CREATE PROC [spGetQuestionsByUserId]
@UserId [int]
AS
BEGIN
SELECT [Questions].[Id],[Head],[Description],[AskedOn],
[ViewedBy],[VotedBy],[ReportedBy],[CategoryId],[ProfileImage]  AS [UserImage] 
,COUNT(Answers.QuestionId) AS [TotalAnswers],
(
	SELECT TOP 1 IsBestSolution FROM Answers WHERE QuestionId=[Questions].Id AND [IsBestSolution]=1
) AS [IsResolved]
FROM [Questions]
INNER JOIN [AspNetUsers] ON [Questions].[AskedBy]=[AspNetUsers].[Id]
LEFT JOIN [Answers] ON [Answers].[QuestionId]=[Questions].[Id]
WHERE [AspNetUsers].[Id]=@UserId  GROUP BY [QuestionId],[Questions].[Id],[Head],[Description],[AskedOn]
,[CategoryId],[AskedBy],[ViewedBy],[VotedBy],[ReportedBy],[ProfileImage];
END

