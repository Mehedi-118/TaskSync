USE [TaskSync]
GO
/****** Object:  Schema [GS]    Script Date: 9/30/2023 11:45:22 PM ******/
CREATE SCHEMA [GS]
GO
/****** Object:  Schema [System]    Script Date: 9/30/2023 11:45:22 PM ******/
CREATE SCHEMA [System]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/30/2023 11:45:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 9/30/2023 11:45:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 9/30/2023 11:45:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[RoleDesciption] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 9/30/2023 11:45:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 9/30/2023 11:45:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 9/30/2023 11:45:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 9/30/2023 11:45:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[UserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[ProfilePictureUrl] [nvarchar](max) NULL,
	[DateOfBirth] [datetime2](7) NULL,
	[Occupation] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 9/30/2023 11:45:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [GS].[Categorys]    Script Date: 9/30/2023 11:45:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GS].[Categorys](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime2](3) NOT NULL,
	[UpdatedAt] [datetime2](3) NULL,
	[DeletedAt] [datetime2](3) NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[TitleEn] [nvarchar](max) NOT NULL,
	[TitleBn] [nvarchar](max) NOT NULL,
	[DescriptionEn] [nvarchar](max) NOT NULL,
	[DescriptionBn] [nvarchar](max) NOT NULL,
	[logo] [nvarchar](max) NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[DeletedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categorys] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [GS].[Reminders]    Script Date: 9/30/2023 11:45:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GS].[Reminders](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime2](3) NOT NULL,
	[UpdatedAt] [datetime2](3) NULL,
	[DeletedAt] [datetime2](3) NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[TodoId] [bigint] NULL,
	[UserId] [nvarchar](450) NULL,
	[RemindAt] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[DeletedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Reminders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [GS].[Todos]    Script Date: 9/30/2023 11:45:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GS].[Todos](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedAt] [datetime2](3) NOT NULL,
	[UpdatedAt] [datetime2](3) NULL,
	[DeletedAt] [datetime2](3) NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[TitleEn] [nvarchar](max) NOT NULL,
	[TitleBn] [nvarchar](max) NOT NULL,
	[DescriptionEn] [nvarchar](max) NOT NULL,
	[DescriptionBn] [nvarchar](max) NOT NULL,
	[StartTime] [datetime2](7) NOT NULL,
	[DueTime] [datetime2](7) NOT NULL,
	[Priority] [int] NOT NULL,
	[HasReminder] [bit] NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[ModifiedBy] [nvarchar](max) NULL,
	[DeletedBy] [nvarchar](max) NULL,
	[CategoryId] [bigint] NULL,
 CONSTRAINT [PK_Todos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [System].[RefreshToken]    Script Date: 9/30/2023 11:45:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [System].[RefreshToken](
	[Id] [nvarchar](450) NOT NULL,
	[UserId] [nvarchar](max) NOT NULL,
	[Token] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ExpiresAt] [datetime2](7) NOT NULL,
	[IsRevoked] [bit] NOT NULL,
	[DeviceInfo] [nvarchar](max) NULL,
 CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230916161754_Initial', N'7.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230916171508_Initial1', N'7.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230929201539_CategoryIdAdded', N'7.0.4')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [UserName], [Email], [ProfilePictureUrl], [DateOfBirth], [Occupation], [Address], [CreatedAt], [NormalizedUserName], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'a76f4462-b299-4ddf-94bb-4dab2f757611', N'Mizan', N'mizan', N'mizan@gmail.com', NULL, NULL, NULL, N'Dhaka', CAST(N'2023-09-17T23:36:47.4201547' AS DateTime2), N'MIZAN', N'MIZAN@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEF59vYEuyPbtmx+U/Mwl2TPUvgz0hfDgoKp5bBSHj+93W6V/oVCb6FTfltHnu/g8kQ==', N'CVGWOPM4OXRORODOCEQBVG4GEBYWGFPZ', N'60f3805a-9f4a-40d5-aa67-02d24af49725', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [UserName], [Email], [ProfilePictureUrl], [DateOfBirth], [Occupation], [Address], [CreatedAt], [NormalizedUserName], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'Mehedi', N'sami', N'sami@gmail.com', NULL, NULL, NULL, N'Dhaka', CAST(N'2023-09-17T23:36:21.7721549' AS DateTime2), N'SAMI', N'SAMI@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEBo1nMTnE2tWzXtHC06K458HeaKGwKcXyZFunCFkqO7spM4tuL2YNoIumBlmXKNz1w==', N'XJEY5ZPEW4ZJC23SU6Y23PVOEPBFNRAD', N'eb377193-8eb1-4bdc-9a53-ed95ae26d2ca', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [GS].[Categorys] ON 

INSERT [GS].[Categorys] ([Id], [CreatedAt], [UpdatedAt], [DeletedAt], [IsDeleted], [IsActive], [TitleEn], [TitleBn], [DescriptionEn], [DescriptionBn], [logo], [UserId], [CreatedBy], [ModifiedBy], [DeletedBy]) VALUES (35, CAST(N'2023-09-17T18:01:01.6920000' AS DateTime2), NULL, NULL, 0, 1, N'Personal', N'', N'Schedule your personal task', N'', N'', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', NULL, NULL)
INSERT [GS].[Categorys] ([Id], [CreatedAt], [UpdatedAt], [DeletedAt], [IsDeleted], [IsActive], [TitleEn], [TitleBn], [DescriptionEn], [DescriptionBn], [logo], [UserId], [CreatedBy], [ModifiedBy], [DeletedBy]) VALUES (36, CAST(N'2023-09-17T18:01:01.7050000' AS DateTime2), NULL, NULL, 0, 1, N'Shopping', N'', N'Schedule your Shopping task', N'', N'', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', NULL, NULL)
INSERT [GS].[Categorys] ([Id], [CreatedAt], [UpdatedAt], [DeletedAt], [IsDeleted], [IsActive], [TitleEn], [TitleBn], [DescriptionEn], [DescriptionBn], [logo], [UserId], [CreatedBy], [ModifiedBy], [DeletedBy]) VALUES (37, CAST(N'2023-09-17T18:01:01.7050000' AS DateTime2), NULL, NULL, 0, 1, N'Office', N'', N'Schedule your Office task', N'', N'', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', NULL, NULL)
INSERT [GS].[Categorys] ([Id], [CreatedAt], [UpdatedAt], [DeletedAt], [IsDeleted], [IsActive], [TitleEn], [TitleBn], [DescriptionEn], [DescriptionBn], [logo], [UserId], [CreatedBy], [ModifiedBy], [DeletedBy]) VALUES (38, CAST(N'2023-09-17T18:01:01.7050000' AS DateTime2), NULL, NULL, 0, 1, N'Miscellenious', N'', N'Schedule your Miscellenious task', N'', N'', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', NULL, NULL)
INSERT [GS].[Categorys] ([Id], [CreatedAt], [UpdatedAt], [DeletedAt], [IsDeleted], [IsActive], [TitleEn], [TitleBn], [DescriptionEn], [DescriptionBn], [logo], [UserId], [CreatedBy], [ModifiedBy], [DeletedBy]) VALUES (39, CAST(N'2023-09-17T18:07:39.0890000' AS DateTime2), NULL, NULL, 0, 1, N'Custom 1', N'string', N'string', N'Custom 1', N'', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', NULL, NULL)
INSERT [GS].[Categorys] ([Id], [CreatedAt], [UpdatedAt], [DeletedAt], [IsDeleted], [IsActive], [TitleEn], [TitleBn], [DescriptionEn], [DescriptionBn], [logo], [UserId], [CreatedBy], [ModifiedBy], [DeletedBy]) VALUES (10014, CAST(N'2023-09-29T20:41:35.1600000' AS DateTime2), NULL, NULL, 0, 1, N'Personal', N'', N'Schedule your personal task', N'', N'', N'a76f4462-b299-4ddf-94bb-4dab2f757611', N'a76f4462-b299-4ddf-94bb-4dab2f757611', NULL, NULL)
INSERT [GS].[Categorys] ([Id], [CreatedAt], [UpdatedAt], [DeletedAt], [IsDeleted], [IsActive], [TitleEn], [TitleBn], [DescriptionEn], [DescriptionBn], [logo], [UserId], [CreatedBy], [ModifiedBy], [DeletedBy]) VALUES (10015, CAST(N'2023-09-29T20:41:35.1720000' AS DateTime2), NULL, NULL, 0, 1, N'Shopping', N'', N'Schedule your Shopping task', N'', N'', N'a76f4462-b299-4ddf-94bb-4dab2f757611', N'a76f4462-b299-4ddf-94bb-4dab2f757611', NULL, NULL)
INSERT [GS].[Categorys] ([Id], [CreatedAt], [UpdatedAt], [DeletedAt], [IsDeleted], [IsActive], [TitleEn], [TitleBn], [DescriptionEn], [DescriptionBn], [logo], [UserId], [CreatedBy], [ModifiedBy], [DeletedBy]) VALUES (10016, CAST(N'2023-09-29T20:41:35.1720000' AS DateTime2), NULL, NULL, 0, 1, N'Office', N'', N'Schedule your Office task', N'', N'', N'a76f4462-b299-4ddf-94bb-4dab2f757611', N'a76f4462-b299-4ddf-94bb-4dab2f757611', NULL, NULL)
INSERT [GS].[Categorys] ([Id], [CreatedAt], [UpdatedAt], [DeletedAt], [IsDeleted], [IsActive], [TitleEn], [TitleBn], [DescriptionEn], [DescriptionBn], [logo], [UserId], [CreatedBy], [ModifiedBy], [DeletedBy]) VALUES (10017, CAST(N'2023-09-29T20:41:35.1720000' AS DateTime2), NULL, NULL, 0, 1, N'Miscellenious', N'', N'Schedule your Miscellenious task', N'', N'', N'a76f4462-b299-4ddf-94bb-4dab2f757611', N'a76f4462-b299-4ddf-94bb-4dab2f757611', NULL, NULL)
SET IDENTITY_INSERT [GS].[Categorys] OFF
GO
SET IDENTITY_INSERT [GS].[Reminders] ON 

INSERT [GS].[Reminders] ([Id], [CreatedAt], [UpdatedAt], [DeletedAt], [IsDeleted], [IsActive], [TodoId], [UserId], [RemindAt], [CreatedBy], [ModifiedBy], [DeletedBy]) VALUES (1, CAST(N'2023-09-17T18:49:16.4370000' AS DateTime2), NULL, NULL, 0, 1, 9, N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', CAST(N'2023-09-17T18:48:07.3830000' AS DateTime2), N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', NULL, NULL)
INSERT [GS].[Reminders] ([Id], [CreatedAt], [UpdatedAt], [DeletedAt], [IsDeleted], [IsActive], [TodoId], [UserId], [RemindAt], [CreatedBy], [ModifiedBy], [DeletedBy]) VALUES (3, CAST(N'2023-09-29T19:21:18.1050000' AS DateTime2), NULL, NULL, 0, 1, 9, N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', CAST(N'2023-10-17T18:48:07.3830000' AS DateTime2), N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', NULL, NULL)
INSERT [GS].[Reminders] ([Id], [CreatedAt], [UpdatedAt], [DeletedAt], [IsDeleted], [IsActive], [TodoId], [UserId], [RemindAt], [CreatedBy], [ModifiedBy], [DeletedBy]) VALUES (4, CAST(N'2023-09-29T19:28:03.9280000' AS DateTime2), NULL, NULL, 0, 1, 9, N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', CAST(N'2023-12-17T18:48:07.3830000' AS DateTime2), N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', NULL, NULL)
SET IDENTITY_INSERT [GS].[Reminders] OFF
GO
SET IDENTITY_INSERT [GS].[Todos] ON 

INSERT [GS].[Todos] ([Id], [CreatedAt], [UpdatedAt], [DeletedAt], [IsDeleted], [IsActive], [TitleEn], [TitleBn], [DescriptionEn], [DescriptionBn], [StartTime], [DueTime], [Priority], [HasReminder], [UserId], [CreatedBy], [ModifiedBy], [DeletedBy], [CategoryId]) VALUES (8, CAST(N'2023-09-17T18:47:49.7130000' AS DateTime2), NULL, NULL, 0, 1, N'string', N'string', N'string', N'string', CAST(N'2023-09-17T18:47:11.9860000' AS DateTime2), CAST(N'2023-09-17T18:47:11.9860000' AS DateTime2), 1, 0, N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'string', NULL, NULL, 35)
INSERT [GS].[Todos] ([Id], [CreatedAt], [UpdatedAt], [DeletedAt], [IsDeleted], [IsActive], [TitleEn], [TitleBn], [DescriptionEn], [DescriptionBn], [StartTime], [DueTime], [Priority], [HasReminder], [UserId], [CreatedBy], [ModifiedBy], [DeletedBy], [CategoryId]) VALUES (9, CAST(N'2023-09-17T18:48:56.2990000' AS DateTime2), NULL, NULL, 0, 1, N'string', N'string', N'string', N'string', CAST(N'2023-09-17T18:48:07.3830000' AS DateTime2), CAST(N'2023-09-17T18:48:07.3830000' AS DateTime2), 1, 1, N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', NULL, NULL, 35)
SET IDENTITY_INSERT [GS].[Todos] OFF
GO
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'05b9ec56-da36-4e4d-b15b-12e7d9fcb663', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'gN8bbtn4Ajf0MQToTiTP4HJRy+fp1Z4RBD5ZY+5GG5UlqfTAnqRPrK423MB+Mg2JKxIqJMQPYchKDYpaBp5rgQ==', CAST(N'2023-09-17T23:43:27.4505890' AS DateTime2), CAST(N'2023-09-18T23:43:27.4486254' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'0717523c-3537-4d0f-846d-4ad84d6ca787', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'YXv2B46jrWcs4Q2nqPqqW2FK02O4OF5aj1ytJfwL/aqFfaqIqpW0zvJNOUWvU/QMuHjfnUaIZhsGHkcxN+yrNg==', CAST(N'2023-09-30T00:53:06.5604210' AS DateTime2), CAST(N'2023-10-01T00:53:06.5603183' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'0d454a94-92e0-4b61-9d19-a1ca9a3bd7ef', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'uRZNeAdOPdDE/xLES0a+skwkMJKs1J4COHXaMwpWDwZFWIR2sTRLXp8iZaU9ztR7AEmrKwc32X1n3j/LfImroA==', CAST(N'2023-09-30T00:58:46.1939194' AS DateTime2), CAST(N'2023-10-01T00:58:46.1938496' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'10ae15d7-7662-4ed5-bc6c-5027e1677c1b', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'RFEH4NvPFGoZZNJE6y3/iZYNGp85Bh6Z6p/vApZdPh31YW7pecuYDGkrYRkhhL49BBfogo0Od9JkX9G/F7xh2w==', CAST(N'2023-09-30T01:43:49.3014110' AS DateTime2), CAST(N'2023-10-01T01:43:49.2993369' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'1667497c-5a3c-4d79-a13b-89184afe32ac', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'Lse/n0FR1HF+PIg4TghfpSCaTnrlLp5aY9CrM4s7c7YWEbd+rVq3ugHqiwE/wrVRS12a336L4UMgSdioNk4B5g==', CAST(N'2023-09-18T00:06:05.8279961' AS DateTime2), CAST(N'2023-09-19T00:06:05.8253511' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'181310f8-685a-4273-9e86-ab6dfbbf4dd7', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'Y+Gk693gldnCIYkw0bvL7e4K3FqO6n83JEXfMyUOLggmqREruSbagYX1nC7F055TeXD47C3Tqg+AjVimkZu1+g==', CAST(N'2023-09-30T23:05:41.2567061' AS DateTime2), CAST(N'2023-10-01T23:05:41.2545527' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'1c5771ab-dd93-43ce-9227-7aa8b862a1e6', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'LzX/mhw1p49AqHbhxp4MZmEjjIpvndNRPazzJS6hliHX2yrlY+0vEUbW0uNQq+1WiRG8LoYbwD0WBJ4/xAMhTA==', CAST(N'2023-09-18T00:40:36.9619642' AS DateTime2), CAST(N'2023-09-19T00:40:36.9618150' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'23a830df-25a3-492e-b828-a6159934212f', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'mKHDy3wshLYVrIrq4VvRxr46t47VmZTymhHMQUW6MVhfQnaPkatXr1hbFPu5qk/ZPTyWMngQkKPRVXHTjrBbNg==', CAST(N'2023-09-30T01:20:51.7026290' AS DateTime2), CAST(N'2023-10-01T01:20:51.7003522' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'23c64daa-a17f-4be9-8a80-7523881c5374', N'a76f4462-b299-4ddf-94bb-4dab2f757611', N'fsH/hIzKO73zJbU/ExkbWA/lM79TOCNEVI4GuKyBwydqEcGKHGVw1KNszxfFn9qXAKrus83euhNiQMaZqY3P7w==', CAST(N'2023-09-30T01:55:52.5246426' AS DateTime2), CAST(N'2023-10-01T01:55:52.5245646' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'3b9e331b-b079-4c91-bf1d-11e2762e296b', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'DZmVkhOMBkVmIpfw4P8w8T8Vli5U6rbjlL8bRMhU6Yujvl15ZXwvnPJYAjH93gGI35wGHlCBvnhEuyUcU8DoLA==', CAST(N'2023-09-18T00:14:11.5353016' AS DateTime2), CAST(N'2023-09-19T00:14:11.5331841' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'404bf2de-6c85-4377-90ec-1a14d75a4de6', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'sK/1ty0R+0KawDgGk6qEzgZtW3KM38kDNopa9fzQ7tO5IXMN/d/FxsDBC6qpVN7SVtl+MTWp4yKbY1gRLc7ixg==', CAST(N'2023-09-30T01:43:51.0970802' AS DateTime2), CAST(N'2023-10-01T01:43:51.0970292' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'491504b3-09e8-4a59-8f0c-f4903509cb6c', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'evev63MSzGg8C5jYO5YXziygd7N3eFk6bl+6gqaH8wcgke06c3SR/raOci3WD1OCjLjdBm6SgWlz6VelPSMI+Q==', CAST(N'2023-09-18T00:00:48.7991893' AS DateTime2), CAST(N'2023-09-19T00:00:48.7970907' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'59779020-0d3f-4c76-9878-3947c0f9dcb8', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'+0muBKf3rV7AQSD7QqbPaQb2WsTApDq+POaNycroEduscQq2KYNczZd8XJN1M03kiLyiik17/I92Oyk23KqGeA==', CAST(N'2023-09-17T23:58:58.1069185' AS DateTime2), CAST(N'2023-09-18T23:58:58.1047324' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'7602f26e-06d4-4e7a-b92b-717795adcd68', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'XBL/MlD2uUZCAGJN1NN9KwNat6XsnVswOcyA5rZUUpjjLb4D0gpOiNzZ93LDzu+RfLR5p9RYf5SQYAZLjd5cGA==', CAST(N'2023-09-30T02:47:31.2143385' AS DateTime2), CAST(N'2023-10-01T02:47:31.2122247' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'7c0367e7-dc5b-4a6a-8b8b-e517faf009d1', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'DFHqYRRBJf0EP6oMfZO9iE7yoQNqfOYhS7XbUAZxwAtCHgPwQlULDK7Ytf1w7Vyw+ZVt3moSibexTkO1sflrQw==', CAST(N'2023-09-17T23:51:14.3562952' AS DateTime2), CAST(N'2023-09-18T23:51:14.3561667' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'7d13d4dc-09c5-48a5-b1ff-7bb10260b3d3', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'gt7PL5kK/whU5trffONtrJ6XmOhED8yYTL6ZF5w8sqYbZF3RVL5+QDinoIeXRiXlY8CJqjoG+l0H66jK5yIa6g==', CAST(N'2023-09-30T23:15:49.1233062' AS DateTime2), CAST(N'2023-10-01T23:15:49.1232083' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'81cca836-298f-4ee5-8f4a-c123113446a6', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'L8fZx1NYL1W69ljOs64e1/I2nlMhcphS+KMpS+YX9wlP5BqFLxBJhnxyaUxIczDPH0ZH4lb/sc9KjuhilKDAOg==', CAST(N'2023-09-18T00:06:05.8280010' AS DateTime2), CAST(N'2023-09-19T00:06:05.8253595' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'825f1386-32e5-4d1a-b24c-96129c6050f8', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'PrlFRLrGR6rDx3YfQO17s671Lw0VTr3BVEMlGXPaKitND76LGf7H2WzH66erm1dDhLVkpDYRHW7fo6iyzquZ6w==', CAST(N'2023-09-17T23:51:12.3710362' AS DateTime2), CAST(N'2023-09-18T23:51:12.3689576' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'858196dd-70f0-4933-9327-46c8da663991', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'5NZ9uIBFwUpjuYFv5xAU2SXqCRbthpAf28d2qWzoU+k4m/2JFFOYefPBcX+22VZigPJ8CzX0aCJ4guKjt73m+w==', CAST(N'2023-09-30T01:20:51.7026340' AS DateTime2), CAST(N'2023-10-01T01:20:51.7003568' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'8f84147b-3d5b-4f35-bd5a-ca44f122cadb', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'nKsL0y9nPDeXgmoby8Ipi79NJalWqFbugKyM2QmscRmmwCNR0lejtZn1wDj9y8Xsc2AKjnBWaFVU/DRLmla6og==', CAST(N'2023-09-17T23:36:54.9845831' AS DateTime2), CAST(N'2023-09-18T23:36:54.9817622' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'912a2700-c329-43d2-b88e-8efab294b04b', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'RGMNko2kyMDo15d9BBhpR9YTWwBoxtccMhWQ6XiE7RggthYiMnwO1kSViMlsqcEkwMGDpUkWWWn2NZhbcRh2RQ==', CAST(N'2023-09-18T00:06:05.8279721' AS DateTime2), CAST(N'2023-09-19T00:06:05.8253348' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'9f3cf352-553e-462b-9258-c06905d1f913', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'7P+9PmoVW3k+4JV6lZ59acfpX4ES0QLks7aXMHq0Aa0WvKQWpb2V2hJznG8pTnej733gdbGNheSw9iFR8NOilw==', CAST(N'2023-09-18T00:40:35.5853701' AS DateTime2), CAST(N'2023-09-19T00:40:35.5833162' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'9fdd3633-85f9-4100-9380-900067ea2b03', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'W45egY6tsaAJx3hOrtsEus0qXas2TidOAeDbZJWpa2rbi4b21+E9AnzR6koO7X3sAzwS9vUNMhtsl+0IqhjI2A==', CAST(N'2023-09-30T02:50:54.6265808' AS DateTime2), CAST(N'2023-10-01T02:50:54.6264097' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'a1259231-f423-422c-bfa2-c1cb67b8a0b9', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'NGC4BQEKy5plFBxZc4MOTeVrIpVfDt5K/U6yyXQt9YqXORoZ8tsEVJXUXDIe0/ain0R2TGUOGdg8XWGIWWWPkA==', CAST(N'2023-09-30T23:15:44.6701404' AS DateTime2), CAST(N'2023-10-01T23:15:44.6682441' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'a7c821a1-585a-4060-bfbd-959f3277d1f2', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'vHLPiSD+UGkNJ+PiQT018mSIaNxoWPg7ZmJ0ScDrKVlOIdIFK/K7kVmiyu+r/tnvPS/gRmDxp4Lf9VQ7O/d9fQ==', CAST(N'2023-09-18T00:06:05.8279666' AS DateTime2), CAST(N'2023-09-19T00:06:05.8253429' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'a825c0c2-3e52-47de-b491-720f8001d929', N'a76f4462-b299-4ddf-94bb-4dab2f757611', N'o9fELPQEhyJY/T93Xumons81HDh2aEwsYG9Ycc0hu5kPMCShQVwFCsT50t1kSA/xZGEJTbCsVi0BTFkDGy+yFw==', CAST(N'2023-09-18T00:08:23.5316274' AS DateTime2), CAST(N'2023-09-19T00:08:23.5315132' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'a928afb6-7b69-4060-9fa3-c2b2e86c6d9b', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'bFTa625aAjupVweSWWA4dG6zJuvqHao27U+wnI9YIvp7gc7QMvp+a97XV7gz72H0vHHLMUlfOPzd1kJs9zoUOA==', CAST(N'2023-09-30T02:50:53.2427382' AS DateTime2), CAST(N'2023-10-01T02:50:53.2405646' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'adfe1356-f123-49d5-a209-49e86be65bd5', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'630UE/box7awIOJe/Q4JYMEamBI2kqSfTsG0SgN7WqCV17TDUefXRaqGkoQTXHG+icHCCWHXmyMr7USI+f/sMQ==', CAST(N'2023-09-30T00:58:43.1544639' AS DateTime2), CAST(N'2023-10-01T00:58:43.1525048' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'b338f700-1af9-4f79-998f-72021378ea4d', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'Lp0MTmyrBoD8gzim7dTxWcZpwDXHKM//K3oTnvfEFz91O13KJ8suQCh1B89OxBfCtJho3SU1liSDcZ9V/IFs/w==', CAST(N'2023-09-18T00:00:50.1460192' AS DateTime2), CAST(N'2023-09-19T00:00:50.1458516' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'b4f87d2f-318d-4d1d-a212-44f4d143a0f9', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'iojM10kI/VRadLYecc91BI+7KRvEKL0manO6PDvf1b9EKMaDH9jGLdjLIOfnjhOzo6KgD0T40WgvZFiNTq5Tgw==', CAST(N'2023-09-30T02:32:24.7138774' AS DateTime2), CAST(N'2023-10-01T02:32:24.7110784' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'ba8a594d-5ab7-47a5-b1b8-6ae96f36bb36', N'a76f4462-b299-4ddf-94bb-4dab2f757611', N'Zs4teetgxAmVAd10VeMOEl87WgulpCwAt2Skmlalzl6cjiCmJplq1GCAOYLBdlIKgM+IEx/+2mqqNGBWG5dLJA==', CAST(N'2023-09-30T02:39:59.5055185' AS DateTime2), CAST(N'2023-10-01T02:39:59.5036312' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'c4364283-1573-488f-9b32-60e870f7bc23', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'Xy7U/swdKGl+owNi497OD5q566oszYzi/vNy2ZZC4w+eYBqfgre7GX19l58wtjjyRwAzPeRp8iw1uXq2wr22ag==', CAST(N'2023-09-30T12:36:26.7046143' AS DateTime2), CAST(N'2023-10-01T12:36:26.7045956' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'cec7a584-0c05-4841-a94a-c15f6bb9f8ed', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'PX0Mx0lnQ2Es8ewk58r/QVvhqeOpaMdDP9ef/p8DdrXwsPPxR0xO55hREPi5McPIumjWnwVn1lvb04t6aLSl6g==', CAST(N'2023-09-30T00:53:01.2886397' AS DateTime2), CAST(N'2023-10-01T00:53:01.2859323' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'd032f07d-e547-468e-a11a-2f0dd745a7f8', N'a76f4462-b299-4ddf-94bb-4dab2f757611', N'ySP+i0OMRnNqZOV9pbNTQVTivJ5xyuePiqz7iPXU6KSNVko0p7vX914lYk9+Db1Pe1Qb1xQe21SDIVQEHUkWzg==', CAST(N'2023-09-30T02:40:59.0400829' AS DateTime2), CAST(N'2023-10-01T02:40:59.0400062' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'd75c3391-9cea-4cc0-9ade-4fa1143565ed', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'bNrytiEFphoTp/J3Gt8M5LjUCZpuHU1IqNO52eIQZLMdEy4ErqLcb4ZySphc3fgIPrbVsMOHQD+WnLWpEq9cxQ==', CAST(N'2023-09-30T01:54:49.8517336' AS DateTime2), CAST(N'2023-10-01T01:54:49.8492558' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'deb0c2f4-7364-4d35-b8a8-96ec2f71f512', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'BRhjFGmRdoiF7WYycn4l+vLYqdzQSBnbEtJzfUU1ET1DafIhf16xYSb0bz12ipHLW0OSo/0WTuKPMRmdA8R26w==', CAST(N'2023-09-18T00:14:12.8650170' AS DateTime2), CAST(N'2023-09-19T00:14:12.8649114' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'ee5bd2de-6f31-4dca-94d4-a026874b832e', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'KVmhgDiETLO9knN8Dx2FVe3t/ruSZLg2X8iPqMIimkeuAU/xRsUjrbd55imH1l3TJqVvAaYYpfJFzr5zs2awNg==', CAST(N'2023-09-30T23:05:44.0001947' AS DateTime2), CAST(N'2023-10-01T23:05:43.9997778' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'f77b37dc-e0b4-4362-81b4-c1fbdc66b88c', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'DWVzNTPv4MyZZpc77tMoR2GUSIhvfX86ZlrO6INLtFeXhVAYYiVaKl+xFj4Keq9hMTuo5X7eUVzsBnJ4Kulslg==', CAST(N'2023-09-17T23:58:59.4393274' AS DateTime2), CAST(N'2023-09-18T23:58:59.4392226' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'fb3ed188-db8d-4826-923c-b6b808915d83', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'v/JPAFSp/JS8CL34MNwnS2L/i5a7fwgLfdBdibZEryZap+WV5MS/BLRkJUST6DTV8LNvJ9M9B+rDfOLFkmlpGw==', CAST(N'2023-09-30T02:42:42.0011449' AS DateTime2), CAST(N'2023-10-01T02:42:42.0011328' AS DateTime2), 0, NULL)
INSERT [System].[RefreshToken] ([Id], [UserId], [Token], [CreatedAt], [ExpiresAt], [IsRevoked], [DeviceInfo]) VALUES (N'fc49a16c-d179-48df-8273-9787240dff0b', N'c8f3bd8d-ae83-4c0e-96e4-56387da46e62', N'0ActRK58eqTpGeGrds/B8Ilhi4dj64Y5c12fW2q0LTG45S5Jp2bY8tiJmiNrNkWRoiJ9ywqhYpCaQNsQlDIvYA==', CAST(N'2023-09-30T02:32:24.7138554' AS DateTime2), CAST(N'2023-10-01T02:32:24.7110803' AS DateTime2), 0, NULL)
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [GS].[Categorys]  WITH CHECK ADD  CONSTRAINT [FK_Categorys_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [GS].[Categorys] CHECK CONSTRAINT [FK_Categorys_AspNetUsers_UserId]
GO
ALTER TABLE [GS].[Reminders]  WITH CHECK ADD  CONSTRAINT [FK_Reminders_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [GS].[Reminders] CHECK CONSTRAINT [FK_Reminders_AspNetUsers_UserId]
GO
ALTER TABLE [GS].[Reminders]  WITH CHECK ADD  CONSTRAINT [FK_Reminders_Todos_TodoId] FOREIGN KEY([TodoId])
REFERENCES [GS].[Todos] ([Id])
GO
ALTER TABLE [GS].[Reminders] CHECK CONSTRAINT [FK_Reminders_Todos_TodoId]
GO
ALTER TABLE [GS].[Todos]  WITH CHECK ADD  CONSTRAINT [FK_Todos_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [GS].[Todos] CHECK CONSTRAINT [FK_Todos_AspNetUsers_UserId]
GO
ALTER TABLE [GS].[Todos]  WITH CHECK ADD  CONSTRAINT [FK_Todos_Categorys_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [GS].[Categorys] ([Id])
GO
ALTER TABLE [GS].[Todos] CHECK CONSTRAINT [FK_Todos_Categorys_CategoryId]
GO
