using System;
using System.IO;
using Aspose.Pdf; // Document, PptxSaveOptions

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath  = "input.pdf";

        // Output PPTX file path
        const string outputPptxPath = "output.pptx";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize PPTX save options
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Optional: render each slide as an image (one image per PDF page)
                // Uncomment the line below if you prefer image‑only slides
                // pptxOptions.SlidesAsImages = true;

                // Save the PDF as a PPTX presentation
                pdfDocument.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: '{outputPptxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}