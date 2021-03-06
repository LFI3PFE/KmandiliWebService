USE [master]
GO
/****** Object:  Database [KmandiliDB]    Script Date: 5/8/2017 11:09:48 AM ******/
CREATE DATABASE [KmandiliDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'KmandiliDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\KmandiliDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'KmandiliDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\KmandiliDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [KmandiliDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [KmandiliDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [KmandiliDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [KmandiliDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [KmandiliDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [KmandiliDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [KmandiliDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [KmandiliDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [KmandiliDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [KmandiliDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [KmandiliDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [KmandiliDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [KmandiliDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [KmandiliDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [KmandiliDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [KmandiliDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [KmandiliDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [KmandiliDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [KmandiliDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [KmandiliDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [KmandiliDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [KmandiliDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [KmandiliDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [KmandiliDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [KmandiliDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [KmandiliDB] SET  MULTI_USER 
GO
ALTER DATABASE [KmandiliDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [KmandiliDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [KmandiliDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [KmandiliDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [KmandiliDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [KmandiliDB] SET QUERY_STORE = OFF
GO
USE [KmandiliDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [KmandiliDB]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Number] [int] NULL,
	[Street] [varchar](max) NULL,
	[City] [varchar](max) NULL,
	[State] [varchar](max) NULL,
	[Country] [varchar](max) NULL,
	[ZipCode] [int] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DeleveryDelay]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeleveryDelay](
	[ID] [int] NOT NULL,
	[MinDelay] [int] NOT NULL,
	[MaxDelay] [int] NOT NULL,
 CONSTRAINT [PK_DeleveryDelay] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DeleveryMethod]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeleveryMethod](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DeleveryType] [varchar](max) NOT NULL,
 CONSTRAINT [PK_DeleveryMethod] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DeleveryMethodPaymentMethod]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeleveryMethodPaymentMethod](
	[PaymentMethod_FK] [int] NOT NULL,
	[DeleveryMethod_FK] [int] NOT NULL,
 CONSTRAINT [PK_DeleveryMethodPaymentMethod] PRIMARY KEY CLUSTERED 
(
	[PaymentMethod_FK] ASC,
	[DeleveryMethod_FK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Order]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Status_FK] [int] NOT NULL,
	[User_FK] [int] NOT NULL,
	[PastryShop_FK] [int] NOT NULL,
	[DeleveryMethod_FK] [int] NOT NULL,
	[PaymentMethod_FK] [int] NOT NULL,
	[SeenUser] [bit] NOT NULL,
	[SeenPastryShop] [bit] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderProduct]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderProduct](
	[Order_FK] [int] NOT NULL,
	[Product_FK] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_OrderProduct] PRIMARY KEY CLUSTERED 
(
	[Order_FK] ASC,
	[Product_FK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Parking]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parking](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParkingType] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Parking] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PastryDeleveryPayment]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PastryDeleveryPayment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PastryShopDeleveryMethod_FK] [int] NOT NULL,
	[Payment_FK] [int] NOT NULL,
 CONSTRAINT [PK_PastryDeleveryPayment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PastryShop]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PastryShop](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[ShortDesc] [varchar](max) NOT NULL,
	[LongDesc] [varchar](max) NOT NULL,
	[ProfilePic] [varchar](max) NOT NULL,
	[CoverPic] [varchar](max) NOT NULL,
	[NumberOfRatings] [int] NOT NULL,
	[RatingSum] [int] NOT NULL,
	[PriceRange_FK] [int] NOT NULL,
	[Address_FK] [int] NOT NULL,
	[Email] [varchar](max) NOT NULL,
	[Password] [varchar](max) NOT NULL,
 CONSTRAINT [PK_PastryShop] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PastryShopCategories]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PastryShopCategories](
	[Category_FK] [int] NOT NULL,
	[PastryShop_FK] [int] NOT NULL,
 CONSTRAINT [PK_PastryShopCategories] PRIMARY KEY CLUSTERED 
(
	[Category_FK] ASC,
	[PastryShop_FK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PastryShopDeleveryMethod]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PastryShopDeleveryMethod](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PastryShop_FK] [int] NOT NULL,
	[DeleveryMethod_FK] [int] NOT NULL,
	[DeleveryDelay_FK] [int] NOT NULL,
 CONSTRAINT [PK_PastryShopDeleveryMethod] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PastryShopPhoneNumber]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PastryShopPhoneNumber](
	[ID] [int] NOT NULL,
	[PastryShop_FK] [int] NOT NULL,
 CONSTRAINT [PK_PastryShopPhoneNumber] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Payment]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PaymentMethod] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhoneNumber]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneNumber](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Number] [char](8) NOT NULL,
	[PhoneNumberType_FK] [int] NOT NULL,
 CONSTRAINT [PK_PhoneNumber] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhoneNumberType]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneNumberType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](max) NOT NULL,
 CONSTRAINT [PK_PhoneNumberType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PointOfSale]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PointOfSale](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [date] NOT NULL,
	[PastryShop_FK] [int] NOT NULL,
	[ParkingType_FK] [int] NOT NULL,
	[Address_FK] [int] NOT NULL,
 CONSTRAINT [PK_PointOfSale] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PointOfSalePhoneNumber]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PointOfSalePhoneNumber](
	[ID] [int] NOT NULL,
	[PointOfSale_FK] [int] NOT NULL,
 CONSTRAINT [PK_PointOfSalePhoneNumber] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PriceRange]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceRange](
	[ID] [int] NOT NULL,
	[MinPriceRange] [float] NOT NULL,
	[MaxPriceRange] [float] NOT NULL,
 CONSTRAINT [PK_PriceRange] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Pic] [varchar](max) NOT NULL,
	[Price] [float] NOT NULL,
	[SaleUnit_FK] [int] NOT NULL,
	[Category_FK] [int] NOT NULL,
	[PastryShop_FK] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SaleUnit]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleUnit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Unit] [varchar](50) NOT NULL,
 CONSTRAINT [PK_SaleUnit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Status]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[LastName] [varchar](max) NOT NULL,
	[Email] [varchar](max) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[JoindDate] [date] NOT NULL,
	[Address_FK] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserPhoneNumber]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPhoneNumber](
	[ID] [int] NOT NULL,
	[User_FK] [int] NOT NULL,
 CONSTRAINT [PK_UserPhoneNumber] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WorkDay]    Script Date: 5/8/2017 11:09:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkDay](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Day] [int] NOT NULL,
	[OpenTime] [time](7) NOT NULL,
	[CloseTime] [time](7) NOT NULL,
	[PointOfSale_FK] [int] NOT NULL,
 CONSTRAINT [PK_WorkDay] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Address] ON 

INSERT [dbo].[Address] ([ID], [Number], [Street], [City], [State], [Country], [ZipCode]) VALUES (1, 32, N'Rue des Narcisses', N'La Marsa', N'Tunis', N'Tunisie', 2076)
INSERT [dbo].[Address] ([ID], [Number], [Street], [City], [State], [Country], [ZipCode]) VALUES (2, 56, N'Rue de l''Oued Madjerda', N'La Marsa', N'Tunis', N'Tunisie', 2076)
INSERT [dbo].[Address] ([ID], [Number], [Street], [City], [State], [Country], [ZipCode]) VALUES (3, 56, N'Rue De l''Oued Madjerda', N'La Marsa', N'Tunis', N'Tunisie', 2076)
INSERT [dbo].[Address] ([ID], [Number], [Street], [City], [State], [Country], [ZipCode]) VALUES (4, 33, N'Narcisse', N'marsa', N'tunis', N'tunisie', 2076)
INSERT [dbo].[Address] ([ID], [Number], [Street], [City], [State], [Country], [ZipCode]) VALUES (9, 23, N'asjl', N'asdjl', N'dalj', N'fajl', 5896)
INSERT [dbo].[Address] ([ID], [Number], [Street], [City], [State], [Country], [ZipCode]) VALUES (16, 56, N'dasj', N'dsakjh', N'daj', N'dasjl', 5896)
INSERT [dbo].[Address] ([ID], [Number], [Street], [City], [State], [Country], [ZipCode]) VALUES (19, 0, N'fdsf', N'sdjl', N'dasj', N'dashj', 2589)
SET IDENTITY_INSERT [dbo].[Address] OFF
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (1, N'Pâtisserie Tunisienne')
INSERT [dbo].[Category] ([ID], [CategoryName]) VALUES (2, N'Pâtisserie Française')
SET IDENTITY_INSERT [dbo].[Category] OFF
INSERT [dbo].[DeleveryDelay] ([ID], [MinDelay], [MaxDelay]) VALUES (1, 0, 0)
INSERT [dbo].[DeleveryDelay] ([ID], [MinDelay], [MaxDelay]) VALUES (2, 1, 2)
INSERT [dbo].[DeleveryDelay] ([ID], [MinDelay], [MaxDelay]) VALUES (3, 2, 6)
INSERT [dbo].[DeleveryDelay] ([ID], [MinDelay], [MaxDelay]) VALUES (4, 6, 15)
INSERT [dbo].[DeleveryDelay] ([ID], [MinDelay], [MaxDelay]) VALUES (5, 15, 30)
SET IDENTITY_INSERT [dbo].[DeleveryMethod] ON 

INSERT [dbo].[DeleveryMethod] ([ID], [DeleveryType]) VALUES (1, N'Sur Place')
INSERT [dbo].[DeleveryMethod] ([ID], [DeleveryType]) VALUES (2, N'Livraison Par Poste')
INSERT [dbo].[DeleveryMethod] ([ID], [DeleveryType]) VALUES (3, N'Livraison À Domicile')
SET IDENTITY_INSERT [dbo].[DeleveryMethod] OFF
INSERT [dbo].[DeleveryMethodPaymentMethod] ([PaymentMethod_FK], [DeleveryMethod_FK]) VALUES (1, 2)
INSERT [dbo].[DeleveryMethodPaymentMethod] ([PaymentMethod_FK], [DeleveryMethod_FK]) VALUES (1, 3)
INSERT [dbo].[DeleveryMethodPaymentMethod] ([PaymentMethod_FK], [DeleveryMethod_FK]) VALUES (2, 2)
INSERT [dbo].[DeleveryMethodPaymentMethod] ([PaymentMethod_FK], [DeleveryMethod_FK]) VALUES (2, 3)
INSERT [dbo].[DeleveryMethodPaymentMethod] ([PaymentMethod_FK], [DeleveryMethod_FK]) VALUES (3, 2)
INSERT [dbo].[DeleveryMethodPaymentMethod] ([PaymentMethod_FK], [DeleveryMethod_FK]) VALUES (3, 3)
INSERT [dbo].[DeleveryMethodPaymentMethod] ([PaymentMethod_FK], [DeleveryMethod_FK]) VALUES (4, 1)
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([ID], [Date], [Status_FK], [User_FK], [PastryShop_FK], [DeleveryMethod_FK], [PaymentMethod_FK], [SeenUser], [SeenPastryShop]) VALUES (25, CAST(N'2017-05-01T18:00:01.543' AS DateTime), 2, 1, 1, 3, 1, 0, 0)
INSERT [dbo].[Order] ([ID], [Date], [Status_FK], [User_FK], [PastryShop_FK], [DeleveryMethod_FK], [PaymentMethod_FK], [SeenUser], [SeenPastryShop]) VALUES (30, CAST(N'2017-05-05T02:25:25.567' AS DateTime), 2, 1, 1, 1, 4, 0, 0)
SET IDENTITY_INSERT [dbo].[Order] OFF
INSERT [dbo].[OrderProduct] ([Order_FK], [Product_FK], [Quantity]) VALUES (25, 6, 1)
INSERT [dbo].[OrderProduct] ([Order_FK], [Product_FK], [Quantity]) VALUES (30, 6, 1)
SET IDENTITY_INSERT [dbo].[Parking] ON 

INSERT [dbo].[Parking] ([ID], [ParkingType]) VALUES (1, N'Rue')
INSERT [dbo].[Parking] ([ID], [ParkingType]) VALUES (2, N'Sous-Sol')
INSERT [dbo].[Parking] ([ID], [ParkingType]) VALUES (3, N'Privé')
SET IDENTITY_INSERT [dbo].[Parking] OFF
SET IDENTITY_INSERT [dbo].[PastryDeleveryPayment] ON 

INSERT [dbo].[PastryDeleveryPayment] ([ID], [PastryShopDeleveryMethod_FK], [Payment_FK]) VALUES (13, 9, 2)
INSERT [dbo].[PastryDeleveryPayment] ([ID], [PastryShopDeleveryMethod_FK], [Payment_FK]) VALUES (14, 9, 3)
SET IDENTITY_INSERT [dbo].[PastryDeleveryPayment] OFF
SET IDENTITY_INSERT [dbo].[PastryShop] ON 

INSERT [dbo].[PastryShop] ([ID], [Name], [ShortDesc], [LongDesc], [ProfilePic], [CoverPic], [NumberOfRatings], [RatingSum], [PriceRange_FK], [Address_FK], [Email], [Password]) VALUES (1, N'Omy Mna', N'Patisserie Tunisienne', N'Patisserie Tunisienne, Description Longue Description ici. Depuit Juin 1998.', N'http://seifiisexpress.sytes.net:300/Uploads/f099b271-f0d0-42f1-97bc-52aeb18ca5a1.jpg', N'http://seifiisexpress.sytes.net:300/Uploads/3f35380c-d155-4be3-8c78-42ff6047d11f.jpg', 8, 36, 2, 2, N'patisserie.omy.mna@gmail.com', N'123457')
SET IDENTITY_INSERT [dbo].[PastryShop] OFF
INSERT [dbo].[PastryShopCategories] ([Category_FK], [PastryShop_FK]) VALUES (2, 1)
SET IDENTITY_INSERT [dbo].[PastryShopDeleveryMethod] ON 

INSERT [dbo].[PastryShopDeleveryMethod] ([ID], [PastryShop_FK], [DeleveryMethod_FK], [DeleveryDelay_FK]) VALUES (9, 1, 2, 1)
SET IDENTITY_INSERT [dbo].[PastryShopDeleveryMethod] OFF
INSERT [dbo].[PastryShopPhoneNumber] ([ID], [PastryShop_FK]) VALUES (15, 1)
INSERT [dbo].[PastryShopPhoneNumber] ([ID], [PastryShop_FK]) VALUES (17, 1)
INSERT [dbo].[PastryShopPhoneNumber] ([ID], [PastryShop_FK]) VALUES (18, 1)
SET IDENTITY_INSERT [dbo].[Payment] ON 

INSERT [dbo].[Payment] ([ID], [PaymentMethod]) VALUES (1, N'À La Livraison')
INSERT [dbo].[Payment] ([ID], [PaymentMethod]) VALUES (2, N'Virement Postal')
INSERT [dbo].[Payment] ([ID], [PaymentMethod]) VALUES (3, N'Virement Bancaire')
INSERT [dbo].[Payment] ([ID], [PaymentMethod]) VALUES (4, N'Sur Place')
SET IDENTITY_INSERT [dbo].[Payment] OFF
SET IDENTITY_INSERT [dbo].[PhoneNumber] ON 

INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (2, N'71749881', 2)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (4, N'26983424', 1)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (5, N'22705729', 1)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (6, N'71749881', 2)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (14, N'53057885', 2)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (15, N'53057885', 1)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (17, N'26983424', 1)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (18, N'71749881', 2)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (21, N'23658744', 1)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (22, N'25632589', 1)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (23, N'58963258', 2)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (24, N'58964521', 1)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (25, N'12000000', 1)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (26, N'13000000', 2)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (27, N'14000000', 1)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (29, N'25471145', 1)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (30, N'25478541', 1)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (34, N'54852635', 1)
INSERT [dbo].[PhoneNumber] ([ID], [Number], [PhoneNumberType_FK]) VALUES (38, N'00000000', 1)
SET IDENTITY_INSERT [dbo].[PhoneNumber] OFF
SET IDENTITY_INSERT [dbo].[PhoneNumberType] ON 

INSERT [dbo].[PhoneNumberType] ([ID], [Type]) VALUES (1, N'Portable')
INSERT [dbo].[PhoneNumberType] ([ID], [Type]) VALUES (2, N'Fixe')
SET IDENTITY_INSERT [dbo].[PhoneNumberType] OFF
SET IDENTITY_INSERT [dbo].[PointOfSale] ON 

INSERT [dbo].[PointOfSale] ([ID], [CreationDate], [PastryShop_FK], [ParkingType_FK], [Address_FK]) VALUES (1, CAST(N'2017-04-28' AS Date), 1, 1, 3)
INSERT [dbo].[PointOfSale] ([ID], [CreationDate], [PastryShop_FK], [ParkingType_FK], [Address_FK]) VALUES (13, CAST(N'2017-05-07' AS Date), 1, 1, 19)
SET IDENTITY_INSERT [dbo].[PointOfSale] OFF
INSERT [dbo].[PointOfSalePhoneNumber] ([ID], [PointOfSale_FK]) VALUES (4, 1)
INSERT [dbo].[PointOfSalePhoneNumber] ([ID], [PointOfSale_FK]) VALUES (38, 13)
INSERT [dbo].[PriceRange] ([ID], [MinPriceRange], [MaxPriceRange]) VALUES (1, 1, 10)
INSERT [dbo].[PriceRange] ([ID], [MinPriceRange], [MaxPriceRange]) VALUES (2, 1, 30)
INSERT [dbo].[PriceRange] ([ID], [MinPriceRange], [MaxPriceRange]) VALUES (3, 10, 50)
INSERT [dbo].[PriceRange] ([ID], [MinPriceRange], [MaxPriceRange]) VALUES (4, 50, 100)
INSERT [dbo].[PriceRange] ([ID], [MinPriceRange], [MaxPriceRange]) VALUES (5, 100, 0)
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [Name], [Description], [Pic], [Price], [SaleUnit_FK], [Category_FK], [PastryShop_FK]) VALUES (6, N'Makroud', N'Makroud au dattes', N'http://seifiisexpress.sytes.net:300/Uploads/5ef41982-cea9-4c68-a842-56be1ec4a773.jpg', 5, 1, 1, 1)
INSERT [dbo].[Product] ([ID], [Name], [Description], [Pic], [Price], [SaleUnit_FK], [Category_FK], [PastryShop_FK]) VALUES (7, N'Youyou', N'TestProd', N'http://seifiisexpress.sytes.net:300/Uploads/db53f44f-9712-4bd2-894f-c3712d5f4dcb.jpg', 10, 2, 1, 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[SaleUnit] ON 

INSERT [dbo].[SaleUnit] ([ID], [Unit]) VALUES (1, N'kg')
INSERT [dbo].[SaleUnit] ([ID], [Unit]) VALUES (2, N'Paquet')
SET IDENTITY_INSERT [dbo].[SaleUnit] OFF
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (1, N'En Attente')
INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (2, N'Acceptée')
INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (3, N'Refusée')
INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (4, N'Livrée')
INSERT [dbo].[Status] ([ID], [StatusName]) VALUES (5, N'Reçue')
SET IDENTITY_INSERT [dbo].[Status] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [Name], [LastName], [Email], [Password], [JoindDate], [Address_FK]) VALUES (1, N'seif', N'abdennadher', N'm.abdennadher.seif@gmail.com', N'123456', CAST(N'2017-05-07' AS Date), 1)
INSERT [dbo].[User] ([ID], [Name], [LastName], [Email], [Password], [JoindDate], [Address_FK]) VALUES (2, N'abdennadher', N'salma', N'salma1.abdennadher@gmail.com', N'salma1997', CAST(N'2017-04-30' AS Date), 4)
SET IDENTITY_INSERT [dbo].[User] OFF
INSERT [dbo].[UserPhoneNumber] ([ID], [User_FK]) VALUES (2, 1)
INSERT [dbo].[UserPhoneNumber] ([ID], [User_FK]) VALUES (5, 2)
INSERT [dbo].[UserPhoneNumber] ([ID], [User_FK]) VALUES (6, 2)
INSERT [dbo].[UserPhoneNumber] ([ID], [User_FK]) VALUES (14, 1)
SET IDENTITY_INSERT [dbo].[WorkDay] ON 

INSERT [dbo].[WorkDay] ([ID], [Day], [OpenTime], [CloseTime], [PointOfSale_FK]) VALUES (1, 1, CAST(N'09:00:00' AS Time), CAST(N'21:00:00' AS Time), 1)
INSERT [dbo].[WorkDay] ([ID], [Day], [OpenTime], [CloseTime], [PointOfSale_FK]) VALUES (2, 2, CAST(N'09:00:00' AS Time), CAST(N'21:00:00' AS Time), 1)
INSERT [dbo].[WorkDay] ([ID], [Day], [OpenTime], [CloseTime], [PointOfSale_FK]) VALUES (3, 3, CAST(N'09:00:00' AS Time), CAST(N'21:00:00' AS Time), 1)
INSERT [dbo].[WorkDay] ([ID], [Day], [OpenTime], [CloseTime], [PointOfSale_FK]) VALUES (4, 4, CAST(N'09:00:00' AS Time), CAST(N'21:00:00' AS Time), 1)
INSERT [dbo].[WorkDay] ([ID], [Day], [OpenTime], [CloseTime], [PointOfSale_FK]) VALUES (5, 5, CAST(N'09:00:00' AS Time), CAST(N'15:00:00' AS Time), 1)
INSERT [dbo].[WorkDay] ([ID], [Day], [OpenTime], [CloseTime], [PointOfSale_FK]) VALUES (6, 6, CAST(N'09:00:00' AS Time), CAST(N'13:00:00' AS Time), 1)
INSERT [dbo].[WorkDay] ([ID], [Day], [OpenTime], [CloseTime], [PointOfSale_FK]) VALUES (84, 1, CAST(N'00:00:00' AS Time), CAST(N'00:00:00' AS Time), 13)
INSERT [dbo].[WorkDay] ([ID], [Day], [OpenTime], [CloseTime], [PointOfSale_FK]) VALUES (85, 2, CAST(N'00:00:00' AS Time), CAST(N'00:00:00' AS Time), 13)
INSERT [dbo].[WorkDay] ([ID], [Day], [OpenTime], [CloseTime], [PointOfSale_FK]) VALUES (86, 3, CAST(N'00:00:00' AS Time), CAST(N'00:00:00' AS Time), 13)
INSERT [dbo].[WorkDay] ([ID], [Day], [OpenTime], [CloseTime], [PointOfSale_FK]) VALUES (87, 4, CAST(N'00:00:00' AS Time), CAST(N'00:00:00' AS Time), 13)
INSERT [dbo].[WorkDay] ([ID], [Day], [OpenTime], [CloseTime], [PointOfSale_FK]) VALUES (88, 5, CAST(N'00:00:00' AS Time), CAST(N'00:00:00' AS Time), 13)
INSERT [dbo].[WorkDay] ([ID], [Day], [OpenTime], [CloseTime], [PointOfSale_FK]) VALUES (89, 6, CAST(N'00:00:00' AS Time), CAST(N'00:00:00' AS Time), 13)
INSERT [dbo].[WorkDay] ([ID], [Day], [OpenTime], [CloseTime], [PointOfSale_FK]) VALUES (90, 7, CAST(N'00:00:00' AS Time), CAST(N'00:00:00' AS Time), 13)
SET IDENTITY_INSERT [dbo].[WorkDay] OFF
ALTER TABLE [dbo].[DeleveryMethodPaymentMethod]  WITH CHECK ADD  CONSTRAINT [FK_DeleveryMethodPaymentMethod_DeleveryMethod] FOREIGN KEY([DeleveryMethod_FK])
REFERENCES [dbo].[DeleveryMethod] ([ID])
GO
ALTER TABLE [dbo].[DeleveryMethodPaymentMethod] CHECK CONSTRAINT [FK_DeleveryMethodPaymentMethod_DeleveryMethod]
GO
ALTER TABLE [dbo].[DeleveryMethodPaymentMethod]  WITH CHECK ADD  CONSTRAINT [FK_DeleveryMethodPaymentMethod_Payment] FOREIGN KEY([PaymentMethod_FK])
REFERENCES [dbo].[Payment] ([ID])
GO
ALTER TABLE [dbo].[DeleveryMethodPaymentMethod] CHECK CONSTRAINT [FK_DeleveryMethodPaymentMethod_Payment]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_DeleveryMethod] FOREIGN KEY([DeleveryMethod_FK])
REFERENCES [dbo].[DeleveryMethod] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_DeleveryMethod]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_PastryShop] FOREIGN KEY([PastryShop_FK])
REFERENCES [dbo].[PastryShop] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_PastryShop]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Payment] FOREIGN KEY([PaymentMethod_FK])
REFERENCES [dbo].[Payment] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Payment]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Status] FOREIGN KEY([Status_FK])
REFERENCES [dbo].[Status] ([ID])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Status]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([User_FK])
REFERENCES [dbo].[User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderProduct]  WITH CHECK ADD  CONSTRAINT [FK_OrderProduct_Order] FOREIGN KEY([Order_FK])
REFERENCES [dbo].[Order] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderProduct] CHECK CONSTRAINT [FK_OrderProduct_Order]
GO
ALTER TABLE [dbo].[OrderProduct]  WITH CHECK ADD  CONSTRAINT [FK_OrderProduct_Product] FOREIGN KEY([Product_FK])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[OrderProduct] CHECK CONSTRAINT [FK_OrderProduct_Product]
GO
ALTER TABLE [dbo].[PastryDeleveryPayment]  WITH CHECK ADD  CONSTRAINT [FK_PastryDeleveryPayment_PastryShopDeleveryMethod] FOREIGN KEY([PastryShopDeleveryMethod_FK])
REFERENCES [dbo].[PastryShopDeleveryMethod] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PastryDeleveryPayment] CHECK CONSTRAINT [FK_PastryDeleveryPayment_PastryShopDeleveryMethod]
GO
ALTER TABLE [dbo].[PastryDeleveryPayment]  WITH CHECK ADD  CONSTRAINT [FK_PastryDeleveryPayment_Payment] FOREIGN KEY([Payment_FK])
REFERENCES [dbo].[Payment] ([ID])
GO
ALTER TABLE [dbo].[PastryDeleveryPayment] CHECK CONSTRAINT [FK_PastryDeleveryPayment_Payment]
GO
ALTER TABLE [dbo].[PastryShop]  WITH CHECK ADD  CONSTRAINT [FK_PastryShop_Address] FOREIGN KEY([Address_FK])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[PastryShop] CHECK CONSTRAINT [FK_PastryShop_Address]
GO
ALTER TABLE [dbo].[PastryShop]  WITH CHECK ADD  CONSTRAINT [FK_PastryShop_PriceRange] FOREIGN KEY([PriceRange_FK])
REFERENCES [dbo].[PriceRange] ([ID])
GO
ALTER TABLE [dbo].[PastryShop] CHECK CONSTRAINT [FK_PastryShop_PriceRange]
GO
ALTER TABLE [dbo].[PastryShopCategories]  WITH CHECK ADD  CONSTRAINT [FK_PastryShopCategories_Category] FOREIGN KEY([Category_FK])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[PastryShopCategories] CHECK CONSTRAINT [FK_PastryShopCategories_Category]
GO
ALTER TABLE [dbo].[PastryShopCategories]  WITH CHECK ADD  CONSTRAINT [FK_PastryShopCategories_PastryShop] FOREIGN KEY([PastryShop_FK])
REFERENCES [dbo].[PastryShop] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PastryShopCategories] CHECK CONSTRAINT [FK_PastryShopCategories_PastryShop]
GO
ALTER TABLE [dbo].[PastryShopDeleveryMethod]  WITH CHECK ADD  CONSTRAINT [FK_PastryShopDeleveryMethod_DeleveryDelay] FOREIGN KEY([DeleveryDelay_FK])
REFERENCES [dbo].[DeleveryDelay] ([ID])
GO
ALTER TABLE [dbo].[PastryShopDeleveryMethod] CHECK CONSTRAINT [FK_PastryShopDeleveryMethod_DeleveryDelay]
GO
ALTER TABLE [dbo].[PastryShopDeleveryMethod]  WITH CHECK ADD  CONSTRAINT [FK_PastryShopDeleveryMethod_DeleveryMethod] FOREIGN KEY([DeleveryMethod_FK])
REFERENCES [dbo].[DeleveryMethod] ([ID])
GO
ALTER TABLE [dbo].[PastryShopDeleveryMethod] CHECK CONSTRAINT [FK_PastryShopDeleveryMethod_DeleveryMethod]
GO
ALTER TABLE [dbo].[PastryShopDeleveryMethod]  WITH CHECK ADD  CONSTRAINT [FK_PastryShopDeleveryMethod_PastryShop] FOREIGN KEY([PastryShop_FK])
REFERENCES [dbo].[PastryShop] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PastryShopDeleveryMethod] CHECK CONSTRAINT [FK_PastryShopDeleveryMethod_PastryShop]
GO
ALTER TABLE [dbo].[PastryShopPhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK__PastryShopPh__ID__408F9238] FOREIGN KEY([ID])
REFERENCES [dbo].[PhoneNumber] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PastryShopPhoneNumber] CHECK CONSTRAINT [FK__PastryShopPh__ID__408F9238]
GO
ALTER TABLE [dbo].[PastryShopPhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_PastryShopPhoneNumber_PastryShop] FOREIGN KEY([PastryShop_FK])
REFERENCES [dbo].[PastryShop] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PastryShopPhoneNumber] CHECK CONSTRAINT [FK_PastryShopPhoneNumber_PastryShop]
GO
ALTER TABLE [dbo].[PhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_PhoneNumber_PhoneNumberType] FOREIGN KEY([PhoneNumberType_FK])
REFERENCES [dbo].[PhoneNumberType] ([ID])
GO
ALTER TABLE [dbo].[PhoneNumber] CHECK CONSTRAINT [FK_PhoneNumber_PhoneNumberType]
GO
ALTER TABLE [dbo].[PointOfSale]  WITH CHECK ADD  CONSTRAINT [FK_PointOfSale_Address] FOREIGN KEY([Address_FK])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[PointOfSale] CHECK CONSTRAINT [FK_PointOfSale_Address]
GO
ALTER TABLE [dbo].[PointOfSale]  WITH CHECK ADD  CONSTRAINT [FK_PointOfSale_Parking] FOREIGN KEY([ParkingType_FK])
REFERENCES [dbo].[Parking] ([ID])
GO
ALTER TABLE [dbo].[PointOfSale] CHECK CONSTRAINT [FK_PointOfSale_Parking]
GO
ALTER TABLE [dbo].[PointOfSale]  WITH CHECK ADD  CONSTRAINT [FK_PointOfSale_PastryShop] FOREIGN KEY([PastryShop_FK])
REFERENCES [dbo].[PastryShop] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PointOfSale] CHECK CONSTRAINT [FK_PointOfSale_PastryShop]
GO
ALTER TABLE [dbo].[PointOfSalePhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK__PointOfSaleP__ID__3F9B6DFF] FOREIGN KEY([ID])
REFERENCES [dbo].[PhoneNumber] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PointOfSalePhoneNumber] CHECK CONSTRAINT [FK__PointOfSaleP__ID__3F9B6DFF]
GO
ALTER TABLE [dbo].[PointOfSalePhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_PointOfSalePhoneNumber_PointOfSale] FOREIGN KEY([PointOfSale_FK])
REFERENCES [dbo].[PointOfSale] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PointOfSalePhoneNumber] CHECK CONSTRAINT [FK_PointOfSalePhoneNumber_PointOfSale]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([Category_FK])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_PastryShop] FOREIGN KEY([PastryShop_FK])
REFERENCES [dbo].[PastryShop] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_PastryShop]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_SaleUnit] FOREIGN KEY([SaleUnit_FK])
REFERENCES [dbo].[SaleUnit] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_SaleUnit]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [user_address_fk] FOREIGN KEY([Address_FK])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [user_address_fk]
GO
ALTER TABLE [dbo].[UserPhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK__UserPhoneNum__ID__3EA749C6] FOREIGN KEY([ID])
REFERENCES [dbo].[PhoneNumber] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserPhoneNumber] CHECK CONSTRAINT [FK__UserPhoneNum__ID__3EA749C6]
GO
ALTER TABLE [dbo].[UserPhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_UserPhoneNumber_User] FOREIGN KEY([User_FK])
REFERENCES [dbo].[User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserPhoneNumber] CHECK CONSTRAINT [FK_UserPhoneNumber_User]
GO
ALTER TABLE [dbo].[WorkDay]  WITH CHECK ADD  CONSTRAINT [FK_WorkDay_PointOfSale] FOREIGN KEY([PointOfSale_FK])
REFERENCES [dbo].[PointOfSale] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WorkDay] CHECK CONSTRAINT [FK_WorkDay_PointOfSale]
GO
USE [master]
GO
ALTER DATABASE [KmandiliDB] SET  READ_WRITE 
GO
