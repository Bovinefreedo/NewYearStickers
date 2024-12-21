

string filePath = "C:/Users/hotso/source/repos/NewYearStickers/NewYearStickers/Extraction/NewYearMenu.xlsx";
if (!File.Exists(filePath))
{
    throw new FileNotFoundException($"Excel file not found at: {filePath}");
}

var extractor = new DataExtractor();
string cellValue = extractor.getCell(2, 1, "Hold"); // Gets cell A1 from Sheet1
Console.WriteLine(cellValue);

List<string> columnData = extractor.getCellsInColumn(1, 1, 50, "Hold");

foreach (string cell in columnData) { 
    Console.WriteLine(cell);
}

var generator = new StickerGenerator();

