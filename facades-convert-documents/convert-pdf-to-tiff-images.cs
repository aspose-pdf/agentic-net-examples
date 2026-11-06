using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Added for Resolution

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "TiffPages";

        // Verify input PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (deterministic disposal)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Create a new converter for each page to keep the workflow simple
                using (PdfConverter converter = new PdfConverter())
                {
                    // Bind the PDF document
                    converter.BindPdf(pdfDoc);

                    // Convert only the current page
                    converter.StartPage = pageNumber;
                    converter.EndPage   = pageNumber;

                    // Optional: set resolution (default is 96 DPI). Adjust if higher quality is needed.
                    converter.Resolution = new Resolution(150);

                    // Prepare the conversion
                    converter.DoConvert();

                    // Build the output file name with .tiff extension
                    string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.tiff");

                    // Save the single‑page TIFF image
                    converter.SaveAsTIFF(outPath);
                }
            }
        }

        Console.WriteLine("PDF successfully converted to TIFF images.");
    }
}
