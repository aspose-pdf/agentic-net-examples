using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_updated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file information facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Update desired metadata fields
            pdfInfo.Title    = "Updated Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "Sample Subject";
            pdfInfo.Keywords = "Aspose, PDF, Metadata";
            pdfInfo.Creator  = "My Application";
            pdfInfo.ModDate  = DateTime.Now.ToString("yyyyMMddHHmmsszzz");

            // Save the PDF with the new metadata
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Metadata successfully saved to '{outputPath}'."
                : $"Failed to save metadata to '{outputPath}'.");
        }
    }
}