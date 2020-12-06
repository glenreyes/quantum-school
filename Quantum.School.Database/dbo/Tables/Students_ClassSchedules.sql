CREATE TABLE [dbo].[Students_ClassSchedules] (
    [ClassScheduleId] UNIQUEIDENTIFIER   NOT NULL,
    [StudentId]       UNIQUEIDENTIFIER   NOT NULL,
    [ClusterId]       INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate]     DATETIMEOFFSET (7) CONSTRAINT [DF_Students_ClassSchedules_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CreatedBy]       UNIQUEIDENTIFIER   NULL,
    CONSTRAINT [PK_Students_ClassSchedules] PRIMARY KEY NONCLUSTERED ([ClassScheduleId] ASC, [StudentId] ASC),
    CONSTRAINT [FK_Students_ClassSchedules_ClassSchedules] FOREIGN KEY ([ClassScheduleId]) REFERENCES [dbo].[ClassSchedules] ([Id]),
    CONSTRAINT [FK_Students_ClassSchedules_Students] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);


GO
CREATE CLUSTERED INDEX [CI_Students_ClassSchedules]
    ON [dbo].[Students_ClassSchedules]([ClassScheduleId] ASC);

