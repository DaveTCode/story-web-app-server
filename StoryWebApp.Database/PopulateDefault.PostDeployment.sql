/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
MERGE INTO Story AS Target 
USING (VALUES 
        (1, 'Story 1', 'The blurb for story 1', '2016-01-01', '2016-01-02'), 
        (2, 'Story 2', '', '2016-01-01', '2016-01-02'), 
        (3, 'Story 3 with a much longer name', 'The blurb for story 3 which is far too long for a simple box to display', '2016-01-01', '2016-01-02')
) 
AS Source (Id, Name, Blurb, DateCreated, DateModified) 
ON Target.Id = Source.Id
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Name, Blurb, DateCreated, DateModified) 
VALUES (Name, Blurb, DateCreated, DateModified);

MERGE INTO Chapter AS Target
USING (VALUES
		(1, 'Chapter 1', '2016-01-01', '2016-01-02'), 
        (2, 'Chapter 2', '2016-01-01', '2016-01-02'), 
        (3, 'Chapter 3 with a much longer name', '2016-01-01', '2016-01-02')
) 
AS Source (Id, Name,  DateCreated, DateModified) 
ON Target.Id = Source.Id
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Name, DateCreated, DateModified) 
VALUES (Name, DateCreated, DateModified);

MERGE INTO StoryChapterMap AS Target
USING (VALUES
		(1, 1, 1),
		(1, 2, 2),
		(1, 3, 3),
		(2, 3, 0)
		)
AS Source (StoryId, ChapterId, ChapterOrder)
ON Target.StoryId = Source.StoryId AND Target.ChapterId = Source.ChapterId
WHEN NOT MATCHED BY TARGET THEN
INSERT (StoryId, ChapterId, ChapterOrder)
VALUES (StoryId, ChapterId, ChapterOrder);