using CheckingElement.Business.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Windows;

namespace CheckingElement.Business.Commponent {
    public class ExcelUtils {

        private IEnumerable<RevitElement> _elements;


        public ExcelUtils(IEnumerable<RevitElement> failedElements) {
            _elements = failedElements;
        }

        public void ExportData(string path) {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("审核未过元素");

            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;

            workSheet.Cells[1, 1].Value = "元素名称";
            workSheet.Cells[1, 2].Value = "元素Id";

            int recordIndex = 2;

            foreach (var article in _elements) {
                workSheet.Cells[recordIndex, 1].Value = article.Name;
                workSheet.Cells[recordIndex, 2].Value = article.ElementId.ToString();
                recordIndex++;
            }

            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();

            //已存在时
            if (File.Exists(path)) {
                try {

                    File.Delete(path);
                } catch (Exception ex) {
                    MessageBox.Show("文件已存在，但是无法删除，自动重新命名");

                    var filePath = Path.GetDirectoryName(path);
                    var extension = Path.GetExtension(path);
                    var name = DateTime.Now.Ticks.ToString();
                    path = Path.Combine(filePath, name + extension);
                }

                File.Create(path).Close();
            }

            FileStream objFileStrm = File.Create(path);
            objFileStrm.Close();

            File.WriteAllBytes(path, excel.GetAsByteArray());
            excel.Dispose();
        }
    }
}
