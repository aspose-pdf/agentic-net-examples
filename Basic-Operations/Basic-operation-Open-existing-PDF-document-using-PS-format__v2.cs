using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PostScript file
        const string inputPsPath = "input.ps";
        // Desired output PDF file path
        const string outputPdfPath = "output.pdf";

        // Verify that the PS file exists before attempting to load it
        if (!File.Exists(inputPsPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPsPath}");
            return;
        }

        // Create load options specific for PS files
        PsLoadOptions psLoadOptions = new PsLoadOptions();

        // Open the PS document inside a using block to ensure proper disposal
        using (Document pdfDoc = new Document(inputPsPath, psLoadOptions))
        {
            // Save the loaded document as a PDF; Document.Save(string) always writes PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Successfully converted '{inputPsPath}' to '{outputPdfPath}'.");
    }
}