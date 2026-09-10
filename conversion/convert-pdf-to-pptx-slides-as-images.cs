using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output PPTX file path
        const string pptxPath = "output.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document and convert to PPTX with each slide rendered as an image
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Initialize PPTX save options
            PptxSaveOptions saveOptions = new PptxSaveOptions
            {
                // Render each page as a raster image on a separate slide
                SlidesAsImages = true
            };

            // Save the document as PPTX using the configured options
            pdfDocument.Save(pptxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {pptxPath}");
    }
}