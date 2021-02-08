CREATE PROCEDURE [SpGetAnswersDetailsByQuestionId]
@QuestionId [int]
AS
BEGIN
SELECT [Answers].[Id],[Answer],[AnsweredOn],[IsBestSolution],[Users].[FullName],[Users].[ProfileImage]
AS [UserImage],[LikedBy],[DislikedBy] FROM [Answers] INNER JOIN [AspNetUsers] [Users] ON 
[Users].[Id]=[Answers].[AnsweredBy]
WHERE [QuestionId]=@QuestionId
END


CREATE PROCEDURE [SpGetQuestionsByUserId]
@UserId [int]
AS
BEGIN
SELECT [Questions].[Id],[Head],[Description],[AskedOn],
[ViewedBy],[VotedBy],[ReportedBy],[CategoryId],[ProfileImage]  AS [UserImage] 
,COUNT(Answers.QuestionId) AS [TotalAnswers],
(
	SELECT TOP 1 IsBestSolution FROM Answers WHERE QuestionId=[Questions].Id AND [IsBestSolution]=1
) AS [IsResolved]
FROM [Questions]
INNER JOIN [AspNetUsers] ON [Questions].[AskedBy]=[AspNetUsers].[Id]
LEFT JOIN [Answers] ON [Answers].[QuestionId]=[Questions].[Id]
WHERE [AspNetUsers].[Id]=@UserId  GROUP BY [QuestionId],[Questions].[Id],[Head],[Description],[AskedOn]
,[CategoryId],[AskedBy],[ViewedBy],[VotedBy],[ReportedBy],[ProfileImage];
END
