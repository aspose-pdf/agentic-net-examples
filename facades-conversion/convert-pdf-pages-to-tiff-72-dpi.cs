using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "TiffPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfConverter implements IDisposable – wrap it in a using block
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(inputPdf);

                // Set the desired resolution (72 DPI) for smaller image size
                converter.Resolution = new Resolution(72);

                // Convert all pages
                converter.StartPage = 1;
                converter.EndPage   = converter.PageCount; // total pages after binding
                converter.DoConvert();

                // Save each page as an individual TIFF file
                for (int pageNumber = 1; pageNumber <= converter.PageCount; pageNumber++)
                {
                    string outPath = Path.Combine(outputDir, $"page_{pageNumber}.tiff");
                    // GetNextImage infers the format from the file extension
                    converter.GetNextImage(outPath);
                    Console.WriteLine($"Saved page {pageNumber} as TIFF → {outPath}");
                }
            }

            Console.WriteLine("All pages have been converted to TIFF images.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
