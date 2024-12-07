using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KT0720HoXuanHung_64130773.Models;

namespace KT0720HoXuanHung_64130773.Controllers
{
    public class SinhVien_64130773Controller : Controller
    {
        private KT0720_64130773Entities db = new KT0720_64130773Entities();

        // GET: SinhVien_64130773
        public ActionResult GioiThieu_64130773()
        {
            return View();
        }
        public ActionResult Index(int? page)
        {
            int pageSize = 5;  // Số lượng sinh viên mỗi trang
            int pageNumber = (page ?? 1);  // Mặc định là trang 1 nếu không có query string

            var sINHVIENs = db.SINHVIENs.Include(s => s.LOP)
                                         .OrderBy(s => s.MaSV)  // Đảm bảo có thứ tự khi phân trang
                                         .Skip((pageNumber - 1) * pageSize)  // Bỏ qua các phần tử của các trang trước
                                         .Take(pageSize)  // Lấy số lượng dữ liệu tương ứng với pageSize
                                         .ToList();

            // Tổng số trang (dựa trên tổng số sinh viên và pageSize)
            int totalItems = db.SINHVIENs.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;

            return View(sINHVIENs);
        }

        // GET: SinhVien_64130773/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);               
            }
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINHVIEN);
        }

        // GET: SinhVien_64130773/Create
        public ActionResult Create()
        {
            // Cung cấp dữ liệu cho dropdown MaLop
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSV,HoSV,TenSV,NgaySinh,GioiTinh,AnhSV,DiaChi,MaLop")] SINHVIEN sINHVIEN)
        {
            // Nhận file từ form
            var uploadedFile = Request.Files["AnhSV"];

            if (ModelState.IsValid)
            {
                if (uploadedFile != null && uploadedFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(uploadedFile.FileName);
                    var relativePath = "~/IMG/" + fileName;
                    var physicalPath = Server.MapPath(relativePath);
                    uploadedFile.SaveAs(physicalPath);

                    sINHVIEN.AnhSV = relativePath;
                }

                db.SINHVIENs.Add(sINHVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop", sINHVIEN.MaLop);
            return View(sINHVIEN);
        }
        // Get: SinhViens64132989/TimKiem
        public ActionResult TimKiem_64130773()
        {
            var sinhViens = db.SINHVIENs.Include(s => s.LOP);
            return View(sinhViens.ToList());
        }

        [HttpGet]
        public ActionResult TimKiem_64130773(string maSV, string hoTen, int page = 1)
        {
            // Lấy danh sách sinh viên từ database và áp dụng bộ lọc
            var sinhViens = db.SINHVIENs.AsQueryable();

            if (!string.IsNullOrEmpty(maSV))
            {
                sinhViens = sinhViens.Where(sv => sv.MaSV == maSV);
            }

            if (!string.IsNullOrEmpty(hoTen))
            {
                sinhViens = sinhViens.Where(sv => sv.HoSV.Contains(hoTen) || sv.TenSV.Contains(hoTen));
            }


            // Thêm OrderBy để tránh lỗi khi sử dụng Skip
            sinhViens = sinhViens.OrderBy(sv => sv.MaSV);
            // Phân trang
            var totalRecords = sinhViens.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / 5);
            var currentRecords = sinhViens.Skip((page - 1) * 5).Take(5).ToList();
            // Truyền dữ liệu phân trang sang View
            ViewBag.CurrentPage = page;
            ViewBag.TotalRecords = totalRecords;
            ViewBag.TotalPages = totalPages;
            return View(currentRecords);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
