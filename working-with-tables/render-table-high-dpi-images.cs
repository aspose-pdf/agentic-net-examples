using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string outputPdfPath  = "TableWithImages.pdf";
        const string outputHtmlPath = "TableWithImages.html";
        const string imagePath1     = "image1.jpg";
        const string imagePath2     = "image2.png";

        // Verify image files exist
        if (!File.Exists(imagePath1) || !File.Exists(imagePath2))
        {
            Console.Error.WriteLine("One or more image files not found.");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the table
            Page page = doc.Pages.Add();

            // Create a table with two columns
            Table table = new Table
            {
                ColumnWidths = "200 200", // two columns, each 200 points wide
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // First row – add high‑DPI images to each cell
            Row row1 = table.Rows.Add();
            // Cell 1 – image1
            Image img1 = new Image
            {
                File = imagePath1,
                // Optional: scale image to fit cell (preserve aspect ratio)
                // Width and Height can be set if needed
            };
            row1.Cells[1].Paragraphs.Add(img1);

            // Cell 2 – image2
            Image img2 = new Image
            {
                File = imagePath2
            };
            row1.Cells[2].Paragraphs.Add(img2);

            // Second row – add some descriptive text
            Row row2 = table.Rows.Add();
            row2.Cells[1].Paragraphs.Add(new TextFragment("Image 1 description"));
            row2.Cells[2].Paragraphs.Add(new TextFragment("Image 2 description"));

            // Add the table to the page
            page.Paragraphs.Add(table);

            // -------------------------------------------------
            // Save as PDF (standard resolution)
            doc.Save(outputPdfPath);
            Console.WriteLine($"PDF saved to '{outputPdfPath}'.");

            // -------------------------------------------------
            // Save as HTML with high‑DPI image rendering
            // HtmlSaveOptions.ImageResolution controls the DPI of raster images
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                ImageResolution = 300, // set desired DPI (e.g., 300)
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            doc.Save(outputHtmlPath, htmlOpts);
            Console.WriteLine($"HTML saved to '{outputHtmlPath}' with ImageResolution={htmlOpts.ImageResolution} DPI.");
        }
    }
}