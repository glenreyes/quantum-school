CREATE TABLE [dbo].[Persons] (
    [Id]          UNIQUEIDENTIFIER   NOT NULL,
    [ClusterId]   INT                IDENTITY (1, 1) NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) CONSTRAINT [DF_Persons_CreatedDate] DEFAULT (sysdatetimeoffset()) NOT NULL,
    [CreatedBy]   UNIQUEIDENTIFIER   NULL,
    [FirstName]   NVARCHAR (50)      NULL,
    [MiddleName]  NVARCHAR (50)      NULL,
    [LastName]    NVARCHAR (50)      NULL,
    [BirthDate]   SMALLDATETIME      NULL,
    [Gender]      VARCHAR (10)       NULL,
    CONSTRAINT [PK_Persons] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE CLUSTERED INDEX [CI_Persons]
    ON [dbo].[Persons]([ClusterId] ASC);

