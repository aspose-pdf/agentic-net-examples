using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class HighDpiTableExample
{
    static void Main()
    {
        // Input image files (ensure they exist)
        string[] imagePaths = { "image1.png", "image2.png", "image3.png", "image4.png" };
        foreach (var path in imagePaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Missing image: {path}");
                return;
            }
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the table
            Page page = doc.Pages.Add();

            // Create a table with 2 columns
            Table table = new Table
            {
                ColumnWidths = "200 200", // set column widths (points)
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };

            // Add rows and cells, inserting high‑DPI images
            for (int row = 0; row < 2; row++)
            {
                // Create a new row
                Row tableRow = table.Rows.Add();

                for (int col = 0; col < 2; col++)
                {
                    // Create a new cell
                    Cell cell = tableRow.Cells.Add();

                    // Load the image (Aspose.Pdf.Image) and add it to the cell
                    Image img = new Image
                    {
                        File = imagePaths[row * 2 + col] // select image for this cell
                    };

                    // Optionally set image scaling to fit the cell
                    img.FixWidth = 180;   // width in points
                    img.FixHeight = 120;  // height in points

                    // Add the image to the cell's paragraph collection
                    cell.Paragraphs.Add(img);
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Prepare HTML save options with a higher image resolution (e.g., 600 DPI)
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                ImageResolution = 600, // high‑DPI rendering for images
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
            };

            // Save the document as HTML using the high‑DPI settings
            doc.Save("HighDpiTable.html", htmlOpts);
        }

        Console.WriteLine("PDF with high‑DPI images rendered as HTML table saved to 'HighDpiTable.html'.");
    }
}