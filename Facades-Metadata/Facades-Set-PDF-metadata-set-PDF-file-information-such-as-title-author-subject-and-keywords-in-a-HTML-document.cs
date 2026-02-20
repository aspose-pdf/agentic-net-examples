using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF with updated metadata
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the PDF file information facade (no new document is created)
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

        // Set the desired metadata properties
        pdfInfo.Title = "Sample PDF Title";
        pdfInfo.Author = "John Doe";
        pdfInfo.Subject = "Demonstration of metadata setting using Aspose.Pdf.Facades";
        pdfInfo.Keywords = "Aspose, PDF, Metadata, Example";

        // Save the PDF with the updated metadata to a new file
        pdfInfo.Save(outputPath); // follows the standard Save(string) pattern

        Console.WriteLine($"Metadata successfully updated. Output saved to '{outputPath}'.");
    }
}