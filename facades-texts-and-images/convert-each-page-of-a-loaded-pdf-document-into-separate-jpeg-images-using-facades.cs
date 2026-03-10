using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Directory where JPEG images will be saved
        const string outputDir = "OutputImages";

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize the PdfConverter facade (implements IDisposable)
                using (PdfConverter converter = new PdfConverter())
                {
                    // Bind the loaded document to the converter
                    converter.BindPdf(pdfDocument);

                    // Prepare the converter for conversion
                    converter.DoConvert();

                    int pageNumber = 1; // 1‑based indexing (global rule)

                    // Iterate over all pages and save each as a JPEG image
                    while (converter.HasNextImage())
                    {
                        string outputFile = Path.Combine(outputDir, $"page_{pageNumber}.jpg");
                        // Save the current page image using the default JPEG format
                        converter.GetNextImage(outputFile);
                        pageNumber++;
                    }

                    // Optional: explicitly close the converter (handled by using as well)
                    converter.Close();
                }
            }

            Console.WriteLine($"All pages have been converted to JPEG images in '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}