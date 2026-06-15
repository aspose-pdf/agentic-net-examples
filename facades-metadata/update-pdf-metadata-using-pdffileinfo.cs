using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "updated_metadata.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Bind the existing PDF to PdfFileInfo, modify its metadata, and save the changes.
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Load the PDF file.
            pdfInfo.BindPdf(inputPath);

            // Update desired metadata fields.
            pdfInfo.Title = "Updated Document Title";
            pdfInfo.Author = "Jane Smith";
            pdfInfo.Subject = "Metadata Update Example";
            pdfInfo.Keywords = "Aspose.Pdf, Metadata, Example";
            pdfInfo.ModDate = DateTime.Now.ToString("yyyyMMddHHmmsszzz"); // string format expected

            // Save the PDF with the new metadata.
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"PDF saved with updated metadata to '{outputPath}'."
                : "Failed to save PDF with updated metadata.");
        }
    }
}