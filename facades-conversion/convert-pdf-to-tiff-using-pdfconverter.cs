using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

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

        Directory.CreateDirectory(outputDir);

        try
        {
            // PdfConverter implements IDisposable; ensure proper disposal
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF file
                converter.BindPdf(inputPdf);

                // Set conversion range and resolution (optional but recommended)
                converter.StartPage = 1;                     // first page to convert
                converter.EndPage   = int.MaxValue;          // convert all pages
                converter.Resolution = new Resolution(150); // DPI – adjust as needed

                // Prepare the conversion – replaces the removed StartConvert/EndConvert methods
                converter.DoConvert();

                int pageNumber = 1;
                while (true)
                {
                    string outPath = Path.Combine(outputDir, $"Page_{pageNumber}.tiff");
                    try
                    {
                        // In newer Aspose.Pdf versions GetNextImage(string) saves the image directly.
                        converter.GetNextImage(outPath);
                    }
                    catch
                    {
                        // No more pages to convert.
                        break;
                    }

                    Console.WriteLine($"Saved page {pageNumber} as TIFF: {outPath}");
                    pageNumber++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
