CREATE TABLE [dbo].[Subjects_Teachers] (
    [SubjectId]   UNIQUEIDENTIFIER   NOT NULL,
    [TeacherId]   UNIQUEIDENTIFIER   NOT NULL,
    [ClusterId]   INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) CONSTRAINT [DF_Subjects_Teachers_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CreatedBy]   UNIQUEIDENTIFIER   NULL,
    CONSTRAINT [PK_Subjects_Teachers] PRIMARY KEY NONCLUSTERED ([SubjectId] ASC, [TeacherId] ASC),
    CONSTRAINT [FK_Subjects_Teachers_Subjects] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subjects] ([Id]),
    CONSTRAINT [FK_Subjects_Teachers_Teachers] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[Teachers] ([Id])
);


GO
CREATE CLUSTERED INDEX [CI_Subjects_Teachers]
    ON [dbo].[Subjects_Teachers]([ClusterId] ASC);

