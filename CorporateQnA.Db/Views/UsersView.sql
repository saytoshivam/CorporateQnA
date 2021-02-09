CREATE VIEW UsersView
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
) AS [QuestionsSolved],
[LikedBy]='['+STUFF 
(
     (  
       SELECT ',' + CAST(TRIM('[]' FROM [LikedBy]) AS VARCHAR(MAX))  
       FROM [Answers] [a]   
       WHERE [a].[AnsweredBy]=[AspNetUsers].[Id]
       FOR XML PATH('')  
     ),1,1,'' 
)  +']',
[DislikedBy]='['+STUFF 
(
     (  
       SELECT ',' + CAST(TRIM('[]' FROM [DislikedBy]) AS VARCHAR(MAX))  
       FROM [Answers] [a]   
       WHERE [a].[AnsweredBy]=[AspNetUsers].[Id]
       FOR XML PATH('')  
     ),1,1,'' 
)  +']'
FROM [AspNetUsers] LEFT JOIN
[Questions] ON [Questions].[AskedBy]=[AspNetUsers].[Id]
GROUP BY [AspNetUsers].[Id],[FullName],[ProfileImage],[JobLocation],[Designation],[Department];