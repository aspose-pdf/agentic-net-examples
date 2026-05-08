using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "new.pdf";

        // Create an empty PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a single page with the default size (A4 is used for a new document)
            doc.Pages.Add();

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created at '{outputPath}'.");
    }
}