USE [master]
GO
/****** Object:  Database [Mobile-ecom]    Script Date: 4/3/2023 1:20:35 PM ******/
CREATE DATABASE [Mobile-ecom]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Mobile-ecom', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Mobile-ecom.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Mobile-ecom_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Mobile-ecom_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Mobile-ecom] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Mobile-ecom].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Mobile-ecom] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Mobile-ecom] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Mobile-ecom] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Mobile-ecom] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Mobile-ecom] SET ARITHABORT OFF 
GO
ALTER DATABASE [Mobile-ecom] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Mobile-ecom] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Mobile-ecom] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Mobile-ecom] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Mobile-ecom] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Mobile-ecom] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Mobile-ecom] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Mobile-ecom] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Mobile-ecom] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Mobile-ecom] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Mobile-ecom] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Mobile-ecom] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Mobile-ecom] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Mobile-ecom] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Mobile-ecom] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Mobile-ecom] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Mobile-ecom] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Mobile-ecom] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Mobile-ecom] SET  MULTI_USER 
GO
ALTER DATABASE [Mobile-ecom] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Mobile-ecom] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Mobile-ecom] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Mobile-ecom] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Mobile-ecom] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Mobile-ecom] SET QUERY_STORE = OFF
GO
USE [Mobile-ecom]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 4/3/2023 1:20:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[categories_id] [int] NOT NULL,
	[name] [nvarchar](max) NULL,
	[note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[categories_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 4/3/2023 1:20:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[customer_id] [varchar](10) NOT NULL,
	[type_id] [int] NULL,
	[image] [varchar](max) NULL,
	[name] [nvarchar](max) NULL,
	[address] [nvarchar](max) NULL,
	[phone] [varchar](50) NULL,
	[gender] [int] NULL,
	[credit_card] [varchar](10) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[customer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CusType]    Script Date: 4/3/2023 1:20:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CusType](
	[type_id] [int] NOT NULL,
	[name] [nvarchar](max) NULL,
	[discount] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailOrder]    Script Date: 4/3/2023 1:20:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailOrder](
	[detailorder_id] [varchar](10) NOT NULL,
	[product_id] [varchar](10) NULL,
	[quantity] [int] NULL,
	[price] [decimal](18, 2) NULL,
	[note] [nvarchar](max) NULL,
 CONSTRAINT [PK_DetailOrder] PRIMARY KEY CLUSTERED 
(
	[detailorder_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 4/3/2023 1:20:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[order_id] [varchar](10) NOT NULL,
	[customer_id] [varchar](10) NULL,
	[start_date] [datetime] NULL,
	[total] [decimal](18, 2) NULL,
	[payment_id] [int] NULL,
	[voucher_id] [varchar](10) NULL,
	[orderstatus_id] [int] NULL,
	[detailorder_id] [varchar](10) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 4/3/2023 1:20:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[orderstatus_id] [int] NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 
(
	[orderstatus_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 4/3/2023 1:20:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[payment_id] [int] NOT NULL,
	[name] [nvarchar](max) NULL,
	[note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 4/3/2023 1:20:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[product_id] [varchar](10) NOT NULL,
	[name] [nvarchar](max) NULL,
	[categories_id] [int] NULL,
	[description] [nvarchar](max) NULL,
	[price] [decimal](18, 2) NULL,
	[cost_price] [decimal](18, 2) NULL,
	[image] [nvarchar](max) NULL,
	[image_note] [nvarchar](max) NULL,
	[quantity] [int] NULL,
	[status_id] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 4/3/2023 1:20:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[role_id] [int] NOT NULL,
	[description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatusProduct]    Script Date: 4/3/2023 1:20:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusProduct](
	[status_id] [int] NOT NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_StatusProduct] PRIMARY KEY CLUSTERED 
(
	[status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/3/2023 1:20:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[user_id] [int] NOT NULL,
	[user_name] [varchar](50) NULL,
	[user_pass] [varchar](50) NULL,
	[email] [varchar](100) NULL,
	[phone] [varchar](50) NULL,
	[role_id] [int] NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 4/3/2023 1:20:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher](
	[voucher_id] [varchar](10) NOT NULL,
	[name] [nvarchar](max) NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[status] [bit] NULL,
	[value] [decimal](18, 2) NULL,
	[quantity] [int] NULL,
 CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED 
(
	[voucher_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Categories] ([categories_id], [name], [note]) VALUES (1, N'Samsung', NULL)
INSERT [dbo].[Categories] ([categories_id], [name], [note]) VALUES (2, N'Iphone', NULL)
GO
INSERT [dbo].[Product] ([product_id], [name], [categories_id], [description], [price], [cost_price], [image], [image_note], [quantity], [status_id]) VALUES (N'SP01', N'Samsung S23', 1, N'new', CAST(2500000.00 AS Decimal(18, 2)), CAST(100000.00 AS Decimal(18, 2)), N'1.png', N'2.png', 10, 1)
INSERT [dbo].[Product] ([product_id], [name], [categories_id], [description], [price], [cost_price], [image], [image_note], [quantity], [status_id]) VALUES (N'SP02', N'Iphone XS', 2, N'new', CAST(100000000.00 AS Decimal(18, 2)), CAST(2500000.00 AS Decimal(18, 2)), N'a.png', N'2.png', 10, 1)
GO
INSERT [dbo].[Role] ([role_id], [description]) VALUES (1, N'ADMIN')
INSERT [dbo].[Role] ([role_id], [description]) VALUES (2, N'ESS')
GO
INSERT [dbo].[StatusProduct] ([status_id], [description]) VALUES (1, N'còn hàng')
GO
INSERT [dbo].[User] ([user_id], [user_name], [user_pass], [email], [phone], [role_id], [status]) VALUES (1, N'admin', N'123', N'admin@gmail.com', N'113', 1, 1)
INSERT [dbo].[User] ([user_id], [user_name], [user_pass], [email], [phone], [role_id], [status]) VALUES (2, N'employee1', N'123', N'ac@gmail.com', N'112', 2, 1)
INSERT [dbo].[User] ([user_id], [user_name], [user_pass], [email], [phone], [role_id], [status]) VALUES (3, N'1', N'1', N'2', N'12331', 1, 1)
INSERT [dbo].[User] ([user_id], [user_name], [user_pass], [email], [phone], [role_id], [status]) VALUES (4, N'13', N'312', N'2411', N'41242', 1, 0)
INSERT [dbo].[User] ([user_id], [user_name], [user_pass], [email], [phone], [role_id], [status]) VALUES (5, N'3142', N'12', N'123', N'1', 2, 1)
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Type] FOREIGN KEY([type_id])
REFERENCES [dbo].[CusType] ([type_id])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Type]
GO
ALTER TABLE [dbo].[DetailOrder]  WITH CHECK ADD  CONSTRAINT [FK_DetailOrder_Product] FOREIGN KEY([product_id])
REFERENCES [dbo].[Product] ([product_id])
GO
ALTER TABLE [dbo].[DetailOrder] CHECK CONSTRAINT [FK_DetailOrder_Product]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([customer_id])
REFERENCES [dbo].[Customer] ([customer_id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_DetailOrder] FOREIGN KEY([detailorder_id])
REFERENCES [dbo].[DetailOrder] ([detailorder_id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_DetailOrder]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_OrderStatus] FOREIGN KEY([orderstatus_id])
REFERENCES [dbo].[OrderStatus] ([orderstatus_id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_OrderStatus]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Payment] FOREIGN KEY([payment_id])
REFERENCES [dbo].[Payment] ([payment_id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Payment]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Voucher] FOREIGN KEY([voucher_id])
REFERENCES [dbo].[Voucher] ([voucher_id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Voucher]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Categories] FOREIGN KEY([categories_id])
REFERENCES [dbo].[Categories] ([categories_id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Categories]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_StatusProduct] FOREIGN KEY([status_id])
REFERENCES [dbo].[StatusProduct] ([status_id])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_StatusProduct]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([role_id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [Mobile-ecom] SET  READ_WRITE 
GO
