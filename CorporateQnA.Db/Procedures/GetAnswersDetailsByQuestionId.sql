CREATE PROCEDURE [GetAnswersDetailsByQuestionId]
@QuestionId [INT]
AS
BEGIN
SELECT
[Answers].[Id],
[Answer],
[AnsweredOn],
[IsBestSolution],
[Users].[FullName],
[Users].[UserImage],
[LikedBy],
[DislikedBy]
FROM [Answers] INNER JOIN [AspNetUsers] [Users] ON [Users].[Id]=[Answers].[AnsweredBy]
WHERE [QuestionId]=@QuestionId
END