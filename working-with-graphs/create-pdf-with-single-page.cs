using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new empty PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single default‑size page (first page is added automatically)
            doc.Pages.Add();

            // Save the document to a file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF document created and saved to '{outputPath}'.");
    }
}