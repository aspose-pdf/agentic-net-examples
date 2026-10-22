using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Image lives here

class Program
{
    static void Main()
    {
        // Input high‑DPI image files (ensure they exist)
        string[] imagePaths = { "highdpi1.jpg", "highdpi2.jpg", "highdpi3.jpg", "highdpi4.jpg" };
        const string outputPdf = "TableWithHighDpiImages.pdf";

        // Verify all images are present
        foreach (var imgPath in imagePaths)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Image not found: {imgPath}");
                return;
            }
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a table with 2 columns and 2 rows
            Table table = new Table
            {
                // Define column widths (percentage of page width)
                ColumnWidths = "50 50",
                // Optional: set border for visual clarity
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add rows and cells, inserting high‑DPI images
            int imgIndex = 0;
            for (int row = 0; row < 2; row++)
            {
                Row tableRow = table.Rows.Add();
                for (int col = 0; col < 2; col++)
                {
                    // Create a new cell
                    Cell cell = tableRow.Cells.Add();

                    // Load the image and add it to the cell
                    using (FileStream imgStream = new FileStream(imagePaths[imgIndex], FileMode.Open, FileAccess.Read))
                    {
                        Image img = new Image
                        {
                            ImageStream = imgStream,
                            // Set a fixed width (points). Height will be scaled proportionally.
                            // The original DPI of the image is preserved, so the rendered quality remains high.
                            FixWidth = 200
                        };
                        cell.Paragraphs.Add(img);
                    }

                    imgIndex++;
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with high‑DPI images saved to '{outputPdf}'.");
    }
}
