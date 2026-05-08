using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class PdfToPptxConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output PPTX file path
        const string outputPptxPath = "output.pptx";

        // Desired image resolution (dpi) for high‑resolution slides
        const int imageDpi = 300;

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure PPTX save options
            PptxSaveOptions saveOptions = new PptxSaveOptions
            {
                // Render each slide as a single image (one per PDF page)
                SlidesAsImages = true,

                // Set the image resolution (dpi) for the generated slide images
                ImageResolution = imageDpi
            };

            // Save the PDF as a PPTX file using the configured options
            pdfDocument.Save(outputPptxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: '{outputPptxPath}'");
    }
}