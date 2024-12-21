public class StickerGenerator
{
    private readonly DataExtractor dataExtractor;

    public StickerGenerator()
    {
        dataExtractor = new DataExtractor();
    }

    public string makeSVG(string hold, string people, string course, string amount)
    {
        const string SVG_TEMPLATE = @"<svg xmlns=""http://www.w3.org/2000/svg"" width=""51mm"" height=""38mm"" viewBox=""0 0 51 38"">
        <circle cx=""8"" cy=""8"" r=""6"" fill=""white"" stroke=""black"" stroke-width=""0.5""/>
        <text x=""8"" y=""9"" font-family=""Arial"" font-size=""5"" text-anchor=""middle"" fill=""black"">{0}</text>
        <text x=""25.5"" y=""22"" font-family=""Arial"" font-size=""5"" text-anchor=""middle"" fill=""black"">{1} x {2}</text>
        <text x=""25.5"" y=""30"" font-family=""Arial"" font-size=""5"" text-anchor=""middle"" fill=""black"">{3}</text>
    </svg>";

        return string.Format(SVG_TEMPLATE, hold, people, course, amount);
    }
}