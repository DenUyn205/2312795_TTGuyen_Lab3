using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2312795_TTGuyen_Lab3
{
    public class SinhVien
    {
        public string MSSV { get; set; }
        public string HovaTenLot { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Lop { get; set; }
        public string SoCMND { get; set; }
        public string SoDT { get; set; }
        public string DiaChi { get; set; }
        public bool GioiTinh { get; set; }
        public List<string> MonHoc { get; set; }

        public SinhVien()
        {

        }
        public SinhVien(string mssv, string hoVaTenLot, string ten, DateTime ngaySinh, string lop, string soCMND, string soDT, string diaChi, bool gioiTinh, List<string> monHoc)
        {
            MSSV = mssv;
            HovaTenLot = hoVaTenLot;
            Ten = ten;
            NgaySinh = ngaySinh;
            Lop = lop;
            SoCMND = soCMND;
            SoDT = soDT;
            DiaChi = diaChi;
            GioiTinh = gioiTinh;
            MonHoc = monHoc;
        }
        public override string ToString()
        {
            string gioiTinh = GioiTinh ? "Nam" : "Nữ";
            string monHoc = string.Join(", ", MonHoc);
            return $"MSSV: {MSSV}, Họ và Tên Lót: {HovaTenLot}, Tên: {Ten}, Ngày Sinh: {NgaySinh.ToShortDateString()}, Lớp: {Lop}, Số CMND: {SoCMND}, Số ĐT: {SoDT}, Địa Chỉ: {DiaChi}, Giới Tính: {gioiTinh}, Môn Học: {monHoc}";
        }
        public string ToFileString()
        {
            string gioiTinh  = GioiTinh.ToString();
            string monHoc = string.Join(",", MonHoc ?? new List<string>()); 
            return $"{MSSV}|{HovaTenLot}|{Ten}|{NgaySinh:dd/MM/yyyy}|{Lop}|{SoCMND}|{SoDT}|{DiaChi}|{gioiTinh}|{monHoc}";
        }
    }
   
}

