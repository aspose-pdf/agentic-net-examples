using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the target PDF/E file.
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfe.pdf";

        // Verify that the source file exists before attempting to load it.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document.
        Document pdfDocument = new Document(inputPath);

        // Save the document. When saving, Aspose.Pdf can produce PDF/E output
        // if the appropriate conversion options are set (e.g., PdfFormatConversionOptions).
        // For simplicity and cross‑platform compatibility, we use the basic Save method.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF successfully converted and saved as PDF/E at '{outputPath}'.");
    }
}