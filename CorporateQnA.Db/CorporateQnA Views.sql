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


CREATE VIEW [CategoryView]
AS
SELECT [Categories].[Id],[Categories].[Name],[Categories].[Description],[Categories].[CreatedOn],
COUNT(CategoryId) AS [TotalTags],
(
SELECT COUNT(*) FROM [Questions] WHERE [Categoryid]= [Categories].[Id] AND DATEDIFF(DAY,AskedOn,GETDATE())<=6
) AS [TagsThisWeek],
(
SELECT COUNT(*) FROM Questions WHERE Categoryid= [Categories].[Id] AND  DATEPART(MONTH,AskedOn)-DATEPART(MONTH,GETDATE())=0 
AND DATEPART(YEAR,AskedOn)-DATEPART(YEAR,GETDATE())=0
) AS [TagsThisMonth]
FROM 
[Categories] LEFT JOIN [Questions] ON [Categories].[Id]=[Questions].[CategoryId]
GROUP BY [Categories].[Id],[Categories].[Name],[Categories].[Description],[Categories].[CreatedOn];