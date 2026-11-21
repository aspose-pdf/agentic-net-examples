using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure PPTX save options: render each slide as a raster image
            PptxSaveOptions pptxOpts = new PptxSaveOptions
            {
                SlidesAsImages = true   // Enable raster image rendering for all slides
            };

            // Save the PDF as PPTX using the explicit save options (required for non‑PDF formats)
            pdfDoc.Save(outputPptxPath, pptxOpts);
        }

        Console.WriteLine($"PDF successfully converted to PPTX with slides as images: {outputPptxPath}");
    }
}