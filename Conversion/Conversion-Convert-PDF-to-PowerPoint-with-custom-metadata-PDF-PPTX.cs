using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output PPTX file path (extension determines the format)
        const string outputPptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Set standard metadata
            pdfDocument.Info.Title = "Converted Presentation";
            pdfDocument.Info.Author = "John Doe";
            pdfDocument.Info.Subject = "PDF to PPTX conversion";
            pdfDocument.Info.Keywords = "PDF,PowerPoint,Conversion";

            // Add custom metadata entries
            pdfDocument.Metadata["CustomProperty"] = "CustomValue";
            pdfDocument.Metadata["ConversionDate"] = DateTime.UtcNow.ToString("o");

            // Save the document as PPTX (extension determines the format)
            pdfDocument.Save(outputPptxPath);

            Console.WriteLine($"Conversion completed successfully. PPTX saved to '{outputPptxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}