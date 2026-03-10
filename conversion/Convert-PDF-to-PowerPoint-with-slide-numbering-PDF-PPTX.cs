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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure PPTX save options
                PptxSaveOptions pptxOptions = new PptxSaveOptions
                {
                    // Keep editable shapes (set to true to render each slide as an image)
                    SlidesAsImages = false
                };

                // Convert and save as PPTX; explicit SaveOptions ensures correct format
                pdfDoc.Save(outputPptx, pptxOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: '{outputPptx}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
    }
}