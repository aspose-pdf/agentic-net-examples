using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new empty PDF document.
        // The Document class implements IDisposable, so wrap it in a using block
        // as required by the lifecycle rule.
        using (Document pdfDoc = new Document())
        {
            // Optional: add a blank page to start building content.
            // Pages collection is 1‑based; Add() appends a new page.
            pdfDoc.Pages.Add();

            // Save the newly created PDF. No SaveOptions are needed because the
            // output format is PDF.
            pdfDoc.Save("newDocument.pdf");
        }

        Console.WriteLine("PDF document created and saved as 'newDocument.pdf'.");
    }
}