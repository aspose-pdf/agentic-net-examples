using System;
using Aspose.Pdf;   // Aspose.Pdf namespace provides Document, DocumentFactory, etc.

class Program
{
    static void Main()
    {
        const string outputPath = "new.pdf";

        // Create an empty PDF document using the DocumentFactory (lifecycle rule)
        DocumentFactory factory = new DocumentFactory();
        using (Document doc = factory.CreateDocument())
        {
            // Add a single default‑size page (Pages.Add() creates a page with the most common size)
            doc.Pages.Add();

            // Save the document as PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created and saved to '{outputPath}'.");
    }
}