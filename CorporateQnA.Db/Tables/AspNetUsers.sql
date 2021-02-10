--This table will be autogenereted using Identity
 CREATE TABLE [AspNetUsers](
	[Id] [INT] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
	[FullName] [NVARCHAR](MAX) NULL,
	[UserImage] [NVARCHAR](MAX) NULL,
	[Designation] [NVARCHAR](MAX) NULL,
	[Department] [NVARCHAR](MAX) NULL,
	[JobLocation] [NVARCHAR](MAX) NULL,
	[UserName] [NVARCHAR](MAX) NOT NULL,
	[Password] [NVARCHAR](MAX) NOT NULL
);
