CREATE TABLE [dbo].[Articles]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), -- 일련번호
	[Title] NVARCHAR(255) NOT NULL,				 -- 제목
)

GO
