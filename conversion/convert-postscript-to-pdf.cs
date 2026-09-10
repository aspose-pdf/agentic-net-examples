using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPsPath  = "input.ps";   // Source PostScript file
        const string outputPdfPath = "output.pdf"; // Destination PDF file

        // Verify the source file exists
        if (!File.Exists(inputPsPath))
        {
            Console.Error.WriteLine($"File not found: {inputPsPath}");
            return;
        }

        // Initialize load options for PostScript files
        PsLoadOptions psLoadOptions = new PsLoadOptions();

        // Load the PS file and convert it to PDF (default settings)
        using (Document pdfDocument = new Document(inputPsPath, psLoadOptions))
        {
            // Save the document as PDF; Save(string) always writes PDF regardless of extension
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PostScript file successfully converted to PDF: '{outputPdfPath}'");
    }
}