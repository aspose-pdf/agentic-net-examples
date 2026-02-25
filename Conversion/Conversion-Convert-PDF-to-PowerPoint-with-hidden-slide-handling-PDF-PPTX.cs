using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure PPTX save options
                PptxSaveOptions pptxOptions = new PptxSaveOptions
                {
                    // Render each slide as an image – useful when the source PDF contains
                    // elements that should not be editable in the resulting presentation
                    SlidesAsImages = true
                };

                // Save the PDF as a PPTX file
                // This operation may require GDI+ on non‑Windows platforms; wrap it to handle possible failures
                try
                {
                    pdfDoc.Save(outputPptxPath, pptxOptions);
                    Console.WriteLine($"PDF successfully converted to PPTX: {outputPptxPath}");
                }
                catch (TypeInitializationException ex)
                {
                    Console.Error.WriteLine("Conversion failed due to missing GDI+ libraries (Windows‑only feature).");
                    Console.Error.WriteLine($"Details: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}