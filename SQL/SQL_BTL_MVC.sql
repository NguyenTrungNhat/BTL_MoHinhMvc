create database BTL_MVC

CREATE TABLE Categories(
	CategoryNumber INT IDENTITY(1,1),
	CategoryName NVARCHAR(200) NOT NULL,
	CategoryLevel INT NOT NULL,
	ParentID INT,
	PRIMARY KEY(CategoryNumber),
);
CREATE TABLE SubCategories(
	SubCategoryNumber INT IDENTITY(1,1) NOT NULL,
	CategoryNumber INT  NOT NULL,
	SubCategoryName NVARCHAR(200),
	PRIMARY KEY(SubCategoryNumber),
	FOREIGN KEY(CategoryNumber) REFERENCES Categories(CategoryNumber) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Customers(
	Username NVARCHAR(200),
	Cname NVARCHAR(200) NOT NULL,
	Email NVARCHAR(200),
	Pass NVARCHAR(200) NOT NULL,
	Phone INT,
	Address NVARCHAR(200),
	PostCode BIGINT,
	GroupID NVARCHAR(200),
	Status bit,
	PRIMARY KEY(Username),
	UNIQUE(PostCode)
);

CREATE TABLE Products(
	ProductNumber INT IDENTITY(1,1),
	CategoryNumber INT,
	Pname NVARCHAR(200) NOT NULL,
	Pimage NVARCHAR(200),
	Price FLOAT NOT NULL,
	Details ntext NOT NULL,
	Availablity INT NOT NULL,
	PRIMARY KEY(ProductNumber),
	FOREIGN KEY (CategoryNumber) REFERENCES Categories(CategoryNumber) ON DELETE CASCADE ON UPDATE CASCADE,
	CHECK(Price >= 0)
);

CREATE TABLE CartProducts(
	Username NVARCHAR(200) NOT NULL,
	ProductNumber INT NOT NULL,
	Quantity INT NOT NULL,
	PRIMARY KEY(Username, ProductNumber),
	FOREIGN KEY (Username) REFERENCES Customers(Username) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (ProductNumber) REFERENCES Products(ProductNumber) ON DELETE CASCADE ON UPDATE CASCADE,
	CHECK(Quantity > 0)
);

CREATE TABLE Orders(
	OrderNumber INT IDENTITY(1,1),
	Username NVARCHAR(200) NOT NULL,
	OrderDate DATE,
	OrderState NVARCHAR(200),
	ShipName NVARCHAR(200),
	ShipMobie NVARCHAR(200),
	ShipAddress NVARCHAR(200),
	ShipEmail NVARCHAR(200),
	PRIMARY KEY(OrderNumber),
);
CREATE TABLE OrderProducts(
	OrderProductsID INT IDENTITY(1,1),
	OrderNumber INT NOT NULL,
	ProductNumber INT NOT NULL,
	Quantity INT NOT NULL,
	Price FLOAT,
	PRIMARY KEY(OrderProductsID),
	FOREIGN KEY (OrderNumber) REFERENCES Orders(OrderNumber) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (ProductNumber) REFERENCES Products(ProductNumber) ON DELETE CASCADE ON UPDATE CASCADE,
	CHECK(Quantity > 0)
);

CREATE TABLE Carts(
	Username NVARCHAR(200),
	Price FLOAT NOT NULL,
	Discount SMALLINT,
	PRIMARY KEY(Username),
	FOREIGN KEY (Username) REFERENCES Customers(Username) ON DELETE CASCADE ON UPDATE CASCADE,
	CHECK(
		Price >= 0
		AND Discount >= 0
		AND Discount <= 100
	)
);

CREATE TABLE Admin(
	Username VARCHAR(40),
	Password VARCHAR(40),
	PRIMARY KEY(Username)
);

