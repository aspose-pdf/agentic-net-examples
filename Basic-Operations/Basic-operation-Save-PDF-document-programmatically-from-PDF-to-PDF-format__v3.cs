using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document and related types

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF file
        const string outputPath = "output.pdf";  // destination PDF file

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF document, perform any required operations, and save it as PDF
        using (Document pdfDoc = new Document(inputPath))
        {
            // No modifications are required for a simple copy;
            // the Save method without SaveOptions always writes PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
    }
}