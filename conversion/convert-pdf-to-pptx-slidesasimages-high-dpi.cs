using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";
        const int imageDpi = 300; // high‑resolution DPI

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure PPTX save options: render each slide as an image and set DPI
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true,
                ImageResolution = imageDpi
            };

            // Save as PPTX using the explicit save options (required for non‑PDF formats)
            pdfDocument.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputPptxPath}' (SlidesAsImages, {imageDpi} DPI)");
    }
}