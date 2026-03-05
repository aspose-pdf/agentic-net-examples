using System;
using System.IO;
using Aspose.Pdf; // Document, PptxSaveOptions, etc.

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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure PPTX save options to render each PDF page as an image.
                PptxSaveOptions pptxOptions = new PptxSaveOptions
                {
                    SlidesAsImages = true   // One image per slide.
                };

                // Save the document as PPTX using the specified options.
                pdfDoc.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX with slides as images: {outputPptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}