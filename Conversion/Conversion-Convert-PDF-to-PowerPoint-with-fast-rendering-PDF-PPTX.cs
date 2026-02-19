using System;
using System.IO;
using Aspose.Pdf; // SaveFormat enum is defined in this namespace; no extra using needed.

class PdfToPptxConverter
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and output PPTX path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToPptxConverter <input.pdf> <output.pptx>");
            return;
        }

        string inputPdfPath = args[0];
        string outputPptxPath = args[1];

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document.
            Document pdfDocument = new Document(inputPdfPath);

            // Save the document as PPTX. The Save method overload with SaveFormat is used.
            pdfDocument.Save(outputPptxPath, SaveFormat.Pptx);

            Console.WriteLine($"Conversion successful. PPTX saved to: {outputPptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
