USE [master]
GO
/****** Object:  Database [BookStore]    Script Date: 01/06/2019 23:18:26 ******/
CREATE DATABASE [BookStore]
GO
ALTER DATABASE [BookStore] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BookStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BookStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BookStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BookStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BookStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BookStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [BookStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BookStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BookStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BookStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BookStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BookStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BookStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BookStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BookStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BookStore] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BookStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BookStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BookStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BookStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BookStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BookStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BookStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BookStore] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BookStore] SET  MULTI_USER 
GO
ALTER DATABASE [BookStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BookStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BookStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BookStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BookStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BookStore] SET QUERY_STORE = OFF
GO
USE [BookStore]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 01/06/2019 23:18:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[AuthorId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[CreatedAt] [datetime2] NOT NULL,
	[LastUpdatedAt] [datetime2] NULL,
	[RowVersion] [int] NOT NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Book]    Script Date: 01/06/2019 23:18:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[AuthorId] [int] NOT NULL,
	[CreatedAt] [datetime2] NOT NULL,
	[LastUpdatedAt] [datetime2] NULL,
	[RowVersion] [int] NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Author] ON 

INSERT [dbo].[Author] ([AuthorId], [FirstName], [LastName], [CreatedAt], [RowVersion]) VALUES (1, N'William', N'Shakespeare', GETDATE(), 1)
INSERT [dbo].[Author] ([AuthorId], [FirstName], [LastName], [CreatedAt], [RowVersion]) VALUES (2, N'Masashi', N'Kishimoto', GETDATE(), 1)
INSERT [dbo].[Author] ([AuthorId], [FirstName], [LastName], [CreatedAt], [RowVersion]) VALUES (3, N'Akira', N'Toriyama', GETDATE(), 1)
INSERT [dbo].[Author] ([AuthorId], [FirstName], [LastName], [CreatedAt], [RowVersion]) VALUES (4, N'Machado', N'Assis', GETDATE(), 1)
SET IDENTITY_INSERT [dbo].[Author] OFF
SET IDENTITY_INSERT [dbo].[Book] ON 

INSERT [dbo].[Book] ([BookId], [Title], [AuthorId], [CreatedAt], [RowVersion]) VALUES (1, N'Hamlet', 1, GETDATE(), 1)
INSERT [dbo].[Book] ([BookId], [Title], [AuthorId], [CreatedAt], [RowVersion]) VALUES (2, N'Othello', 1, GETDATE(), 1)
INSERT [dbo].[Book] ([BookId], [Title], [AuthorId], [CreatedAt], [RowVersion]) VALUES (3, N'MacBeth', 1, GETDATE(), 1)
INSERT [dbo].[Book] ([BookId], [Title], [AuthorId], [CreatedAt], [RowVersion]) VALUES (4, N'Naruto', 2, GETDATE(), 1)
INSERT [dbo].[Book] ([BookId], [Title], [AuthorId], [CreatedAt], [RowVersion]) VALUES (5, N'Dragon Ball', 3, GETDATE(), 1)
INSERT [dbo].[Book] ([BookId], [Title], [AuthorId], [CreatedAt], [RowVersion]) VALUES (6, N'Dom Casmurro', 4, GETDATE(), 1)
INSERT [dbo].[Book] ([BookId], [Title], [AuthorId], [CreatedAt], [RowVersion]) VALUES (7, N'Quincas Borba', 4, GETDATE(), 1)
INSERT [dbo].[Book] ([BookId], [Title], [AuthorId], [CreatedAt], [RowVersion]) VALUES (8, N'A mão e a luva', 4, GETDATE(), 1)
SET IDENTITY_INSERT [dbo].[Book] OFF
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Author] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Author] ([AuthorId])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Author]
GO
USE [master]
GO
ALTER DATABASE [BookStore] SET  READ_WRITE 
GO
