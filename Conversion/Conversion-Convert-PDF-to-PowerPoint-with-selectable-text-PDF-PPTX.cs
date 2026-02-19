using System;
using System.IO;
using Aspose.Pdf; // SaveFormat enum is defined in this namespace

class Program
{
    static void Main(string[] args)
    {
        // Paths for input PDF and output PPTX
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

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

            // Convert and save the document as PPTX.
            // The default conversion preserves selectable text.
            pdfDocument.Save(outputPptxPath, SaveFormat.Pptx);

            Console.WriteLine($"Conversion successful. PPTX saved to '{outputPptxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
