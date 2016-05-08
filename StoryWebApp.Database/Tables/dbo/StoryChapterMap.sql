CREATE TABLE [dbo].[StoryChapterMap]
(
	[StoryId] INT NOT NULL ,
	[ChapterId] INT NOT NULL ,
	[ChapterOrder] INT NOT NULL ,
	PRIMARY KEY CLUSTERED ([StoryId], [ChapterId] ASC) ,
	FOREIGN KEY ([StoryId]) REFERENCES Story([Id]),
	FOREIGN KEY ([ChapterId]) REFERENCES Chapter([Id]),
	CONSTRAINT UQ_StoryChapterOrder UNIQUE([StoryId], [ChapterOrder]) 
)
