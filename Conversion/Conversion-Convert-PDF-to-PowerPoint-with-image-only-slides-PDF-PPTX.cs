using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPpt = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure PPTX save options to render each slide as an image
            PptxSaveOptions pptxOptions = new PptxSaveOptions
            {
                SlidesAsImages = true
            };

            try
            {
                // Save the PDF as a PPTX file with image‑only slides
                pdfDoc.Save(outputPpt, pptxOptions);
                Console.WriteLine($"Conversion successful: '{outputPpt}'");
            }
            catch (TypeInitializationException)
            {
                // GDI+ (required for image rendering) is unavailable on non‑Windows platforms
                Console.WriteLine("PDF‑to‑PPTX conversion with image rendering requires Windows (GDI+). Skipped on this platform.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Conversion failed: {ex.Message}");
            }
        }
    }
}