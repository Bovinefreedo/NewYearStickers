
namespace NewYearStickers.Stickers
{
    public class StickerGenerator
    {
        public string makeSVG(string hold, string people, string course, string amount)
        {
            return $@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""51mm"" height=""38mm"" viewBox=""0 0 51 38"">
        <circle cx=""8"" cy=""8"" r=""6"" fill=""white"" stroke=""black"" stroke-width=""0.5""/>
        <text x=""8"" y=""9"" font-family=""Arial"" font-size=""5"" text-anchor=""middle"" fill=""black"">{hold}</text>
        <text x=""25.5"" y=""22"" font-family=""Arial"" font-size=""5"" text-anchor=""middle"" fill=""black"">{people} x {course}</text>
        <text x=""25.5"" y=""30"" font-family=""Arial"" font-size=""5"" text-anchor=""middle"" fill=""black"">{amount}</text>
    </svg>";
        }
    }
}