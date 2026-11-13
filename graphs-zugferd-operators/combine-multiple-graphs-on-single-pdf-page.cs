using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the four graph images (PNG, JPEG, etc.)
        string[] graphFiles = { "graph1.png", "graph2.png", "graph3.png", "graph4.png" };
        const string outputPath = "combined.pdf";

        // Verify that all image files exist before proceeding
        foreach (var file in graphFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Graph file not found: {file}");
                return;
            }
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Add a single blank page (first page, 1‑based indexing)
            Page page = pdfDoc.Pages.Add();

            // Determine page dimensions (points). Default size is A4 (595 x 842) unless changed.
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Define a 2x2 grid: each cell occupies half the width and half the height
            double cellWidth = pageWidth / 2;
            double cellHeight = pageHeight / 2;

            // Place each graph image into its corresponding grid cell
            for (int i = 0; i < graphFiles.Length; i++)
            {
                // Row (0 or 1) and column (0 or 1) for the current image
                int row = i / 2;
                int col = i % 2;

                // Calculate lower‑left corner of the cell (origin is bottom‑left)
                double llx = col * cellWidth;
                double lly = pageHeight - ((row + 1) * cellHeight);

                // Create an ImageStamp for the image and set its bounds using XIndent/YIndent and size
                ImageStamp imgStamp = new ImageStamp(graphFiles[i])
                {
                    // Center the image within the cell while preserving aspect ratio
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    // Position the stamp at the lower‑left corner of the cell
                    XIndent = llx,
                    YIndent = lly,
                    // Scale the image to fit the cell (optional – remove if original size is desired)
                    Width  = cellWidth,
                    Height = cellHeight
                };

                // Add the stamp to the page
                page.AddStamp(imgStamp);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Combined PDF saved to '{outputPath}'.");
    }
}
