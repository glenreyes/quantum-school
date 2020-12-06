CREATE TABLE [dbo].[ClassSchedules] (
    [Id]          UNIQUEIDENTIFIER   NOT NULL,
    [ClusterId]   INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) CONSTRAINT [DF_ClassSchedules_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CreatedBy]   UNIQUEIDENTIFIER   NULL,
    [SubjectId]   UNIQUEIDENTIFIER   NOT NULL,
    [TeacherId]   UNIQUEIDENTIFIER   NOT NULL,
    [Location]    NVARCHAR (80)      NULL,
    CONSTRAINT [PK_ClassSchedules] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClassSchedules_Subjects] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subjects] ([Id]),
    CONSTRAINT [FK_ClassSchedules_Teachers] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[Teachers] ([Id])
);


GO
CREATE CLUSTERED INDEX [CI_ClassSchedules]
    ON [dbo].[ClassSchedules]([ClusterId] ASC);

