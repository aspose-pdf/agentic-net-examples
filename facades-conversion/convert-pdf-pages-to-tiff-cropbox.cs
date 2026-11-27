using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "OutputImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Convert pages 3 and 4 to separate TIFF files using the CropBox dimensions
            for (int pageNumber = 3; pageNumber <= 4; pageNumber++)
            {
                using (PdfConverter converter = new PdfConverter())
                {
                    // Bind the source PDF
                    converter.BindPdf(pdfPath);

                    // NOTE: In recent Aspose.Pdf versions PdfConverter uses CropBox by default.
                    // The CoordinateType property has been removed, so we omit it.

                    // Restrict conversion to the current page
                    converter.StartPage = pageNumber;
                    converter.EndPage   = pageNumber;

                    // Optional: set resolution for higher quality output
                    converter.Resolution = new Resolution(150);

                    // Perform the conversion preparation
                    converter.DoConvert();

                    // Save the page as a TIFF image (CropBox is now applied)
                    string outputPath = Path.Combine(outputDir, $"page{pageNumber}.tiff");
                    converter.SaveAsTIFF(outputPath);
                    Console.WriteLine($"Page {pageNumber} saved as TIFF to '{outputPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
