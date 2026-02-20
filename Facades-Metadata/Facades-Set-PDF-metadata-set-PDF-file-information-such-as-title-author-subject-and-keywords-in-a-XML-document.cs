using System;
using System.IO;
using Aspose.Pdf.Facades;

class SetPdfMetadata
{
    static void Main(string[] args)
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output PDF file path (metadata will be written to this file)
        const string outputPdfPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF using PdfFileInfo facade
            PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath);

            // Set desired metadata properties
            pdfInfo.Title = "Sample PDF Title";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Demonstration of setting PDF metadata";
            pdfInfo.Keywords = "Aspose.Pdf, metadata, example";

            // Save the PDF with updated metadata
            pdfInfo.Save(outputPdfPath); // document-save rule applied

            Console.WriteLine($"Metadata updated and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}