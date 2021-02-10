CREATE VIEW [UsersView]
AS
SELECT 
[AspNetUsers].[Id],
[FullName],
[UserImage],
[JobLocation],
[Designation],
[Department], 
COUNT([AskedBy]) AS [QuestionsAsked],
[LikedBy],
[DisLikedBy],
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
[Questions] ON [Questions].[AskedBy]=[AspNetUsers].[Id] LEFT JOIN [Answers]
ON [Answers].[AnsweredBy]=[AspNetUsers].[Id]
GROUP BY [AspNetUsers].[Id],[FullName],[UserImage],[JobLocation],[Designation],[Department],
[LikedBy],[DislikedBy];