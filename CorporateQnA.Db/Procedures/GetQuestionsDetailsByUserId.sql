CREATE PROCEDURE [GetQuestionsDetailsByUserId]
@UserId [INT]
AS
BEGIN
SELECT
[Questions].[Id],
[Title],
[Description],
[AskedOn],
[ViewedBy],
[VotedBy],
[ReportedBy],
[CategoryId],
[UserImage]  AS [UserImage] ,
COUNT(Answers.QuestionId) AS [AnswerCount],
ISNULL(
(SELECT TOP 1 IsBestSolution FROM Answers WHERE QuestionId=[Questions].Id AND [IsBestSolution]=1),0
) AS [IsResolved]
FROM [Questions]
INNER JOIN [AspNetUsers] ON [Questions].[AskedBy]=[AspNetUsers].[Id]
LEFT JOIN [Answers] ON [Answers].[QuestionId]=[Questions].[Id]
WHERE [AspNetUsers].[Id]=@UserId 
GROUP BY [QuestionId],[Questions].[Id],[Title],[Description],[AskedOn],
[CategoryId],[AskedBy],[ViewedBy],[VotedBy],[ReportedBy],[UserImage];
END