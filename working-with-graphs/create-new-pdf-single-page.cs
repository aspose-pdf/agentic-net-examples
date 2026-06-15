using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "new.pdf";

        // Create an empty PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single default‑size page (size is chosen automatically)
            Page page = doc.Pages.Add();

            // Save the document to a file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created at '{outputPath}'.");
    }
}