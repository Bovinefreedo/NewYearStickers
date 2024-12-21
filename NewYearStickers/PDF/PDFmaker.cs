using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Text;
using Svg;

public class PDFGenerator
{
    private List<StickerData> stickers;
    private readonly StickerGenerator stickerGenerator;
    private const int STICKERS_PER_ROW = 2;
    private const int STICKERS_PER_PAGE = 8;
    private const float STICKER_WIDTH = 200;
    private const float STICKER_HEIGHT = 100;
    private const float MARGIN = 20;

    public PDFGenerator(StickerGenerator stickerGenerator)
    {
        // Register encoding provider for PDF generation
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        this.stickerGenerator = stickerGenerator;
        stickers = new List<StickerData>();
    }

    public class StickerData
    {
        public string Hold { get; set; }
        public string People { get; set; }
        public string Course { get; set; }
        public string Amount { get; set; }

        public StickerData(string hold, string people, string course, string amount)
        {
            Hold = hold;
            People = people;
            Course = course;
            Amount = amount;
        }
    }

    public void AddSticker(string hold, string people, string course, string amount)
    {
        stickers.Add(new StickerData(hold, people, course, amount));
    }

    public void GeneratePDF(string outputPath)
    {
        using (PdfDocument document = new PdfDocument())
        {
            int currentSticker = 0;

            while (currentSticker < stickers.Count)
            {
                PdfPage page = document.AddPage();
                page.Size = PdfSharp.PageSize.A4;
                using (XGraphics gfx = XGraphics.FromPdfPage(page))
                {
                    for (int row = 0; row < 4 && currentSticker < stickers.Count; row++)
                    {
                        for (int col = 0; col < STICKERS_PER_ROW && currentSticker < stickers.Count; col++)
                        {
                            float x = MARGIN + (col * (STICKER_WIDTH + MARGIN));
                            float y = MARGIN + (row * (STICKER_HEIGHT + MARGIN));

                            DrawSticker(gfx, stickers[currentSticker], x, y);
                            currentSticker++;
                        }
                    }
                }
            }

            document.Save(outputPath);
        }
    }

    private void DrawSticker(XGraphics gfx, StickerData sticker, float x, float y)
    {
        // Create temporary SVG file
        string tempSvgPath = Path.GetTempFileName() + ".svg";
        try
        {
            // Use the existing StickerGenerator to generate the SVG
            stickerGenerator.GenerateSpecificSticker(
                sticker.Hold,
                sticker.People,
                sticker.Course,
                sticker.Amount,
                tempSvgPath
            );

            // Load and render SVG
            var svgDocument = SvgDocument.Open(tempSvgPath);
            using (var bitmap = svgDocument.Draw())
            {
                // Convert bitmap to XImage
                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ms.Position = 0;
                    XImage xImage = XImage.FromStream(ms);

                    // Draw the image
                    gfx.DrawImage(xImage, x, y, STICKER_WIDTH, STICKER_HEIGHT);
                }
            }
        }
        finally
        {
            // Cleanup temporary file
            if (File.Exists(tempSvgPath))
            {
                File.Delete(tempSvgPath);
            }
        }
    }

    public void ClearStickers()
    {
        stickers.Clear();
    }
}