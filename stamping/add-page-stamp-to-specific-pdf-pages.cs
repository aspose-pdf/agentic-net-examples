using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose a source page to be used as the stamp (e.g., the first page)
            Page sourcePage = doc.Pages[1];

            // Create a PdfPageStamp from the source page
            Aspose.Pdf.PdfPageStamp pageStamp = new Aspose.Pdf.PdfPageStamp(sourcePage);

            // Optional: configure stamp appearance (background, opacity, etc.)
            pageStamp.Background = false;   // stamp appears on top of content
            pageStamp.Opacity   = 0.8;      // semi‑transparent

            // Apply the stamp to pages 5 through 10 (inclusive)
            // Ensure we do not exceed the actual page count
            int lastPage = Math.Min(10, doc.Pages.Count);
            for (int i = 5; i <= lastPage; i++)
            {
                // Each page has an AddStamp method that accepts a Stamp instance
                doc.Pages[i].AddStamp(pageStamp);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}