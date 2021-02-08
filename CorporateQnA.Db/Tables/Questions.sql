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