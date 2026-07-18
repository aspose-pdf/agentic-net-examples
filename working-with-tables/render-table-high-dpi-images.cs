using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string imagePath = "highres.png";
        const string pdfOutput = "table_highdpi.pdf";
        const string htmlOutput = "table_highdpi.html";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with two columns
            Table table = new Table
            {
                // Define column widths (in points)
                ColumnWidths = "250 250",
                // Optional: set a default border for all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black)
            };

            // Add the first row
            Row row = table.Rows.Add();

            // First cell – add high‑DPI image
            Cell cell1 = row.Cells.Add();
            Aspose.Pdf.Image img1 = new Aspose.Pdf.Image
            {
                // Load image from file
                File = imagePath,
                // Scale image to fit cell (optional)
                FixWidth = 240
            };
            cell1.Paragraphs.Add(img1);

            // Second cell – add the same image (or another one)
            Cell cell2 = row.Cells.Add();
            Aspose.Pdf.Image img2 = new Aspose.Pdf.Image
            {
                File = imagePath,
                FixWidth = 240
            };
            cell2.Paragraphs.Add(img2);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the document as PDF (standard resolution)
            doc.Save(pdfOutput);

            // Save the document as HTML with high‑DPI images
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Set image resolution to 600 DPI for higher quality
                ImageResolution = 600,
                // Embed raster images as PNG inside SVG wrappers
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                // Embed all resources into a single HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
            };
            doc.Save(htmlOutput, htmlOpts);
        }

        Console.WriteLine("Table with high‑DPI images saved as PDF and HTML.");
    }
}