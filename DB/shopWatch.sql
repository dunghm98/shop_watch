-- ****************** SqlDBM: Microsoft SQL Server ******************
-- ******************************************************************
USE master
GO
CREATE DATABASE shop_watch;
GO
USE shop_watch;
GO
-- ************************************** [categories]

CREATE TABLE [categories]
(
 [ID]          int NOT NULL IDENTITY(1,1) PRIMARY KEY,
 [name]        nvarchar(100) NOT NULL ,
 [description] ntext NULL ,
 [parentID]    int NULL ,
 [order]	int NULL,
 [is_visible]	bit NULL,
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO

-- ************************************** [dbo].[products]
CREATE TABLE [products]
(
 [ID]                     bigint NOT NULL IDENTITY(1,1) PRIMARY KEY,
 [is_enable]			  bit not null,
 [name]                   nvarchar(50) NOT NULL ,
 [cost]					  money NOT NULL ,
 [image]				  text null,
 [origin]                 nvarchar(100) NULL ,
 [machine_type]           nvarchar(50) NULL ,
 [for_gender]             int,
 [size]                   varchar(50) NULL ,
 [height]                 varchar(50) NULL ,
 [shell_material]         nvarchar(50) NULL ,
 [chain_material]         nvarchar(50) NULL ,
 [glasses_material]       nvarchar(50) NULL ,
 [functions]              nvarchar(255) NULL ,
 [water_resistance_lv]    char(50) NULL ,
 [international_warranty] nvarchar(50) NULL ,
 [description]            ntext NULL ,
 [short_description]      ntext NULL ,
 [is_out_of_stock]	      bit NULL ,
 [sale]					  float NULL ,
 [brand_id]               int NOT NULL ,
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO

-- ************************************** [dbo].[product_category]
CREATE TABLE [product_category]
(
 [ID]	int not null IDENTITY(1,1) PRIMARY KEY,
 [product_id]	int not null,
 [category_id]	int not null,
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO
-- ************************************** [chinh_sach_bao_hanh]
CREATE TABLE [chinh_sach_bao_hanh]
(
 [ID]         int NOT NULL IDENTITY(1,1) PRIMARY KEY,
 [noi_dung]   nvarchar(250) NOT NULL ,
 [chinh_sach] bit NOT NULL ,
 [shop_watch_id] int not null ,
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO

-- ************************************** [brands]
CREATE TABLE [brands]
(
 [ID]          int NOT NULL IDENTITY(1,1) PRIMARY KEY,
 [name]        nvarchar(250) NOT NULL ,
 [logo]        ntext NULL ,
 [description] ntext NULL ,
 [KOL]         int NOT NULL,
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO

-- ************************************** [menus]
CREATE TABLE [menus]
(
 [ID]          int NOT NULL IDENTITY(1,1) PRIMARY KEY,
 [name]        nvarchar(100) NOT NULL ,
 [description] text NULL ,
 [parentID]    int NULL ,
 [url]         varchar(100) NULL ,
 [order]       int NULL ,
 [is_visible]  bit NULL ,
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO

-- ************************************** [knowledges]
CREATE TABLE [knowledges]
(
 [ID]          int NOT NULL IDENTITY(1,1) PRIMARY KEY,
 [title]       nvarchar(250) NOT NULL ,
 [content]     text NULL ,
 [category_id] int NOT NULL ,
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO

-- ************************************** [open_box_videos]
CREATE TABLE [open_box_videos]
(
 [ID]      int NOT NULL IDENTITY(1,1) PRIMARY KEY,
 [content] text NOT NULL , 
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO

-- ************************************** [products_reviews]
CREATE TABLE [product_review]
(
 [ID]         bigint NOT NULL IDENTITY(1,1) PRIMARY KEY,
 [product_id] bigint NOT NULL ,
 [review_id]  bigint NOT NULL , 
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO

-- ************************************** [shop_info]
CREATE TABLE [shop_info]
(
 [ID]         int NOT NULL IDENTITY(1,1) PRIMARY KEY,
 [locate]     nvarchar(250) NULL ,
 [hotline]    char(12) NULL ,
 [website]    varchar(50) NULL ,
 [email]      varchar(50) NULL ,
 [open_time]  nvarchar(50) NULL ,
 [open_dates] nvarchar(50) NULL ,
 [facebook]   varchar(100) NULL ,
 [youtube]    varchar(100) NULL ,
 [maps]       text NULL ,
 [review_img] text NULL ,
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO

-- ************************************** [reviews]
CREATE TABLE [reviews]
(
 [ID]             bigint NOT NULL IDENTITY(1,1) PRIMARY KEY,
 [customer_name]  nvarchar(50) NOT NULL ,
 [customer_email] varchar(250) NULL ,
 [customer_phone] char(12) NULL ,
 [rate]           char(1) NULL ,
 [review_text]    text NOT NULL ,
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO

-- ************************************** [users]
CREATE TABLE [users]
(
 [ID]       int NOT NULL IDENTITY(1,1) PRIMARY KEY,
 [username] char(16) NOT NULL ,
 [email]    char(250) NOT NULL ,
 [phone]	char(12) NULL ,
 [name]     nvarchar(50) NULL ,
 [password] char(60) NOT NULL ,
 [role]     int NULL default 0 , -- 0: user bình thường, 1: super user
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO

-- ************************************** [shop_watch]
CREATE TABLE [shop_watch]
(
 [ID]                 int NOT NULL IDENTITY(1,1) PRIMARY KEY,
 [bao_hiem_shopwatch] nvarchar(250) NULL ,
 [bao_hanh_shopwatch] int NOT NULL ,
 [tham_dinh]          nvarchar(250) NOT NULL ,
 [giao_hang]          nvarchar(250) NOT NULL ,
 [thoi_gian_bao_hanh] nvarchar(50) NOT NULL ,
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO

-- ************************************** [slides]
CREATE TABLE [slides]
(
 [id]         int NOT NULL IDENTITY (1, 1) PRIMARY KEY,
 [image]      text NOT NULL ,
 [alt]        nvarchar(100) NULL ,
 [is_visible] bit NULL , 
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO

-- ************************************** [home_content_layouts]
CREATE TABLE [home_content_layouts]
(
 [ID]           int IDENTITY (1, 1) NOT NULL PRIMARY KEY,
 [content_type] varchar(50) NOT NULL ,
 [content_name] nvarchar(100) NULL ,
 [order]        int NOT NULL default 0, 
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO

-- ************************************** [orders]
CREATE TABLE [orders]
(
 [ID]               bigint NOT NULL IDENTITY(1, 1) PRIMARY KEY,
 [order_code]       varchar(32) NOT NULL ,
 [customer_name]    nvarchar(50) NULL ,
 [customer_phone]   char(12) NOT NULL ,
 [customer_address] nvarchar(250) NULL ,
 [payment_method]   int default 1 , -- 1: COD, 0: thanh toán tại cửa hàng, 2: chuyển khoản
 [order_date]		datetime default '01/01/1970' ,
 [order_status]		int default 0 , -- 0 Chờ xác nhận, -1 là không chấp nhận, 1 là chấp nhận đơn hàng
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO

-- ************************************** [order_product]
CREATE TABLE [order_product]
(
 [ID]         bigint IDENTITY (1, 1) NOT NULL PRIMARY KEY,
 [order_id]   bigint NOT NULL ,
 [product_id] bigint NOT NULL ,
 [created_at]	datetime NULL,
 [updated_at]	datetime NULL,
);
GO
INSERT INTO [dbo].[shop_info]
           ([locate]
           ,[hotline]
           ,[website]
           ,[email]
           ,[open_time]
           ,[open_dates]
           ,[facebook]
           ,[youtube]
           ,[maps]
           ,[review_img]
           ,[created_at]
           ,[updated_at])
     VALUES
           (N'Số 218 Lê Duẩn - Hà Nội'
           ,'0975592299'
           ,'shopwatch.vn'
           ,'hotroshopwatch@gmail.com'
           ,'9h00- 19h30'
           ,N'Tất cả các ngày trong tuần'
           ,'https://www.facebook.com/dongho.shopwatch/'
           ,'https://www.youtube.com/channel/UC55dyKDpPM3rWjgMQerD-mQ'
           ,'https://www.google.com/maps/place/ShopWatch+-+%C4%90%E1%BB%93ng+h%E1%BB%93+%C4%91eo+tay+ch%C3%ADnh+h%C3%A3ng/@21.016887,105.8413076,15z/data=!4m5!3m4!1s0x0:0x78cd9dcf4fbefd45!8m2!3d21.016887!4d105.8413076'
           ,NULL
           ,GETDATE()
           ,GETDATE())
GO
INSERT INTO [shop_watch].[dbo].[users]
           ([username]
           ,[email]
           ,[phone]
           ,[name]
           ,[password]
           ,[role]
           ,[created_at]
           ,[updated_at])
     VALUES
           ('root'
           ,'root@vadu.dev'
           ,'0327811555'
           ,N'Lục Thần Ca'
           ,'$2a$10$lzW6pHiotFPLjZA5PPbOu.pD7d5fNsrwfebmg/KGqw7a7jZjq9g6m'
           ,1
           ,GETDATE()
           ,GETDATE())
GO

