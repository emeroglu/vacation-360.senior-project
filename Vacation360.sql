CREATE TABLE MemberType
(
	PK int primary key identity(1,1),
	Name varchar(100) NOT NULL
)
GO

CREATE TABLE Member
(
	PK int primary key identity(1,1),
	TypeFK int foreign key references MemberType(PK),
	Name varchar(250) NOT NULL,
	Surname varchar(100) NOT NULL,
	Email varchar(2500) NOT NULL,
	Username varchar(50) NOT NULL,
	Password varchar(25) NOT NULL,
	LastSeen datetime NOT NULL,
	Online bit NOT NULL
)
GO

CREATE TABLE City
(
	PK int primary key identity(1,1),
	Name varchar(250) NOT NULL
)
GO

CREATE TABLE Hotel
(
	PK int primary key identity(1,1),
	MemberFK int foreign key references Member(PK),
	CityFK int foreign key references City(PK),
	Name varchar(250) NOT NULL,
	Stars int NOT NULL,
	Address varchar(5000) NOT NULL
)
GO

CREATE TABLE Photo
(
	PK int primary key identity(1,1),
	HotelFK int foreign key references Hotel(PK),
	Title varchar(1000) NOT NULL,
	Rating int NOT NULL,
	Likes int NOT NULL,
	RawUrl varchar(5000) NOT NULL,
	PanoramicUrl varchar(5000) NOT NULL
)
GO

CREATE TABLE Comment
(
	PK int primary key identity(1,1),
	PhotoFK int foreign key references Photo(PK),
	Text varchar(5000) NOT NULL
)
GO

CREATE TABLE Favorite
(
	PK int primary key identity(1,1),
	PhotoFK int foreign key references Photo(PK),
	MemberFK int foreign key references Member(PK)
)
GO

CREATE TABLE Tag
(
	PK int primary key identity(1,1),
	Name varchar(250) NOT NULL
)
GO

CREATE TABLE PhotoTag
(
	PK int primary key identity(1,1),
	PhotoFK int foreign key references Photo(PK),
	TagFK int foreign key references Tag(PK)
)
GO

