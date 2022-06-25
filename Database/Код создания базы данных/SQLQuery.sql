--Создал базу данных
CREATE DATABASE OnlineStoreDb

DROP DATABASE OnlineStoreDb

DROP TABLE Roles
DROP TABLE Сities
DROP TABLE Users
DROP TABLE Categories
DROP TABLE Brands
DROP TABLE Categories_Brands
DROP TABLE Products
DROP TABLE Product_Info
DROP TABLE Baskets
DROP TABLE Basket_Products

--Создаём таблицу ролей
CREATE TABLE Roles(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(20) NOT NULL
)

--Создаём таблицу разрешения
CREATE TABLE Permissions(
    Id INT IDENTITY PRIMARY KEY,
    Title NVARCHAR(20) NOT NULL,
    Description NVARCHAR(100) NULL
)

--Создаём таблицу разрешения
CREATE TABLE Roles_Permissions(
    RoleId INT NOT NULL,
    PermissionId INT NOT NULL,

    PRIMARY KEY (RoleId,PermissionId),

    FOREIGN KEY (RoleId)  REFERENCES Roles (Id),
    FOREIGN KEY (PermissionId)  REFERENCES Permissions (Id),
)

--Создаём таблицу городов
--CREATE TABLE Сities(
--    Id INT IDENTITY,
--    Name NVARCHAR(20) NOT NULL,
--
--    CONSTRAINT PK_City_Id PRIMARY KEY (Id),
--)

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

--Создаём таблицу Брендов
CREATE TABLE Brands(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(40) NOT NULL
)

--Создаём таблицу КатегорийБрендов
--CREATE TABLE Categories_Brands(
 --   Id INT IDENTITY,
--    CategoryId INT,
--    BrandId INT,

--    CONSTRAINT PK_Categories_Brand_Id PRIMARY KEY (Id),

 --   CONSTRAINT FK_Categories_Brands_To_Categories FOREIGN KEY (CategoryId) REFERENCES Categories (Id),
 --   CONSTRAINT FK_Categories_Brands_To_Brands FOREIGN KEY (BrandId) REFERENCES Brands (Id),
--)

--Создаём таблицу Товаров
CREATE TABLE Products(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(40) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Rating BIT NULL,

    CategoryId INT,
    BrandId INT,

    FOREIGN KEY (CategoryId) REFERENCES Categories (Id),
    FOREIGN KEY (BrandId) REFERENCES Brands (Id),
)

--Создаём таблицу Характеристик устройства
--CREATE TABLE Product_Info(
--    Id INT IDENTITY PRIMARY KEY,
--    Title NVARCHAR(40) NOT NULL,
--    Description NVARCHAR(200),
--
--    ProductId INT NULL,
--
--    FOREIGN KEY (ProductId) REFERENCES Products (Id) ON DELETE SET NULL,
--)

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

--Создаём таблицу КарзинаТовары
--CREATE TABLE Baskets_Products(
--   Id INT IDENTITY,
--   BasketId INT,
--   ProductId INT,

--   CONSTRAINT PK_Basket_Product_Id PRIMARY KEY (Id),

--   CONSTRAINT FK_Baskets_Products_To_Basket FOREIGN KEY (BasketId)  REFERENCES Baskets (Id),
--   CONSTRAINT FK_Baskets_Products_To_Products FOREIGN KEY (ProductId)  REFERENCES Products (Id),
--)

--Создаём таблицу Магазины
CREATE TABLE Stores(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(20) NULL,
)

--Создаём таблицу МагазиныПродукты
CREATE TABLE Stores_Products(
    StoreId INT NOT NULL,
    ProductId INT NOT NULL,
    AmountProduct INT CHECK(AmountProduct >= 0),

    PRIMARY KEY (StoreId,ProductId),

    FOREIGN KEY (StoreId)  REFERENCES Stores (Id),
    FOREIGN KEY (ProductId)  REFERENCES Products (Id),
)

--Создаём таблицу Адресов
CREATE TABLE Addresses(
    Id INT IDENTITY PRIMARY KEY,
    Region NVARCHAR(20) NOT NULL,
    City NVARCHAR(20) NOT NULL,
    Street NVARCHAR(20) NOT NULL,
    House NVARCHAR(10) NOT NULL,
    Housing NVARCHAR(10) NOT NULL,

    StoreId INT NULL,

    FOREIGN KEY (StoreId) REFERENCES Stores (Id) ON DELETE CASCADE
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

CREATE TABLE Stores_Users(
    StoreId INT NOT NULL,
    UserId INT NOT NULL,

    PRIMARY KEY (StoreId,UserId),

    FOREIGN KEY (StoreId)  REFERENCES Stores (Id),
    FOREIGN KEY (UserId)  REFERENCES Users (Id),
)

CREATE TABLE Сharacteristics(
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(30) NOT NULL
)

CREATE TABLE CharacteristicValues(
    Id INT IDENTITY PRIMARY KEY,
    СharacteristicId INT NOT NULL,
    Value NVARCHAR(30) NOT NULL,

    FOREIGN KEY (СharacteristicId)  REFERENCES Сharacteristics (Id)
)

CREATE TABLE Product_CharacteristicValues(
    ProductId INT NOT NULL,
    CharacteristicValueId INT NOT NULL,

    PRIMARY KEY (ProductId,CharacteristicValueId),

    FOREIGN KEY (ProductId)  REFERENCES Products (Id),
    FOREIGN KEY (CharacteristicValueId)  REFERENCES CharacteristicValues (Id),
)


DROP TABLE Сharacteristics
DROP TABLE Stores_Products
