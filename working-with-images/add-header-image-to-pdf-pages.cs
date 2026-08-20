using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string headerImagePath = "header.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(headerImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImagePath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Fixed height for the header image (in points)
            const double headerHeight = 50.0;

            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Get page dimensions
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Define rectangle for the header image:
                // lower‑left (0, pageHeight - headerHeight), upper‑right (pageWidth, pageHeight)
                // This places the image at the top of the page without overlapping existing content.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    0,                     // llx
                    pageHeight - headerHeight, // lly
                    pageWidth,            // urx
                    pageHeight            // ury
                );

                // Add the image to the page. The method keeps the image aspect ratio.
                page.AddImage(headerImagePath, rect);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header image added to each page. Saved as '{outputPath}'.");
    }
}