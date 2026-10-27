using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "TiffPages";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Bind the PDF document to the converter
        PdfConverter converter = new PdfConverter();
        converter.BindPdf(inputPath);

        // Define the page range (pages 3 through 8, inclusive)
        int startPage = 3;
        int endPage   = 8;

        // Loop through each page in the range and save it as a separate TIFF file
        for (int pageNum = startPage; pageNum <= endPage; pageNum++)
        {
            // Output file name for the current page
            string outPath = Path.Combine(outputDir, $"page_{pageNum}.tiff");

            // Save the current page as TIFF.
            // The overload SaveAsTIFF(string, int, int) uses the page's CropBox
            // and the default resolution when no additional options are supplied.
            converter.SaveAsTIFF(outPath, pageNum, pageNum);
        }

        // Release any resources held by the converter
        converter.Close();

        Console.WriteLine("Pages 3‑8 have been converted to TIFF images.");
    }
}