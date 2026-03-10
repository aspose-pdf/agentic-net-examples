using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure PPTX save options for faster rendering
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                // Enable multithreading for PPTX conversion (instance property)
                IsMultiThreading = true,
                // Cache glyphs to improve conversion speed (uses more memory)
                CacheGlyphs = true
            };

            // Save the PDF as a PPTX file
            pdfDoc.Save(outputPptx, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {outputPptx}");
    }
}
