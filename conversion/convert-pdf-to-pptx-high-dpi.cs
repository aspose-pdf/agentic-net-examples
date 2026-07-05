using System;
using System.IO;
using Aspose.Pdf;   // Contains Document and PptxSaveOptions

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the destination PPTX file
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Desired image resolution in DPI for the generated slides
        const int imageDpi = 300;

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure PPTX save options
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true,      // Export each page as a single image slide
                ImageResolution = imageDpi // Set high‑resolution DPI for the images
            };

            // Save the PDF as PPTX using the configured options
            pdfDocument.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
    }
}