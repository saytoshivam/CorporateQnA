CREATE VIEW [QuestionsView]
AS
SELECT [Questions].[Id],[Questions].[Head],[Questions].[Description],[CategoryId],
[AskedOn],[AskedBy],[ViewedBy],[VotedBy],[ReportedBy],(
SELECT [ProfileImage] FROM [AspNetUsers] AS [User] WHERE [User].[Id]=[AskedBy]
) AS [UserImage],(
SELECT TOP 1 [IsBestSolution] FROM Answers WHERE [QuestionId]=[Questions].[Id] AND [IsBestSolution]=1
) AS [IsResolved]
FROM [Questions];