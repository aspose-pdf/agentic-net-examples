using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageStamp using the first page of the document as the stamp source
            PdfPageStamp stamp = new PdfPageStamp(doc.Pages[1]);

            // Optional visual settings for the stamp
            stamp.Background = true;   // place stamp behind page content
            stamp.Opacity    = 0.5f;   // semi‑transparent

            // Apply the stamp to pages 5 through 10 (inclusive)
            // Ensure we do not exceed the actual page count
            for (int i = 5; i <= 10 && i <= doc.Pages.Count; i++)
            {
                Page targetPage = doc.Pages[i]; // 1‑based indexing
                targetPage.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}