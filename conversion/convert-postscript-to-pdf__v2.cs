using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPsPath  = "input.ps";   // PostScript source file
        const string outputPdfPath = "output.pdf"; // Destination PDF file

        // Verify the input file exists
        if (!File.Exists(inputPsPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPsPath}");
            return;
        }

        // Load the PostScript file using PsLoadOptions.
        // No FontsFolders are specified – the default font search locations are used.
        PsLoadOptions loadOptions = new PsLoadOptions();

        // Wrap the Document in a using block for deterministic disposal (rule: document-disposal-with-using)
        using (Document pdfDoc = new Document(inputPsPath, loadOptions))
        {
            // The document is now a PDF representation of the PostScript input.
            // Save it as a PDF file. No SaveOptions are needed because the target format is PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PostScript file converted and saved as PDF: {outputPdfPath}");
    }
}