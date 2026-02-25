using System;
using Aspose.Pdf;   // Aspose.Pdf contains Document and related types

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (optional – a document must have at least one page to be saved)
            doc.Pages.Add();

            // Save the document as a PDF file. No SaveOptions are needed because the default format is PDF.
            doc.Save("output.pdf");
        }

        Console.WriteLine("Document saved as 'output.pdf'.");
    }
}