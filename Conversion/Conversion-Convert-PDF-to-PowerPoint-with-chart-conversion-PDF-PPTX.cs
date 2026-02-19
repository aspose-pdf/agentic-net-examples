using System;
using System.IO;
using Aspose.Pdf;

class PdfToPptxConverter
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfToPptxConverter <input-pdf-path> <output-pptx-path>");
            return;
        }

        string inputPdfPath = args[0];
        string outputPptxPath = args[1];

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Save the document as PPTX. The .pptx extension tells Aspose.Pdf to perform
            // conversion, including chart conversion, automatically.
            pdfDocument.Save(outputPptxPath);

            Console.WriteLine($"Conversion successful. PPTX saved to: {outputPptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}