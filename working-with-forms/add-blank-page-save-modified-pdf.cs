using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the original and the modified PDF files
        const string inputPath  = "original.pdf";
        const string outputPath = "modified_copy.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the original PDF, make modifications, and save to a new file
        using (Document pdfDoc = new Document(inputPath))
        {
            // Example modification: add a blank page at the end of the document
            pdfDoc.Pages.Add();

            // Save the modified document to a new file name to preserve the original
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
    }
}