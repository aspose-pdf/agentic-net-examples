using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "new_document.pdf";

        // Create an empty PDF document, add a blank page, and save it as PDF.
        using (Document doc = new Document())
        {
            // Ensure the document has at least one page.
            doc.Pages.Add();

            // Save the document; without specifying SaveOptions it defaults to PDF format.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF document created at '{outputPath}'.");
    }
}