using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_updated.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfFileInfo instance bound to the existing PDF
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Update desired metadata fields
            pdfInfo.Title    = "Updated Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "Sample subject";
            pdfInfo.Keywords = "Aspose, PDF, metadata";
            pdfInfo.Creator  = "My Application";
            pdfInfo.ModDate  = DateTime.Now.ToString("yyyyMMddHHmmsszzz");

            // Save the PDF with the new metadata to a new file
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Metadata successfully saved to '{outputPath}'."
                : $"Failed to save metadata to '{outputPath}'.");
        }
    }
}