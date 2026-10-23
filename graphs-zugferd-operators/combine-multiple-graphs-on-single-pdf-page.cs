using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the four graph images (PNG, JPEG, etc.)
        string[] graphPaths = {
            "graph1.png",
            "graph2.png",
            "graph3.png",
            "graph4.png"
        };

        // Verify that all image files exist
        foreach (string path in graphPaths)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Image not found: {path}");
                return;
            }
        }

        // Create a new PDF document and add a single page
        using (Document pdfDoc = new Document())
        {
            Page page = pdfDoc.Pages.Add();

            // Use A4 size for simplicity (points: 595 x 842)
            page.PageInfo.Width = 595;
            page.PageInfo.Height = 842;

            // Calculate cell dimensions for a 2x2 grid
            double cellWidth  = page.PageInfo.Width  / 2.0;
            double cellHeight = page.PageInfo.Height / 2.0;

            // Place each graph in its grid cell
            for (int i = 0; i < graphPaths.Length; i++)
            {
                // Determine row (0 or 1) and column (0 or 1)
                int row = i / 2;      // 0 = top, 1 = bottom
                int col = i % 2;      // 0 = left, 1 = right

                // Compute lower‑left corner of the cell (Aspose.Pdf uses lower‑left origin)
                double llx = col * cellWidth;
                double lly = page.PageInfo.Height - ((row + 1) * cellHeight);

                // Create an ImageStamp for the graph
                ImageStamp imgStamp = new ImageStamp(graphPaths[i])
                {
                    // Fit the image inside the cell while preserving aspect ratio
                    Width  = cellWidth,
                    Height = cellHeight,
                    // Align to the lower‑left corner of the cell
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    // Offset from the page origin to the cell's lower‑left corner
                    XIndent = (float)llx,
                    YIndent = (float)lly,
                    // Do not place the image behind page content
                    Background = false
                };

                // Add the stamp to the page
                page.AddStamp(imgStamp);
            }

            // Save the resulting PDF
            const string outputPath = "combined_graphs.pdf";
            pdfDoc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}
