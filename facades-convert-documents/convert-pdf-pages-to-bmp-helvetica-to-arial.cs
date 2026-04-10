using System;
using System.IO;
using System.Drawing.Imaging;               // ImageFormat for BMP
using Aspose.Pdf;                           // Core PDF classes
using Aspose.Pdf.Facades;                   // PdfConverter facade
using Aspose.Pdf.Text;                      // FontRepository for substitution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputFolder = "BmpPages";           // folder for BMP images

        // Verify that the source PDF exists before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: The file '{inputPdfPath}' was not found.");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Apply font substitution: replace Helvetica with Arial
            // Register a fallback substitution via FontRepository
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));

            // Initialize the PdfConverter facade with the loaded document
            using (PdfConverter converter = new PdfConverter(pdfDoc))
            {
                // Convert only pages 5 through 7 (1‑based indexing)
                converter.StartPage = 5;
                converter.EndPage   = 7;

                // Prepare the converter
                converter.DoConvert();

                int pageNumber = 5; // start page number for naming
                while (converter.HasNextImage())
                {
                    // Build output file name, e.g., "BmpPages/page5.bmp"
                    string outputPath = Path.Combine(outputFolder, $"page{pageNumber}.bmp");

                    // The ImageFormat.Bmp type is Windows‑specific. Suppress the CA1416 warning for this call.
#pragma warning disable CA1416 // Platform compatibility
                    converter.GetNextImage(outputPath, ImageFormat.Bmp);
#pragma warning restore CA1416 // Platform compatibility

                    pageNumber++;
                }
            }
        }

        Console.WriteLine("Pages 5‑7 have been converted to BMP images.");
    }
}
