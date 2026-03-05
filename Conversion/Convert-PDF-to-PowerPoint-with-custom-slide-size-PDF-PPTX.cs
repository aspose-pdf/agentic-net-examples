using System;
using System.IO;
using Aspose.Pdf;

class PdfToPptxConverter
{
    static void Main()
    {
        // Input PDF and output PPTX paths
        const string inputPdfPath  = "input.pdf";
        const string outputPptxPath = "output.pptx";

        // Desired slide size in points (1 point = 1/72 inch)
        // Example: 13.33 inches × 7.5 inches => 960 pt × 540 pt
        const double slideWidth  = 960.0; // width in points
        const double slideHeight = 540.0; // height in points

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Adjust each page to the desired slide size
                foreach (Page page in pdfDocument.Pages)
                {
                    page.PageInfo.Width  = slideWidth;
                    page.PageInfo.Height = slideHeight;
                }

                // Initialize PPTX save options (no special settings required for size)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as a PPTX file with the custom slide dimensions
                pdfDocument.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"Conversion completed. PPTX saved to '{outputPptxPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}