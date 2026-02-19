using System;
using System.IO;
using Aspose.Pdf; // Provides Document, CgmLoadOptions, etc.

class Program
{
    static void Main(string[] args)
    {
        // Input CGM file path (change as needed)
        const string inputCgmPath = "input.cgm";
        // Desired output PDF file path
        const string outputPdfPath = "output.pdf";

        // Verify that the CGM source file exists
        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Error: CGM file not found at '{inputCgmPath}'.");
            return;
        }

        try
        {
            // Load the CGM file using default load options
            // CgmLoadOptions derives from LoadOptions and allows CGM → PDF conversion
            Document pdfDocument = new Document(inputCgmPath, new CgmLoadOptions());

            // Save the resulting document as a standard PDF
            pdfDocument.Save(outputPdfPath);

            Console.WriteLine($"Conversion successful. PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors and report them
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}