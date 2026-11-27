using System;
using System.IO;
using Aspose.Pdf;
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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (FormPresentationMode property does not exist in current API)
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded document to the converter
                converter.BindPdf(doc);

                // Set the resolution for rendering (96 DPI as in the original example)
                converter.Resolution = new Resolution(96);

                // Convert pages 1 and 2 to individual TIFF images
                for (int pageNumber = 1; pageNumber <= 2; pageNumber++)
                {
                    // Configure the page range for this iteration (single page)
                    converter.StartPage = pageNumber;
                    converter.EndPage   = pageNumber;

                    // Perform the conversion
                    converter.DoConvert();

                    // Save the rendered page as a TIFF file
                    string tiffPath = Path.Combine(outputDir, $"Page_{pageNumber}.tiff");
                    converter.SaveAsTIFF(tiffPath);

                    Console.WriteLine($"Page {pageNumber} saved as TIFF: {tiffPath}");
                }
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}
