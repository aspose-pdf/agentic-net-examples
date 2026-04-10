using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";
        const int dpi = 300; // Desired image resolution

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure PPTX save options
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true,   // Export each slide as an image
                ImageResolution = dpi    // Set high‑resolution DPI
            };

            // Save the document as PPTX using the specified options
            pdfDoc.Save(outputPptx, pptxOptions);
        }

        Console.WriteLine($"Conversion completed: {outputPptx}");
    }
}