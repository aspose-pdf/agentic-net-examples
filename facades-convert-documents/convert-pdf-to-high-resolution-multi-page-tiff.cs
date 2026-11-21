using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Added for Resolution

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "TiffPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Convert each page to a separate TIFF image using PdfConverter (cross‑platform API)
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    string outputPath = Path.Combine(outputFolder, $"page_{pageNumber}.tiff");

                    // PdfConverter must be created per page when using the newer API
                    using (PdfConverter converter = new PdfConverter())
                    {
                        // Bind the source PDF
                        converter.BindPdf(pdfDoc);

                        // Define the page range (single page) and resolution (400 DPI)
                        converter.StartPage = pageNumber;
                        converter.EndPage   = pageNumber;
                        converter.Resolution = new Resolution(400);

                        // Prepare the conversion
                        converter.DoConvert();

                        // Save the selected page as a TIFF image
                        converter.SaveAsTIFF(outputPath);
                    }

                    Console.WriteLine($"Saved page {pageNumber} as TIFF: {outputPath}");
                }
            }

            Console.WriteLine("PDF to TIFF conversion completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
