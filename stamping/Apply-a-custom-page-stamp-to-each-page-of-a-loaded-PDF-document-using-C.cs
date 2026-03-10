using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // PDF to be stamped
        const string stampPath = "stamp.pdf";   // PDF containing the stamp page
        const string outputPath = "output.pdf"; // Resulting PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp PDF not found: {stampPath}");
            return;
        }

        // Load the target document and the stamp document inside using blocks for deterministic disposal
        using (Document doc = new Document(inputPath))
        using (Document stampDoc = new Document(stampPath))
        {
            // Use the first page of the stamp document as the stamp content
            Page stampPage = stampDoc.Pages[1];

            // Create a PdfPageStamp based on the stamp page and configure its appearance
            PdfPageStamp pdfStamp = new PdfPageStamp(stampPage)
            {
                Opacity = 0.5,                     // 50% transparent
                Background = false,                // draw on top of existing content
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Zoom = 0.3                         // scale to 30% of the target page size
            };

            // Apply the stamp to every page of the target document (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.AddStamp(pdfStamp);
            }

            // Save the stamped PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}