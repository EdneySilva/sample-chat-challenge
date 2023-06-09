USE [master]
GO
/****** Object:  Database [webchat]    Script Date: 3/24/2023 6:00:36 PM ******/
CREATE DATABASE [webchat]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'webchat', FILENAME = N'/var/opt/mssql/data/webchat.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'webchat_log', FILENAME = N'/var/opt/mssql/data/webchat_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [webchat] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [webchat].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [webchat] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [webchat] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [webchat] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [webchat] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [webchat] SET ARITHABORT OFF 
GO
ALTER DATABASE [webchat] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [webchat] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [webchat] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [webchat] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [webchat] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [webchat] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [webchat] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [webchat] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [webchat] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [webchat] SET  ENABLE_BROKER 
GO
ALTER DATABASE [webchat] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [webchat] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [webchat] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [webchat] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [webchat] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [webchat] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [webchat] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [webchat] SET RECOVERY FULL 
GO
ALTER DATABASE [webchat] SET  MULTI_USER 
GO
ALTER DATABASE [webchat] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [webchat] SET DB_CHAINING OFF 
GO
ALTER DATABASE [webchat] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [webchat] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [webchat] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [webchat] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'webchat', N'ON'
GO
ALTER DATABASE [webchat] SET QUERY_STORE = OFF
GO
USE [webchat]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/24/2023 6:00:36 PM ******/
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
/****** Object:  Table [dbo].[Accounts]    Script Date: 3/24/2023 6:00:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[UserName] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Thumbnail] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[CurrentConnectionId] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BotCommands]    Script Date: 3/24/2023 6:00:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BotCommands](
	[Command] [nvarchar](450) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Data] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_BotCommands] PRIMARY KEY CLUSTERED 
(
	[Command] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 3/24/2023 6:00:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[Id] [nvarchar](450) NOT NULL,
	[DisplayName] [nvarchar](max) NOT NULL,
	[Thumbnail] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Threads]    Script Date: 3/24/2023 6:00:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Threads](
	[MessageId] [nvarchar](450) NOT NULL,
	[Room] [nvarchar](max) NOT NULL,
	[From] [nvarchar](max) NOT NULL,
	[To] [nvarchar](max) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[ContentType] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[MessageDelivered] [datetime2](7) NULL,
	[MessageRead] [datetime2](7) NULL,
	[ChatRoomId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Threads] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersByRoom]    Script Date: 3/24/2023 6:00:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersByRoom](
	[RoomsId] [nvarchar](450) NOT NULL,
	[UsersUserName] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UsersByRoom] PRIMARY KEY CLUSTERED 
(
	[RoomsId] ASC,
	[UsersUserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Threads_ChatRoomId]    Script Date: 3/24/2023 6:00:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_Threads_ChatRoomId] ON [dbo].[Threads]
(
	[ChatRoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UsersByRoom_UsersUserName]    Script Date: 3/24/2023 6:00:36 PM ******/
CREATE NONCLUSTERED INDEX [IX_UsersByRoom_UsersUserName] ON [dbo].[UsersByRoom]
(
	[UsersUserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Accounts] ADD  DEFAULT (N'') FOR [Password]
GO
ALTER TABLE [dbo].[Threads]  WITH CHECK ADD  CONSTRAINT [FK_Threads_Rooms_ChatRoomId] FOREIGN KEY([ChatRoomId])
REFERENCES [dbo].[Rooms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Threads] CHECK CONSTRAINT [FK_Threads_Rooms_ChatRoomId]
GO
ALTER TABLE [dbo].[UsersByRoom]  WITH CHECK ADD  CONSTRAINT [FK_UsersByRoom_Accounts_UsersUserName] FOREIGN KEY([UsersUserName])
REFERENCES [dbo].[Accounts] ([UserName])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersByRoom] CHECK CONSTRAINT [FK_UsersByRoom_Accounts_UsersUserName]
GO
ALTER TABLE [dbo].[UsersByRoom]  WITH CHECK ADD  CONSTRAINT [FK_UsersByRoom_Rooms_RoomsId] FOREIGN KEY([RoomsId])
REFERENCES [dbo].[Rooms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersByRoom] CHECK CONSTRAINT [FK_UsersByRoom_Rooms_RoomsId]
GO
USE [master]
GO
ALTER DATABASE [webchat] SET  READ_WRITE 
GO
