using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions, including PptxSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output PPTX file path
        const string outputPptx = "output.pptx";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Desired image resolution in DPI for high‑resolution slides
        const int imageDpi = 300;

        // Load the PDF, configure PPTX save options, and save
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Initialize PPTX save options
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                // Render each slide as a single image (one per PDF page)
                SlidesAsImages = true,
                // Set the image resolution (dpi) for high‑quality output
                ImageResolution = imageDpi
            };

            // Save the document as PPTX using the configured options
            pdfDocument.Save(outputPptx, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptx}");
    }
}