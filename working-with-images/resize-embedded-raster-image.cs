using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Text;          // For text-related types if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // PDF containing the raster image
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Scan the page's paragraph collection for Image objects
                for (int i = 1; i <= page.Paragraphs.Count; i++)
                {
                    // The Paragraphs collection also uses 1‑based indexing
                    if (page.Paragraphs[i] is Image img)
                    {
                        // Set desired dimensions (in points; 1 point = 1/72 inch)
                        // Example: resize to 200x150 points
                        img.FixWidth  = 200;   // Width
                        img.FixHeight = 150;   // Height

                        // Optional: adjust alignment if needed
                        img.HorizontalAlignment = HorizontalAlignment.Center;
                        img.VerticalAlignment   = VerticalAlignment.Center;
                    }
                }
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}