CREATE PROCEDURE [dbo].[GetStories]
AS
	SELECT *
	FROM StoryCombinedData
	ORDER BY ChapterOrder, SectionOrder;

	RETURN 0
