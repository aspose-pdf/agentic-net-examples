using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string headerImagePath = "header.png";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(headerImagePath))
        {
            Console.Error.WriteLine($"Header image not found: {headerImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Create a new ImageStamp for each page
                using (FileStream imgStream = File.OpenRead(headerImagePath))
                {
                    ImageStamp stamp = new ImageStamp(imgStream)
                    {
                        // Place the stamp over existing content
                        Background = false,

                        // Center horizontally, align to the top vertically
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment   = VerticalAlignment.Top,

                        // Add a small indent from the top edge to avoid overlap
                        YIndent = 10   // points from the top margin
                    };

                    // Add the stamp to the current page
                    doc.Pages[pageNum].AddStamp(stamp);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Header image added to each page. Saved as '{outputPdfPath}'.");
    }
}