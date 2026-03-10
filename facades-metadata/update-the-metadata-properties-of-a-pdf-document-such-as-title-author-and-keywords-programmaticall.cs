using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file information using the Facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Update metadata properties
            pdfInfo.Title    = "Updated Document Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Keywords = "Aspose, PDF, Metadata";

            // Save the updated PDF to a new file
            bool success = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(success
                ? $"Metadata updated and saved to '{outputPath}'."
                : "Failed to save the updated PDF.");
        }
    }
}