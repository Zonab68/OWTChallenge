USE [OWTChallenge]
GO
/****** Object:  Table [dbo].[Skill]    Script Date: 5/2/2022 4:10:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skill](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Level]    Script Date: 5/2/2022 4:10:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Level](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Level] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 5/2/2022 4:10:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactSkillRel]    Script Date: 5/2/2022 4:10:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactSkillRel](
	[ContactId] [int] NOT NULL,
	[SkillId] [int] NOT NULL,
	[LevelId] [int] NOT NULL,
 CONSTRAINT [PK_ContactSkillRel] PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC,
	[SkillId] ASC,
	[LevelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ContactSkillLevelView]    Script Date: 5/2/2022 4:10:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ContactSkillLevelView]
AS
SELECT dbo.Contact.FirstName, dbo.[Level].Name, dbo.Skill.Name AS Expr1, dbo.ContactSkillRel.ContactId, dbo.ContactSkillRel.SkillId, dbo.ContactSkillRel.LevelId
FROM   dbo.Contact INNER JOIN
             dbo.ContactSkillRel ON dbo.Contact.Id = dbo.ContactSkillRel.ContactId INNER JOIN
             dbo.[Level] ON dbo.ContactSkillRel.LevelId = dbo.[Level].Id INNER JOIN
             dbo.Skill ON dbo.ContactSkillRel.SkillId = dbo.Skill.Id
GO
SET IDENTITY_INSERT [dbo].[Contact] ON 

INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Address], [Email], [PhoneNumber]) VALUES (4, N'John', N'Doe', N'Jumpstreet 21, Vancouver, Canada', N'John.Doe@gmail.ca', N'006046661234')
INSERT [dbo].[Contact] ([Id], [FirstName], [LastName], [Address], [Email], [PhoneNumber]) VALUES (6, N'Jane', N'Eyre', N'Thornfield Hall 1, London, United Kingdom', N'Jane.Eyre@gmail.com', N'004471245678')
SET IDENTITY_INSERT [dbo].[Contact] OFF
GO
INSERT [dbo].[ContactSkillRel] ([ContactId], [SkillId], [LevelId]) VALUES (4, 1, 4)
INSERT [dbo].[ContactSkillRel] ([ContactId], [SkillId], [LevelId]) VALUES (4, 5, 1)
INSERT [dbo].[ContactSkillRel] ([ContactId], [SkillId], [LevelId]) VALUES (6, 2, 5)
INSERT [dbo].[ContactSkillRel] ([ContactId], [SkillId], [LevelId]) VALUES (6, 3, 6)
INSERT [dbo].[ContactSkillRel] ([ContactId], [SkillId], [LevelId]) VALUES (6, 4, 3)
INSERT [dbo].[ContactSkillRel] ([ContactId], [SkillId], [LevelId]) VALUES (6, 5, 5)
GO
SET IDENTITY_INSERT [dbo].[Level] ON 

INSERT [dbo].[Level] ([Id], [Name]) VALUES (1, N'Beginner')
INSERT [dbo].[Level] ([Id], [Name]) VALUES (2, N'Elementary')
INSERT [dbo].[Level] ([Id], [Name]) VALUES (3, N'Intermediate')
INSERT [dbo].[Level] ([Id], [Name]) VALUES (4, N'Upper Intermediate')
INSERT [dbo].[Level] ([Id], [Name]) VALUES (5, N'Advanced')
INSERT [dbo].[Level] ([Id], [Name]) VALUES (6, N'Proficient')
SET IDENTITY_INSERT [dbo].[Level] OFF
GO
SET IDENTITY_INSERT [dbo].[Skill] ON 

INSERT [dbo].[Skill] ([Id], [Name]) VALUES (1, N'Problem-Solving')
INSERT [dbo].[Skill] ([Id], [Name]) VALUES (2, N'Programming')
INSERT [dbo].[Skill] ([Id], [Name]) VALUES (3, N'Teamwork')
INSERT [dbo].[Skill] ([Id], [Name]) VALUES (4, N'Database Design')
INSERT [dbo].[Skill] ([Id], [Name]) VALUES (5, N'Organisational')
INSERT [dbo].[Skill] ([Id], [Name]) VALUES (6, N'Self-Development')
SET IDENTITY_INSERT [dbo].[Skill] OFF
GO
ALTER TABLE [dbo].[ContactSkillRel]  WITH CHECK ADD  CONSTRAINT [FK_ContactSkillRel_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
GO
ALTER TABLE [dbo].[ContactSkillRel] CHECK CONSTRAINT [FK_ContactSkillRel_Contact]
GO
ALTER TABLE [dbo].[ContactSkillRel]  WITH CHECK ADD  CONSTRAINT [FK_ContactSkillRel_Level] FOREIGN KEY([LevelId])
REFERENCES [dbo].[Level] ([Id])
GO
ALTER TABLE [dbo].[ContactSkillRel] CHECK CONSTRAINT [FK_ContactSkillRel_Level]
GO
ALTER TABLE [dbo].[ContactSkillRel]  WITH CHECK ADD  CONSTRAINT [FK_ContactSkillRel_Skill] FOREIGN KEY([SkillId])
REFERENCES [dbo].[Skill] ([Id])
GO
ALTER TABLE [dbo].[ContactSkillRel] CHECK CONSTRAINT [FK_ContactSkillRel_Skill]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Contact"
            Begin Extent = 
               Top = 9
               Left = 57
               Bottom = 206
               Right = 279
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "ContactSkillRel"
            Begin Extent = 
               Top = 9
               Left = 336
               Bottom = 179
               Right = 558
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Level"
            Begin Extent = 
               Top = 9
               Left = 615
               Bottom = 152
               Right = 837
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Skill"
            Begin Extent = 
               Top = 9
               Left = 894
               Bottom = 152
               Right = 1116
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ContactSkillLevelView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ContactSkillLevelView'
GO
