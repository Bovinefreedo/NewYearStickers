
using NewYearStickers.Extraction;
using NewYearStickers.Stickers;
using System.Security.Cryptography.X509Certificates;
Random random = new Random();
var svgList = new List<string>
{
    @"<svg xmlns=""http://www.w3.org/2000/svg"" width=""51mm"" height=""38mm"" viewBox=""0 0 51 38"">
        <circle cx=""8"" cy=""8"" r=""6"" fill=""white"" stroke=""black"" stroke-width=""0.5""/>
        <text x=""8"" y=""9"" font-family=""Arial"" font-size=""5"" text-anchor=""middle"" fill=""black"">1</text>
        <text x=""25.5"" y=""22"" font-family=""Arial"" font-size=""5"" text-anchor=""middle"" fill=""black"">10 x Bisque</text>
        <text x=""25.5"" y=""30"" font-family=""Arial"" font-size=""5"" text-anchor=""middle"" fill=""black"">750</text>
    </svg>",
};

for (int i = 2; i <= 40; i++) {
    int person = random.Next(15);
    int amount = 75 * person;
    string baseString = @$"<svg xmlns=""http://www.w3.org/2000/svg"" width=""51mm"" height=""38mm"" viewBox=""0 0 51 38"">
        <circle cx=""8"" cy=""8"" r=""6"" fill=""white"" stroke=""black"" stroke-width=""0.5""/>
        <text x=""8"" y=""9"" font-family=""Arial"" font-size=""5"" text-anchor=""middle"" fill=""black"">{i}</text>
        <text x=""25.5"" y=""22"" font-family=""Arial"" font-size=""5"" text-anchor=""middle"" fill=""black"">{person} x Bisque</text>
        <text x=""25.5"" y=""30"" font-family=""Arial"" font-size=""5"" text-anchor=""middle"" fill=""black"">{amount}</text>
    </svg>";
    svgList.Add(baseString);
}
DataExtractor data = new();

//TheStickersCore core = new TheStickersCore();

//core.printAllStickers();

//List<string> strings = data.getCellsInColumn(1, 2, 50, "Hold");

//int[,] ints = data.intsInRange(2, 2, 101, 7, "Hold");
//for (int i = 0; i < ints.GetLength(0); i++) {
//    for (int j = 0; j < ints.GetLength(1); j++) {
//        Console.Write(ints[i, j]);
//    }
//    Console.WriteLine();
//}

//var svgGridPdf = new SvgGridPdf();
//svgGridPdf.AddSvgGridToPdf(svgList, @"C:\Users\hotso\Documents\Stickers\stickers.pdf");
//Console.ReadLine();