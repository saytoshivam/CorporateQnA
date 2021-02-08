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