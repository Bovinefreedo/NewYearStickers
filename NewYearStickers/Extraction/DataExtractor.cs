using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
namespace NewYearStickers.Extraction
{
    public class DataExtractor
    {
        private readonly string filePath;

        public DataExtractor()
        {
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
            this.filePath = Path.Combine(projectDirectory, "Extraction", "NewYearMenu.xlsx");

            Console.WriteLine($"Attempting to access file at: {this.filePath}"); // Debug line

            if (!File.Exists(this.filePath))
            {
                throw new FileNotFoundException($"Excel file not found at: {this.filePath}");
            }
        }

        public string getCell(int row, int column, string sheet)
        {
            Excel.Application excel = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            Excel.Range cell = null;

            try
            {
                // Create Excel Application
                excel = new Excel.Application();
                excel.Visible = false;

                workbook = excel.Workbooks.Open(filePath);

                worksheet = (Excel.Worksheet)workbook.Sheets[sheet];

                cell = (Excel.Range)worksheet.Cells[row, column];

                string value = cell.Value2?.ToString() ?? string.Empty;

                return value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
            finally
            {
                // Cleanup in reverse order of creation
                if (cell != null) Marshal.ReleaseComObject(cell);
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

        public List<string> getCellsInColumn(int column, int startRow, int endRow, string sheet)
        {
            Excel.Application excel = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            Excel.Range range = null;
            List<string> values = new List<string>();

            try
            {
                excel = new Excel.Application();
                excel.Visible = false;

                workbook = excel.Workbooks.Open(filePath);
                worksheet = (Excel.Worksheet)workbook.Sheets[sheet];

                // Get the entire range at once for better performance
                range = worksheet.Range[worksheet.Cells[startRow, column], worksheet.Cells[endRow, column]];

                // Convert the range to an array
                object[,] rangeArray = range.Value2 as object[,];

                // Extract values from the array
                for (int i = 1; i <= rangeArray.GetLength(0); i++)
                {
                    string value = rangeArray[i, 1]?.ToString() ?? string.Empty;
                    values.Add(value);
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

        public int[,] intsInRange(int startRow, int startColumn, int endRow, int endColumn)
        {
            throw new NotImplementedException();
        }

        public List<string> getCellsInRow(int row, int startColumn, int endColumn, string sheet)
        {
            Excel.Application excel = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            Excel.Range range = null;
            List<string> values = new List<string>();

            try
            {
                excel = new Excel.Application();
                excel.Visible = false;

                workbook = excel.Workbooks.Open(filePath);
                worksheet = (Excel.Worksheet)workbook.Sheets[sheet];

                // Get the entire range at once
                range = worksheet.Range[worksheet.Cells[row, startColumn], worksheet.Cells[row, endColumn]];

                // Convert the range to an array
                object[,] rangeArray = range.Value2 as object[,];

                // Extract values from the array
                for (int i = 1; i <= rangeArray.GetLength(1); i++)
                {
                    string value = rangeArray[1, i]?.ToString() ?? string.Empty;
                    values.Add(value);
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
    }
}