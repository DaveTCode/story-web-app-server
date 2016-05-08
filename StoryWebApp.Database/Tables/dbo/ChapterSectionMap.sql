CREATE TABLE [dbo].[ChapterSectionMap]
(
	[ChapterId] INT NOT NULL ,
	[SectionId] INT NOT NULL ,
	[SectionOrder] INT NOT NULL ,
	PRIMARY KEY CLUSTERED ([ChapterId], [SectionId] ASC),
	FOREIGN KEY ([ChapterId]) REFERENCES Chapter([Id]),
	FOREIGN KEY ([SectionId]) REFERENCES Section([Id]),
	CONSTRAINT UQ_ChapterSectionOrder UNIQUE([ChapterId], [SectionOrder])
)
