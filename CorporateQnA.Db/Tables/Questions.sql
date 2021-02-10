CREATE TABLE [Questions](
	[Id] [INT] IDENTITY(1,1) PRIMARY KEY,
	[Title] [NVARCHAR](MAX) NOT NULL,
	[Description] [NVARCHAR](MAX) NULL,
	[CategoryId] [INT] NOT NULL REFERENCES [Categories](Id),
	[AskedBy] [INT] NOT NULL REFERENCES [AspNetUsers](Id),
	[AskedOn] [DATETIME] NOT NULL,
	[ViewedBy] [NVARCHAR](MAX) NULL,
	[VotedBy] [NVARCHAR](MAX) NULL,
	[ReportedBy] [NVARCHAR](MAX) NULL
);