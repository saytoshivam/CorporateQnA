CREATE TABLE [Categories](
	[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL
 );