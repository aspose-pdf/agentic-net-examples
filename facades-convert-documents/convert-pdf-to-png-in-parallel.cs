using System;
using System.IO;
using System.Drawing.Imaging;          // ImageFormat enum
using System.Threading.Tasks;          // Parallel processing
using Aspose.Pdf;                      // Document class
using Aspose.Pdf.Facades;              // PdfConverter facade

class PdfToPngParallel
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output directory for PNG images
        const string outputDir = "PngPages";

        // Validate input file
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF once to obtain the page count (must be disposed)
        int pageCount;
        using (Document doc = new Document(pdfPath))
        {
            pageCount = doc.Pages.Count;   // 1‑based indexing
        }

        // Convert each page to PNG in parallel
        Parallel.For(1, pageCount + 1, pageNumber =>
        {
            // Each thread creates its own PdfConverter instance
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(pdfPath);

                // Restrict conversion to a single page
                converter.StartPage = pageNumber;
                converter.EndPage   = pageNumber;

                // Prepare the converter
                converter.DoConvert();

                // Build the output file name
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");

                // Save the page as PNG (default resolution can be changed via converter.Resolution if needed)
                converter.GetNextImage(outPath, ImageFormat.Png);
            }
        });

        Console.WriteLine($"Conversion completed. PNG files are saved in '{outputDir}'.");
    }
}