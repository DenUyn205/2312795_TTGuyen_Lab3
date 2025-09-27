using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2312795_TTGuyen_Lab3
{
    public class DocGhiFile
    {
        private static string FILE_NAME = "danhsachsinhvien.txt";

        public static void GhiFile(List<SinhVien> danhSachSinhVien)
        {
            try
            {
                using (var writer = new StreamWriter(FILE_NAME))
                {
                    foreach (var sv in danhSachSinhVien)
                    {
                        writer.WriteLine(sv.ToFileString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi ghi file: {ex.Message}");
            }
        }

        public static List<SinhVien> DocFile()
        {
            var danhSachSinhVien = new List<SinhVien>();
            try
            {
                if (File.Exists(FILE_NAME))
                {
                    using (var reader = new StreamReader(FILE_NAME))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
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
                                var sv = new SinhVien(mssv, hoVaTenLot, ten, ngaySinh, lop, soCMND, soDT, diaChi, gioiTinh, monHoc);
                                danhSachSinhVien.Add(sv);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đọc file: {ex.Message}");
            }
            return danhSachSinhVien;
        }



    }
}
