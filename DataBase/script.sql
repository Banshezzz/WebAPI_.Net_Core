USE [master]
GO
/****** Object:  Database [Bank_System]    Script Date: 10/30/2022 3:23:50 PM ******/
CREATE DATABASE [Bank_System]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Bank_System', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Bank_System.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Bank_System_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Bank_System_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Bank_System] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Bank_System].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Bank_System] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Bank_System] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Bank_System] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Bank_System] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Bank_System] SET ARITHABORT OFF 
GO
ALTER DATABASE [Bank_System] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Bank_System] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Bank_System] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Bank_System] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Bank_System] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Bank_System] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Bank_System] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Bank_System] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Bank_System] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Bank_System] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Bank_System] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Bank_System] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Bank_System] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Bank_System] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Bank_System] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Bank_System] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Bank_System] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Bank_System] SET RECOVERY FULL 
GO
ALTER DATABASE [Bank_System] SET  MULTI_USER 
GO
ALTER DATABASE [Bank_System] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Bank_System] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Bank_System] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Bank_System] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Bank_System] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Bank_System] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Bank_System', N'ON'
GO
ALTER DATABASE [Bank_System] SET QUERY_STORE = OFF
GO
USE [Bank_System]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 10/30/2022 3:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Username] [nvarchar](50) NOT NULL,
	[PasswordHash] [varbinary](200) NOT NULL,
	[PasswordSalt] [varbinary](200) NOT NULL,
	[Status] [int] NOT NULL,
	[BankCode] [int] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [int] NULL,
	[Birthday] [date] NULL,
	[Address] [nvarchar](50) NULL,
	[PassportId] [nchar](20) NOT NULL,
 CONSTRAINT [PK_Account_1] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bank]    Script Date: 10/30/2022 3:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bank](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BankName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Passport]    Script Date: 10/30/2022 3:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Passport](
	[Username] [nvarchar](50) NOT NULL,
	[Front] [nvarchar](255) NOT NULL,
	[Back] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Passport] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supporter]    Script Date: 10/30/2022 3:23:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supporter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [int] NULL,
	[BankCode] [int] NOT NULL,
 CONSTRAINT [PK_Supporter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Account] ([Username], [PasswordHash], [PasswordSalt], [Status], [BankCode], [Email], [Phone], [Birthday], [Address], [PassportId]) VALUES (N'string', 0xF2A1279BFA0F3BDD06E2B4D800D93EDE0E76315410A65A52E30FD757582B928D56A3CA3375CFB7DA9FA798569DDC7F46B56F5AFED1C64D07C03701E7C1270D36, 0x0B45E87543E3F8487473D65F50E53A9E7A99BD02762ADE7D050B66F526DA8E55BF444E37A56A9A9BFE237D78F3AF19A0FF117D23FC7A7798C681842ACC8AC90D67DF1E407878747A019F566C9525AC9718D4C2E199AE0360323FD9882BB2D8090342A7CC90B7196B5EC81C89EF20A9B97A025A8013BE2DEDD340AEC2A4A76094, 0, 1, N'string', 0, NULL, NULL, N'string1             ')
GO
SET IDENTITY_INSERT [dbo].[Bank] ON 

INSERT [dbo].[Bank] ([Id], [BankName]) VALUES (1, N'Vietinbank')
INSERT [dbo].[Bank] ([Id], [BankName]) VALUES (2, N'ACB')
SET IDENTITY_INSERT [dbo].[Bank] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Passport]    Script Date: 10/30/2022 3:23:50 PM ******/
ALTER TABLE [dbo].[Passport] ADD  CONSTRAINT [IX_Passport] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Bank] FOREIGN KEY([BankCode])
REFERENCES [dbo].[Bank] ([Id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Bank]
GO
ALTER TABLE [dbo].[Passport]  WITH NOCHECK ADD  CONSTRAINT [FK_Passport_Account] FOREIGN KEY([Username])
REFERENCES [dbo].[Account] ([Username])
GO
ALTER TABLE [dbo].[Passport] CHECK CONSTRAINT [FK_Passport_Account]
GO
ALTER TABLE [dbo].[Supporter]  WITH CHECK ADD  CONSTRAINT [FK_Supporter_Bank] FOREIGN KEY([BankCode])
REFERENCES [dbo].[Bank] ([Id])
GO
ALTER TABLE [dbo].[Supporter] CHECK CONSTRAINT [FK_Supporter_Bank]
GO
USE [master]
GO
ALTER DATABASE [Bank_System] SET  READ_WRITE 
GO
