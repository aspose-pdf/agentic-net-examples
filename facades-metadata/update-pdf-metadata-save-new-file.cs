using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file info facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Update desired metadata fields
            pdfInfo.Title = "Updated Title";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Sample Subject";
            pdfInfo.Keywords = "Aspose, PDF, Metadata";

            // Persist changes to a new file
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Metadata successfully saved to '{outputPath}'."
                : $"Failed to save metadata to '{outputPath}'.");
        }
    }
}