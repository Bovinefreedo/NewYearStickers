using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using NewYearStickers.Stickers;
namespace NewYearStickers.Extraction
{
    public class DataExtractor
    {
        private readonly string filePath;
        private Excel.Application excel = null;
        private Excel.Workbook workbook = null;
        private Excel.Worksheet worksheet = null;
        private Excel.Range range = null;

        public DataExtractor()
        {

            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            this.filePath = Path.Combine(projectDirectory, "Extraction", "NewYearMenu.xlsx");

            if (!File.Exists(this.filePath))
            {
                throw new FileNotFoundException($"Excel file not found at: {this.filePath}");
            }
        }

        public void openWorkbook() {
            // Create Excel Application
            excel = new Excel.Application();
            excel.Visible = false;
            workbook = excel.Workbooks.Open(filePath);
        }


        public int[,] intsInRange()
        {
            Excel.Range range = null;
            int[,] values = new int[100,5];

            try
            {
                excel = new Excel.Application();
                excel.Visible = false;
                int number = 0;

                workbook = excel.Workbooks.Open(filePath);
                worksheet = (Worksheet)workbook.Sheets["Hold"];

                // Get the entire range at once
                range = worksheet.Range[worksheet.Cells[2, 2], worksheet.Cells[101, 7]];

                // Convert the range to an array
                object[,] rangeArray = range.Value2 as object[,];

                // Extract values from the array
                for (int i = 1; i < rangeArray.GetLength(0); i++)
                {
                    for (int j = 1; j < rangeArray.GetLength(1); j++) {
                        string? value = rangeArray[i, j]?.ToString();
                        if (value == null)
                            number = 0;
                        else
                            number = int.Parse(value);
                        values[i-1, j-1] = number;
                    }
                }

                return values;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
            finally
            {
                // Cleanup in reverse order
                if (range != null) Marshal.ReleaseComObject(range);
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);
                if (workbook != null)
                {
                    workbook.Close(false);
                    Marshal.ReleaseComObject(workbook);
                }
                if (excel != null)
                {
                    excel.Quit();
                    Marshal.ReleaseComObject(excel);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        //List<string> list = dataExtractor.getCellsInRow(2 + j, (i * 2) + 1, (i * 2) + 2, "Menu");
        public MenuElement[][] getMenuElements() {
            MenuElement[][] menu = new MenuElement[5][];
            openWorkbook();
            worksheet = (Worksheet)workbook.Sheets["Retter"];
            
            for (int i = 0; i < 5; i++) {
                int j = 2;
                List<MenuElement> dishElements = new();
                while (true) {
                    range = worksheet.Range[worksheet.Cells[j , (i*2)+1], worksheet.Cells[j , (i*2)+2]];

                    // Convert the range to an array
                    object[,] rangeArray = range.Value2 as object[,];
                    if (rangeArray[1, 1] == null || rangeArray[1, 2] == null) {
                        break;
                    }
                    MenuElement menuElement = new MenuElement {
                        name=rangeArray[1, 1].ToString(),
                        amount= int.Parse(rangeArray[1, 2].ToString())
                    };
                    dishElements.Add(menuElement);
                    j++;
                }
                menu[i] = dishElements.ToArray();
            }

            return menu;         
        }


        //public string getCell(int row, int column, string sheet)
        //{
        //    Excel.Range cell = null;

        //    try
        //    {


        //        worksheet = (Excel.Worksheet)workbook.Sheets[sheet];

        //        cell = (Excel.Range)worksheet.Cells[row, column];

        //        string value = cell.Value2?.ToString() ?? string.Empty;

        //        return value;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //        Console.WriteLine($"Stack trace: {ex.StackTrace}");
        //        throw;
        //    }
        //    finally
        //    {
        //        // Cleanup in reverse order of creation
        //        if (cell != null) Marshal.ReleaseComObject(cell);
        //        if (worksheet != null) Marshal.ReleaseComObject(worksheet);
        //        if (workbook != null)
        //        {
        //            workbook.Close(false);
        //            Marshal.ReleaseComObject(workbook);
        //        }
        //        if (excel != null)
        //        {
        //            excel.Quit();
        //            Marshal.ReleaseComObject(excel);
        //        }

        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //    }
        //}

        //public List<string> getCellsInColumn(int column, int startRow, int endRow, string sheet)
        //{
        //    Excel.Range range = null;
        //    List<string> values = new List<string>();

        //    try
        //    {
        //        excel = new Excel.Application();
        //        excel.Visible = false;

        //        workbook = excel.Workbooks.Open(filePath);
        //        worksheet = (Excel.Worksheet)workbook.Sheets[sheet];

        //        // Get the entire range at once for better performance
        //        range = worksheet.Range[worksheet.Cells[startRow, column], worksheet.Cells[endRow, column]];

        //        // Convert the range to an array
        //        object[,] rangeArray = range.Value2 as object[,];

        //        // Extract values from the array
        //        for (int i = 1; i <= rangeArray.GetLength(0); i++)
        //        {
        //            string value = rangeArray[i, 1]?.ToString() ?? string.Empty;
        //            values.Add(value);
        //        }

        //        return values;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //        Console.WriteLine($"Stack trace: {ex.StackTrace}");
        //        throw;
        //    }
        //    finally
        //    {
        //        // Cleanup in reverse order
        //        if (range != null) Marshal.ReleaseComObject(range);
        //        if (worksheet != null) Marshal.ReleaseComObject(worksheet);
        //        if (workbook != null)
        //        {
        //            workbook.Close(false);
        //            Marshal.ReleaseComObject(workbook);
        //        }
        //        if (excel != null)
        //        {
        //            excel.Quit();
        //            Marshal.ReleaseComObject(excel);
        //        }

        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //    }
        //}

        //public List<string> getCellsInRow(int row, int startColumn, int endColumn, string sheet)
        //{
        //    Excel.Range range = null;
        //    List<string> values = new List<string>();

        //    try
        //    {
        //        excel = new Excel.Application();
        //        excel.Visible = false;

        //        workbook = excel.Workbooks.Open(filePath);
        //        worksheet = (Excel.Worksheet)workbook.Sheets[sheet];

        //        // Get the entire range at once
        //        range = worksheet.Range[worksheet.Cells[row, startColumn], worksheet.Cells[row, endColumn]];

        //        // Convert the range to an array
        //        object[,] rangeArray = range.Value2 as object[,];

        //        // Extract values from the array
        //        for (int i = 1; i <= rangeArray.GetLength(1); i++)
        //        {
        //            string value = rangeArray[1, i]?.ToString() ?? string.Empty;
        //            values.Add(value);
        //        }

        //        return values;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //        Console.WriteLine($"Stack trace: {ex.StackTrace}");
        //        throw;
        //    }
        //    finally
        //    {
        //        // Cleanup in reverse order
        //        if (range != null) Marshal.ReleaseComObject(range);
        //        if (worksheet != null) Marshal.ReleaseComObject(worksheet);
        //        if (workbook != null)
        //        {
        //            workbook.Close(false);
        //            Marshal.ReleaseComObject(workbook);
        //        }
        //        if (excel != null)
        //        {
        //            excel.Quit();
        //            Marshal.ReleaseComObject(excel);
        //        }

        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //    }
        //}
    }
}