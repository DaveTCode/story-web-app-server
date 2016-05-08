﻿CREATE TABLE [dbo].[Story]
(
	[Id] INT IDENTITY(1, 1) NOT NULL,
	[Name] NVARCHAR(MAX) NOT NULL,
	[Blurb] NVARCHAR(MAX) NULL,
	[DateCreated] DATETIME NOT NULL,
	[DateModified] DATETIME NOT NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC) 
)