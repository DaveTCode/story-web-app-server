﻿CREATE TABLE [dbo].[Section]
(
	[Id] INT IDENTITY(1, 1) NOT NULL ,
	[Name] NVARCHAR(50) NOT NULL ,
	[Snippet] NVARCHAR(100) NULL ,
	[Contents] NVARCHAR(MAX) NOT NULL DEFAULT ''  ,
	[DateCreated] DATETIME NOT NULL ,
	[DateModified] DATETIME NOT NULL ,
	PRIMARY KEY CLUSTERED ([Id])
)
