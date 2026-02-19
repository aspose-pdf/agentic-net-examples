using System;
using System.IO;
using Aspose.Pdf; // PptxSaveOptions resides in this namespace

class PdfToPptxConverter
{
    static void Main(string[] args)
    {
        // Input PDF path (first argument) and output PPTX path (second argument)
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: PdfToPptxConverter <input.pdf> <output.pptx>");
            return;
        }

        string inputPdfPath = args[0];
        string outputPptxPath = args[1];

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure PPTX save options to render each slide as an image
            var pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true   // one image per PDF page
            };

            // Save the document as PPTX with the specified options
            pdfDocument.Save(outputPptxPath, pptxOptions);

            Console.WriteLine($"Conversion successful. PPTX saved to: {outputPptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
