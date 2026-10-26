using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "single_page.pdf";

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single default‑size page (A4 by default)
            doc.Pages.Add();

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}