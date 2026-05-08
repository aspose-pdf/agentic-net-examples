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

        // Initialize PdfFileInfo with the source PDF file
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Update desired metadata fields
            pdfInfo.Title = "Updated Document Title";
            pdfInfo.Author = "Jane Smith";
            pdfInfo.Subject = "Demonstration of SaveNewInfo";
            pdfInfo.Keywords = "Aspose.Pdf, Metadata, Example";
            pdfInfo.Creator = "MetadataUpdaterApp";
            pdfInfo.ModDate = DateTime.Now.ToString("yyyyMMddHHmmsszzz");

            // Persist the changes to a new PDF file
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Metadata successfully saved to '{outputPath}'."
                : $"Failed to save metadata to '{outputPath}'.");
        }
    }
}