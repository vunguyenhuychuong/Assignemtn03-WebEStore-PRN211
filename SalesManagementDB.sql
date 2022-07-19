
USE master

CREATE DATABASE SalesManagementDB
GO

USE SalesManagementDB;

CREATE TABLE [dbo].[Member](
	[MemberId][INT]IDENTITY(1,1) NOT NULL,
	[Email][varchar](100) NOT NULL,
	[CompanyName][varchar](40) NOT NULL,
	[City][varchar](15) NOT NULL,
	[Country][varchar](15) NOT NULL,
	[Password][varchar](30) NOT NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


INSERT INTO [dbo].[Member] ([Email],[CompanyName],[City],[Country],[Password])
values
('thanhhiep@gmail.com', 'FPT', 'HCM', 'Viet Nam', '12345678'),
('chuong@gmail.com', 'FPT', 'Ninh Thuan', 'Viet Nam', '1010'),
('tai@gmail.com', 'FPT', 'Tien Giang', 'Viet Nam', '12345678')
GO

SELECT * FROM Member
SELECT * FROM [dbo].[Order]
DELETE FROM Member where MemberId = 2

DELETE FROM [dbo].[Order] WHERE OrderId = 2
go
-- --------------------------------------------------
CREATE TABLE [dbo].[Order](
	[OrderId][INT] IDENTITY(1,1),
	[MemberId][INT] NOT NULL,
	[OrderDate][datetime] NOT NULL,
	[RequiredDate][datetime] NULL,
	[ShippedDate][datetime] NULL,
	[Freight][money] NULL
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


INSERT INTO [dbo].[Order](MemberId, OrderDate, RequiredDate, ShippedDate, Freight)
values
(16, '2022-06-25 10:30:30', '2022-07-25 10:30:30', '2022-07-10 10:30:30', 100),
(16, '2022-06-26 10:30:30', '2022-07-26 10:30:30', '2022-07-11 10:30:30', 200),
(16, '2022-06-27 10:30:30', '2022-07-27 10:30:30', '2022-07-12 10:30:30', 300)
GO

-- -------------------------------

CREATE TABLE [dbo].[OrderDetail](
	[OrderId][INT],
	[ProductId][INT] NOT NULL,
	[UnitPrice][money] NOT NULL,
	[Quantity][int] NOT NULL,
	[Discount][float] NOT NULL
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO OrderDetail (OrderId, ProductId, UnitPrice, Quantity, Discount)
values
(16, 11, 40000, 8, 20)
GO

-- ----------------------------
CREATE TABLE [dbo].[Product](
	[ProductId][INT] IDENTITY(1,1),
	[CategoryId][INT] NOT NULL,
	[ProductName][varchar](40) NOT NULL,
	[Weight][varchar](20) NOT NULL,
	[UnitPrice][money] NOT NULL,
	[UnitslnStock][INT] NOT NULL
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


INSERT INTO Product (CategoryId, ProductName, Weight, UnitPrice, UnitslnStock)
values
(1, 'Bắp Xú', '2KG', 30000, 100),
(2, 'Cải mầm', '6 KG', 25000, 200),
(3, 'Rau Diếp', '2 KG', 40000, 300),
(4, 'Xà lách', '3 KG', 20000, 400),
(5, 'Lá hẹ', '4 KG', 50000, 500)
GO
SELECT * FROM Product



ALTER TABLE [dbo].[Order] ADD CONSTRAINT FK_MemberId FOREIGN KEY (MemberId) REFERENCES [dbo].[Member](MemberId)

ALTER TABLE [dbo].[OrderDetail] ADD CONSTRAINT FK_OrderId FOREIGN KEY (OrderId) REFERENCES [dbo].[Order](OrderId)

ALTER TABLE [dbo].[OrderDetail] ADD CONSTRAINT FK_ProductId FOREIGN KEY (ProductId) REFERENCES [dbo].[Product](ProductId)

SELECT * FROM Member
SELECT * FROM [dbo].[Order]
SELECT * FROM [OrderDetail]
SELECT * FROM [Product]

SELECT * FROM [dbo].[Order] where MemberId = 15
SELECT * FROM [dbo].[Order] where OrderId = 18
