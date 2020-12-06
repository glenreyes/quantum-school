CREATE TABLE [dbo].[Students] (
    [Id]          UNIQUEIDENTIFIER   NOT NULL,
    [ClusterId]   INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) CONSTRAINT [DF_Students_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CreatedBy]   UNIQUEIDENTIFIER   NULL,
    [Gpa]         DECIMAL (4, 3)     NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Students_Persons] FOREIGN KEY ([Id]) REFERENCES [dbo].[Persons] ([Id])
);


GO
CREATE CLUSTERED INDEX [CI_Students]
    ON [dbo].[Students]([ClusterId] ASC);

