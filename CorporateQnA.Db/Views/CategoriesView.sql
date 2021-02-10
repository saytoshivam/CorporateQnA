CREATE VIEW [CategoriesView]
AS
SELECT
[Categories].[Id],
[Categories].[Name],
[Categories].[Description],
[Categories].[CreatedOn],
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