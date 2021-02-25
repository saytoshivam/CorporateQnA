CREATE PROCEDURE [dbo].[MarkSolution]
@UserId [INT],
@AnswerId [INT]
AS
BEGIN
	DECLARE @QuestionId INT;
	SELECT @QuestionId=[QuestionId] FROM [Answers] WHERE [Id] = @AnswerId
	IF EXISTS(SELECT [AskedBy] FROM [Questions] WHERE [Id] = @QuestionId AND [AskedBy] = @UserId )
	BEGIN
		UPDATE [Answers] SET
		[IsBestSolution] = 1^(SELECT [IsBestSolution] FROM [Answers] WHERE [Id]=@AnswerId)
		WHERE [Id]=@AnswerId;
	END 
END


