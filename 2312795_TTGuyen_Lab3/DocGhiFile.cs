using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2312795_TTGuyen_Lab3
{
    public class DocGhiFile
    {
        private string FILE_NAME = "danhsachsinhvien.txt";

        public DocGhiFile(string file)
        {
            FILE_NAME = file;
        }

        public List<Student> DocFile()
        {
            List<Student> students = new List<Student>();

            try
            {
                if (!File.Exists(FILE_NAME))
                {
                    MessageBox.Show("File không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return students;
                }

                string[] lines = File.ReadAllLines(FILE_NAME);

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('|');
                    if (parts.Length >= 10)
                    {
                        Student student = new Student();

                        // Parse dữ liệu từ file
                        student.MSSV = parts[0].Trim();
                        student.HovaTenLot = parts[1].Trim();
                        student.Ten = parts[2].Trim();
                        student.Lop = parts[3].Trim();

                        // Parse ngày sinh
                        string ngaySinhStr = parts[4].Trim();
                        string[] dateParts = ngaySinhStr.Split('/');
                        if (dateParts.Length == 3)
                        {
                            int day = int.Parse(dateParts[0]);
                            int month = int.Parse(dateParts[1]);
                            int year = int.Parse(dateParts[2]);
                            student.NgaySinh = new DateTime(year, month, day);
                        }
                        student.SoCMND = parts[5].Trim();
                        student.SoDT = parts[6].Trim();
                        student.DiaChi = parts[7].Trim();

                        // Parse giới tính (0: Nữ, 1: Nam)
                        student.GioiTinh = parts[8].Trim() == "1";

                        // Parse môn học
                        string[] monHoc = parts[9].Split(';');
                        student.MonHoc = new List<string>();
                        foreach (string mh in monHoc)
                        {
                            if (!string.IsNullOrWhiteSpace(mh))
                                student.MonHoc.Add(mh.Trim());
                        }

                        students.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return students;
        }
        public bool GhiFile(List<Student> students)
        {
            try
            {
                List<string> lines = new List<string>();

                foreach (Student student in students)
                {
                    string line = $"{student.MSSV}|{student.HovaTenLot}|{student.Ten}|{student.Lop}|" +
                                 $"{student.NgaySinh:dd/MM/yyyy}|{student.SoCMND}|{student.SoDT}|{student.DiaChi}|" +
                                 $"{(student.GioiTinh ? "1" : "0")}|{string.Join("; ", student.MonHoc)}";
                    lines.Add(line);
                }

                File.WriteAllLines(FILE_NAME, lines);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi ghi file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Hiển thị danh sách sinh viên lên ListView
        public void DisplayStudentsToListView(List<Student> students, ListView listView)
        {
            try
            {
                listView.Items.Clear();
                listView.View = View.Details;

                // Thiết lập columns nếu chưa có
                if (listView.Columns.Count == 0)
                {
                    listView.Columns.Add("MSSV", 100);
                    listView.Columns.Add("Họ tên lót", 150);
                    listView.Columns.Add("Tên", 100);
                    listView.Columns.Add("Lớp", 80);
                    listView.Columns.Add("Ngày sinh", 100);
                    listView.Columns.Add("CCCD", 120);
                    listView.Columns.Add("SĐT", 100);
                    listView.Columns.Add("Địa chỉ", 200);
                    listView.Columns.Add("Giới tính", 80);
                    listView.Columns.Add("Môn học", 200);
                }

                foreach (Student student in students)
                {
                    ListViewItem item = new ListViewItem(student.MSSV);
                    item.SubItems.Add(student.HovaTenLot);
                    item.SubItems.Add(student.Ten);
                    item.SubItems.Add(student.Lop);
                    item.SubItems.Add(student.NgaySinh.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(student.SoCMND);
                    item.SubItems.Add(student.SoDT);
                    item.SubItems.Add(student.DiaChi);
                    item.SubItems.Add(student.GioiTinh ? "Nam" : "Nữ");
                    item.SubItems.Add(string.Join("; ", student.MonHoc));

                    // Lưu đối tượng student vào Tag để sử dụng sau này
                    item.Tag = student;

                    listView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị danh sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
    }
}

