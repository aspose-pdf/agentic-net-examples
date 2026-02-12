using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PostScript file and the resulting PDF file
        const string inputPath = "input.ps";
        const string outputPath = "output.pdf";

        // Verify that the source file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Create load options for PS format (default options are sufficient for most cases)
        PsLoadOptions loadOptions = new PsLoadOptions();

        // Load the PS file into a PDF Document instance
        using (Document pdfDocument = new Document(inputPath, loadOptions))
        {
            // Save the document as a PDF (uses the provided document-save rule)
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Successfully converted '{inputPath}' to '{outputPath}'.");
    }
}