using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input image files (high‑DPI PNGs, JPEGs, etc.)
        string[] imagePaths = { "image1.png", "image2.png", "image3.png", "image4.png" };
        const string outputPdfPath = "TableWithHighDpiImages.pdf";
        const string outputHtmlPath = "TableWithHighDpiImages.html";

        // Verify that all image files exist
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Image not found: {path}");
                return;
            }
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with 2 columns and 2 rows (adjust as needed)
            Table table = new Table
            {
                // Position the table on the page (coordinates are in points)
                // Fully qualify Rectangle to avoid ambiguity with System.Drawing
                Left = 50,
                Top = 700,
                // Optional: set column widths (percentage of the page width)
                ColumnWidths = "250 250"
            };

            // Add rows and cells, inserting an image into each cell
            int imgIndex = 0;
            for (int r = 0; r < 2; r++)
            {
                Row row = table.Rows.Add();
                for (int c = 0; c < 2; c++)
                {
                    Cell cell = row.Cells.Add();

                    // Load the image from file
                    Aspose.Pdf.Image img = new Aspose.Pdf.Image();
                    img.File = imagePaths[imgIndex % imagePaths.Length];
                    // The image is added to the cell's paragraph collection.
                    // No ImageFragment class exists; Image is the correct type.
                    cell.Paragraphs.Add(img);

                    imgIndex++;
                }
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save as PDF (default format)
            doc.Save(outputPdfPath);

            // OPTIONAL: Save as HTML with a higher image resolution.
            // HtmlSaveOptions.ImageResolution controls the DPI of raster images in the HTML output.
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                ImageResolution = 600, // High‑DPI rendering (e.g., 600 DPI)
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
            };
            doc.Save(outputHtmlPath, htmlOpts);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
        Console.WriteLine($"HTML saved to '{outputHtmlPath}' with high‑DPI images.");
    }
}