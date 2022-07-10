Use MemberObject_Ass01Group8

CREATE TABLE MemberObject
(
Email      nchar(50) PRIMARY KEY,
MemberName nvarchar(15) NOT Null,
Password   varchar(50) NOT NULL,
City       nvarchar(15) NOT NULL,
Country    nvarchar(15) NOT NULL,
isAdmin     bit   NOT NULL,
);

--DROP TABLE MemberObject

INSERT INTO MemberObject(Email, MemberName, Password, City, Country, isAdmin ) VALUES('admin@fstore.com', 'Admin', 'admin@@', 'Ho Chi Minh', 'Viet Nam', 'true');
INSERT INTO MemberObject(Email, MemberName, Password, City, Country, isAdmin ) VALUES('namnguyen@fstore.com', 'Nguyen Hai Nam', 'namnguyen123', 'Ho Chi Minh', 'Viet Nam', 'false');
INSERT INTO MemberObject(Email, MemberName, Password, City, Country, isAdmin ) VALUES('tinguyen@fstore.com', 'nguyen van ti', 'tinguyen123','Ha Noi', 'Viet Nam', 'false');

SELECT * FROM MemberObject

GO

CREATE PROC USP_Login
@Email nchar(50), @passWord varchar(50)
AS
BEGIN
      SELECT * FROM MemberObject WHERE Email = @Email AND PassWord = @passWord
END
GO

SELECT Email, MemberName, Password, City, Country, isAdmin FROM MemberObject WHERE Email = 'a@fstore.com'

