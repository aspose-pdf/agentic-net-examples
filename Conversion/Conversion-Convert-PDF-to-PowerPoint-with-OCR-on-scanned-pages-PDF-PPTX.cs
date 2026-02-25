using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPptx = "output.pptx";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdf))
            {
                // Configure PPTX save options
                Aspose.Pdf.PptxSaveOptions pptxOptions = new Aspose.Pdf.PptxSaveOptions
                {
                    // Render each slide as an image – preserves the appearance of scanned pages
                    SlidesAsImages = true,
                    // Extract OCR sub‑layer (text) from scanned pages
                    ExtractOcrSublayerOnly = true,
                    // Increase image resolution for better visual quality (optional)
                    ImageResolution = 300
                };

                // Save the PDF as PPTX using the configured options
                pdfDoc.Save(outputPptx, pptxOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: {outputPptx}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}