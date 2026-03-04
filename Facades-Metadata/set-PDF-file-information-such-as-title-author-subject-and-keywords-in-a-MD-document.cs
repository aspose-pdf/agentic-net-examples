using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF using the PdfFileInfo facade
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
            {
                // Set the desired document information
                pdfInfo.Title    = "Sample PDF Title";
                pdfInfo.Author   = "John Doe";
                pdfInfo.Subject  = "Demonstration of setting PDF metadata";
                pdfInfo.Keywords = "Aspose.Pdf, Metadata, Example";

                // Save the updated PDF to a new file
                pdfInfo.SaveNewInfo(outputPath);
            }

            Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}