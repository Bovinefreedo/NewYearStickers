using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Svg;

public class SvgGridPdf
{
    public void AddSvgGridToPdf(List<string> svgList, string outputPath)
    {
        // Define A4 page dimensions in points (72 DPI)
        const double pageWidth = 595;  // A4 width
        const double pageHeight = 842; // A4 height

        // Grid dimensions
        const int columns = 4;
        const int rows = 10;
        double cellWidth = pageWidth / columns;
        double cellHeight = pageHeight / rows;

        // Create a new PDF document
        using (PdfDocument document = new PdfDocument())
        {
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Loop through the SVGs and render them in the grid
            for (int i = 0; i < svgList.Count; i++)
            {
                Console.WriteLine(svgList[i]);
                if (i % 40 == 0 && i > 0)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                }
                // Calculate grid position
                int row = (i % 40) / columns;
                int column = (i % 40) % columns;

                // Calculate top-left corner of the cell
                double x = column * cellWidth;
                double y = row * cellHeight;

                // Parse the SVG and render as an image
                var svgDocument = SvgDocument.FromSvg<SvgDocument>(svgList[i]);
                using (var bitmap = svgDocument.Draw())
                {
                    // Convert the image to a memory stream
                    using (var memoryStream = new MemoryStream())
                    {
                        bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        memoryStream.Position = 0;
                        using (XImage xImage = XImage.FromStream(memoryStream))
                        {

                            // Scale the image to fit within the cell
                            gfx.DrawImage(xImage, x, y, cellWidth, cellHeight);
                        }
                    }
                }
            }

            // Save the PDF
            document.Save(outputPath);
        }
    }
}