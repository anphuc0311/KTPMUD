
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






select * from users;

create table giong(
	id INT identity(1,1) primary key,
	giongcay NVARCHAR(100),
	soluong int,
	mota NVARCHAR(100),
	tencs NVARCHAR(255),
	soluongcs INT,
);

create table coso(
	id INT IDENTITY(1,1) PRIMARY KEY,
	TenCoSo NVARCHAR(100),
    DiaChi NVARCHAR(255),
    LoaiGiong INT FOREIGN KEY REFERENCES giong(id),
);



CREATE TABLE LoaiHinhSanXuat (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    LoaiHinhSanXuat NVARCHAR(255),
    ThongTinCoSo NVARCHAR(255),
    HinhThucHoatDong NVARCHAR(255),
    ThongKeThang INT,
    ThongKeQuy INT,
    ThongKeNam INT
);


CREATE TABLE DongVat (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenDongVat NVARCHAR(255),
    LoaiDongVat NVARCHAR(255),
    SoLuong INT,
    CoSoLuuTru NVARCHAR(255),
    BienDong NVARCHAR(255),
    ThongKeThang INT,
    ThongKeQuy INT,
    ThongKeNam INT
);



-- Tạo bảng giong
CREATE TABLE giong (
    id INT IDENTITY(1,1) PRIMARY KEY,
    giongcay NVARCHAR(100),
    soluong INT,
    mota NVARCHAR(100),
    tencs NVARCHAR(255),
    soluongcs INT
);

-- Chèn dữ liệu mẫu


	-- Chèn dữ liệu mẫu về cây trồng rừng
INSERT INTO giong (giongcay, soluong, mota, csxs, soluongcs)
VALUES 
    (N'Keo lai', 5000, N'Cây keo phát triển nhanh, dùng cho gỗ dăm và giấy', N'Vườn ươm A', 2500),
    (N'Bạch đàn', 4000, N'Cây gỗ bền, chịu hạn tốt, dùng trong xây dựng', N'Vườn ươm B', 2000),
    (N'Mỡ', 3000, N'Cây lấy gỗ, tốc độ sinh trưởng nhanh', N'Vườn ươm C', 1500),
    (N'Sến đỏ', 1500, N'Gỗ quý, dùng trong nội thất cao cấp', N'Vườn ươm D', 750),
    (N'Gõ đỏ', 1000, N'Gỗ cứng, chống mối mọt, giá trị kinh tế cao', N'Vườn ươm E', 500),
    (N'Lim xanh', 800, N'Cây gỗ quý, sinh trưởng chậm, giá trị kinh tế cao', N'Vườn ươm F', 400),
    (N'Re', 2000, N'Cây trồng rừng phòng hộ, phục hồi đất', N'Vườn ươm G', 1000),
    (N'Sao đen', 1200, N'Cây gỗ quý, được trồng để phủ xanh đất trống', N'Vườn ươm H', 600),
    (N'Trám trắng', 2500, N'Cây gỗ trung bình, lá có thể làm thức ăn gia súc', N'Vườn ươm I', 1250),
    (N'Lát hoa', 1800, N'Gỗ tốt, chịu lực tốt, giá trị kinh tế cao', N'Vườn ươm J', 900);

SET IDENTITY_INSERT LoaiHinhSanXuat ON;

INSERT INTO LoaiHinhSanXuat (ID, LoaiHinhSanXuat, ThongTinCoSo, HinhThucHoatDong, ThongKeThang, ThongKeQuy, ThongKeNam)
VALUES 
(1, N'Công ty sản xuất gỗ tự nhiên', N'Cơ sở 1 - Hà Nội', N'Sản xuất và chế biến gỗ', 120, 300, 2024),
(2, N'Công ty sản xuất gỗ công nghiệp', N'Cơ sở 2 - TP. HCM', N'Sản xuất gỗ công nghiệp', 150, 350, 2024),
(3, N'Cơ sở sản xuất gỗ ép', N'Cơ sở 3 - Đà Nẵng', N'Gỗ ép và gỗ MDF', 100, 250, 2024),
(4, N'Nhà máy chế biến gỗ xuất khẩu', N'Cơ sở 4 - Bình Dương', N'Chế biến gỗ xuất khẩu', 200, 500, 2024),
(5, N'Xưởng sản xuất đồ nội thất', N'Cơ sở 5 - Đồng Nai', N'Thiết kế và gia công nội thất', 180, 400, 2024),
(6, N'Nhà máy gỗ ván ép', N'Cơ sở 6 - Hải Phòng', N'Sản xuất gỗ ván ép và ván MDF', 140, 320, 2024),
(7, N'Công ty sản xuất giấy', N'Cơ sở 7 - Quảng Nam', N'Sản xuất giấy từ nguyên liệu gỗ', 130, 280, 2024),
(8, N'Xưởng chế biến gỗ xuất khẩu', N'Cơ sở 8 - Tây Ninh', N'Chế biến gỗ xuất khẩu sang châu Âu', 220, 600, 2024);

SET IDENTITY_INSERT LoaiHinhSanXuat OFF;

SET IDENTITY_INSERT DongVat ON;

INSERT INTO DongVat (ID, TenDongVat, LoaiDongVat, SoLuong, CoSoLuuTru, BienDong, ThongKeThang, ThongKeQuy, ThongKeNam)
VALUES 
(1, N'Voọc chà vá chân đỏ', N'Linh trưởng', 50, N'Vườn Quốc Gia Phong Nha', N'+3', 50, 150, 2024),
(2, N'Hổ Đông Dương', N'Động vật ăn thịt', 10, N'Khu bảo tồn Cúc Phương', N'-1', 10, 30, 2024),
(3, N'Báo hoa mai', N'Động vật ăn thịt', 15, N'Khu bảo tồn Nam Cát Tiên', N'+2', 15, 45, 2024),
(4, N'Voi châu Á', N'Động vật có vú', 8, N'Khu bảo tồn Yok Đôn', N'-2', 8, 24, 2024),
(5, N'Hươu sao', N'Động vật móng guốc', 120, N'Vườn Quốc Gia Ba Vì', N'+5', 120, 360, 2024),
(6, N'Tê giác Java', N'Động vật có vú', 4, N'Khu bảo tồn Cát Tiên', N'-1', 4, 12, 2024),
(7, N'Gấu ngựa', N'Động vật ăn thịt', 20, N'Vườn Quốc Gia Tam Đảo', N'+3', 20, 60, 2024),
(8, N'Cá sấu hoa cà', N'Bò sát', 30, N'Khu bảo tồn Tràm Chim', N'-2', 30, 90, 2024),
(9, N'Chồn bay Malayan', N'Linh trưởng', 18, N'Vườn Quốc Gia U Minh', N'+1', 18, 54, 2024),
(10, N'Bò tót', N'Động vật móng guốc', 6, N'Khu bảo tồn Phước Bình', N'-1', 6, 18, 2024);

SET IDENTITY_INSERT DongVat OFF;
