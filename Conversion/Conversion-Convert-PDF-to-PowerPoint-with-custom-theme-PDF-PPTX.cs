using System;
using System.IO;
using Aspose.Pdf;

class PdfToPptxConverter
{
    static void Main(string[] args)
    {
        // Input PDF path – replace with your actual file path
        string inputPdfPath = "input.pdf";

        // Output PPTX path – the file extension determines the target format
        string outputPptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Optional: set some document metadata (theme handling is not directly exposed)
            pdfDocument.Info.Title = "Converted Presentation";
            pdfDocument.Info.Author = Environment.UserName;

            // Save the document as PPTX – the format is inferred from the file extension
            pdfDocument.Save(outputPptxPath);

            Console.WriteLine($"Conversion successful. PPTX saved to '{outputPptxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}