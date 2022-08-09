/*
ArticleApp의 배포 스크립트

이 코드는 도구를 사용하여 생성되었습니다.
파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
변경 내용이 손실됩니다.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "ArticleApp"
:setvar DefaultFilePrefix "ArticleApp"
:setvar DefaultDataPath "C:\Users\kkh28\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\mssqllocaldb\"
:setvar DefaultLogPath "C:\Users\kkh28\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\mssqllocaldb\"

GO
:on error exit
GO
/*
SQLCMD 모드가 지원되지 않으면 SQLCMD 모드를 검색하고 스크립트를 실행하지 않습니다.
SQLCMD 모드를 설정한 후에 이 스크립트를 다시 사용하려면 다음을 실행합니다.
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'이 스크립트를 실행하려면 SQLCMD 모드를 사용하도록 설정해야 합니다.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[Articles]에 대한 명명되지 않은 제약 조건을(를) 삭제하는 중...';


GO
ALTER TABLE [dbo].[Articles] DROP CONSTRAINT [DF__Articles__Create__24927208];


GO
PRINT N'[dbo].[Articles] 테이블 다시 빌드 시작...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Articles] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Title]      NVARCHAR (255) NOT NULL,
    [Content]    NVARCHAR (MAX) NULL,
    [CreatedBy]  NVARCHAR (255) NULL,
    [Created]    DATETIME       DEFAULT (GetDate()) NULL,
    [ModifiedBy] NVARCHAR (255) NULL,
    [Modified]   DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Articles])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Articles] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Articles] ([Id], [Title], [CreatedBy], [Created], [ModifiedBy], [Modified])
        SELECT   [Id],
                 [Title],
                 [CreatedBy],
                 [Created],
                 [ModifiedBy],
                 [Modified]
        FROM     [dbo].[Articles]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Articles] OFF;
    END

DROP TABLE [dbo].[Articles];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Articles]', N'Articles';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'업데이트가 완료되었습니다.';


GO
