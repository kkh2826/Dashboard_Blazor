CREATE TABLE [dbo].[Articles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- 일련번호
	[Title] NVARCHAR(255) NOT NULL,				 -- 제목

	[Content] NVARCHAR(MAX) NOT NULL,

	[CreatedBy] NVARCHAR(255) NULL,
	[Created] DateTime Default(GetDate()),
	[ModifiedBy] NVARCHAR(255) NULL,
	[Modified] DateTime NULL,
	
)

GO
