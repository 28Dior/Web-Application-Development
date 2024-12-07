namespace KT0720HoXuanHung_64130773.Models
{
using System;
using System.ComponentModel.DataAnnotations;

public partial class SINHVIEN
{
    [Display(Name = "Mã SV")]
    public string MaSV { get; set; }

    [Display(Name = "Họ SV")]
    public string HoSV { get; set; }

    [Display(Name = "Tên SV")]
    public string TenSV { get; set; }

    [Display(Name = "Ngày sinh")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public System.DateTime NgaySinh { get; set; }

    [Display(Name = "Giới Tính")]
    public bool GioiTinh { get; set; }

    [Display(Name = "Ảnh SV")]
    public string AnhSV { get; set; }

    [Display(Name = "Địa Chỉ")]
    public string DiaChi { get; set; }

    [Display(Name = "Mã Lớp")]
    public string MaLop { get; set; }

    public virtual LOP LOP { get; set; }
}
}

