USE [master]
GO
/****** Object:  Database [StoreHonda]    Script Date: 06/10/2024 5:32:48 CH ******/
CREATE DATABASE [StoreHonda]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StoreHonda', FILENAME = N'D:\Program Files\MSSQL16.MSSQLSERVER\MSSQL\DATA\StoreHonda.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StoreHonda_log', FILENAME = N'D:\Program Files\MSSQL16.MSSQLSERVER\MSSQL\DATA\StoreHonda_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [StoreHonda] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StoreHonda].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StoreHonda] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StoreHonda] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StoreHonda] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StoreHonda] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StoreHonda] SET ARITHABORT OFF 
GO
ALTER DATABASE [StoreHonda] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StoreHonda] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StoreHonda] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StoreHonda] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StoreHonda] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StoreHonda] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StoreHonda] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StoreHonda] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StoreHonda] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StoreHonda] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StoreHonda] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StoreHonda] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StoreHonda] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StoreHonda] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StoreHonda] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StoreHonda] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StoreHonda] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StoreHonda] SET RECOVERY FULL 
GO
ALTER DATABASE [StoreHonda] SET  MULTI_USER 
GO
ALTER DATABASE [StoreHonda] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StoreHonda] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StoreHonda] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StoreHonda] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [StoreHonda] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [StoreHonda] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'StoreHonda', N'ON'
GO
ALTER DATABASE [StoreHonda] SET QUERY_STORE = ON
GO
ALTER DATABASE [StoreHonda] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [StoreHonda]
GO
/****** Object:  Table [dbo].[ChiTietGioHang]    Script Date: 06/10/2024 5:32:49 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietGioHang](
	[MaGioHang] [int] NOT NULL,
	[MaSanPham] [int] NOT NULL,
	[MaMauSac] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
 CONSTRAINT [PK_ChiTietGioHang] PRIMARY KEY CLUSTERED 
(
	[MaGioHang] ASC,
	[MaSanPham] ASC,
	[MaMauSac] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 06/10/2024 5:32:49 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDon](
	[MaHoaDon] [int] NOT NULL,
	[MaSanPham] [int] NOT NULL,
	[MaMauSac] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[Gia] [decimal](18, 0) NULL,
 CONSTRAINT [PK_ChiTietHoaDon] PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC,
	[MaSanPham] ASC,
	[MaMauSac] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DanhMuc]    Script Date: 06/10/2024 5:32:49 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhMuc](
	[MaDanhMuc] [int] IDENTITY(1,1) NOT NULL,
	[TenDanhMuc] [nvarchar](100) NOT NULL,
	[TrangThai] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDanhMuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GioHang]    Script Date: 06/10/2024 5:32:49 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GioHang](
	[MaGioHang] [int] IDENTITY(1,1) NOT NULL,
	[MaKhachHang] [int] NULL,
	[NgayTao] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaGioHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 06/10/2024 5:32:49 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[MaHoaDon] [int] IDENTITY(1,1) NOT NULL,
	[MaKhachHang] [int] NULL,
	[NgayLap] [date] NOT NULL,
	[TongTien] [decimal](18, 2) NOT NULL,
	[TrangThai] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 06/10/2024 5:32:49 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKhachHang] [int] IDENTITY(1,1) NOT NULL,
	[TenKhachHang] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[SoDienThoai] [nvarchar](15) NULL,
	[DiaChi] [nvarchar](255) NULL,
	[NgayTao] [date] NOT NULL,
	[MatKhau] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MauSac]    Script Date: 06/10/2024 5:32:49 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MauSac](
	[MaMauSac] [int] IDENTITY(1,1) NOT NULL,
	[TenMauSac] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaMauSac] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 06/10/2024 5:32:49 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSanPham] [int] IDENTITY(1,1) NOT NULL,
	[TenSanPham] [nvarchar](100) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[Gia] [decimal](18, 2) NOT NULL,
	[HinhAnh] [nvarchar](255) NULL,
	[MaDanhMuc] [int] NULL,
	[TrangThai] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSanPham] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham_MauSac]    Script Date: 06/10/2024 5:32:49 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham_MauSac](
	[MaSanPham] [int] NOT NULL,
	[MaMauSac] [int] NOT NULL,
	[HinhMauSac] [nvarchar](255) NULL,
	[TrangThai] [bit] NULL,
 CONSTRAINT [PK_SanPham_MauSac] PRIMARY KEY CLUSTERED 
(
	[MaSanPham] ASC,
	[MaMauSac] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoanAdmin]    Script Date: 06/10/2024 5:32:49 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoanAdmin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TaiKhoan] [nvarchar](50) NULL,
	[MatKhau] [nvarchar](50) NULL,
	[Ten] [nvarchar](50) NULL,
	[Email] [nchar](10) NULL,
 CONSTRAINT [PK_TaiKhoanAdmin] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ChiTietGioHang] ([MaGioHang], [MaSanPham], [MaMauSac], [SoLuong]) VALUES (2, 7, 3, 4)
GO
INSERT [dbo].[ChiTietHoaDon] ([MaHoaDon], [MaSanPham], [MaMauSac], [SoLuong], [Gia]) VALUES (1, 7, 1, 2, CAST(73921091 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHoaDon] ([MaHoaDon], [MaSanPham], [MaMauSac], [SoLuong], [Gia]) VALUES (1, 13, 6, 1, CAST(499000000 AS Decimal(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[DanhMuc] ON 

INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc], [TrangThai]) VALUES (1, N'Xe Máy', 1)
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc], [TrangThai]) VALUES (2, N'Ô tô', 1)
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc], [TrangThai]) VALUES (3, N'Xe đạp', 1)
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc], [TrangThai]) VALUES (1004, N'Xe Điện', 1)
SET IDENTITY_INSERT [dbo].[DanhMuc] OFF
GO
SET IDENTITY_INSERT [dbo].[GioHang] ON 

INSERT [dbo].[GioHang] ([MaGioHang], [MaKhachHang], [NgayTao]) VALUES (1, 1, CAST(N'2024-09-25' AS Date))
INSERT [dbo].[GioHang] ([MaGioHang], [MaKhachHang], [NgayTao]) VALUES (2, 2, CAST(N'2024-09-25' AS Date))
SET IDENTITY_INSERT [dbo].[GioHang] OFF
GO
SET IDENTITY_INSERT [dbo].[HoaDon] ON 

INSERT [dbo].[HoaDon] ([MaHoaDon], [MaKhachHang], [NgayLap], [TongTien], [TrangThai]) VALUES (1, 1, CAST(N'2024-10-03' AS Date), CAST(646842182.00 AS Decimal(18, 2)), N'Đã duyệt')
SET IDENTITY_INSERT [dbo].[HoaDon] OFF
GO
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [Email], [SoDienThoai], [DiaChi], [NgayTao], [MatKhau]) VALUES (1, N'khach 1', N'khach@gmail.com', N'011111111111', N'abc ho chi minh q1', CAST(N'0001-01-01' AS Date), N'123456')
INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [Email], [SoDienThoai], [DiaChi], [NgayTao], [MatKhau]) VALUES (2, N'test', N'test@gmail.com', N'123456789', N'cu chi thanh pho hcm', CAST(N'0001-01-01' AS Date), N'123')
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
GO
SET IDENTITY_INSERT [dbo].[MauSac] ON 

INSERT [dbo].[MauSac] ([MaMauSac], [TenMauSac]) VALUES (1, N'Trắng đen')
INSERT [dbo].[MauSac] ([MaMauSac], [TenMauSac]) VALUES (2, N'Xanh đen')
INSERT [dbo].[MauSac] ([MaMauSac], [TenMauSac]) VALUES (3, N'Đỏ đen')
INSERT [dbo].[MauSac] ([MaMauSac], [TenMauSac]) VALUES (4, N'Đen')
INSERT [dbo].[MauSac] ([MaMauSac], [TenMauSac]) VALUES (5, N'Xanh')
INSERT [dbo].[MauSac] ([MaMauSac], [TenMauSac]) VALUES (6, N'Đỏ')
INSERT [dbo].[MauSac] ([MaMauSac], [TenMauSac]) VALUES (7, N'Trắng')
SET IDENTITY_INSERT [dbo].[MauSac] OFF
GO
SET IDENTITY_INSERT [dbo].[SanPham] ON 

INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MoTa], [Gia], [HinhAnh], [MaDanhMuc], [TrangThai]) VALUES (6, N'SH350i', N'Tại Việt Nam, hình ảnh mẫu xe SH từ lâu đã trở thành biểu tượng cho tính đẳng cấp, sang trọng và sự hoàn hảo. Kế thừa những nét đặc trưng đó, mẫu xe SH350i ra mắt năm 2021 đã gây ấn tượng mạnh mẽ với vẻ đẹp đậm chất ""hiện đại công nghệ"" và “bề thế”. Với động cơ mạnh mẽ và thiết kế sang trọng nhất, cùng chi tiết phối màu mới gây ấn tượng, mẫu SH350i mới phô diễn được sức mạnh cùng khả năng vận hành đột phá, thể hiện đẳng cấp của chủ sở hữu, xứng đáng với vị trí ông hoàng trong phân khúc xe tay ga cao cấp tại Việt Nam.', CAST(150990000.00 AS Decimal(18, 2)), N'5.png', 1, 1)
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MoTa], [Gia], [HinhAnh], [MaDanhMuc], [TrangThai]) VALUES (7, N'SH160i/125i', N'Kế thừa tinh hoa của dòng xe SH với những đường nét thanh lịch, sang trọng mang hơi thở Châu Âu cùng động cơ cải tiến đột phá và công nghệ tiên tiến, SH160i/125i mới sở hữu diện mạo đầy ấn tượng và nổi bật.', CAST(73921091.00 AS Decimal(18, 2)), N'h9TheYxZITC0FtJOlGmK.png', 1, 1)
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MoTa], [Gia], [HinhAnh], [MaDanhMuc], [TrangThai]) VALUES (8, N'Sh mode 125', N'Thuộc phân khúc xe ga cao cấp và thừa hưởng thiết kế sang trọng nổi tiếng của dòng xe SH, Sh mode luôn được đánh giá cao nhờ kiểu dáng sang trọng, tinh tế tới từng đường nét, động cơ tiên tiến và các tiện nghi cao cấp xứng tầm phong cách sống thời thượng, đẳng cấp.', CAST(57132000.00 AS Decimal(18, 2)), N'3mJZ9NV7sBmWVJalt796.png', 1, 1)
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MoTa], [Gia], [HinhAnh], [MaDanhMuc], [TrangThai]) VALUES (9, N'Vario 160', N'Vario 160 lần đầu tiên được giới thiệu với thiết kế của mẫu xe ga thể thao độc đáo, năng động.', CAST(51990000.00 AS Decimal(18, 2)), N'OdEB73r6Io8GOwX51wTV.png', 1, 1)
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MoTa], [Gia], [HinhAnh], [MaDanhMuc], [TrangThai]) VALUES (10, N'Air Blade 160/125', N'Air Blade nay có thêm hai phiên bản mới cùng bảng màu đa dạng cho những cá tính riêng biệt.', CAST(42012000.00 AS Decimal(18, 2)), N'QxRl3u0g5F47OdyXMioE.png', 1, 1)
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MoTa], [Gia], [HinhAnh], [MaDanhMuc], [TrangThai]) VALUES (11, N'Vario 125', N'Vario 125 sở hữu thiết kế thể thao vô cùng trẻ trung ấn tượng, mang đậm dấu ấn cá nhân.', CAST(40735637.00 AS Decimal(18, 2)), N'rFb1TP0vIDYY5pQjIULy.png', 1, 1)
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MoTa], [Gia], [HinhAnh], [MaDanhMuc], [TrangThai]) VALUES (12, N'LEAD ABS', N'Sở hữu ngôn ngữ thiết kế hiện đại, màu sắc mới nổi bật, động cơ thế hệ mới nhất eSP+ cùng nhiều tiện ích mới vượt trội, Honda LEAD mới là sự kết hợp hoàn mỹ giữa vẻ đẹp thời thượng và công năng ưu việt, khẳng định vị thế một trong những mẫu xe thiết thực nhất trong lòng khách hàng.', CAST(39557455.00 AS Decimal(18, 2)), N'7ra4EOo6Nv3k96tNlo16.png', 1, 1)
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MoTa], [Gia], [HinhAnh], [MaDanhMuc], [TrangThai]) VALUES (13, N'CITY', N'Phong cách thiết kế thể thao đầy mạnh mẽ, cuốn hút là tuyên ngôn cho dấu ấn riêng đậm chất cá tính của chủ sở hữu. Honda City đón đầu mọi xu hướng, kiến tạo diện mạo mới đầy kiêu hãnh để bạn luôn là tâm điểm của mọi ánh nhìn', CAST(499000000.00 AS Decimal(18, 2)), N'vAbVibqaCLz6WjcVuCmc.png', 2, 1)
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MoTa], [Gia], [HinhAnh], [MaDanhMuc], [TrangThai]) VALUES (14, N'BR-V', N'Là cú nhảy vọt trong thiết kế của dòng xe MPV, Honda BR-V mang lại sự vững tin tuyệt đối với ngoại hình tổng thể hiện đại và mạnh mẽ', CAST(661000000.00 AS Decimal(18, 2)), N'y7gDmu9NFnawre66iwKx.png', 2, 1)
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MoTa], [Gia], [HinhAnh], [MaDanhMuc], [TrangThai]) VALUES (16, N'Xe Máy Điện Honda H12 Chính Hãng Honda Nhật Bản', N'an', CAST(23000000.00 AS Decimal(18, 2)), N'1687412715_h12_result.jpg', 3, NULL)
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MoTa], [Gia], [HinhAnh], [MaDanhMuc], [TrangThai]) VALUES (17, N'Xe Máy Điện Honda Q1 - Chính Hãng Honda Nhật Bản', N'vipp', CAST(25000000.00 AS Decimal(18, 2)), N'1687412931_q1_result.jpg', 3, NULL)
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MoTa], [Gia], [HinhAnh], [MaDanhMuc], [TrangThai]) VALUES (18, N'Xe Máy Điện Honda Q2 - Chính Hãng Honda Nhật Bản', N'Xe điện thích hợp cho giới trẻ', CAST(1900000000.00 AS Decimal(18, 2)), N'1701078484_Q2 (1).jpg', 3, NULL)
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MoTa], [Gia], [HinhAnh], [MaDanhMuc], [TrangThai]) VALUES (19, N'Xe Máy Điện Honda VSUN V2 - Chính Hãng Honda', N'xe điện ', CAST(15800000.00 AS Decimal(18, 2)), N'1590824328_honda-vsun-v2.jpg', 3, NULL)
INSERT [dbo].[SanPham] ([MaSanPham], [TenSanPham], [MoTa], [Gia], [HinhAnh], [MaDanhMuc], [TrangThai]) VALUES (1013, N'Honda Y', N'xe chiến', CAST(10000000.00 AS Decimal(18, 2)), N'avd.jpg', 1004, 1)
SET IDENTITY_INSERT [dbo].[SanPham] OFF
GO
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (6, 1, N'5.png', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (6, 3, N'0.png', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (7, 1, N'UT6CDKZPp1pnlSGUI0bI.jpg', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (7, 3, N'6.png', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (8, 2, N'6.png', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (8, 3, N'6.png', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (9, 2, N'6.png', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (9, 3, N'OdEB73r6Io8GOwX51wTV.png', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (10, 2, N'11.png', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (10, 3, N'11.png', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (11, 4, N'6.png', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (12, 5, N'6.png', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (12, 6, N'6.png', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (13, 6, N'1.png', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (13, 7, N'1.png', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (14, 7, N'1png', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (1013, 1, N'abc.jpg', 1)
INSERT [dbo].[SanPham_MauSac] ([MaSanPham], [MaMauSac], [HinhMauSac], [TrangThai]) VALUES (1013, 4, N'avd.jpg', 1)
GO
SET IDENTITY_INSERT [dbo].[TaiKhoanAdmin] ON 

INSERT [dbo].[TaiKhoanAdmin] ([ID], [TaiKhoan], [MatKhau], [Ten], [Email]) VALUES (2, N'admin', N'123456', N'Admin', N'admin     ')
SET IDENTITY_INSERT [dbo].[TaiKhoanAdmin] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__KhachHan__A9D105347D002944]    Script Date: 06/10/2024 5:32:49 CH ******/
ALTER TABLE [dbo].[KhachHang] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GioHang] ADD  DEFAULT (getdate()) FOR [NgayTao]
GO
ALTER TABLE [dbo].[HoaDon] ADD  DEFAULT (getdate()) FOR [NgayLap]
GO
ALTER TABLE [dbo].[KhachHang] ADD  DEFAULT (getdate()) FOR [NgayTao]
GO
ALTER TABLE [dbo].[ChiTietGioHang]  WITH CHECK ADD  CONSTRAINT [FK_GioHang] FOREIGN KEY([MaGioHang])
REFERENCES [dbo].[GioHang] ([MaGioHang])
GO
ALTER TABLE [dbo].[ChiTietGioHang] CHECK CONSTRAINT [FK_GioHang]
GO
ALTER TABLE [dbo].[ChiTietGioHang]  WITH CHECK ADD  CONSTRAINT [FK_MauSac_CTGH] FOREIGN KEY([MaMauSac])
REFERENCES [dbo].[MauSac] ([MaMauSac])
GO
ALTER TABLE [dbo].[ChiTietGioHang] CHECK CONSTRAINT [FK_MauSac_CTGH]
GO
ALTER TABLE [dbo].[ChiTietGioHang]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_CTGH] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[ChiTietGioHang] CHECK CONSTRAINT [FK_SanPham_CTGH]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon] FOREIGN KEY([MaHoaDon])
REFERENCES [dbo].[HoaDon] ([MaHoaDon])
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_HoaDon]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_MauSac_CTHD] FOREIGN KEY([MaMauSac])
REFERENCES [dbo].[MauSac] ([MaMauSac])
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_MauSac_CTHD]
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD  CONSTRAINT [FK_SanPham_CTHD] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[ChiTietHoaDon] CHECK CONSTRAINT [FK_SanPham_CTHD]
GO
ALTER TABLE [dbo].[GioHang]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang_GH] FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[KhachHang] ([MaKhachHang])
GO
ALTER TABLE [dbo].[GioHang] CHECK CONSTRAINT [FK_KhachHang_GH]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang] FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[KhachHang] ([MaKhachHang])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_KhachHang]
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD  CONSTRAINT [FK_DanhMuc] FOREIGN KEY([MaDanhMuc])
REFERENCES [dbo].[DanhMuc] ([MaDanhMuc])
GO
ALTER TABLE [dbo].[SanPham] CHECK CONSTRAINT [FK_DanhMuc]
GO
ALTER TABLE [dbo].[SanPham_MauSac]  WITH CHECK ADD  CONSTRAINT [FK_MauSac] FOREIGN KEY([MaMauSac])
REFERENCES [dbo].[MauSac] ([MaMauSac])
GO
ALTER TABLE [dbo].[SanPham_MauSac] CHECK CONSTRAINT [FK_MauSac]
GO
ALTER TABLE [dbo].[SanPham_MauSac]  WITH CHECK ADD  CONSTRAINT [FK_SanPham] FOREIGN KEY([MaSanPham])
REFERENCES [dbo].[SanPham] ([MaSanPham])
GO
ALTER TABLE [dbo].[SanPham_MauSac] CHECK CONSTRAINT [FK_SanPham]
GO
USE [master]
GO
ALTER DATABASE [StoreHonda] SET  READ_WRITE 
GO
