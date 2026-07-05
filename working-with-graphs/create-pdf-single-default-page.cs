using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new empty PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a single page with default size (A4 is used by default)
            doc.Pages.Add();

            // Save the document as PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created at '{outputPath}'.");
    }
}