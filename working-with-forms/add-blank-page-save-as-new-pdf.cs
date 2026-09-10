using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Facades;      // Optional, if you need Facades features

class Program
{
    static void Main()
    {
        // Paths for the original and the new PDF files
        const string inputPath  = "original.pdf";
        const string outputPath = "modified_copy.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF, make a simple modification, and save to a new file
        // Document implements IDisposable, so wrap it in a using block (see document-disposal-with-using rule)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Example modification: add a blank page at the end
            // (Pages collection is 1‑based, see page-indexing-one-based rule)
            pdfDoc.Pages.Add();

            // Save the modified document to a new file name.
            // Using the overload Save(string) preserves the PDF format.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
    }
}