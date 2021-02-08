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