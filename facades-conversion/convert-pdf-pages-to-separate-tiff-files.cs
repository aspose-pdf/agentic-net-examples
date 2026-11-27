using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";

        // Directory where individual TIFF files will be saved
        const string outputDir = "TiffPages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Bind the PDF and convert each page to a separate TIFF file
        using (PdfConverter converter = new PdfConverter())
        {
            // Load the PDF document (path overload is sufficient)
            converter.BindPdf(pdfPath);

            // Configure conversion – we want all pages
            converter.StartPage = 1;
            // After binding, PageCount returns the total number of pages in the document
            converter.EndPage = converter.PageCount;
            converter.Resolution = new Resolution(150); // DPI, adjust as needed
            // PageCoordinateType lives in the Aspose.Pdf namespace
            converter.CoordinateType = PageCoordinateType.CropBox;

            // Prepare the conversion (no arguments)
            converter.DoConvert();

            // Base name of the source PDF (without extension)
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            int pageNumber = converter.StartPage;

            // Loop through the generated images – one per page
            while (converter.HasNextImage())
            {
                string tiffPath = Path.Combine(outputDir, $"{baseName}_{pageNumber}.tiff");
                // Save the current page as a TIFF image
                converter.GetNextImage(tiffPath, ImageFormat.Tiff);
                Console.WriteLine($"Saved page {pageNumber} as '{tiffPath}'.");
                pageNumber++;
            }

            // Release resources
            converter.Close();
        }
    }
}
