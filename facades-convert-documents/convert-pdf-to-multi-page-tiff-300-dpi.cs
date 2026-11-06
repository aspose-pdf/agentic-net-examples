using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where TIFF images will be saved
        const string outputDir = "TiffPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfConverter is a Facades class that converts PDF pages to images
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document to the converter
            converter.BindPdf(inputPdf);

            // Set the desired resolution (300 DPI) for high‑quality output
            converter.Resolution = new Resolution(300);

            // Define the page range to convert (all pages)
            converter.StartPage = 1;
            converter.EndPage   = converter.PageCount; // total pages after binding

            // Prepare the conversion
            converter.DoConvert();

            // Iterate through each page and save it as a TIFF image
            for (int pageNumber = converter.StartPage; pageNumber <= converter.EndPage; pageNumber++)
            {
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.tiff");
                // Save the current page as TIFF; format is inferred from the file extension
                converter.GetNextImage(outputPath);
            }
        }

        Console.WriteLine($"PDF pages have been converted to TIFF images in '{outputDir}'.");
    }
}
