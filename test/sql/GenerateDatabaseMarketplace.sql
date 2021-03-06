USE [master]
GO
/****** Object:  Database [MarketplaceContext]    Script Date: 14/07/2019 21:44:58 ******/
CREATE DATABASE [MarketplaceContext]
GO
ALTER DATABASE [MarketplaceContext] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MarketplaceContext].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MarketplaceContext] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MarketplaceContext] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MarketplaceContext] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MarketplaceContext] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MarketplaceContext] SET ARITHABORT OFF 
GO
ALTER DATABASE [MarketplaceContext] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MarketplaceContext] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MarketplaceContext] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MarketplaceContext] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MarketplaceContext] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MarketplaceContext] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MarketplaceContext] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MarketplaceContext] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MarketplaceContext] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MarketplaceContext] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MarketplaceContext] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MarketplaceContext] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MarketplaceContext] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MarketplaceContext] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MarketplaceContext] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MarketplaceContext] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MarketplaceContext] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MarketplaceContext] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MarketplaceContext] SET  MULTI_USER 
GO
ALTER DATABASE [MarketplaceContext] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MarketplaceContext] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MarketplaceContext] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MarketplaceContext] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MarketplaceContext] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MarketplaceContext] SET QUERY_STORE = OFF
GO
USE [MarketplaceContext]
GO
/****** Object:  Table [dbo].[LogEntity]    Script Date: 14/07/2019 21:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogEntity](
	[LogEntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[EntityName] [varchar](100) NOT NULL,
	[EntityFullName] [varchar](255) NOT NULL,
	[EntityAssembly] [varchar](100) NOT NULL,
	[LogActionType] [varchar](10) NOT NULL,
	[OriginalValues] [varchar](1000) NOT NULL,
	[ChangedValues] [varchar](1000) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[TransactionId] [varchar](36) NOT NULL,
 CONSTRAINT [PK_LogEntity] PRIMARY KEY CLUSTERED 
(
	[LogEntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 14/07/2019 21:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[Customer] [varchar](150) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 14/07/2019 21:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[OrdemItemId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[OrdemItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 14/07/2019 21:44:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](50) NOT NULL,
	[Price] [decimal](5, 2) NOT NULL,
	[ProductCategory] [varchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[LastUpdatedAt] [datetime] NULL,
	[RowVersion] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Order]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Product]
GO
USE [master]
GO
ALTER DATABASE [MarketplaceContext] SET  READ_WRITE 
GO
