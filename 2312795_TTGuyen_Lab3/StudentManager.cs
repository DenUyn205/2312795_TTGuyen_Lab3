using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2312795_TTGuyen_Lab3
{
    public class StudentManager
    {
        public List<Student> DanhSachSinhVien { get; set; }
        private readonly string filePath = "danhsachsinhvien.txt";
        public StudentManager()
        {
            DanhSachSinhVien = new List<Student>();
            LoadFromFile();


        }

        public StudentManager(string sv)
        {
        }

        public void ThemSinhVien(Student sv)
        {
            DanhSachSinhVien.Add(sv);
            SaveToFile();

        }
        public void XoaSinhVien(string mssv)
        {
            var sv = TimSinhVien(mssv);
            if (sv != null)
            {
                DanhSachSinhVien.Remove(sv);
                SaveToFile();
            }
        }
        public void XoaNhieuSinhVien(List<string> dsMSSV)
        {
            DanhSachSinhVien.RemoveAll(sv => dsMSSV.Contains(sv.MSSV));
            SaveToFile();
        }
        public Student TimSinhVien(string mssv)
        {
            return DanhSachSinhVien.FirstOrDefault(sv => sv.MSSV == mssv);
        }
        public List<Student> TimKiemTheoTen(string ten)
        {
            return DanhSachSinhVien.Where(sv => sv.Ten.IndexOf(ten, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }
        public List<Student> TimKiemTheoLop(string lop)
        {
            return DanhSachSinhVien.Where(sv => sv.Lop.Equals(lop, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public void CapNhatSinhVien(Student svCapNhat)
        {
            var sv = TimSinhVien(svCapNhat.MSSV);
            if (sv != null)
            {
                sv.HovaTenLot = svCapNhat.HovaTenLot;
                sv.Ten = svCapNhat.Ten;
                sv.NgaySinh = svCapNhat.NgaySinh;
                sv.Lop = svCapNhat.Lop;
                sv.SoCMND = svCapNhat.SoCMND;
                sv.SoDT = svCapNhat.SoDT;
                sv.DiaChi = svCapNhat.DiaChi;
                sv.GioiTinh = svCapNhat.GioiTinh;
                sv.MonHoc = new List<string>(svCapNhat.MonHoc);
                SaveToFile();
            }
        }
        public void SaveToFile()
        {
            var lines = DanhSachSinhVien.Select(sv => sv.ToFileString()).ToArray();
            File.WriteAllLines(filePath, lines, Encoding.UTF8);
        }
        public void LoadFromFile()
        {
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath, Encoding.UTF8);
                DanhSachSinhVien = lines.Select(line =>
                {
                    var parts = line.Split('|');
                    if (parts.Length == 10)
                    {
                        var mssv = parts[0];
                        var hoVaTenLot = parts[1];
                        var ten = parts[2];
                        var ngaySinh = DateTime.ParseExact(parts[3], "dd/MM/yyyy", null);
                        var lop = parts[4];
                        var soCMND = parts[5];
                        var soDT = parts[6];
                        var diaChi = parts[7];
                        var gioiTinh = bool.Parse(parts[8]);
                        var monHoc = parts[9].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        return new Student(mssv, hoVaTenLot, ten, ngaySinh, lop, soCMND, soDT, diaChi, gioiTinh, monHoc);
                    }
                    return null;
                }).Where(sv => sv != null).ToList();
            }
        }
    }
}
