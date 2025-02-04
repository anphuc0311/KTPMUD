-- 1. Tạo database
Create database anphuc
Go
-- 2. Sử dụng database vừa tạo
USE anphuc;
GO

-- 3. Tạo bảng user_cososanxuat
CREATE TABLE user_cososanxuat (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES users(id) ON DELETE CASCADE,
    coso_id INT FOREIGN KEY REFERENCES cososanxuat(MaCoSo) ON DELETE CASCADE,
    role NVARCHAR(50) DEFAULT N'Quản lý' -- Vai trò của user trong cơ sở
);
-- 4. Tạo bảng Roles (Nhóm quyền)
CREATE TABLE roles (
    id INT IDENTITY(1,1) PRIMARY KEY,
    role_name NVARCHAR(50) UNIQUE
);

-- 5. Tạo bảng Users
CREATE TABLE users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    fullname NVARCHAR(255),
    username NVARCHAR(25) UNIQUE,
    password NVARCHAR(72),
    email NVARCHAR(50),
    role_id INT FOREIGN KEY REFERENCES roles(id),
    status BIT DEFAULT 1
);

-- 6. Tạo bảng User Activity
CREATE TABLE user_activity (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES users(id) ON DELETE CASCADE,
    IsActive BIT DEFAULT 0,
    LastActivity DATETIME DEFAULT GETDATE()
);

-- 7. Tạo bảng Login History
CREATE TABLE login_history (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES users(id) ON DELETE CASCADE,
    login_time DATETIME DEFAULT GETDATE(),
    ip_address NVARCHAR(25)
);

-- 8. Tạo bảng Huyện
CREATE TABLE huyen (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(50) UNIQUE,
    code NVARCHAR(25) UNIQUE
);

-- 9. Tạo bảng Xã
CREATE TABLE xa (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(50),
    code NVARCHAR(25) UNIQUE,
    huyen_id INT FOREIGN KEY REFERENCES huyen(id) ON DELETE CASCADE
);

-- 10. Tạo bảng Cơ Sở Sản Xuất
CREATE TABLE cososanxuat (
    MaCoSo INT PRIMARY KEY IDENTITY,
    TenCoSo NVARCHAR(100),
    DiaChi NVARCHAR(255),
    LoaiGiong INT FOREIGN KEY REFERENCES giong(id),
    SoLuong INT,
    ToaDo NVARCHAR(100),
    xa_id INT FOREIGN KEY REFERENCES xa(id) ON DELETE SET NULL
);

-- 11. Tạo bảng Giống Cây Trồng
CREATE TABLE giong (
    id INT IDENTITY(1,1) PRIMARY KEY,
    giongcay NVARCHAR(100),
    soluong INT,
    mota NVARCHAR(255)
);

-- 12. Tạo bảng Loại Hình Sản Xuất
CREATE TABLE LoaiHinhSanXuat (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    LoaiHinhSanXuat NVARCHAR(255),
    CoSoSanXuat INT FOREIGN KEY REFERENCES cososanxuat(MaCoSo),
    HinhThucHoatDong NVARCHAR(255),
    ThongKeThang INT,
    ThongKeQuy INT,
    ThongKeNam INT
);

-- 13. Tạo bảng Động Vật
CREATE TABLE DongVat (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    TenDongVat NVARCHAR(255),
    LoaiDongVat NVARCHAR(255),
    SoLuong INT,
    CoSoLuuTru INT FOREIGN KEY REFERENCES cososanxuat(MaCoSo),
    BienDong NVARCHAR(255),
    ThongKeThang INT,
    ThongKeQuy INT,
    ThongKeNam INT
);

-- 14. Tạo bảng Permissions (Quyền)
CREATE TABLE permissions (
    id INT IDENTITY(1,1) PRIMARY KEY,
    permission_name NVARCHAR(50) UNIQUE
);

-- 15. Bảng Nhiều - Nhiều giữa Users và Permissions
CREATE TABLE user_permissions (
    id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT FOREIGN KEY REFERENCES users(id) ON DELETE CASCADE,
    permission_id INT FOREIGN KEY REFERENCES permissions(id) ON DELETE CASCADE
);

-- 16. Chèn dữ liệu mẫu cho huyện
INSERT INTO huyen (name, code) VALUES 
('Huyện A', 'HYA'),
('Huyện B', 'HYB');

-- 17. Chèn dữ liệu mẫu cho xã
INSERT INTO xa (name, code, huyen_id) VALUES 
('Xã 1', 'XA1', 1),
('Xã 2', 'XA2', 2);

-- 18. Chèn dữ liệu mẫu vào bảng giống cây trồng
INSERT INTO giong (giongcay, soluong, mota) VALUES 
('Xoài cát Hòa Lộc', 100, 'Giống xoài thơm ngon'),
('Bưởi da xanh', 150, 'Bưởi ngọt, ít hạt');

-- 19. Chèn dữ liệu mẫu vào bảng cơ sở sản xuất
INSERT INTO cososanxuat (TenCoSo, DiaChi, LoaiGiong, SoLuong, ToaDo, xa_id) VALUES 
('Nhà vườn A', 'Hà Nội', 1, 50, '21.0285,105.8542', 1),
('Nhà vườn B', 'TP.HCM', 2, 70, '10.7769,106.7009', 2);

-- 20. Chèn dữ liệu mẫu vào bảng loại hình sản xuất
INSERT INTO LoaiHinhSanXuat (LoaiHinhSanXuat, CoSoSanXuat, HinhThucHoatDong, ThongKeThang, ThongKeQuy, ThongKeNam) VALUES 
('Chế biến gỗ', 1, 'Sản xuất gỗ nội thất', 120, 3, 2024);

-- 21. Chèn dữ liệu mẫu vào bảng động vật
INSERT INTO DongVat (TenDongVat, LoaiDongVat, SoLuong, CoSoLuuTru, BienDong, ThongKeThang, ThongKeQuy, ThongKeNam) VALUES 
('Hổ', 'Động vật có vú', 5, 1, 'Tăng', 3, 1, 2024),
('Voi', 'Động vật có vú', 2, 2, 'Không đổi', 2, 1, 2024);

-- 22. Chèn dữ liệu mẫu vào bảng Roles
INSERT INTO roles (role_name) VALUES ('Admin'), ('User');

-- 23. Chèn dữ liệu mẫu vào bảng Users
INSERT INTO users (fullname, username, password, email, role_id, status) VALUES 
('Do Ton Nguyen', 'king', 'anphuc', 'blabla@gmail.com', 1, 1);

-- 24. Chèn dữ liệu mẫu vào bảng Permissions
INSERT INTO permissions (permission_name) VALUES 
('READ'), ('WRITE'), ('DELETE');

-- 25. Cấp quyền READ và WRITE cho user ID 1
INSERT INTO user_permissions (user_id, permission_id) VALUES (1, 1), (1, 2);

-- 26. Chèn dữ liệu vào bảng login_history
INSERT INTO login_history (user_id, login_time, ip_address) VALUES (1, GETDATE(), '192.168.1.1');

-- 27. Hiển thị danh sách user
SELECT * FROM users;

-- 28. Hiển thị danh sách cơ sở sản xuất
SELECT * FROM cososanxuat;

-- 29. Hiển thị danh sách loại hình sản xuất
SELECT * FROM LoaiHinhSanXuat;

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
