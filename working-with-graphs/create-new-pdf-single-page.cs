using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "single_page.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a default‑size page to the document
            doc.Pages.Add();

            // Save the document as a PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created at: {outputPath}");
    }
}