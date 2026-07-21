using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Resolution class
using Aspose.Pdf.Text;   // <-- required for TextFragment

class PdfToJpegConverter
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // source PDF
        const string outputFolder = "Images";      // folder for JPEGs

        // ---------------------------------------------------------------------
        // 1. Create a sample PDF so the example works in a clean sandbox.
        // ---------------------------------------------------------------------
        CreateSamplePdf(inputPdfPath);

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the loaded document to the converter
            converter.BindPdf(pdfDoc);

            // Set resolution to 150 DPI (Resolution object required)
            converter.Resolution = new Resolution(150);

            // Prepare conversion
            converter.DoConvert();

            int pageIndex = 1;
            // Iterate through all pages and save each as a progressive JPEG
            while (converter.HasNextImage())
            {
                // Build output file name (extension determines format)
                string outputFile = Path.Combine(outputFolder, $"page_{pageIndex}.jpg");

                // Save current page as JPEG. Aspose.Pdf saves JPEGs in progressive mode by default.
                converter.GetNextImage(outputFile);

                pageIndex++;
            }
        }

        Console.WriteLine("PDF pages have been converted to JPEG images.");
    }

    // Helper that creates a minimal PDF file so the demo can run without external files.
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            // Add a single page with a simple text fragment.
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample page for conversion"));
            doc.Save(path);
        }
    }
}
