using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output PPTX file path
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure PPTX save options
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                // Render each slide as a raster image (one image per PDF page)
                SlidesAsImages = true,
                // Set high‑resolution DPI for the generated images (e.g., 300 DPI)
                ImageResolution = 300
            };

            // Save the document as PPTX using the specified options
            pdfDoc.Save(outputPptx, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptx}");
    }
}