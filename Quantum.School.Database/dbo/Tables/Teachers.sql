CREATE TABLE [dbo].[Teachers] (
    [Id]          UNIQUEIDENTIFIER   NOT NULL,
    [ClusterId]   INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) CONSTRAINT [DF_Teachers_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CreatedBy]   UNIQUEIDENTIFIER   NULL,
    [Title]       NVARCHAR (10)      NULL,
    CONSTRAINT [PK_Teachers] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Teachers_Persons] FOREIGN KEY ([Id]) REFERENCES [dbo].[Persons] ([Id])
);


GO
CREATE CLUSTERED INDEX [CI_Teachers]
    ON [dbo].[Teachers]([ClusterId] ASC);

