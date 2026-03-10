using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Define the output PDF file path
        const string outputPath = "output.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Add a blank page (Pages collection is 1‑based)
            pdfDoc.Pages.Add();

            // Save the document as a PDF file.
            // Document.Save(string) always writes PDF regardless of the file extension.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF document saved to '{outputPath}'.");
    }
}