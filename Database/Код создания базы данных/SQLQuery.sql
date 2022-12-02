--Создал базу данных
CREATE DATABASE OnlineStore_Db

DROP DATABASE OnlineStoreDb



--Создаём таблицу ролей
CREATE TABLE Roles(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(20) NOT NULL
)


--Создаём таблицу клиентов
CREATE TABLE Users(
    Id INT IDENTITY PRIMARY KEY,
    FirstName NVARCHAR(20) NOT NULL,
    LastName NVARCHAR(20) NOT NULL,
    Email NVARCHAR(30) NOT NULL UNIQUE,
    Password NVARCHAR(30) NOT NULL ,
    Phone VARCHAR(20) NOT NULL UNIQUE,
    RegistrationDate DATE NOT NULL,

    RoleId INT,

    FOREIGN KEY (RoleId) REFERENCES Roles (Id) ON DELETE SET NULL

)

--Создаём таблицу Категорий
CREATE TABLE Categories(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(40) NOT NULL,

    CategoryId INT,

    FOREIGN KEY (CategoryId) REFERENCES Categories (Id)
)

--Создаём таблицу Товаров
CREATE TABLE Products(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(40) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Rating INT NULL,

    CategoryId INT,

    FOREIGN KEY (CategoryId) REFERENCES Categories (Id),
)

--Создаём таблицу Картинок
CREATE TABLE Pictures(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(30),
    Path NVARCHAR(max),

    ProductId INT,

    FOREIGN KEY (ProductId) REFERENCES Products (Id) ON DELETE SET NULL,
)

--Создаём таблицу Корзин
CREATE TABLE Baskets(
    UserId INT,
    ProductId INT,
    AmountProduct INT,
    
    PRIMARY KEY (UserId,ProductId),

    FOREIGN KEY (UserId) REFERENCES Users (Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products (Id) ON DELETE CASCADE,
)


CREATE TABLE PurchaseHistory(
    Id INT IDENTITY PRIMARY KEY,
    UserId INT NOT NULL,
    ProductId INT NOT NULL,
    AmountProduct INT NOT NULL,
    DatePurchase DATETIME NOT NULL,

    FOREIGN KEY (UserId) REFERENCES Users (Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products (Id) ON DELETE CASCADE,
)


CREATE TABLE Characteristics(
    Id INT IDENTITY PRIMARY KEY,
    Value NVARCHAR(30) NOT NULL,
    Name NVARCHAR(30) NOT NULL
)

CREATE TABLE Product_Characteristics(
    ProductId INT NOT NULL,
    CharacteristicId INT NOT NULL,

    PRIMARY KEY (ProductId,CharacteristicId),

    FOREIGN KEY (ProductId)  REFERENCES Products (Id),
    FOREIGN KEY (CharacteristicId)  REFERENCES Characteristics (Id),
)
--Таблица мониторинга изменений данных в таблице истории покупок
CREATE TABLE [dbo].[Monitor_Database](
	[Id_item] [int] IDENTITY(1,1) NOT NULL,
	[Hostname] [nvarchar](50) NULL,
	[Nt_username] [nvarchar](100) NULL,
	[NameOperation] [nvarchar](100) NULL,
	[NameTable] [nvarchar](50) NULL,
	[NameColumns] [nvarchar](50) NULL,
	[IdRecord] [nvarchar](100) NULL,
	[OldRecord] [nvarchar](100) NULL,
	[NewRecord] [nvarchar](100) NULL,
	[DataModificationRecord] [datetime] NULL,
 CONSTRAINT [PK_Monitor_Database] PRIMARY KEY CLUSTERED 
(
	[Id_item] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


-- Создание процедуры
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create PROCEDURE [dbo].[INS_Monitor_Database]
	@NameOperation	varchar(100),	-- название операции с данными
	@NameTable		varchar(50),	-- имя таблицы   
	@NameColumns	varchar(50),	-- имя столбца  
	@IdRecord		varchar(100),	-- идентификатор записи (ключ), которая подверглась вставке/корректировке/удалению
	@OldRecord		varchar(100),	-- предыдущая запись 
	@NewRecord		varchar(100),	-- обновленная запись
	@debug			bit = 0			-- параметр отладки (0 - рабочий, 1 - тест)
AS

DECLARE	@Hostname varchar(50),		-- имя хоста, который пытается вставить запись
		@Nt_username varchar(50)	-- имя пользователя, который пытается вставить запись

BEGIN

	-- Установить имя хоста, имя пользователя
		SELECT 	@Hostname = Z.hostname, 
				@Nt_username = Z.Nt_username				
			FROM master.dbo.sysprocesses as Z WHERE Z.SPID=@@SPID

		-- закомментировал: можно посмотреть дополнительные параметры этого подключения из системной таблицы
		-- SELECT * FROM master.dbo.sysprocesses as M   WHERE M.SPID=@@SPID

		-- Вставить запись в таблицу мониторинга [Monitor_Database]
		INSERT INTO [dbo].[Monitor_Database]  (
				Hostname,
				Nt_username,
			    NameOperation,
				NameTable,
				NameColumns,
				IdRecord,				
				OldRecord, 
				NewRecord,
				DataModificationRecord 			)
			VALUES (
				@Hostname,
				@Nt_username,
				@NameOperation,
				@NameTable,
				@NameColumns,
				@IdRecord,				
				@OldRecord,
				@NewRecord,
				GETDATE()			)

END

if @debug = 1
	SELECT * FROM [dbo].[Monitor_Database]



GO

--Создание триггеров

--Триггер на удаление
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[PurchaseHistory_Trigger_Delete] ON [dbo].[PurchaseHistory] 
FOR DELETE
AS

DECLARE @NameTable					varchar(50),	-- имя таблицы
		@NameOperation				varchar(100),	-- описание операции с данными
		@OldRecord					varchar(100),	-- предыдущая запись 
		@NewRecord					varchar(100),	-- обновленная запись (здесь будет NULL),
		@IdRecord					varchar(100),	-- идентификатор записи (записывает ключевые поля удаленной записи)
		@Old_Id				        int,			-- удаленное значение
		@Old_UserId					int,			-- удаленное значение
		@Old_ProductId			    int,			-- удаленное значение	
        @Old_AmountProduct			int,			-- удаленное значение
        @Old_DatePurchase			DATETIME 		-- удаленное значение	

 SET @NameTable = 'PurchaseHistory'
 SET @NameOperation = 'Удаление записи' 

 -- заполнить значения параметров выборкой из таблицы с удаленными значениями
 SELECT @IdRecord = 'Id=' + CONVERT(varchar(50), Old.Id),
		@Old_Id			        =	CONVERT(int, Old.Id),
		@Old_UserId			    =	CONVERT(int, Old.UserId),
		@Old_ProductId		    =	CONVERT(int, Old.ProductId),
        @Old_AmountProduct		=	CONVERT(int, Old.AmountProduct),
        @Old_DatePurchase		=	CONVERT(DATETIME, Old.DatePurchase)		 
	FROM Deleted Old 

/*
	Вызвать хранимую процедуру INS_Monitor_Database,
    чтобы сделать запись в таблице мониторинга Monitor_Database об удалении строки 
*/ 

	EXEC INS_Monitor_Database
			@NameOperation	= @NameOperation,
			@NameTable		= @NameTable,
			@NameColumns	= 'Id',
			@IdRecord		= @IdRecord, 			
			@OldRecord		= @Old_Id, 
			@NewRecord		= null, 
			@debug			= 0

	EXEC INS_Monitor_Database
			@NameOperation	= @NameOperation,
			@NameTable		= @NameTable,
			@NameColumns	= 'UserId',
			@IdRecord		= @IdRecord, 			
			@OldRecord		= @Old_UserId, 
			@NewRecord		= null, 
			@debug			= 0

	EXEC INS_Monitor_Database
			@NameOperation	= @NameOperation,
			@NameTable		= @NameTable,
			@NameColumns	= 'ProductId',
			@IdRecord		= @IdRecord, 			
			@OldRecord		= @Old_ProductId, 
			@NewRecord		= null, 
			@debug			= 0

    EXEC INS_Monitor_Database
			@NameOperation	= @NameOperation,
			@NameTable		= @NameTable,
			@NameColumns	= 'AmountProduct',
			@IdRecord		= @IdRecord, 			
			@OldRecord		= @Old_AmountProduct, 
			@NewRecord		= null, 
			@debug			= 0
    
    EXEC INS_Monitor_Database
			@NameOperation	= @NameOperation,
			@NameTable		= @NameTable,
			@NameColumns	= 'DatePurchase',
			@IdRecord		= @IdRecord, 			
			@OldRecord		= @Old_DatePurchase, 
			@NewRecord		= null, 
			@debug			= 0
	

GO

--Триггер на создание

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE TRIGGER [dbo].[PurchaseHistory_Trigger_Insert] ON [dbo].[PurchaseHistory]
FOR INSERT
AS

DECLARE @NameTable					varchar(50),	-- имя таблицы
		@NameOperation				varchar(100),	-- описание операции с данными
		@OldRecord					varchar(100),	-- предыдущая запись (здесь будет NULL) 
		@NewRecord					varchar(100),	-- вставленная запись,
		@IdRecord					varchar(100),	-- идентификатор записи (записывает ключевые поля новой записи)
		@New_Id		                varchar(100),	-- новое значение
		@New_UserId  			    varchar(100),	-- новое значение
		@New_ProductId			    varchar(100),	-- новое значение	
        @New_AmountProduct			varchar(100),	-- новое значение
        @New_DatePurchase			varchar(100)	-- новое значение
		
 SET @NameTable = 'PurchaseHistory'
 SET @NameOperation = 'Добавление записи' 

 -- заполнить значения параметров выборкой из таблицы с новыми значениями
 SELECT @IdRecord = 'Id=' + CONVERT(varchar(50), New.Id   ),
		@New_Id	                =	CONVERT(varchar(100)	,New.Id	),
		@New_UserId		        =	CONVERT(varchar(100)	,New.UserId		),
		@New_ProductId		    =	CONVERT(varchar(100)	,New.ProductId		),
        @New_AmountProduct		=	CONVERT(varchar(100)	,New.AmountProduct		),
        @New_DatePurchase		=	CONVERT(varchar(100)	,New.DatePurchase		)
	FROM Inserted New 

/*
	Вызвать хранимую процедуру INS_Monitor_Database,
    чтобы сделать запись в таблице мониторинга Monitor_Database о вставке данных в каждую колонку
*/ 

	EXEC INS_Monitor_Database
			@NameOperation	= @NameOperation,
			@NameTable		= @NameTable,
			@NameColumns	= 'Id',
			@IdRecord		= @IdRecord, 			
			@OldRecord		= null,	-- предыдущего значения не было, т.к. происходит вставка новой строки
			@NewRecord		= @New_Id, 
			@debug			= 0

	EXEC INS_Monitor_Database
			@NameOperation	= @NameOperation,
			@NameTable		= @NameTable,
			@NameColumns	= 'UserId',
			@IdRecord		= @IdRecord, 			
			@OldRecord		= null, -- предыдущего значения не было, т.к. происходит вставка новой строки
			@NewRecord		= @New_UserId, 
			@debug			= 0

	EXEC INS_Monitor_Database
			@NameOperation	= @NameOperation,
			@NameTable		= @NameTable,
			@NameColumns	= 'ProductId',
			@IdRecord		= @IdRecord, 			
			@OldRecord		= null, -- предыдущего значения не было, т.к. происходит вставка новой строки
			@NewRecord		= @New_ProductId, 
			@debug			= 0
    
    EXEC INS_Monitor_Database
			@NameOperation	= @NameOperation,
			@NameTable		= @NameTable,
			@NameColumns	= 'AmountProduct',
			@IdRecord		= @IdRecord, 			
			@OldRecord		= null, -- предыдущего значения не было, т.к. происходит вставка новой строки
			@NewRecord		= @New_AmountProduct, 
			@debug			= 0

    EXEC INS_Monitor_Database
			@NameOperation	= @NameOperation,
			@NameTable		= @NameTable,
			@NameColumns	= 'DatePurchase',
			@IdRecord		= @IdRecord, 			
			@OldRecord		= null, -- предыдущего значения не было, т.к. происходит вставка новой строки
			@NewRecord		= @New_DatePurchase, 
			@debug			= 0

	

GO

--Триггер на изменение

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[PurchaseHistory_Trigger_Update] ON [dbo].[PurchaseHistory] 
FOR UPDATE 
AS

DECLARE @NameTable					varchar(50),	-- имя таблицы
		@NameOperation				varchar(100),	-- описание операции с данными
		@OldRecord					varchar(100),	-- предыдущая запись 
		@NewRecord					varchar(100),	-- обновленная запись,
		@IdRecord					varchar(100),	-- идентификатор записи (записывает ключевые поля скорректированной записи)
		@New_Id				        varchar(100),	-- новое значение
		@New_UserId				    varchar(100),	-- новое значение
		@New_ProductId			    varchar(100),	-- новое значение
        @New_AmountProduct			varchar(100),	-- новое значение	
        @New_DatePurchase			varchar(100),	-- новое значение			
		@Old_Id				        varchar(100),	-- предыдущее значение
		@Old_UserId				    varchar(100),	-- предыдущее значение
		@Old_ProductId			    varchar(100),	-- предыдущее значение
        @Old_AmountProduct			varchar(100),	-- предыдущее значение
        @Old_DatePurchase			varchar(100)	-- предыдущее значение	

 SET @NameTable = 'PurchaseHistory'
 SET @NameOperation = 'Обновлена запись' 

 -- заполнить значения параметров выборкой из таблиц с новыми и предыдущими значениями
 SELECT @New_Id			        =	CONVERT(varchar(100)	,New.Id			),
		@New_UserId			    =	CONVERT(varchar(100)	,New.UserId			),
		@New_ProductId		    =	CONVERT(varchar(100)	,New.ProductId		),
        @New_AmountProduct		=	CONVERT(varchar(100)	,New.AmountProduct		),
        @New_DatePurchase		=	CONVERT(varchar(100)	,New.DatePurchase		)			
	FROM PurchaseHistory New	INNER JOIN 	Deleted Old 
		ON Old.Id= New.Id

 SELECT @IdRecord = 'Id=' + CONVERT(varchar(50), Old.Id			),		
		@Old_Id			        =	CONVERT(varchar(100)	,Old.Id			),
		@Old_UserId				=	CONVERT(varchar(100)	,Old.UserId				),
		@Old_ProductId		    =	CONVERT(varchar(100)	,Old.ProductId		),
        @Old_AmountProduct		=	CONVERT(varchar(100)	,Old.AmountProduct		),
        @Old_DatePurchase		=	CONVERT(varchar(100)	,Old.DatePurchase		)		
	FROM Deleted Old 
	
/*
	Проверить все колонки: 
	если новые и старые записи отличаются, то вызвать хранимую процедуру INS_Monitor_Database,
    чтобы сделать запись в таблице мониторинга Monitor_Database об обновлении данных в каждой колонке
*/ 

IF (@New_Id <> @Old_Id) 
	EXEC INS_Monitor_Database
			@NameOperation	= @NameOperation,
			@NameTable		= @NameTable,
			@NameColumns	= 'Id',
			@IdRecord		= @IdRecord, 			
			@OldRecord		= @Old_Id, 
			@NewRecord		= @New_Id, 
			@debug			= 0

IF (@New_UserId <> @Old_UserId) 
	EXEC INS_Monitor_Database
			@NameOperation	= @NameOperation,
			@NameTable		= @NameTable,
			@NameColumns	= 'UserId',
			@IdRecord		= @IdRecord, 			
			@OldRecord		= @Old_UserId, 
			@NewRecord		= @New_UserId, 
			@debug			= 0

IF (@New_ProductId <> @Old_ProductId) 
	EXEC INS_Monitor_Database
			@NameOperation	= @NameOperation,
			@NameTable		= @NameTable,
			@NameColumns	= 'ProductId',
			@IdRecord		= @IdRecord, 			
			@OldRecord		= @Old_ProductId, 
			@NewRecord		= @New_ProductId, 
			@debug			= 0

IF (@New_AmountProduct <> @Old_AmountProduct) 
	EXEC INS_Monitor_Database
			@NameOperation	= @NameOperation,
			@NameTable		= @NameTable,
			@NameColumns	= 'AmountProduct',
			@IdRecord		= @IdRecord, 			
			@OldRecord		= @Old_AmountProduct, 
			@NewRecord		= @New_AmountProduct, 
			@debug			= 0

IF (@New_DatePurchase <> @Old_DatePurchase) 
	EXEC INS_Monitor_Database
			@NameOperation	= @NameOperation,
			@NameTable		= @NameTable,
			@NameColumns	= 'DatePurchase',
			@IdRecord		= @IdRecord, 			
			@OldRecord		= @Old_DatePurchase, 
			@NewRecord		= @New_DatePurchase, 
			@debug			= 0



GO

ALTER TABLE Products
ADD OnSale BIT NOT NULL DEFAULT 1 CHECK(OnSale>=0 AND OnSale<=1);

ALTER TABLE Products
ADD Description NVARCHAR(1000) NULL ;

UPDATE Products
SET Description = 'Нет описания'

ALTER TABLE Categories
ADD OnSale BIT NOT NULL DEFAULT 1 CHECK(OnSale>=0 AND OnSale<=1);

