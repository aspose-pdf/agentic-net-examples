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
        // Path to the source PDF
        const string pdfPath = "source.pdf";

        // Verify the PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Prepare output directory (same folder as PDF)
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(pdfPath));
        if (string.IsNullOrEmpty(outputDir))
            outputDir = Directory.GetCurrentDirectory();

        // Base name without extension, used for output file naming
        string baseName = Path.GetFileNameWithoutExtension(pdfPath);

        try
        {
            // PdfConverter implements IDisposable – wrap in using for deterministic cleanup
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF document to the converter (accepts file path or Document)
                converter.BindPdf(pdfPath);

                // Configure conversion settings
                converter.StartPage = 1;                                 // first page to convert
                converter.EndPage = converter.PageCount;                // last page to convert (all pages)
                converter.Resolution = new Resolution(150);            // DPI – adjust as needed
                converter.CoordinateType = PageCoordinateType.CropBox; // optional, use CropBox

                // Prepare the conversion (no arguments)
                converter.DoConvert();

                // Loop through each generated image and save it as a separate TIFF file
                int pageNumber = converter.StartPage;
                while (converter.HasNextImage())
                {
                    string outputPath = Path.Combine(outputDir, $"{baseName}_page_{pageNumber}.tiff");
                    // GetNextImage writes the image directly to the supplied path using the specified format
                    converter.GetNextImage(outputPath, ImageFormat.Tiff);
                    Console.WriteLine($"Saved page {pageNumber} as '{outputPath}'.");
                    pageNumber++;
                }

                // Release any internal resources (optional, as using will call Dispose)
                converter.Close();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
