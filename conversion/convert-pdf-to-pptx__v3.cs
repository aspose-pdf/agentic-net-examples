using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input PDF and output PPTX
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document using the core Aspose.Pdf API
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize PPTX save options (default configuration)
            PptxSaveOptions pptxOptions = new PptxSaveOptions();

            // Convert and save the PDF as a PPTX presentation
            pdfDocument.Save(outputPptxPath, pptxOptions);
        }

        // --------------------------------------------------------------------
        // NOTE:
        // Applying a custom slide master template to the generated PPTX
        // requires a library that can manipulate PowerPoint files (e.g.,
        // Aspose.Slides). The core Aspose.Pdf API does not expose any
        // functionality for editing slide masters, so this step cannot be
        // performed using only Aspose.Pdf namespaces.
        // --------------------------------------------------------------------

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
    }
}