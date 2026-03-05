using System;
using System.IO;
using Aspose.Pdf;               // Document, PptxSaveOptions

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";
        // Output PPTX file path
        const string outputPptxPath = "output.pptx";

        // Verify the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document, convert and save as PPTX with image‑only slides
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize save options for PPTX
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                // Convert each PDF page to a single image on a slide
                SlidesAsImages = true
            };

            // Save the document as PPTX using the specified options
            pdfDocument.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputPptxPath}'");
    }
}