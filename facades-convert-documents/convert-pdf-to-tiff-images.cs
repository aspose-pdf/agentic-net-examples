using System;
using System.IO;
using System.Drawing.Imaging;          // For ImageFormat enum
using Aspose.Pdf.Facades;             // PdfConverter resides here

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";               // Source PDF
        const string outputDir  = "TiffPages";               // Folder for TIFF files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Initialize the converter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the PDF file to the converter
                converter.BindPdf(inputPdf);

                // Prepare for conversion (required step)
                converter.DoConvert();

                int pageNumber = 1;
                // Iterate through all pages and save each as a separate TIFF file
                while (converter.HasNextImage())
                {
                    string tiffPath = Path.Combine(outputDir, $"page_{pageNumber}.tiff");
                    // Save current page image as TIFF using default settings
                    converter.GetNextImage(tiffPath, ImageFormat.Tiff);
                    pageNumber++;
                }

                // Release resources held by the converter
                converter.Close();
            }

            Console.WriteLine($"PDF successfully converted to TIFF images in '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}