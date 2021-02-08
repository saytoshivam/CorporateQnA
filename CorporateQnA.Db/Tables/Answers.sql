CREATE TABLE [Answers](
	[Id] [INT] IDENTITY(1,1) PRIMARY KEY,
	[QuestionId] [INT] NOT NULL REFERENCES [Questions](Id),
	[AnsweredBy] [INT] NOT NULL REFERENCES [AspNetUsers](Id),
	[Answer] [NVARCHAR](MAX) NOT NULL,
	[IsBestSolution] [BIT] NOT NULL,
	[LikedBy] [NVARCHAR](MAX) NULL,
	[DislikedBy] [NVARCHAR](MAX) NULL,
	[AnsweredOn] [DATETIME] NOT NULL
);