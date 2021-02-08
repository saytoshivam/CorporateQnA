CREATE VIEW [UsersView]
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