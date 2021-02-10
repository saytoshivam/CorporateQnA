CREATE VIEW [QuestionsView]
AS
SELECT
[Questions].[Id],
[Questions].[Title],
[Questions].[Description],
[CategoryId],
[AskedOn],
[AskedBy],
[ViewedBy],
[VotedBy],
[ReportedBy],
COUNT(Answers.QuestionId) AS [AnswerCount],
(
SELECT [UserImage] FROM [AspNetUsers] AS [User] WHERE [User].[Id]=[AskedBy]
) AS [UserImage],
ISNULL(
(SELECT TOP 1 [IsBestSolution] FROM Answers WHERE [QuestionId]=[Questions].[Id] AND [IsBestSolution]=1)
,0) AS [IsResolved]
FROM [Questions] LEFT JOIN [Answers] ON [Answers].[QuestionId]=[Questions].[Id]
GROUP BY [Questions].[Id],[Questions].[Title],[Questions].[Description],[CategoryId],
[AskedOn],[AskedBy],[ViewedBy],[VotedBy],[ReportedBy];