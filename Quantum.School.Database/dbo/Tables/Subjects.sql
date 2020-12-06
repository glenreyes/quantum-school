CREATE TABLE [dbo].[Subjects] (
    [Id]          UNIQUEIDENTIFIER   NOT NULL,
    [ClusterId]   INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) CONSTRAINT [DF_Subjects_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CreatedBy]   UNIQUEIDENTIFIER   NULL,
    [Name]        NVARCHAR (50)      NOT NULL,
    [Description] NVARCHAR (500)     NULL,
    CONSTRAINT [PK_Subjects] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE CLUSTERED INDEX [CI_Subjects]
    ON [dbo].[Subjects]([ClusterId] ASC);

