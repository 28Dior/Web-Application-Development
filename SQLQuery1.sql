CREATE DATABASE KT0720_64130773;
GO
USE KT0720_64130773;
GO
CREATE TABLE LOP (
    MaLop NVARCHAR(10) PRIMARY KEY,
    TenLop NVARCHAR(50) NOT NULL
);
CREATE TABLE SINHVIEN (
    MaSV NVARCHAR(10) PRIMARY KEY,
    HoSV NVARCHAR(50) NOT NULL,
    TenSV NVARCHAR(50) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh BIT NOT NULL, -- True: Nam, False: Nữ
    AnhSV NVARCHAR(100),
    DiaChi NVARCHAR(255),
    MaLop NVARCHAR(10) NOT NULL,
    FOREIGN KEY (MaLop) REFERENCES LOP(MaLop)
);

INSERT INTO LOP (MaLop, TenLop)
VALUES
    ('L01', N'Công nghệ thông tin 1'),
    ('L02', N'Công nghệ phần mềm 2'),
    ('L03', N'Hệ thống thông tin 3'),
    ('L04', N'Kỹ thuật phần cứng'),
    ('L05', N'Trí tuệ nhân tạo'),
    ('L06', N'An toàn thông tin'),
    ('L07', N'Khoa học máy tính'),
    ('L08', N'Lập trình Web'),
    ('L09', N'Công nghệ mạng'),
    ('L10', N'Lập trình di động');

INSERT INTO SINHVIEN (MaSV, HoSV, TenSV, NgaySinh, GioiTinh, AnhSV, DiaChi, MaLop)
VALUES
    ('SV001', N'Nguyễn', N'An', '2002-01-15', 1, 'anh1.jpg', N'123 Đường A', 'L01'),
    ('SV002', N'Lê', N'Bảo', '2003-02-20', 0, 'anh2.jpg', N'456 Đường B', 'L02'),
    ('SV003', N'Trần', N'Cường', '2004-03-10', 1, 'anh3.jpg', N'789 Đường C', 'L03'),
    ('SV004', N'Phạm', N'Diệu', '2001-06-25', 0, 'anh4.jpg', N'12 Đường D', 'L01'),
    ('SV005', N'Võ', N'Đạt', '2003-12-30', 1, 'anh5.jpg', N'34 Đường E', 'L04'),
    ('SV006', N'Hoàng', N'Hà', '2002-11-21', 0, 'anh6.jpg', N'56 Đường F', 'L05'),
    ('SV007', N'Bùi', N'Hùng', '2001-09-15', 1, 'anh7.jpg', N'78 Đường G', 'L06'),
    ('SV008', N'Ngô', N'Linh', '2003-07-12', 0, 'anh8.jpg', N'90 Đường H', 'L07'),
    ('SV009', N'Đặng', N'Minh', '2002-03-05', 1, 'anh9.jpg', N'11 Đường I', 'L08'),
    ('SV010', N'Phan', N'Nghĩa', '2001-01-01', 1, 'anh10.jpg', N'13 Đường J', 'L09'),
    ('SV011', N'Lương', N'Phong', '2004-04-10', 1, 'anh11.jpg', N'15 Đường K', 'L10'),
    ('SV012', N'Tạ', N'Quỳnh', '2002-05-20', 0, 'anh12.jpg', N'17 Đường L', 'L01'),
    ('SV013', N'Đỗ', N'Triều', '2003-02-14', 1, 'anh13.jpg', N'19 Đường M', 'L02'),
    ('SV014', N'Vũ', N'Uyên', '2004-06-25', 0, 'anh14.jpg', N'21 Đường N', 'L03'),
    ('SV015', N'Trịnh', N'Vân', '2001-08-11', 0, 'anh15.jpg', N'23 Đường O', 'L04'),
    ('SV016', N'Lý', N'Xuân', '2002-10-30', 0, 'anh16.jpg', N'25 Đường P', 'L05'),
    ('SV017', N'Tô', N'Yến', '2003-09-19', 0, 'anh17.jpg', N'27 Đường Q', 'L06'),
    ('SV018', N'Hà', N'Anh', '2004-07-07', 1, 'anh18.jpg', N'29 Đường R', 'L07'),
    ('SV019', N'Kiều', N'Bình', '2002-05-15', 1, 'anh19.jpg', N'31 Đường S', 'L08'),
    ('SV020', N'Đinh', N'Cúc', '2003-12-22', 0, 'anh20.jpg', N'33 Đường T', 'L09');

	CREATE FUNCTION TimKiemSinhVien
(
    @TuKhoa NVARCHAR(100) -- Từ khóa tìm kiếm
)
RETURNS TABLE
AS
RETURN
(
    SELECT MaSV, HoSV, TenSV, NgaySinh, GioiTinh, AnhSV, DiaChi, MaLop
    FROM SINHVIEN
    WHERE MaSV LIKE '%' + @TuKhoa + '%'
       OR HoSV + ' ' + TenSV LIKE '%' + @TuKhoa + '%'
);
GO
