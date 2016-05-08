CREATE VIEW [dbo].[StoryCombinedData]
	AS 
	SELECT 
		s.Id AS 'StoryId',
		s.Name AS 'StoryName',
		s.Blurb AS 'StoryBlurb',
		s.DateCreated AS 'StoryDateCreated',
		s.DateModified AS 'StoryDateModified',
		c.Id AS 'ChapterId',
		c.Name AS 'ChapterName',
		c.DateCreated AS 'ChapterDateCreated',
		c.DateModified AS 'ChapterDateModified',
		scm.ChapterOrder AS 'ChapterOrder',
		sec.Id AS 'SectionId',
		sec.Name AS 'SectionName',
		sec.Snippet AS 'SectionSnippet',
		sec.DateCreated AS 'SectionDateCreated',
		sec.DateModified AS 'SectionDateModified',
		csm.SectionOrder AS 'SectionOrder'
	FROM Story AS s
	LEFT JOIN StoryChapterMap AS scm ON s.Id = scm.StoryId
	LEFT JOIN Chapter AS c ON c.Id = scm.ChapterId
	LEFT JOIN ChapterSectionMap AS csm ON c.Id = csm.ChapterId
	LEFT JOIN Section AS sec ON sec.Id = csm.SectionId
