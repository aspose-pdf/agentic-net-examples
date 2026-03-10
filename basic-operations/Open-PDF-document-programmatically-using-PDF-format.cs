using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document (PDF format) using the Document constructor
        // The using block ensures deterministic disposal of the Document object
        using (Document pdfDoc = new Document(inputPath))
        {
            // Example operation: output the total number of pages (1‑based indexing)
            Console.WriteLine($"Page count: {pdfDoc.Pages.Count}");

            // Optionally, save a copy of the opened document
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Document processed and saved to '{outputPath}'.");
    }
}