using Cars.ViewModels;
using OfficeOpenXml;

namespace Cars.ExportTools
{
    public class ExcelExport
    {
        public byte[] Generate(ServiceCenterViewModel viewModel)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets.Add("Service Centers");

            sheet.Cells["B2"].Value = "Firm";
            sheet.Cells["C2"].Value = "Name";
            sheet.Cells["D2"].Value = "Car Amount";
            sheet.Cells["E2"].Value = "City";
            sheet.Cells["F2"].Value = "Street";
            sheet.Cells["G2"].Value = "Building";

            var column = 2;
            var row = 3;

            foreach (var serviceCenter in viewModel.ServiceCenters)
            {
                sheet.Cells[row, column].Value = serviceCenter.FirmName;
                sheet.Cells[row, column + 1].Value = serviceCenter.CenterName;
                sheet.Cells[row, column + 2].Value = serviceCenter.CarsAmount;
                sheet.Cells[row, column + 3].Value = serviceCenter.City;
                sheet.Cells[row, column + 4].Value = serviceCenter.Street;
                sheet.Cells[row, column + 5].Value = serviceCenter.Building;
                row++;
            }

            return package.GetAsByteArray();
        }
    }
}
