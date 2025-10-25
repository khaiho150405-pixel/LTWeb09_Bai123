CREATE DATABASE QL_NhanSu;
GO

USE QL_NhanSu;
GO

CREATE TABLE Department (
    DeptId INT PRIMARY KEY,
    Name NVARCHAR(255)
);

CREATE TABLE Employee (
    Id INT PRIMARY KEY,
    Name NVARCHAR(255),
    Gender NVARCHAR(10),
    City NVARCHAR(100),
	Img CHAR(255),
    DeptId INT,
    FOREIGN KEY (DeptId) REFERENCES Department(DeptId)
);
GO

INSERT INTO Department (DeptId, Name) VALUES
(1, N'Khoa CNTT'),
(2, N'Khoa Ngoại Ngữ'),
(3, N'Khoa Tài Chính'),
(4, N'Khoa Thực Phẩm'),
(5, N'Phòng Đào Tạo');

INSERT INTO Employee (Id, Name, Gender, City, Img, DeptId) VALUES
(1, N'Nguyễn Hải Yến', N'Nữ', N'Đà Lạt','/Content/Images/anh1.png', 1),
(2, N'Trương Mạnh Hùng', N'Nam', N'TP.HCM','/Content/Images/anh2.png', 1),
(3, N'Đinh Duy Minh', N'Nam', N'Thái Bình','/Content/Images/anh3.png', 2),
(4, N'Ngô Thị Nguyệt', N'Nữ', N'Long An','/Content/Images/anh4.png', 2),
(5, N'Đào Minh Châu', N'Nữ', N'Bạc Liêu','/Content/Images/anh5.png', 3),
(14, N'Phan Thị Ngọc Mai', N'Nữ', N'Bến Tre','/Content/Images/anh6.png', 3),
(15, N'Trương Nguyễn Quỳnh Anh', N'Nữ', N'TP.HCM','/Content/Images/anh7.png', 4),
(16, N'Lê Thanh Liêm', N'Nam', N'TP.HCM','/Content/Images/anh8.png', 4)
GO

select * from Employee