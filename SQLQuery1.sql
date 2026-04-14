CREATE TABLE .[Member] (
    [FirstName]     VARCHAR (20) NOT NULL,
    [SurName]       VARCHAR (30) NOT NULL,
    [PhoneNumber]   VARCHAR (11) NOT NULL,
    [Address]       VARCHAR (50) NOT NULL,
    [City]          VARCHAR (30) NOT NULL,
    [Mail]          VARCHAR (50) NOT NULL,
    [TheMemberType] VARCHAR (20) NULL,
    [TheMemberRole] VARCHAR (20) NOT NULL,
    [Id]            INT          NOT NULL,
    [MemberImage]   VARCHAR (50),
    PRIMARY KEY CLUSTERED ([PhoneNumber] ASC),
    CONSTRAINT [TheMemberType] CHECK ([TheMemberType]='Senior' OR [TheMemberType]='Adult' OR [TheMemberType]='Junior' OR [TheMemberType] IS NULL),
    CONSTRAINT [TheMemberRole] CHECK ([TheMemberRole]='Chairman' OR [TheMemberRole]='Member' OR [TheMemberRole]='Admin' OR [TheMemberRole] IS NULL)
);
CREATE TABLE.[Boat]
(
    TheBoatType VARCHAR(50)
	CONSTRAINT TheBoatType CHECK (TheBoatType in ('TERA', 'FEVA', 'LASERJOLLE','EUROPAJOLLE','SNIPEJOLLE','WAYFARER', 'LYNÆS') OR TheBoatType IS NULL),
    Model VARCHAR(50) NOT NULL,
    SailNumber VARCHAR(50) NOT NULL PRIMARY KEY,
    EngineInfo VARCHAR(50) NOT NULL,
    Draft int NOT NULL,
    Width int NOT NULL,
    [Length] int NOT NULL,
    YearOfConstruction VARCHAR(50) NOT NULL,
    Id INT NOT NULL 
);

CREATE TABLE [Booking]
(
	[Id] int NOT NULL  PRIMARY KEY,
	[StartDate] DATE NOT NULL,
	[EndDate] DATE NOT NULL,
	[IsActive] BIT NOT NULL,
	[SailComleted] BIT NOT NULL,
	[Deatination] VARCHAR(100) NOT NULL,
	PhoneNumber  VARCHAR(11) NOT NULL,
	SailNumber VARCHAR(50) NOT NULL
	FOREIGN KEY (PhoneNumber) REFERENCES [dbo].[Member](PhoneNumber),
    FOREIGN KEY (SailNumber) REFERENCES Boat(SailNumber)
);


--Insert members:
INSERT INTO Member
Values('Poul', 'Poulsen',20202020,'Poulvej 42', 'Poulløse', 'Poul@mail.com', 'Adult', 'Member', '', 1)

--Insert Boat:
INSERT INTO Boat
Values('TERA', 'Model2000','291-120','fast', 7, 4, 3, '2021', 1)

--Insert Booking
INSERT INTO Booking
Values(1, '2014-7-19', '2014-7-21', 0, 1, 'Roskilde', 20202020, '291-120')

--select list Members
Select * from Member

--select list Boats
Select * from Boat

--select list Booking
Select * from Booking

--Update member--
Update Member 
Set Mail = 'example@mail.com', PhoneNumber = 12121212
Where Id = 1;

--Delete booking
Delete From Booking
Where Id=1

--select Member(s)
Select * from Member

--Select (join) booking & boat
Select *
From Booking
INNER JOIN [Member] ON Booking.PhoneNumber=Member.PhoneNumber

--SELECT med filter på enum
SELECT * FROM [Member]
WHERE TheMemberType = 'Junior';

--SELECT med søgning: søg i fornavn/efternavn/phone -Lav seperate sql sætninger
Select * from [Member]
Where FirstName Like '%PO%';

Select * from [Member]
Where SurName Like '%SEN%';

Select * from [Member]
Where PhoneNumber Like '%12%';

--AGGREGAT: antal bookings per båd 
SELECT COUNT(*) Bookings
FROM Booking
Group by Id;

--Vis aktuelle aktive bookings (beregnet i DB ud fra NOW)
Select Count(*) activeBookings
From Booking
Where CURRENT_TIMESTAMP between StartDate AND EndDate
