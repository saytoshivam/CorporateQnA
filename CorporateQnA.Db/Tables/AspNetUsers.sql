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
