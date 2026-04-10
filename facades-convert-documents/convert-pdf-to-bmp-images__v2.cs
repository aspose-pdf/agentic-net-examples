using System;
using System.IO;
using System.Drawing.Imaging;          // ImageFormat enum (used on Windows only)
using Aspose.Pdf;                     // Document class
using Aspose.Pdf.Facades;             // PdfConverter class
using Aspose.Pdf.Devices;             // Resolution class
using Aspose.Pdf.Text;                // TextFragment class

class PdfToBmpConverter
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Directory where BMP images will be saved
        const string outputDir = "BmpImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // If the input PDF does not exist, create a minimal sample PDF so the example can run out‑of‑the‑box.
        if (!File.Exists(inputPdfPath))
        {
            CreateSamplePdf(inputPdfPath);
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded PDF document to the converter
                converter.BindPdf(pdfDocument);

                // Set the desired resolution (200 DPI)
                converter.Resolution = new Resolution(200);

                // Perform any necessary initial processing
                converter.DoConvert();

                int pageNumber = 1;
                // Iterate over each page image produced by the converter
                while (converter.HasNextImage())
                {
                    // Build the output file name for the current page
                    string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.bmp");

                    // The ImageFormat.Bmp enum is Windows‑only and triggers CA1416.
                    // Suppress the warning for this specific call because Aspose.Pdf internally handles the format.
#pragma warning disable CA1416 // Validate platform compatibility
                    converter.GetNextImage(outputPath, ImageFormat.Bmp);
#pragma warning restore CA1416 // Validate platform compatibility

                    pageNumber++;
                }
            }
        }

        Console.WriteLine("PDF has been converted to BMP images successfully.");
    }

    /// <summary>
    /// Creates a very small PDF containing a single page with the text "Sample PDF".
    /// This helper is only used when the expected input file is missing, allowing the
    /// sample code to be executed without external resources.
    /// </summary>
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample PDF"));
            doc.Save(path);
        }
    }
}
