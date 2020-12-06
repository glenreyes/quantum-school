IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Teachers]') AND type in (N'U'))
ALTER TABLE [dbo].[Teachers] DROP CONSTRAINT IF EXISTS [FK_Teachers_Persons]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subjects_Teachers]') AND type in (N'U'))
ALTER TABLE [dbo].[Subjects_Teachers] DROP CONSTRAINT IF EXISTS [FK_Subjects_Teachers_Teachers]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subjects_Teachers]') AND type in (N'U'))
ALTER TABLE [dbo].[Subjects_Teachers] DROP CONSTRAINT IF EXISTS [FK_Subjects_Teachers_Subjects]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Students_ClassSchedules]') AND type in (N'U'))
ALTER TABLE [dbo].[Students_ClassSchedules] DROP CONSTRAINT IF EXISTS [FK_Students_ClassSchedules_Students]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Students_ClassSchedules]') AND type in (N'U'))
ALTER TABLE [dbo].[Students_ClassSchedules] DROP CONSTRAINT IF EXISTS [FK_Students_ClassSchedules_ClassSchedules]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Students]') AND type in (N'U'))
ALTER TABLE [dbo].[Students] DROP CONSTRAINT IF EXISTS [FK_Students_Persons]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClassSchedules]') AND type in (N'U'))
ALTER TABLE [dbo].[ClassSchedules] DROP CONSTRAINT IF EXISTS [FK_ClassSchedules_Teachers]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClassSchedules]') AND type in (N'U'))
ALTER TABLE [dbo].[ClassSchedules] DROP CONSTRAINT IF EXISTS [FK_ClassSchedules_Subjects]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Teachers]') AND type in (N'U'))
ALTER TABLE [dbo].[Teachers] DROP CONSTRAINT IF EXISTS [DF_Teachers_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subjects_Teachers]') AND type in (N'U'))
ALTER TABLE [dbo].[Subjects_Teachers] DROP CONSTRAINT IF EXISTS [DF_Subjects_Teachers_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subjects]') AND type in (N'U'))
ALTER TABLE [dbo].[Subjects] DROP CONSTRAINT IF EXISTS [DF_Subjects_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Students_ClassSchedules]') AND type in (N'U'))
ALTER TABLE [dbo].[Students_ClassSchedules] DROP CONSTRAINT IF EXISTS [DF_Students_ClassSchedules_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Students]') AND type in (N'U'))
ALTER TABLE [dbo].[Students] DROP CONSTRAINT IF EXISTS [DF_Students_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Persons]') AND type in (N'U'))
ALTER TABLE [dbo].[Persons] DROP CONSTRAINT IF EXISTS [DF_Persons_CreatedDate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClassSchedules]') AND type in (N'U'))
ALTER TABLE [dbo].[ClassSchedules] DROP CONSTRAINT IF EXISTS [DF_ClassSchedules_CreatedDate]
GO
/****** Object:  Index [CI_Teachers]    Script Date: 30 Nov 2020 7:21:05 PM ******/
DROP INDEX IF EXISTS [CI_Teachers] ON [dbo].[Teachers] WITH ( ONLINE = OFF )
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 30 Nov 2020 7:21:05 PM ******/
DROP TABLE IF EXISTS [dbo].[Teachers]
GO
/****** Object:  Index [CI_Subjects_Teachers]    Script Date: 30 Nov 2020 7:21:05 PM ******/
DROP INDEX IF EXISTS [CI_Subjects_Teachers] ON [dbo].[Subjects_Teachers] WITH ( ONLINE = OFF )
GO
/****** Object:  Table [dbo].[Subjects_Teachers]    Script Date: 30 Nov 2020 7:21:05 PM ******/
DROP TABLE IF EXISTS [dbo].[Subjects_Teachers]
GO
/****** Object:  Index [CI_Subjects]    Script Date: 30 Nov 2020 7:21:05 PM ******/
DROP INDEX IF EXISTS [CI_Subjects] ON [dbo].[Subjects] WITH ( ONLINE = OFF )
GO
/****** Object:  Table [dbo].[Subjects]    Script Date: 30 Nov 2020 7:21:05 PM ******/
DROP TABLE IF EXISTS [dbo].[Subjects]
GO
/****** Object:  Index [CI_Students_ClassSchedules]    Script Date: 30 Nov 2020 7:21:05 PM ******/
DROP INDEX IF EXISTS [CI_Students_ClassSchedules] ON [dbo].[Students_ClassSchedules] WITH ( ONLINE = OFF )
GO
/****** Object:  Table [dbo].[Students_ClassSchedules]    Script Date: 30 Nov 2020 7:21:05 PM ******/
DROP TABLE IF EXISTS [dbo].[Students_ClassSchedules]
GO
/****** Object:  Index [CI_Students]    Script Date: 30 Nov 2020 7:21:05 PM ******/
DROP INDEX IF EXISTS [CI_Students] ON [dbo].[Students] WITH ( ONLINE = OFF )
GO
/****** Object:  Table [dbo].[Students]    Script Date: 30 Nov 2020 7:21:05 PM ******/
DROP TABLE IF EXISTS [dbo].[Students]
GO
/****** Object:  Index [CI_Persons]    Script Date: 30 Nov 2020 7:21:05 PM ******/
DROP INDEX IF EXISTS [CI_Persons] ON [dbo].[Persons] WITH ( ONLINE = OFF )
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 30 Nov 2020 7:21:05 PM ******/
DROP TABLE IF EXISTS [dbo].[Persons]
GO
/****** Object:  Index [CI_ClassSchedules]    Script Date: 30 Nov 2020 7:21:05 PM ******/
DROP INDEX IF EXISTS [CI_ClassSchedules] ON [dbo].[ClassSchedules] WITH ( ONLINE = OFF )
GO
/****** Object:  Table [dbo].[ClassSchedules]    Script Date: 30 Nov 2020 7:21:05 PM ******/
DROP TABLE IF EXISTS [dbo].[ClassSchedules]
GO
/****** Object:  Table [dbo].[ClassSchedules]    Script Date: 30 Nov 2020 7:21:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassSchedules](
	[Id] [uniqueidentifier] NOT NULL,
	[ClusterId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[SubjectId] [uniqueidentifier] NOT NULL,
	[TeacherId] [uniqueidentifier] NOT NULL,
	[Location] [nvarchar](80) NULL,
 CONSTRAINT [PK_ClassSchedules] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [CI_ClassSchedules]    Script Date: 30 Nov 2020 7:21:05 PM ******/
CREATE CLUSTERED INDEX [CI_ClassSchedules] ON [dbo].[ClassSchedules]
(
	[ClusterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 30 Nov 2020 7:21:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[Id] [uniqueidentifier] NOT NULL,
	[ClusterId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[FirstName] [nvarchar](50) NULL,
	[MiddleName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[BirthDate] [smalldatetime] NULL,
	[Gender] [varchar](10) NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [CI_Persons]    Script Date: 30 Nov 2020 7:21:05 PM ******/
CREATE CLUSTERED INDEX [CI_Persons] ON [dbo].[Persons]
(
	[ClusterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 30 Nov 2020 7:21:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[Id] [uniqueidentifier] NOT NULL,
	[ClusterId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[Gpa] [decimal](4, 3) NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [CI_Students]    Script Date: 30 Nov 2020 7:21:05 PM ******/
CREATE CLUSTERED INDEX [CI_Students] ON [dbo].[Students]
(
	[ClusterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students_ClassSchedules]    Script Date: 30 Nov 2020 7:21:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students_ClassSchedules](
	[ClassScheduleId] [uniqueidentifier] NOT NULL,
	[StudentId] [uniqueidentifier] NOT NULL,
	[ClusterId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Students_ClassSchedules] PRIMARY KEY NONCLUSTERED 
(
	[ClassScheduleId] ASC,
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [CI_Students_ClassSchedules]    Script Date: 30 Nov 2020 7:21:05 PM ******/
CREATE CLUSTERED INDEX [CI_Students_ClassSchedules] ON [dbo].[Students_ClassSchedules]
(
	[ClassScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subjects]    Script Date: 30 Nov 2020 7:21:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subjects](
	[Id] [uniqueidentifier] NOT NULL,
	[ClusterId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_Subjects] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [CI_Subjects]    Script Date: 30 Nov 2020 7:21:05 PM ******/
CREATE CLUSTERED INDEX [CI_Subjects] ON [dbo].[Subjects]
(
	[ClusterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subjects_Teachers]    Script Date: 30 Nov 2020 7:21:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subjects_Teachers](
	[SubjectId] [uniqueidentifier] NOT NULL,
	[TeacherId] [uniqueidentifier] NOT NULL,
	[ClusterId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Subjects_Teachers] PRIMARY KEY NONCLUSTERED 
(
	[SubjectId] ASC,
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [CI_Subjects_Teachers]    Script Date: 30 Nov 2020 7:21:05 PM ******/
CREATE CLUSTERED INDEX [CI_Subjects_Teachers] ON [dbo].[Subjects_Teachers]
(
	[ClusterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 30 Nov 2020 7:21:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[Id] [uniqueidentifier] NOT NULL,
	[ClusterId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[Title] [nvarchar](10) NULL,
 CONSTRAINT [PK_Teachers] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [CI_Teachers]    Script Date: 30 Nov 2020 7:21:05 PM ******/
CREATE CLUSTERED INDEX [CI_Teachers] ON [dbo].[Teachers]
(
	[ClusterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClassSchedules] ADD  CONSTRAINT [DF_ClassSchedules_CreatedDate]  DEFAULT (sysdatetimeoffset()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Persons] ADD  CONSTRAINT [DF_Persons_CreatedDate]  DEFAULT (sysdatetimeoffset()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [DF_Students_CreatedDate]  DEFAULT (sysdatetimeoffset()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Students_ClassSchedules] ADD  CONSTRAINT [DF_Students_ClassSchedules_CreatedDate]  DEFAULT (sysdatetimeoffset()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Subjects] ADD  CONSTRAINT [DF_Subjects_CreatedDate]  DEFAULT (sysdatetimeoffset()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Subjects_Teachers] ADD  CONSTRAINT [DF_Subjects_Teachers_CreatedDate]  DEFAULT (sysdatetimeoffset()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Teachers] ADD  CONSTRAINT [DF_Teachers_CreatedDate]  DEFAULT (sysdatetimeoffset()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ClassSchedules]  WITH CHECK ADD  CONSTRAINT [FK_ClassSchedules_Subjects] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subjects] ([Id])
GO
ALTER TABLE [dbo].[ClassSchedules] CHECK CONSTRAINT [FK_ClassSchedules_Subjects]
GO
ALTER TABLE [dbo].[ClassSchedules]  WITH CHECK ADD  CONSTRAINT [FK_ClassSchedules_Teachers] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teachers] ([Id])
GO
ALTER TABLE [dbo].[ClassSchedules] CHECK CONSTRAINT [FK_ClassSchedules_Teachers]
GO
ALTER TABLE [dbo].[Students]  WITH CHECK ADD  CONSTRAINT [FK_Students_Persons] FOREIGN KEY([Id])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[Students] CHECK CONSTRAINT [FK_Students_Persons]
GO
ALTER TABLE [dbo].[Students_ClassSchedules]  WITH CHECK ADD  CONSTRAINT [FK_Students_ClassSchedules_ClassSchedules] FOREIGN KEY([ClassScheduleId])
REFERENCES [dbo].[ClassSchedules] ([Id])
GO
ALTER TABLE [dbo].[Students_ClassSchedules] CHECK CONSTRAINT [FK_Students_ClassSchedules_ClassSchedules]
GO
ALTER TABLE [dbo].[Students_ClassSchedules]  WITH CHECK ADD  CONSTRAINT [FK_Students_ClassSchedules_Students] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([Id])
GO
ALTER TABLE [dbo].[Students_ClassSchedules] CHECK CONSTRAINT [FK_Students_ClassSchedules_Students]
GO
ALTER TABLE [dbo].[Subjects_Teachers]  WITH CHECK ADD  CONSTRAINT [FK_Subjects_Teachers_Subjects] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subjects] ([Id])
GO
ALTER TABLE [dbo].[Subjects_Teachers] CHECK CONSTRAINT [FK_Subjects_Teachers_Subjects]
GO
ALTER TABLE [dbo].[Subjects_Teachers]  WITH CHECK ADD  CONSTRAINT [FK_Subjects_Teachers_Teachers] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teachers] ([Id])
GO
ALTER TABLE [dbo].[Subjects_Teachers] CHECK CONSTRAINT [FK_Subjects_Teachers_Teachers]
GO
ALTER TABLE [dbo].[Teachers]  WITH CHECK ADD  CONSTRAINT [FK_Teachers_Persons] FOREIGN KEY([Id])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[Teachers] CHECK CONSTRAINT [FK_Teachers_Persons]
GO
