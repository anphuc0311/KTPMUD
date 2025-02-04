
use anphuc;

-- Table: users
CREATE TABLE users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    fullname NVARCHAR(255),
    username NVARCHAR(25) UNIQUE,
    password NVARCHAR(72),
    email NVARCHAR(25),
    roles NVARCHAR(25),
    status BIT
);

-- Table: user_activity
CREATE TABLE user_activity (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES users(id),
    IsActive BIT DEFAULT 0,
    LastActivity DATETIME DEFAULT GETUTCDATE()
);



-- Table: permissions


-- Table: user_groups
CREATE TABLE user_groups (
    id INT IDENTITY(1,1) PRIMARY KEY,
    group_name NVARCHAR(25),
    description NVARCHAR(255)
);

-- Table: user_group_members
CREATE TABLE user_group_members (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES users(id),
    group_id INT FOREIGN KEY REFERENCES user_groups(id)
);

-- Table: login_history
CREATE TABLE login_history (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES users(id),
    login_time DATETIME,
    ip_address NVARCHAR(25)
);

-- Table: huyen
CREATE TABLE huyen (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(25),
    code NVARCHAR(25)
);

-- Table: xa
CREATE TABLE xa (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(25),
    code NVARCHAR(25),
    huyen_id INT FOREIGN KEY REFERENCES huyen(id)
);

-- Table: legal_documents
CREATE TABLE legaldocuments (
    id INT IDENTITY(1,1) PRIMARY KEY,
    title NVARCHAR(25),
    files NVARCHAR(25),
    issue_date DATE
);

--Table: giongcaytrong
CREATE TABLE giongcaytrong(
	id INT PRIMARY KEY,
	giongcay NVARCHAR(100),
	soluong int,
	Mota Nvarchar(100),
);

--Table: cososanxuat
CREATE TABLE CoSoSanXuat (
    MaCoSo INT PRIMARY KEY IDENTITY,
    TenCoSo NVARCHAR(100),
    DiaChi NVARCHAR(255),
    LoaiGiong INT FOREIGN KEY REFERENCES giongcaytrong(id),
    SoLuong INT,
    ToaDo NVARCHAR(100) -- 
);

Insert into huyen(name) Values ('Huyen A'), ('Huyen B');


INSERT into users(fullname,username,password,email,roles,status) values 
('Do Ton Nguyen','king','anphuc','blabla@gmail.com','Admin',0);

select * from users;

create table giong(
	id INT identity(1,1) primary key,
	giongcay NVARCHAR(100),
	soluong int,
	mota NVARCHAR(100),
);

create table coso(
	id INT IDENTITY(1,1) PRIMARY KEY,
	TenCoSo NVARCHAR(100),
    DiaChi NVARCHAR(255),
    LoaiGiong INT FOREIGN KEY REFERENCES giong(id),
);
